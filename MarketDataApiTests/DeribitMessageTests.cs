using MarketDataApi.Models.Deribit.Requests;
using MarketDataApi.Models.Deribit.Responses;
using Newtonsoft.Json;
using Xunit;

namespace MarketDataApiTests
{
    public class DeribitMessageTests
    {
        [Fact]
        public void DeribitAuthenticationRequest_Serialize_ShouldReturnSameMessage()
        {
            DeribitRequest<AuthenticationParams> request = new DeribitRequest<AuthenticationParams>
            {
                Method = "public/auth",
                Params = new AuthenticationParams
                {
                    ClientId = "fo7WAPRm4P",
                    ClientSecret = "W0H6FJW4IRPZ1MOQ8FP6KMC5RZDUUKXS",
                    GrantType = "client_credentials"
                }
            };

            var expected = $"{{\"jsonrpc\":\"2.0\",\"id\":{request.Id},\"method\":\"public/auth\",\"params\":{{\"grant_type\":\"client_credentials\",\"client_id\":\"fo7WAPRm4P\",\"client_secret\":\"W0H6FJW4IRPZ1MOQ8FP6KMC5RZDUUKXS\"}}}}";

            var result = JsonConvert.SerializeObject(request);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void DeribitAuthenticationResponse_Serialize_ShouldReturnSameMessage()
        {
            DeribitResponse<AuthenticationResponse> request = new DeribitResponse<AuthenticationResponse>
            {
                Result = new AuthenticationResponse
                {
                    AccessToken = "1582628593469.1MbQ-J_4.CBP-OqOwm_FBdMYj4cRK2dMXyHPfBtXGpzLxhWg31nHu3H_Q60FpE5_vqUBEQGSiMrIGzw3nC37NMb9d1tpBNqBOM_Ql9pXOmgtV9Yj3Pq1c6BqC6dU6eTxHMFO67x8GpJxqw_QcKP5IepwGBD-gfKSHfAv9AEnLJkNu3JkMJBdLToY1lrBnuedF3dU_uARm",
                    ExpiresIn = 31536000,
                    RefreshToken = "1582628593469.1GP4rQd0.A9Wa78o5kFRIUP49mScaD1CqHgiK50HOl2VA6kCtWa8BQZU5Dr03BhcbXPNvEh3I_MVixKZXnyoBeKJwLl8LXnfo180ckAiPj3zOclcUu4zkXuF3NNP3sTPcDf1B3C1CwMKkJ1NOcf1yPmRbsrd7hbgQ-hLa40tfx6Oa-85ymm_3Z65LZcnCeLrqlj_A9jM",
                    Scope = "connection mainaccount",
                    TokenType = "bearer"
                }
            };

            var expected = $"{{\"jsonrpc\":\"2.0\",\"id\":{request.Id},\"result\":{{\"access_token\":\"1582628593469.1MbQ-J_4.CBP-OqOwm_FBdMYj4cRK2dMXyHPfBtXGpzLxhWg31nHu3H_Q60FpE5_vqUBEQGSiMrIGzw3nC37NMb9d1tpBNqBOM_Ql9pXOmgtV9Yj3Pq1c6BqC6dU6eTxHMFO67x8GpJxqw_QcKP5IepwGBD-gfKSHfAv9AEnLJkNu3JkMJBdLToY1lrBnuedF3dU_uARm\",\"expires_in\":31536000,\"refresh_token\":\"1582628593469.1GP4rQd0.A9Wa78o5kFRIUP49mScaD1CqHgiK50HOl2VA6kCtWa8BQZU5Dr03BhcbXPNvEh3I_MVixKZXnyoBeKJwLl8LXnfo180ckAiPj3zOclcUu4zkXuF3NNP3sTPcDf1B3C1CwMKkJ1NOcf1yPmRbsrd7hbgQ-hLa40tfx6Oa-85ymm_3Z65LZcnCeLrqlj_A9jM\",\"scope\":\"connection mainaccount\",\"token_type\":\"bearer\"}}}}";

            var result = JsonConvert.SerializeObject(request);

            Assert.Equal(expected, result);
        }
    }
}