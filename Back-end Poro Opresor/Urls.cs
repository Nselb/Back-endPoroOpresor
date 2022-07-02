namespace Back_end_Poro_Opresor
{
    public static class Urls
    {
        public static string LeagueBaseUrl = ".api.riotgames.com";
        public static string APIKey = "RGAPI-43f8aa45-e371-46f5-95e7-4d046328a5c0";
        public static string GetRoutingValue(string server)
        {
            return server switch
            {
                "LA1" or "LA2" or "BR1" or "NA1" or "OC1" => "AMERICAS",
                "KR" or "JP1" => "ASIA",
                "EUW1" or "EUN1" or "TR1" or "RU" => "EUROPE",
                _ => "",
            };
        }
    }
}
