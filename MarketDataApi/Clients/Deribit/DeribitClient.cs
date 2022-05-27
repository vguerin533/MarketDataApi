using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using MarketDataApi.Config.Deribit;
using MarketDataApi.Models.Deribit;
using MarketDataApi.Models.Deribit.Requests;
using MarketDataApi.Models.Deribit.Responses;
using Newtonsoft.Json;
using StreamJsonRpc;

namespace MarketDataApi.Clients.Deribit
{
    public class DeribitClient : IDisposable, IDeribitClient
    {
        private readonly IDeribitConfig? _deribitConfig;
        private readonly ClientWebSocket _socket;
        private readonly JsonRpc _jsonRpc;
        public bool IsDisposed;

        public DeribitClient(IDeribitConfig? deribitConfig, ClientWebSocket socket)
        {
            _deribitConfig = deribitConfig;
            _socket = socket;
            _jsonRpc = new JsonRpc(new WebSocketMessageHandler(_socket));
        }

        ~DeribitClient()
        {
            Dispose();
        }

        public void Dispose()
        {
            _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Client closing",
                CancellationToken.None).GetAwaiter().GetResult();

            if (!IsDisposed)
            {
                if (!_jsonRpc.IsDisposed)
                {
                    _jsonRpc.Dispose();
                }
                _socket.Dispose();
            }
        }

        private async Task ConnectAsync()
        {
            await _socket.ConnectAsync(new Uri(_deribitConfig?.BaseUrl), CancellationToken.None);

            DeribitRequest<AuthenticationParams> request = new DeribitRequest<AuthenticationParams>
            {
                Method = DeribitRoutes.GetAuthenticationRoute(),
                Params = new AuthenticationParams
                {
                    ClientId = _deribitConfig.ClientId,
                    ClientSecret = _deribitConfig.ClientSecret,
                    GrantType = "client_credentials"
                }
            };
            _jsonRpc.StartListening();
            await _jsonRpc.InvokeWithParameterObjectAsync<AuthenticationResponse>(DeribitRoutes.GetAuthenticationRoute(), request.Params);
        }

        private async Task DisconnectAsync()
        {
            await _jsonRpc.NotifyAsync(DeribitRoutes.GetLogoutRoute());
        }

        public async Task SubscribeAsync(string ticker, BlockingCollection<DeribitTicker> tickersCollection)
        {
            await ConnectAsync();
            TickerInformationParams request = new TickerInformationParams
            {
                Channels = new List<string> { ticker }
            };
            await _jsonRpc.InvokeWithParameterObjectAsync<List<dynamic>>(DeribitRoutes.GetTicketInformationRoute(), request);

            while (_socket.State == WebSocketState.Open)
            {
                await PushTickerAsync(tickersCollection);
            }
        }

        public async Task PushTickerAsync(BlockingCollection<DeribitTicker> tickersCollection)
        {
            ArraySegment<byte> bytesReceived = new ArraySegment<byte>(new byte[1024]);
            WebSocketReceiveResult result = await _socket.ReceiveAsync(bytesReceived, CancellationToken.None);
            if (bytesReceived.Array != null)
            {
                var tickerJson = Encoding.UTF8.GetString(bytesReceived.Array, 0, result.Count);
                var ticker = JsonConvert.DeserializeObject<DeribitTicker>(tickerJson);
                if (ticker != null)
                {
                    tickersCollection.TryAdd(ticker);
                }
            }
        }

        public async Task UnsubscribeAsync()
        {
            await _jsonRpc.NotifyAsync(DeribitRoutes.GetUnsuscribeAllRoute());
            await DisconnectAsync();
            Dispose();
        }
    }
}
