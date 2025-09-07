using InControl;
using Modding;
using Modding.Converters;
using Newtonsoft.Json;
using Satchel;
using Satchel.BetterMenus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SacredTreasure
{
    using STPreloads = Dictionary<string, Dictionary<string, GameObject>>;
    public class SacredTreasure : Mod, ICustomMenuMod, IGlobalSettings<STGlobalSettings>
    {

        private Menu MenuRef;
        private Menu PrimaryRef;
        private Menu SecondaryRef;
        private CustomSlider pSliderX = new(
                        name: "P X Scale",
                        storeValue: val => {
                            GS.primaryScaleX = val;
                        },
                        loadValue: () => GS.primaryScaleX,
                        minValue: -5,
                        maxValue: 5,
                        wholeNumbers: false
                    );
        private CustomSlider pSliderY = new(
                        name: "P Y Scale",
                        storeValue: val => {
                            GS.primaryScaleY = val;
                        },
                        loadValue: () => GS.primaryScaleY,
                        minValue: -5,
                        maxValue: 5,
                        wholeNumbers: false
                    );
        private CustomSlider sSliderX = new(
                        name: "S X Scale",
                        storeValue: val =>
                        {
                            GS.secondaryScaleX = val;
                        },
                        loadValue: () => GS.secondaryScaleX,
                        minValue: -5,
                        maxValue: 5,
                        wholeNumbers: false
                    );
        private CustomSlider sSliderY = new(
                        name: "S Y Slider",
                        storeValue: val =>
                        {
                            GS.secondaryScaleY = val;
                        },
                        loadValue: () => GS.secondaryScaleY,
                        minValue: -5,
                        maxValue: 5,
                        wholeNumbers: false
                    );

        public static STGlobalSettings GS { get; set; } = new STGlobalSettings();

        public bool ToggleButtonInsideMenu => false;

        public void OnLoadGlobal(STGlobalSettings s)
        {
            GS = s;
        }

        public STGlobalSettings OnSaveGlobal()
        {
            return GS;
        }

        public MenuScreen GetMenuScreen(MenuScreen modListMenu, ModToggleDelegates? modToggleDelegates)
        {
            PrimaryRef ??= new Menu(
                name: "Primary Summon",
                elements: [
                    new HorizontalOption(
                        name: "Boss",
                        description: "Object to summon with the primary button",
                        values: Lists.fancyNames,
                        applySetting: index => GS.primarySummon = Lists.bossAbbrev[index],
                        loadSetting: () => Array.IndexOf(Lists.bossAbbrev, GS.primarySummon)
                    ),
                    Blueprints.KeyAndButtonBind(
                        name: "Bindings",
                        keyBindAction: GS.binds.primarySummon,
                        buttonBindAction: GS.binds.primarySummon
                    ),
                    pSliderX,
                    new MenuButton("Reset P XScale",
                        "Resets Primary X-Scale to 1.0",
                        _ => {
                            GS.primaryScaleX = 1f;
                            pSliderX.value = 1f;
                            pSliderX.slider.value = 1f;
                        }),
                    pSliderY,
                    new MenuButton("Reset P YScale",
                        "Resets Primary Y-Scale to 1.0",
                        _ => {
                            GS.primaryScaleY = 1f;
                            pSliderY.value = 1f;
                            pSliderY.slider.value = 1f;
                        }),
                ]
            );

            SecondaryRef ??= new Menu(
                name: "Secondary Summon",
                elements: [
                    new HorizontalOption(
                        name: "Boss",
                        description: "Object to summon with the secondary button",
                        values: Lists.fancyNames,
                        applySetting: index => GS.secondarySummon = Lists.bossAbbrev[index],
                        loadSetting: () => Array.IndexOf(Lists.bossAbbrev, GS.secondarySummon)
                    ),
                    Blueprints.KeyAndButtonBind(
                        name: "Bindings",
                        keyBindAction: GS.binds.secondarySummon,
                        buttonBindAction: GS.binds.secondarySummon
                    ),
                    sSliderX,
                    new MenuButton("Reset S XScale",
                        "Resets Secondary X-Scale to 1.0",
                        _ => {
                            GS.secondaryScaleX = 1f;
                            sSliderX.value = 1f;
                            sSliderX.slider.value = 1f;
                        }),
                    sSliderY,
                    new MenuButton("Reset S YScale",
                        "Resets Secondary Y-Scale to 1.0",
                        _ => {
                            GS.secondaryScaleY = 1f;
                            sSliderY.value = 1f;
                            sSliderY.slider.value = 1f;
                        })
                ]
            );

            if (MenuRef == null)
            {
                MenuRef = new Menu(
                    name: "Sacred Treasure",
                    elements: []
                );

                MenuRef.AddElement(new MenuButton("Primary", "Primary Summon", _ => Utils.GoToMenuScreen(PrimaryRef.returnScreen == MenuRef.menuScreen ? PrimaryRef.menuScreen : PrimaryRef.GetMenuScreen(MenuRef.menuScreen)), true));
                MenuRef.AddElement(new MenuButton("Secondary", "Secondary Summon", _ => Utils.GoToMenuScreen(SecondaryRef.returnScreen == MenuRef.menuScreen ? SecondaryRef.menuScreen : SecondaryRef.GetMenuScreen(MenuRef.menuScreen)), true));
            }
            return MenuRef.GetMenuScreen(modListMenu);
        }

        public Action mainHookCacheForRemoval;

        public static Dictionary<String, Action<STPreloads, (float, float)>> summoners =
            Lists.bossAbbrev.Where(v => typeof(Summoning).GetMethod("summon" + v, BindingFlags.Public | BindingFlags.Static) != null).Select(v =>
            {
                var _ref = typeof(Summoning).GetMethod("summon" + v, BindingFlags.Public | BindingFlags.Static);
                return new { v, action = (Action<STPreloads, (float, float)>)Delegate.CreateDelegate(typeof(Action<STPreloads, (float, float)>), _ref) };
            }).ToDictionary(x => x.v, x => x.action);

        public SacredTreasure() : base("SacredTreasure") {}
        public override string GetVersion() => "1.0.0";
        public override List<(string, string)> GetPreloadNames() => Lists.loadings;
        public override void Initialize(STPreloads preloadedObjects)
        {
            Summoning.AfterSummon += LogFSMs;
            foreach (var item in summoners)
            {
                Log("Found summoner for: " + item.Key);
            }
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += DumpScene;
            mainHookCacheForRemoval = () =>
            {
                if (GS.binds.primarySummon.HasChanged && GS.binds.primarySummon.IsPressed)
                {
                    Summoning.BeforeSummon?.Invoke();
                    summoners[GS.primarySummon](preloadedObjects, (GS.primaryScaleX, GS.primaryScaleY));
                }
                if (GS.binds.secondarySummon.HasChanged && GS.binds.secondarySummon.IsPressed)
                {
                    Summoning.BeforeSummon?.Invoke();
                    summoners[GS.secondarySummon](preloadedObjects, (GS.secondaryScaleX, GS.secondaryScaleY));
                }
            };
            ModHooks.HeroUpdateHook += mainHookCacheForRemoval;
            ModHooks.SlashHitHook += (other, _) =>
            {
            };
        }

        private void DumpScene(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
        {
            Log($"--- Dumping Scene: {scene.name} ---");

            foreach (GameObject go in scene.GetRootGameObjects())
            {
                go.LogWithChildren();
                LogFSMs(go);
            }
        }

        public void LogFSMs(GameObject o)
        {
            foreach (var item in o.GetComponents<PlayMakerFSM>())
            {
                Log($"FSM: {item.FsmName}");
                foreach (var item1 in item.FsmVariables.GetAllNamedVariables())
                {
                    Log($"Variable: {item1.Name}, Value: {item1.RawValue}");
                }
            }
        }
    }

    public class STBinds : PlayerActionSet
    {
        public PlayerAction primarySummon;
        public PlayerAction secondarySummon;
        public STBinds()
        {
            primarySummon = CreatePlayerAction("STPrimary");
            primarySummon.AddDefaultBinding(Key.O);
            secondarySummon = CreatePlayerAction("STSecondary");
            secondarySummon.AddDefaultBinding(Key.P);
        }
    }

    public class STGlobalSettings
    {
        public String primarySummon = "H2";
        public String secondarySummon = "BV";
        public float primaryScaleX = 1f;
        public float primaryScaleY = 1f;
        public float secondaryScaleX = 1f;
        public float secondaryScaleY = 1f;

        [JsonConverter(typeof(PlayerActionSetConverter))]
        public STBinds binds = new();
    }
}