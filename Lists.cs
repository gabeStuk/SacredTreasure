using System;
using System.Collections.Generic;
using UnityEngine;

namespace SacredTreasure
{
    using STPreloads = Dictionary<string, Dictionary<string, GameObject>>;
    internal class Lists
    {
        public static string[] bossAbbrev = [
            "VK",
            "GM",
            "FK",
            "MMC",
            "H1",
            "GORB",
            "DD",
            "SW",
            "BM",
            "BRO",
            "0",
            "CG",
            "SM",
            "OBOB",
            "ML",
            "SOB",
            "MAR",
            "FM",
            "BV",
            "GAL",
            "PATOMAS_SHEO",
            "HiK",
            "HU",
            "JC",
            "GT",
            "TMG",
            "WK",
            "UMU",
            "NSK",
            "WN",
            "SLY",
            "H2",
            "EG",
            "LK99",
            "NE",
            "TL",
            "WD_GASTER",
            "ST",
            "BITCH",
            "GPZ",
            "FC",
            "NKG",
            "HK",
            "PV",
            "RAD",
            "AR",
        ];

        public static string[] fancyNames = [
            // VK
            "Vengefly King",
            // GM
            "Gruz Mother",
            // FK
            "False Knight",
            // MMC
            "Massive Moss Charger",
            // H1
            "Hornet 1",
            // GORB
            "Gorb",
            // DD
            "Dung Defender",
            // SW
            "Soul Warrior",
            // BM
            "Brooding Malwek",
            // BRO
            "Oro & Mato",
            // 0
            "Xero",
            // CG
            "Crystal Guardian",
            // SM
            "Soul Master",
            // OBOB
            "Oblobble",
            // ML
            "Mantis Lords",
            // SOB
            "Sisters of Battle",
            // MAR
            "Marmu",
            // FM
            "Flukemark",
            // BV
            "Broken Vessel",
            // GAL
            "Galien",
            // PATOMAS SHEO
            "Sheo",
            // HiK
            "Hive Knight",
            // HU
            "Elder Hu",
            // JC
            "Collector",
            // GT
            "God Tamer",
            // TMG
            "Troupe Master Grimm",
            // WK
            "Watcher Knights (x6)",
            // UMU
            "Uumuu",
            //NSK
            "Nosk",
            //WN
            "Winged Nosk",
            // SLY
            "Sly",
            // H2
            "Hornet 2",
            // EG
            "Enraged Guardian",
            // LK99
            "Lost Kin",
            // NE
            "No Eyes",
            // TL
            "Traitor Lord",
            // WD GASTER
            "White Defender",
            // ST
            "Soul Tyrant",
            // BITCH
            "Markoth",
            // GPZ
            "Grey Prince Zote",
            // FC
            "Failed Champion",
            // NKG
            "Nightmare King Grimm",
            // HK
            "Hollow Knight",
            // PV
            "Pure Vessel",
            // RAD
            "The Radiance",
            // AR
            "Absolute Radiance",
        ];

