namespace MarketDataApi.Clients.Deribit
{
    public static class DeribitRoutes
    {
        public static string GetAuthenticationRoute()
        {
            return "public/auth";
        }

        public static string GetTicketInformationRoute()
        {
            return "public/subscribe";
        }

        public static string GetUnsuscribeAllRoute()
        {
            return "public/unsubscribe_all";
        }

        public static string GetLogoutRoute()
        {
            return "private/logout";
        }
    }
}
