namespace Back_end_Poro_Opresor
{
    public static class Urls
    {
        public static string LeagueBaseUrl = ".api.riotgames.com";
        public static string APIKey = "RGAPI-e77b9b12-274d-4e09-b30d-c84911988cd2";
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
