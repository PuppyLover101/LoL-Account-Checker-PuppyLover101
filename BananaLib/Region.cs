namespace BananaLib
{
    public enum Region
    {
        [FullName("North America"), GameServerAddress("prod.na2.lol.riotgames.com"), LoginQueue("https://lqak.na2.lol.riotgames.com/"), Locale("en_US"), UseGarena(false)] NA,
        [FullName("Europe West"), GameServerAddress("prod.euw1.lol.riotgames.com"), LoginQueue("https://lqak.euw1.lol.riotgames.com/"), Locale("en_GB"), UseGarena(false)] EUW,
        [FullName("Europe Nordic and East"), GameServerAddress("prod.eun1.lol.riotgames.com"), LoginQueue("https://lqak.eun1.lol.riotgames.com/"), Locale("en_GB"), UseGarena(false)] EUNE,
        [FullName("South Korea"), GameServerAddress("prod.kr.lol.riotgames.com"), LoginQueue("https://lqak.kr.lol.riotgames.com/"), Locale("ko_KR"), UseGarena(false)] KR,
        [FullName("Brazil"), GameServerAddress("prod.br.lol.riotgames.com"), LoginQueue("https://lqak.br.lol.riotgames.com/"), Locale("pt_BR"), UseGarena(false)] BR,
        [FullName("Turkey"), GameServerAddress("prod.tr.lol.riotgames.com"), LoginQueue("https://lqak.tr.lol.riotgames.com/"), Locale("pt_BR"), UseGarena(false)] TR,
        [FullName("Public Beta Environment"), GameServerAddress("prod.pbe1.lol.riotgames.com"), LoginQueue("https://lq.pbe1.lol.riotgames.com/"), Locale("en_US"), UseGarena(false)] PBE,
        
        [FullName("Russia"), GameServerAddress("prod.ru.lol.riotgames.com"), LoginQueue("https://lqak.ru.lol.riotgames.com/"), Locale("en_US"), UseGarena(false)] RU,
        [FullName("Oceania"), GameServerAddress("prod.oc1.lol.riotgames.com"), LoginQueue("https://lqak.oc1.lol.riotgames.com/"), Locale("en_US"), UseGarena(false)] OCE,
        [FullName("Latin America North"), GameServerAddress("prod.la1.lol.riotgames.com"), LoginQueue("https://lqak.la1.lol.riotgames.com/"), Locale("en_US"), UseGarena(false)] LAN,
        [FullName("Latin America South"), GameServerAddress("prod.la2.lol.riotgames.com"), LoginQueue("https://lqak.la2.lol.riotgames.com/"), Locale("en_US"), UseGarena(false)] LAS,
        [FullName("Japan"), GameServerAddress("prod.jp1.lol.riotgames.com"), LoginQueue("https://lqak.jp1.lol.riotgames.jp/"), Locale("ja_JP"), UseGarena(false)] JP,

        [FullName("Singapore"), GameServerAddress("prod.lol.garenanow.com"), LoginQueue("https://lqak.lol.garenanow.com/"), UseGarena(true)] SG,
        [FullName("Malaysia"), GameServerAddress("prod.lol.garenanow.com"), LoginQueue("https://lqak.lol.garenanow.com/"), UseGarena(true)] MY,
        [FullName("Singapore/Malaysia"), GameServerAddress("prod.lol.garenanow.com"), LoginQueue("https://lqak.lol.garenanow.com/"), Locale("en_US"), UseGarena(true)] SGMY,
        [FullName("Taiwan"), GameServerAddress("prodtw1.lol.garenanow.com"), LoginQueue("https://lqtw1.lol.garenanow.com/"), Locale("en_US"), UseGarena(true)] TW,
        [FullName("Thailand"), GameServerAddress("prodth.lol.garenanow.com"), LoginQueue("https://lqth.lol.garenanow.com/"), Locale("en_US"), UseGarena(true)] TH,
        [FullName("Phillipines"), GameServerAddress("prodph.lol.garenanow.com"), LoginQueue("https://lqph.lol.garenanow.com/"), Locale("en_US"), UseGarena(true)] PH,
        [FullName("Vietnam"), GameServerAddress("prodvn1.lol.garenanow.com"), LoginQueue("https://lqvn1.lol.garenanow.com/"), Locale("en_US"), UseGarena(true)] VN,
        [FullName("Indonesia"), GameServerAddress("prodid.lol.garenanow.com"), LoginQueue("https://lqid.lol.garenanow.com/"), Locale("en_US"), UseGarena(true)] ID
    }
}