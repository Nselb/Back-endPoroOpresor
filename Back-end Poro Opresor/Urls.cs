namespace Back_end_Poro_Opresor
{
    public static class Urls
    {
        public static string LeagueBaseUrl = ".api.riotgames.com";
        public static string APIKey = "RGAPI-87f7f5f6-e10a-49d0-874b-0afc29d2ebed";
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
