using System;
using System.Collections.Generic;

namespace Notira.Notira.Cards;

public static class CardPackDefinitions
{
    public static Dictionary<string, List<Type>> GetPacks()
    {
        return new()
        {
            ["CLANNAD"] = new()
            {
                typeof(Clannad),
                typeof(FurukawaNagisa), typeof(FurukawaAkio),
                typeof(IbukiFuko), typeof(IchinoseKotomi),
                typeof(SakagamiTomoyo),
                typeof(SunoharaMei), typeof(SunoharaYohei),
                typeof(YoshinoYusuke),
                typeof(KoumuraToshio),
                typeof(MiyazawaYukine), typeof(SagaraMisae),
                typeof(FujibayashiKyo), typeof(FujibayashiRyo),
            },
            ["WHITE ALBUM 2"] = new()
            {
                typeof(OgisoSetsuna), typeof(ToumaKazusa), typeof(WHITEALBUM2),
            },
            ["Aokana"] = new() { typeof(Aokana) },
            ["ATRI"] = new() { typeof(Atri) },
            ["STEINS;GATE"] = new() { typeof(STEINSGATE) },
            ["Euphoria"] = new() { typeof(Euphoria) },
            ["Ever17"] = new() { typeof(Ever17) },
            ["Rance"] = new() { typeof(Rance) },
            ["Muramasa"] = new() { typeof(Muramasa) },
            ["Narcissu"] = new() { typeof(Narcissus) },
            ["eden*"] = new() { typeof(Eden) },
            ["Swan Song"] = new() { typeof(SwanSong) },
            ["Raging Loop"] = new() { typeof(RagingLoop) },
            ["Sakura"] = new() { typeof(SakuraMoyu), typeof(Sakuretto) },
            ["Erewhom"] = new() { typeof(Erewhom) },
            ["Euthanasia"] = new() { typeof(Euthanasia) },
            ["DaitoshokannoHitsujikai"] = new() { typeof(DaitoshokannoHitsujikai) },
            ["Majo Nikki"] = new() { typeof(MajoNikki) },
            ["Haison Shoujo"] = new() { typeof(HaisonShoujo) },
            ["Feimeng Gaoshou"] = new() { typeof(Feimenggaoshou) },
            ["ChronoBox"] = new() { typeof(ChronoBox) },
            ["Mushi Mederu Shoujo"] = new() { typeof(MushiMederuShoujo) },
            ["Nieno Hakoniwa"] = new() { typeof(NienoHakoniwa) },
            ["Shiroha"] = new() { typeof(Shiroha) },
            ["NoWing"] = new() { typeof(NoWing) },
            ["Himukai Kanata"] = new() { typeof(HimukaiKanata) },
            ["Himeno"] = new()
            {
                typeof(HimenoSena), typeof(HimenoTowa),
            },
            ["TsukiniYorisouOtomenoSahou"] = new() { typeof(TsukiniYorisouOtomenoSahou) },
            ["Kamimaho"] = new()
            {
                typeof(KamimahoChoice), typeof(KamimahoDayikira), typeof(KamimahoDayisiki),
            },
            ["Kisainote"] = new() { typeof(Kisainote) },
            ["Shioiri Kukuri"] = new() { typeof(ShioiriKukuri) },
            ["Chigasaki Yura"] = new() { typeof(ChigasakiYra) },
            ["Roaring Tides"] = new() { typeof(RoaringTides) },
            ["Classic"] = new()
            {
                typeof(Classic), typeof(Classic2),
                typeof(ClassicCream), typeof(ClassicGlasses),
                typeof(ClassicNoGlasses), typeof(ClassicPuff),
            },
            ["ClChoices"] = new() { typeof(ClChoices) },
            ["Istoria"] = new() { typeof(Istoria) },
            ["Route"] = new()
            {
                typeof(Route), typeof(Routekokyu), typeof(RoutePureLove),
            },
            ["Rupekari"] = new()
            {
                typeof(RupekariAlive), typeof(RupekariChoice), typeof(RupekariDead),
            },
            ["Beatrice"] = new() { typeof(Beatrice) },
            ["Sacle"] = new() { typeof(Sacle) },
            ["The Order"] = new() { typeof(TheOrder) },
            ["ENDs"] = new() { typeof(TrueEND), typeof(NormalEND), typeof(BadEND) },
            ["Whatcolorisyourattribute"] = new() { typeof(Whatcolorisyourattribute) },
            ["Hokejo"] = new() { typeof(Hokejo) },
            ["Enki Dekina"] = new() { typeof(EnkiDekina) },
            ["Savior"] = new() { typeof(Savior) },
            ["Sister Julia"] = new() { typeof(SisterJuliah) },
            ["Syusaku"] = new() { typeof(Syusaku) },
            ["Trinity"] = new() { typeof(Trinity) },
            ["Cutthroat"] = new() { typeof(Cutthroat) },
            ["I Was Born For You"] = new() { typeof(Iwasbornforyou) },
            ["Luna"] = new() { typeof(Luna) },
            ["MDB"] = new() { typeof(MDB) },
            ["SDB"] = new() { typeof(SDB) },
            ["Combo Card"] = new() { typeof(ComboCard) },
            ["Practised"] = new() { typeof(Practised) },
            ["Amakano"] = new() { typeof(Amakano) },
            ["Amanatus"] = new() { typeof(Amanatus) },
            ["Ginka"] = new() { typeof(Ginka) },
            ["Kanchuanmaru"] = new() { typeof(Kanchuanmaru) },
            ["Light Sheep"] = new() { typeof(LightSheep) },
            ["Sofurin"] = new() { typeof(Sofurin) },
            ["Karehan"] = new() { typeof(Karehan) },
            ["Kagome"] = new() { typeof(Kagome) },
            ["School Girlfriend"] = new() { typeof(SchoolGirlfriend) },
            ["Myodo"] = new() { typeof(Myodo) },
            ["Siguan"] = new() { typeof(Siguan) },
            ["oneArmedCrayfish"] = new() { typeof(oneArmedCrayfish) },
            ["Heishou"] = new() { typeof(Heishou) },
            ["Purple"] = new() { typeof(Purple) },
            ["Black Lily"] = new() { typeof(BlackLily) },
            ["Nimi Sora"] = new() { typeof(NimiSora) },
            ["Prologue"] = new() { typeof(Prologue) },
            ["Ciallo"] = new() { typeof(Ciallo) },
            ["Aug"] = new() { typeof(Aug) },
            ["Four Seasons"] = new()
            {
                typeof(Spring), typeof(Summer), typeof(Fall), typeof(Winter),
            },
            ["Notira0721"] = new() { typeof(Notira0721) },
            ["Fraternite"] = new() { typeof(Fraternite) },
            ["Desire"] = new() { typeof(Desire) },
            ["Prank"] = new() { typeof(Prank) },
            ["The Song of Saya"] = new() { typeof(TheSongofSaya) },
            ["Sushimeka"] = new() { typeof(Sushimeka) },
            ["Failure"] = new() { typeof(Failure) },
            ["Chessboard"] = new() { typeof(Chessboard) },
            ["Notira Base"] = new()
            {
                typeof(NotiraAttack), typeof(NotiraBlock),
            },
        };
    }
}
