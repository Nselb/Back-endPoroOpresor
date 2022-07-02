﻿using Back_end_Poro_Opresor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_end_Poro_Opresor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChampionMasteriesController : ControllerBase
    {
        private static GameDBContext db { get; set; }
        public ChampionMasteriesController(GameDBContext context)
        {
            db = context;
        }

        [HttpGet("{summonerId}")]
        public List<ChampionMastery> GetMastery(string summonerId)
        {
            return db.ChampionMastery.Where(cm => cm.SummonerId.Equals(summonerId)).ToList();
        }

        [HttpPost]
        public async Task<List<ChampionMastery>> PostMastery(string summonerId, string regionId)
        {
            HttpClient client = new();
            client.DefaultRequestHeaders.Add("X-Riot-Token", Urls.APIKey);
            string url = $"https://{regionId}{Urls.LeagueBaseUrl}/lol/champion-mastery/v4/champion-masteries/by-summoner/{summonerId}";
            List<ChampionMastery> result = await client.GetFromJsonAsync<List<ChampionMastery>>(url);
            if (result != null)
            {
                try
                {
                    foreach (var item in result)
                    {
                        item.ChampionName = GetChampName(item.ChampionId);
                        item.SummonerId = summonerId;
                        List<ChampionMastery> coso = db.ChampionMastery.Where(cm => cm.SummonerId.Equals(summonerId)).ToList();
                        if (coso.Any())
                        {
                            if (coso.Where(cm => cm.ChampionName.Equals(item.ChampionName)).FirstOrDefault() != null)
                                continue;
                        }
                        db.ChampionMastery.Add(item);
                    }
                    db.SaveChanges();
                    return GetMastery(summonerId);
                }
                catch (Exception)
                {
                    return new();
                }
            }
            return new();
        }
        [HttpGet]
        public string GetChampName(int id)
        {
            return id switch
            {
                266 => "Aatrox",
                103 => "Ahri",
                84 => "Akali",
                166 => "Akshan",
                12 => "Alistar",
                32 => "Amumu",
                34 => "Anivia",
                1 => "Annie",
                523 => "Aphelios",
                22 => "Ashe",
                136 => "Aurelion Sol",
                268 => "Azir",
                432 => "Bard",
                200 => "Bel'Veth",
                53 => "Blitzcrank",
                63 => "Brand",
                201 => "Braum",
                51 => "Caitlyn",
                164 => "Camille",
                69 => "Cassiopeia",
                31 => "Cho'Gath",
                42 => "Corki",
                122 => "Darius",
                131 => "Diana",
                119 => "Draven",
                36 => "Dr. Mundo",
                245 => "Ekko",
                60 => "Elise",
                28 => "Evelynn",
                81 => "Ezreal",
                9 => "Fiddlesticks",
                114 => "Fiora",
                105 => "Fizz",
                3 => "Galio",
                41 => "Gangplank",
                86 => "Garen",
                150 => "Gnar",
                79 => "Gragas",
                104 => "Graves",
                887 => "Gwen",
                120 => "Hecarim",
                74 => "Heimerdinger",
                420 => "Illaoi",
                39 => "Irelia",
                427 => "Ivern",
                40 => "Janna",
                59 => "Jarvan IV",
                24 => "Jax",
                126 => "Jayce",
                202 => "Jhin",
                222 => "Jinx",
                145 => "Kai'Sa",
                429 => "Kalista",
                43 => "Karma",
                30 => "Karthus",
                38 => "kassadin",
                55 => "Katarina",
                10 => "Kayle",
                141 => "Kayn",
                85 => "Kennen",
                121 => "Kha'Zix",
                203 => "Kindred",
                240 => "Kled",
                96 => "Kog'Maw",
                7 => "LeBlanc",
                64 => "Lee Sin",
                89 => "Leona",
                876 => "Lillia",
                127 => "Lissandra",
                236 => "Lucian",
                117 => "Lulu",
                99 => "Lux",
                54 => "Malphite",
                90 => "Malzahar",
                57 => "Maokai",
                11 => "Maestro Yi",
                21 => "Miss Fortune",
                62 => "Wukong",
                82 => "Mordekaiser",
                25 => "Morgana",
                267 => "Nami",
                75 => "Nasus",
                111 => "Nautilus",
                518 => "Neeko",
                76 => "Nidalee",
                56 => "Nocturne",
                20 => "Nunu",
                2 => "Olaf",
                61 => "Orianna",
                516 => "Ornn",
                80 => "Pantheon",
                78 => "Poppy",
                555 => "Pyke",
                246 => "Qiyana",
                133 => "Quinn",
                497 => "Rakan",
                33 => "Rammus",
                421 => "Rek'Sai",
                526 => "Rell",
                888 => "Renata Glasc",
                58 => "Renekton",
                107 => "Rengar",
                92 => "Riven",
                68 => "Rumble",
                13 => "Ryze",
                360 => "Samira",
                113 => "Sejuani",
                235 => "Senna",
                147 => "Seraphine",
                875 => "Sett",
                35 => "Shaco",
                98 => "Shen",
                102 => "Shyvana",
                27 => "Singed",
                14 => "Sion",
                15 => "Sivir",
                72 => "Skarner",
                37 => "Sona",
                16 => "Soraka",
                50 => "Swain",
                517 => "Sylas",
                134 => "Syndra",
                223 => "Tahm Kench",
                163 => "Taliyah",
                91 => "Talon",
                44 => "Taric",
                17 => "Teemo",
                412 => "Thresh",
                18 => "Tristana",
                48 => "Trundle",
                23 => "Tryndamere",
                4 => "Twisted Fate",
                29 => "Twitch",
                77 => "Udyr",
                6 => "Urgot",
                110 => "Varus",
                67 => "Vayne",
                45 => "Veigar",
                161 => "Vel'Koz",
                711 => "Vex",
                254 => "Vi",
                234 => "Viego",
                112 => "Viktor",
                8 => "Vladimir",
                106 => "Volibear",
                19 => "Warwick",
                498 => "Xayah",
                101 => "Xerath",
                5 => "Xin Zhao",
                157 => "Yasuo",
                777 => "Yone",
                83 => "Yorick",
                350 => "Yuumi",
                154 => "Zac",
                238 => "Zed",
                221 => "Zeri",
                115 => "Ziggs",
                26 => "Zilean",
                142 => "Zoe",
                143 => "Zyra",
                _ => "",
            };
        }
    }
}
