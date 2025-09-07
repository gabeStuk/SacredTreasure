using Satchel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UObject = UnityEngine.Object;

namespace SacredTreasure
{
    internal class Summoning
    {
        public static Action BeforeSummon;
        public static Action<GameObject> AfterSummon = o => o.LogWithChildren();
        // VK -- good, review
        public static void summonVK(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Vengefly_V"]["Giant Buzzer Col"]);
            a.SetActive(true);
            PlayMakerFSM fsm = a.LocateMyFSM("Big Buzzer");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("SUMMON");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("RIGHT");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            a.GetComponent<HealthManager>().hp = 430;
            fsm.FsmVariables.GetFsmFloat("Swoop Height").Value = HeroController.instance.transform.position.y;
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(10f, 5f);
            AfterSummon?.Invoke(a);
        }
        // GM -- good
        public static void summonGM(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Gruz_Mother_V"]["_Enemies/Giant Fly"]);
            a.SetActive(true);
            a.LocateMyFSM("Big Fly Control").SetState("FLY");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(10f, 5f);
            AfterSummon?.Invoke(a);
        }
        // FK -- need spawn w/o drop
        public static void summonFK(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var b = UObject.Instantiate(preloadedObjects["GG_False_Knight"]["Battle Scene/Rubble Fall"]);
            b.SetActive(true);
            b.transform.position = HeroController.instance.transform.position;
            var a = UObject.Instantiate(preloadedObjects["GG_False_Knight"]["Battle Scene/False Knight New"]);
            a.SetActive(true);
            PlayMakerFSM fsm = a.LocateMyFSM("FalseyControl");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("BATTLE START");
            fsm.SendEvent("FALL");
            fsm.SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 50f);
            AfterSummon?.Invoke(a);
        }
        // MMC -- need to investigate, no longer work
        public static void summonMMC(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Mega_Moss_Charger"]["Mega Moss Charger"]);
            a.SetActive(true);
            PlayMakerFSM fsm = a.LocateMyFSM("Mossy Control");
            fsm.SetState("HIDDEN");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(0f, 3f);
            fsm.FsmVariables.GetFsmFloat("Self Y").Value = HeroController.instance.transform.position.y + 10f;
            fsm.FsmVariables.GetFsmFloat("Start Y").Value = HeroController.instance.transform.position.y + 10f;
            AfterSummon?.Invoke(a);
        }
        // H1 -- good, less jank
        public static void summonH1(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Hornet_1"]["Boss Holder/Hornet Boss 1"]);
            // allow hornet to move
            a.RemoveComponent<ConstrainPosition>();
            a.SetActive(true);
            PlayMakerFSM fsm = a.LocateMyFSM("Control");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("GG BOSS");
            // localize vaiables
            fsm.FsmVariables.GetFsmFloat("Floor Y").Value = HeroController.instance.transform.position.y - 0.5f;
            fsm.FsmVariables.GetFsmFloat("Sphere Y").Value = HeroController.instance.transform.position.y + 5.75f;
            fsm.FsmVariables.GetFsmFloat("Roof Y").Value = HeroController.instance.transform.position.y + 12.5f;
            fsm.FsmVariables.GetFsmFloat("Wall X Left").Value = HeroController.instance.transform.position.x - 11f;
            fsm.FsmVariables.GetFsmFloat("Wall X Right").Value = HeroController.instance.transform.position.x + 11f;
            //a.LocateMyFSM("Control").SendEvent("LAND");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 0f);
            AfterSummon?.Invoke(a);
        }
        // GORB -- everything is FING HARDCODED he goes to the sky
        public static void summonGORB(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Ghost_Gorb_V"]["Warrior/Ghost Warrior Slug"]);
            // localize tp tpoints
            PlayMakerFSM fsm = a.LocateMyFSM("Movement");
            fsm.FsmVariables.GetFsmVector3("P1").Value = HeroController.instance.transform.position + new Vector3(3.95f, 7.59f);
            fsm.FsmVariables.GetFsmVector3("P2").Value = HeroController.instance.transform.position + new Vector3(3.95f, 2.59f);
            fsm.FsmVariables.GetFsmVector3("P3").Value = HeroController.instance.transform.position + new Vector3(-5.7f, 2.59f);
            fsm.FsmVariables.GetFsmVector3("P4").Value = HeroController.instance.transform.position + new Vector3(13.67f, 2.59f);
            fsm.FsmVariables.GetFsmVector3("P5").Value = HeroController.instance.transform.position + new Vector3(3.95f, 4.29f);
            fsm.FsmVariables.GetFsmVector3("P6").Value = HeroController.instance.transform.position + new Vector3(10.31f, 4.29f);
            fsm.FsmVariables.GetFsmVector3("P7").Value = HeroController.instance.transform.position + new Vector3(-2.31f, 4.29f);
            a.SetActive(true);
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 5f);
            AfterSummon?.Invoke(a);
        }
        // DD
        public static void summonDD(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Dung_Defender"]["Dung Defender"]);
            a.SetActive(true);
            var fsm = a.LocateMyFSM("Dung Defender");
            // localize variables (room spawn: 74.04î + 7.4ĵ)
            GameObject burrow = a.transform.Find("Burrow Effect").gameObject;
            burrow.LocateMyFSM("Burrow Effect").SendEvent("FINISHED");
            burrow.LocateMyFSM("Keep Y").SendEvent("FINISHED");
            burrow.transform.position = new Vector3(burrow.transform.position.x, HeroController.instance.transform.position.y + 1f);
            fsm.FsmVariables.GetFsmFloat("Centre X").Value = HeroController.instance.transform.position.x + 2.59f; // was 76.63
            fsm.FsmVariables.GetFsmFloat("Buried Y").Value = HeroController.instance.transform.position.y - 3f;
            fsm.FsmVariables.GetFsmFloat("Erupt Y").Value = HeroController.instance.transform.position.y;
            fsm.GetValidState("Quaked Out").AddCustomAction(() => a.transform.position = new Vector3(a.transform.position.x, HeroController.instance.transform.position.y + 0.5f)); // what in the mother of f
            fsm.FsmVariables.GetFsmFloat("Dolphin Max X").Value = HeroController.instance.transform.position.x + 13.14f; // was 87.18
            fsm.FsmVariables.GetFsmFloat("Dolphin Min X").Value = HeroController.instance.transform.position.x - 8.55f; // was 65.49
            fsm.FsmVariables.GetFsmFloat("Erupt Peak Y").Value = HeroController.instance.transform.position.y + 2.5f; // was 12
            fsm.FsmVariables.GetFsmFloat("Max X").Value = HeroController.instance.transform.position.x + 16.53f; // was 90.57
            fsm.FsmVariables.GetFsmFloat("Min X").Value = HeroController.instance.transform.position.x - 12.26f; // was 61.78
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("GG BOSS");
            a.transform.position = HeroController.instance.transform.position + new Vector3(3f, -10.4f);
            a.SetScale(scale.Item1, scale.Item2);
            AfterSummon?.Invoke(a);

        }
        // SW
        public static void summonSW(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Mage_Knight_V"]["Balloon Spawner"]);
            a.transform.position = HeroController.instance.transform.position;
            a.SetActive(true);
            a.LocateMyFSM("Battle Control").FsmVariables.GetFsmBool("Activated").Value = true;
            a.LocateMyFSM("Battle Control").FsmVariables.GetFsmInt("Battle Enemies").Value = 5;
            a.LocateMyFSM("Battle Control").SendEvent("FINISHED");
            a.LocateMyFSM("Battle Control").SendEvent("FINISHED");
            var mk = a.transform.Find("Mage Knight").gameObject;
            var mkfsm = mk.LocateMyFSM("Mage Knight");
            mkfsm.SendEvent("FINISHED");
            mkfsm.SendEvent("FINISHED");
            mkfsm.SendEvent("GG BOSS");
            mkfsm.SendEvent("WAKE");
            mkfsm.FsmVariables.GetFsmFloat("Floor Y").Value = HeroController.instance.transform.position.y + 2f;
            mkfsm.FsmVariables.GetFsmFloat("Tele X Max").Value = HeroController.instance.transform.position.y + 15f;
            mkfsm.FsmVariables.GetFsmFloat("Tele X Min").Value = HeroController.instance.transform.position.y - 15f;
            mk.transform.position = HeroController.instance.transform.position + new Vector3(5f, 8f);
            mk.SetScale(scale.Item1, scale.Item2);
            var mkhm = mk.GetComponent<HealthManager>();
            removePatomas(mkhm);
            mkhm.hp = 750;
            AfterSummon?.Invoke(a);
            AfterSummon?.Invoke(mk);
        }
        // BM -- good
        public static void summonBM(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Brooding_Mawlek"]["Battle Scene/Mawlek Body"]);
            a.SetActive(true);
            PlayMakerFSM fsm = a.LocateMyFSM("Mawlek Control");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("GG_BOSS");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("LANDED");
            fsm.SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(15f, 0f);
            AfterSummon?.Invoke(a);
        }
        // BRO
        public static void summonBRO(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Nailmasters"]["Brothers"]);
            a.SetActive(true);
            a.LocateMyFSM("Combo Control").SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(-50f, -5f);
            AfterSummon?.Invoke(a);
        }
        // 0 -- tolerances also hard coded, but chases so not too bad
        public static void summon0(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Ghost_Xero_V"]["Warrior/Ghost Warrior Xero"]);
            a.SetActive(true);
            var fsm = a.LocateMyFSM("Movement");
            a.transform.position = HeroController.instance.transform.position + new Vector3(0f, 8f);
            a.SetScale(scale.Item1, scale.Item2);
            AfterSummon?.Invoke(a);
        }
        // CG -- no lasers from above
        public static void summonCG(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Crystal_Guardian"]["Mega Zombie Beam Miner (1)"]);
            a.SetActive(true);
            var fsm = a.LocateMyFSM("Beam Miner");
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 0f);
            a.SetScale(scale.Item1, scale.Item2);
            var h = a.GetComponent<HealthManager>();
            h.hp = 650;
            removePatomas(h);
            AfterSummon?.Invoke(a);
        }
        // SM -- todo: shockwaves+phase2 don't work
        public static void summonSM(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a1 = UObject.Instantiate(preloadedObjects["GG_Soul_Master"]["Mage Lord"]);
            var a2 = UObject.Instantiate(preloadedObjects["GG_Soul_Master"]["Mage Lord Phase2"]);
            a1.SetActive(true);
            var fsm1 = a1.LocateMyFSM("Mage Lord");
            var fsm2 = a2.LocateMyFSM("Mage Lord 2");
            fsm1.SendEvent("FINISHED");
            fsm1.SendEvent("GG BOSS");
            fsm1.SendEvent("FINISHED");
            fsm1.SendEvent("FINISHED");
            fsm1.FsmVariables.GetFsmFloat("Bot Y").Value = HeroController.instance.transform.position.y + 1.5f;
            fsm1.FsmVariables.GetFsmFloat("Ground Y").Value = HeroController.instance.transform.position.y + 1.5f;
            fsm1.FsmVariables.GetFsmFloat("Hero Mid X").Value = HeroController.instance.transform.position.x;
            fsm1.FsmVariables.GetFsmFloat("Left X").Value = HeroController.instance.transform.position.x - 13f;
            fsm1.FsmVariables.GetFsmFloat("Right X").Value = HeroController.instance.transform.position.x + 15f;
            fsm1.FsmVariables.GetFsmFloat("Knight Quake Y Max").Value = HeroController.instance.transform.position.y + 4f;
            fsm1.FsmVariables.GetFsmFloat("Quake Y").Value = HeroController.instance.transform.position.y + 7f;
            fsm1.FsmVariables.GetFsmFloat("Shockwave Y").Value = HeroController.instance.transform.position.y - 4f;
            fsm1.FsmVariables.GetFsmFloat("Top Y").Value = HeroController.instance.transform.position.y + 7f;

            fsm2.SendEvent("FINISHED");
            fsm2.SendEvent("FINISHED");
            fsm2.SendEvent("PHASE 2");
            fsm2.SendEvent("FINISHED");
            fsm2.SendEvent("GG BOSS");
            fsm2.SendEvent("FINISHED");
            fsm2.SendEvent("FINISHED");
            fsm2.FsmVariables.GetFsmFloat("Bot Y").Value = HeroController.instance.transform.position.y;
            fsm2.FsmVariables.GetFsmFloat("Ground Y").Value = HeroController.instance.transform.position.y;
            fsm2.FsmVariables.GetFsmFloat("Hero Mid X").Value = HeroController.instance.transform.position.x;
            fsm2.FsmVariables.GetFsmFloat("Knight Quake Y Max").Value = HeroController.instance.transform.position.y + 4f;
            fsm2.FsmVariables.GetFsmFloat("Left X").Value = HeroController.instance.transform.position.x - 13f;
            fsm2.FsmVariables.GetFsmFloat("Right X").Value = HeroController.instance.transform.position.x + 15f;
            fsm2.FsmVariables.GetFsmFloat("Quake Y").Value = HeroController.instance.transform.position.y + 7f;
            fsm2.FsmVariables.GetFsmFloat("Shockwave Y").Value = HeroController.instance.transform.position.y + 3f;
            fsm2.FsmVariables.GetFsmFloat("Orb Max X").Value = HeroController.instance.transform.position.x + 20f;
            fsm2.FsmVariables.GetFsmFloat("Orb Max Y").Value = HeroController.instance.transform.position.y + 8f;
            fsm2.FsmVariables.GetFsmFloat("Orb Min X").Value = HeroController.instance.transform.position.x - 10f;
            fsm2.FsmVariables.GetFsmFloat("Orb Min Y").Value = HeroController.instance.transform.position.y;
            a1.transform.position = HeroController.instance.transform.position + new Vector3(5f, 5f);
            a1.SetScale(scale.Item1, scale.Item2);
            a2.SetScale(scale.Item1, scale.Item2);
            a2.transform.position = HeroController.instance.transform.position + new Vector3(5f, 5f);
            AfterSummon?.Invoke(a1);
            AfterSummon?.Invoke(a2);

        }
        // OBOB
        public static void summonOBOB(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Oblobbles"]["Mega Fat Bee"]);
            var a2 = UObject.Instantiate(preloadedObjects["GG_Oblobbles"]["Mega Fat Bee (1)"]);
            a.SetActive(true);
            a2.SetActive(true);
            var fsm = a.LocateMyFSM("fat fly bounce");
            var fsm2 = a2.LocateMyFSM("fat fly bounce");
            fsm.SendEvent("FINISHED");
            fsm.FsmVariables.GetFsmFloat("X Max").Value = HeroController.instance.transform.position.x + 16f;
            fsm.FsmVariables.GetFsmFloat("X Min").Value = HeroController.instance.transform.position.x - 16f;
            fsm2.FsmVariables.GetFsmFloat("X Max").Value = HeroController.instance.transform.position.x + 16f;
            fsm2.FsmVariables.GetFsmFloat("X Min").Value = HeroController.instance.transform.position.x - 16f;
            fsm2.SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a2.SetScale(scale.Item1, scale.Item2);
            a.SetScale(1, 1);
            a2.SetScale(1, 1);
            a.transform.position = HeroController.instance.transform.position + new Vector3(10f, 20f, 25f);
            a2.transform.position = HeroController.instance.transform.position + new Vector3(-10f, 20f, 25f);
            AfterSummon?.Invoke(a);
            AfterSummon?.Invoke(a2);
        }
        // ML -- wall heights are hardcoded
        public static void summonML(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Mantis_Lords"]["Mantis Battle"]);
            var m1 = a.transform.Find("Battle Main").Find("Mantis Lord").gameObject;
            var m21 = a.transform.Find("Battle Sub").Find("Mantis Lord S1").gameObject;
            var m22 = a.transform.Find("Battle Sub").Find("Mantis Lord S2").gameObject;
            a.SetActive(true);
            m1.SetActive(true);
            var fsm1 = m1.LocateMyFSM("Mantis Lord");
            var fsm21 = m21.LocateMyFSM("Mantis Lord");
            var fsm22 = m22.LocateMyFSM("Mantis Lord");
            mantisLoc(fsm1);
            mantisLoc(fsm21);
            mantisLoc(fsm22);
            fsm1.SendEvent("FINISHED");
            fsm1.SendEvent("FINISHED");
            fsm1.SendEvent("MLORD START MAIN");
            a.transform.position = HeroController.instance.transform.position - new Vector3(30f, 10f);
            m1.transform.position = HeroController.instance.transform.position + new Vector3(5f, 0f);
            m1.SetScale(scale.Item1, scale.Item2);
            m21.SetScale(scale.Item1, scale.Item2);
            m22.SetScale(scale.Item1, scale.Item2);
            AfterSummon?.Invoke(a);
        }
        private static void mantisLoc(PlayMakerFSM fsm)
        {
            fsm.FsmVariables.GetFsmFloat("Dash X L").Value = HeroController.instance.transform.position.x - 7.6f;
            fsm.FsmVariables.GetFsmFloat("Dash X R").Value = HeroController.instance.transform.position.x + 7.6f;
            fsm.FsmVariables.GetFsmFloat("Dash Y").Value = HeroController.instance.transform.position.y + 3f;
            fsm.FsmVariables.GetFsmFloat("Dstab X Min").Value = HeroController.instance.transform.position.x - 12f;
            fsm.FsmVariables.GetFsmFloat("Dstab X Max").Value = HeroController.instance.transform.position.x + 12f;
            fsm.FsmVariables.GetFsmFloat("Dstab Y").Value = HeroController.instance.transform.position.y + 9f;
            fsm.FsmVariables.GetFsmFloat("Land Y").Value = HeroController.instance.transform.position.y + 1.5f;
            fsm.FsmVariables.GetFsmFloat("Wall L").Value = HeroController.instance.transform.position.x - 15f;
            fsm.FsmVariables.GetFsmFloat("Wall R").Value = HeroController.instance.transform.position.x + 15f;
            fsm.FsmVariables.GetFsmFloat("Wall Y Max").Value = HeroController.instance.transform.position.y + 8f;
            fsm.FsmVariables.GetFsmFloat("Wall Y Min").Value = HeroController.instance.transform.position.y + 5f;
        }
        // SOB
        public static void summonSOB(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Mantis_Lords_V"]["Mantis Battle"]);
            var m1 = a.transform.Find("Battle Main").Find("Mantis Lord").gameObject;
            var m21 = a.transform.Find("Battle Sub").Find("Mantis Lord S1").gameObject;
            var m22 = a.transform.Find("Battle Sub").Find("Mantis Lord S2").gameObject;
            var m23 = a.transform.Find("Battle Sub").Find("Mantis Lord S3").gameObject;
            a.SetActive(true);
            var fsm1 = m1.LocateMyFSM("Mantis Lord");
            var fsm21 = m21.LocateMyFSM("Mantis Lord");
            var fsm22 = m22.LocateMyFSM("Mantis Lord");
            var fsm23 = m23.LocateMyFSM("Mantis Lord");
            mantisLoc(fsm1);
            mantisLoc(fsm21);
            mantisLoc(fsm22);
            mantisLoc(fsm23);
            fsm1.SendEvent("FINISHED");
            fsm1.SendEvent("FINISHED");
            fsm1.SendEvent("MLORD START MAIN");
            a.transform.position = HeroController.instance.transform.position - new Vector3(30f, 10f);
            m1.transform.position = HeroController.instance.transform.position + new Vector3(5f, 0f);
            m1.SetScale(scale.Item1, scale.Item2);
            m21.SetScale(scale.Item1, scale.Item2);
            m22.SetScale(scale.Item1, scale.Item2);
            m23.SetScale(scale.Item1, scale.Item2);
            AfterSummon?.Invoke(a);
        }
        // MAR
        public static void summonMAR(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Ghost_Marmu_V"]["Warrior/Ghost Warrior Marmu"]);
            a.SetActive(true);
            var fsm = a.LocateMyFSM("Control");
            fsm.FsmVariables.GetFsmFloat("Tele X Max").Value = HeroController.instance.transform.position.x + 8f;
            fsm.FsmVariables.GetFsmFloat("Tele X Min").Value = HeroController.instance.transform.position.x - 8f;
            fsm.FsmVariables.GetFsmFloat("Tele Y Min").Value = HeroController.instance.transform.position.y + 2f;
            fsm.FsmVariables.GetFsmFloat("Tele Y Max").Value = HeroController.instance.transform.position.y + 7f;
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(0f, 10f);
            AfterSummon?.Invoke(a);
        }
        // FM
        public static void summonFM(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Flukemarm"]["Fluke Mother"]);
            a.SetActive(true);
            var fsm = a.LocateMyFSM("Fluke Mother");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("GG BOSS");
            fsm.SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(0f, 10f);
            AfterSummon?.Invoke(a);
        }
        // BV
        public static void summonBV(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Broken_Vessel"]["Infected Knight"]);
            a.SetActive(true);
            PlayMakerFSM fsm = a.LocateMyFSM("IK Control");
            fsm.FsmVariables.GetFsmFloat("Air Dash Height").Value = HeroController.instance.transform.position.y + 3.1f;
            fsm.FsmVariables.GetFsmFloat("Left X").Value = HeroController.instance.transform.position.x - 10f;
            fsm.FsmVariables.GetFsmFloat("Right X").Value = HeroController.instance.transform.position.x + 10f;
            fsm.FsmVariables.GetFsmFloat("Min Dstab Height").Value = HeroController.instance.transform.position.y + 4.91f;
            fsm.SendEvent("FINISHED"); // 22, 28.4
            fsm.SendEvent("ACTIVE");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("GG_BOSS");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("LAND");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            fsm.SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(10f, 0f);
            AfterSummon?.Invoke(a);
        }
        // GAL
        public static void summonGAL(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var h = UObject.Instantiate(preloadedObjects["GG_Ghost_Galien"]["Warrior/Galien Hammer"]);
            h.SetActive(true);
            h.transform.position = HeroController.instance.transform.position + new Vector3(8f, 0f);
            h.SetScale(scale.Item1, scale.Item2);
            h.LocateMyFSM("Control").SendEvent("READY");
            h.LocateMyFSM("Control").SendEvent("READY");
            var fsmh = h.LocateMyFSM("Attack");
            fsmh.FsmVariables.GetFsmFloat("Floor Y").Value = HeroController.instance.transform.position.y;
            fsmh.FsmVariables.GetFsmFloat("Slam Y").Value = HeroController.instance.transform.position.y;
            fsmh.FsmVariables.GetFsmFloat("Wall L X").Value = HeroController.instance.transform.position.x - 16f;
            fsmh.FsmVariables.GetFsmFloat("Wall R X").Value = HeroController.instance.transform.position.x + 16f;
            var a = UObject.Instantiate(preloadedObjects["GG_Ghost_Galien"]["Warrior/Ghost Warrior Galien"]);
            a.SetActive(true);
            // todo: localize p1-p7
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 5f);
        }
        // PATOMAS SHEO
        public static void summonPATOMAS_SHEO(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var p = UObject.Instantiate(preloadedObjects["GG_Painter"]["Battle Scene/Paint Splats"]);
            p.SetActive(true);
            var a = UObject.Instantiate(preloadedObjects["GG_Painter"]["Battle Scene/Sheo Boss"]);
            a.SetActive(true);
            var fsm = a.LocateMyFSM("nailmaster_sheo");
            fsm.SendEvent("FINISHED");
            fsm.FsmVariables.GetFsmFloat("Topslash Y").Value = HeroController.instance.transform.position.y + 10.61f;
            a.SetScale(scale.Item1, scale.Item2); // 41.85, 5.41
            a.transform.position = HeroController.instance.transform.position + new Vector3(10f, 1.5f);
            AfterSummon?.Invoke(a);
        }
        // HiK
        // HU
        // JC
        public static void summonJC(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Collector"]["Battle Scene/Jar Collector"]);
            a.SetActive(true);
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("WAKE");
            a.LocateMyFSM("Control").SendEvent("LAND");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 0f);
            AfterSummon?.Invoke(a);
        }
        // GT
        // TMG
        // WK
        // UMU
        // NSK
        // WN
        public static void summonWN(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Nosk_Hornet"]["Battle Scene/Hornet Nosk"]);
            a.SetActive(true);
            a.LocateMyFSM("Hornet Nosk").SendEvent("FINISHED");
            a.LocateMyFSM("Hornet Nosk").SendEvent("FINISHED");
            a.LocateMyFSM("Hornet Nosk").SendEvent("START");
            a.LocateMyFSM("Hornet Nosk").SendEvent("FINISHED");
            a.LocateMyFSM("Hornet Nosk").SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 10f);
            AfterSummon?.Invoke(a);
        }
        // SLY
        // H2
        public static void summonH2(Dictionary<string, Dictionary<string, GameObject>> preloadedObjects, (float, float) scale)
        {
            var b = UObject.Instantiate(preloadedObjects["GG_Hornet_2"]["Barb Region"]);
            b.SetActive(true);
            b.transform.position = HeroController.instance.transform.position + new Vector3(0f, 0f);
            var a = UObject.Instantiate(preloadedObjects["Deepnest_East_Hornet_boss"]["Hornet Boss 2"]);
            a.LocateMyFSM("destroy_if_playerdatabool").FsmVariables.GetFsmString("playerData bool").Value = "";
            a.SetActive(true);
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("GG BOSS");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("WAKE");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("LAND");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.LocateMyFSM("Control").SendEvent("FINISHED");
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(5f, 0f);
            AfterSummon?.Invoke(a);
        }
        // EG
        // LK99
        // NE
        // TL
        // WD GASTER
        // ST
        // BITCH
        // GPZ
        // FC
        // NKG
        // HK
        // PV
        // RAD
        // AR
        public static void summonAR(Dictionary<String, Dictionary<String, GameObject>> preloadedObjects, (float, float) scale)
        {
            var a = UObject.Instantiate(preloadedObjects["GG_Radiance"]["Boss Control"]);
            a.SetActive(true);
            a.SetScale(scale.Item1, scale.Item2);
            a.transform.position = HeroController.instance.transform.position + new Vector3(0f, 100f);
            AfterSummon?.Invoke(a);
        }

        public static void removePatomas(HealthManager hm)
        {
            hm.SetGeoLarge(0);
            hm.SetGeoMedium(0);
            hm.SetGeoSmall(0);
        }
    }
}