        public static List<(String, String)> loadings =
        [
            // VK
            ("GG_Vengefly_V", "Giant Buzzer Col"),
            // GM
            ("GG_Gruz_Mother_V", "_Enemies/Giant Fly"),
            // FK
            ("GG_False_Knight", "Battle Scene/False Knight New"),
            ("GG_False_Knight", "Battle Scene/Rubble Fall"),
            // MMC
            ("GG_Mega_Moss_Charger", "Mega Moss Charger"),
            // H1
            ("GG_Hornet_1", "Boss Holder/Hornet Boss 1"),
            ("GG_Hornet_1", "Music Region"),
            // GORB
            ("GG_Ghost_Gorb_V", "Warrior/Ghost Warrior Slug"),
            // DD
            ("GG_Dung_Defender", "Dung Defender"),
            // SW
            ("GG_Mage_Knight_V", "Balloon Spawner"),
            // BM
            ("GG_Brooding_Mawlek", "Battle Scene/Mawlek Body"),
            // BRO
            ("GG_Nailmasters", "Brothers"),
            // 0
            ("GG_Ghost_Xero_V", "Warrior/Ghost Warrior Xero"),
            // CG
            ("GG_Crystal_Guardian", "Mega Zombie Beam Miner (1)"),
            // SM
            ("GG_Soul_Master", "Mage Lord"),
            ("GG_Soul_Master", "Mage Lord Phase2"),
            // OBOB
            ("GG_Oblobbles", "Mega Fat Bee"),
            ("GG_Oblobbles", "Mega Fat Bee (1)"),
            // ML
            ("GG_Mantis_Lords", "Mantis Battle"),
            // SOB
            ("GG_Mantis_Lords_V", "Mantis Battle"),
            // MAR
            ("GG_Ghost_Marmu_V", "Warrior/Ghost Warrior Marmu"),
            // FM
            ("GG_Flukemarm", "Fluke Mother"),
            // BV
            ("GG_Broken_Vessel", "Infected Knight"),
            // GAL
            ("GG_Ghost_Galien", "Warrior/Ghost Warrior Galien"),
            ("GG_Ghost_Galien", "Warrior/Galien Hammer"),
            // PATOMAS SHEO
            ("GG_Painter", "Battle Scene/Sheo Boss"),
            ("GG_Painter", "Battle Scene/Paint Splats"),
            // HiK
            ("GG_Hive_Knight", "Battle Scene/Hive Knight"),
            ("GG_Hive_Knight", "Battle Scene/Globs"),
            ("GG_Hive_Knight", "Battle Scene/Droppers"),
            // HU
            ("GG_Ghost_Hu", "Warrior/Ghost Warrior Hu"),
            ("GG_Ghost_Hu", "Ring Holder"),
            // JC
            ("GG_Collector", "Battle Scene/Jar Collector"),
            // GT
            ("GG_God_Tamer", "Entry Object"),
            // TMG
            ("GG_Grimm", "Grimm Spike Holder"),
            ("GG_Grimm", "Grimm Bats"),
            ("GG_Grimm", "Grimm Scene/Grimm Boss"),
            // WK
            ("GG_Watcher_Knight", "Battle Control"),
            // UMU
            ("GG_Uumuu_V", "Mega Jellyfish Multizaps"),
            ("GG_Uumuu_V", "Mega Jellyfish GG"),
            // WN
            ("GG_Nosk_Hornet", "Battle Scene/Hornet Nosk"),
            // SLY
            ("GG_Sly", "Battle Scene"),
            // H2
            ("GG_Hornet_2", "Barb Region"),
            ("Deepnest_East_Hornet_boss", "Hornet Boss 2"),
            // EG
            ("GG_Crystal_Guardian_2", "Laser Turret Mega"),
            ("GG_Crystal_Guardian_2", "Battle Scene/Zombie Beam Miner Rematch"),
            // LK99
            ("GG_Lost_Kin", "Lost Kin"),
            // NE
            ("GG_Ghost_No_Eyes_V", "Warrior/Ghost Warrior No Eyes"),
            ("GG_Ghost_No_Eyes_V", "No Eyes Head"),
            // TL
            ("GG_Traitor_Lord", "Battle Scene/Wave 3"),
            // WD GASTER
            ("GG_White_Defender", "White Defender"),
            // ST
            ("GG_Soul_Tyrant", "Dream Mage Lord"),
            ("GG_Soul_Tyrant", "Dream Mage Lord Phase2"),
            // BITCH
            ("GG_Ghost_Markoth_V", "Warrior/Ghost Warrior Markoth"),
            // GPZ
            ("GG_Grey_Prince_Zote", "Grey Prince"),
            // FC
            ("GG_Failed_Champion", "False Knight Dream"),
            ("GG_Failed_Champion", "Rubble Fall"),
            // NKG
            ("GG_Grimm_Nightmare", "Nightmare Grimm Boss"),
            // HK
            // PV
            ("GG_Hollow_Knight", "Battle Scene"),
            // RAD
            // AR
            ("GG_Radiance", "Boss Control"),
        ];
    }
}
