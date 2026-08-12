using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using RandomizedTexts.Patches;
using System.Collections.Generic;

namespace RandomizedTexts
{
    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
    public class RandomizedTexts : BaseUnityPlugin
    {
        public static RandomizedTexts Instance { get; private set; } = null!;
        internal new static ManualLogSource Logger { get; private set; } = null!;
        internal static Harmony? Harmony { get; set; }

        public static bool seedBasedRandom = false;
        public static List<string> aliveMessages = [];
        public static float aliveFontSize = 30f;
        public static List<string> handsFullMessages = [];
        public static float handsFullFontSize = 16f;
        public static List<string> criticalInjuryMessages = [];
        public static float criticalInjuryFontSize = 35f;
        public static List<string> deathMessages = [];
        public static float deathFontSize = 45f;
        public static List<string> gameOverMessages = [];
        public static float gameOverFontSize = 80f;
        public static List<string> gameOverSubtitles = [];
        public static float gameOverSubtitleFontSize = 23f;
        public static List<string> loadingTextMessages = [];
        public static float loadingTextFontSize = 35f;

        private void Awake()
        {
            Logger = base.Logger;
            Instance = this;

            seedBasedRandom = Config.Bind<bool>("General","SeedBasedRandom",false,"Should the random texts be based on the current seed?").Value;
            aliveMessages = StringToList(Config.Bind<string>("General", "AliveMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            aliveFontSize = Config.Bind<float>("General","AliveFontSize", 30f,"Set text font size here.").Value;
            handsFullMessages = StringToList(Config.Bind<string>("General", "HandsFullMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            handsFullFontSize = Config.Bind<float>("General", "HandsFullFontSize", 16f, "Set text font size here.").Value;
            criticalInjuryMessages = StringToList(Config.Bind<string>("General", "CriticalInjuryMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            criticalInjuryFontSize = Config.Bind<float>("General", "CriticalInjuryFontSize", 35f, "Set text font size here.").Value;
            deathMessages = StringToList(Config.Bind<string>("General", "DeathMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            deathFontSize = Config.Bind<float>("General", "DeathFontSize", 45f, "Set text font size here.").Value;
            gameOverMessages = StringToList(Config.Bind<string>("General", "GameOverMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            gameOverFontSize = Config.Bind<float>("General", "GameOverFontSize", 80f, "Set text font size here.").Value;
            gameOverSubtitles = StringToList(Config.Bind<string>("General", "GameOverSubtitleMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            gameOverSubtitleFontSize = Config.Bind<float>("General", "GameOverSubtitleFontSize", 23f, "Set text font size here.").Value;
            loadingTextMessages = StringToList(Config.Bind<string>("General", "LoadingTextMessages", "", "Add texts to display here. Separate messages using the | symbol.").Value);
            loadingTextFontSize = Config.Bind<float>("General", "LoadingTextFontSize", 35f, "Set text font size here.").Value;

            Patch();

            Logger.LogInfo($"{MyPluginInfo.PLUGIN_GUID} v{MyPluginInfo.PLUGIN_VERSION} has loaded!");
        }

        internal static void Patch()
        {
            Harmony ??= new Harmony(MyPluginInfo.PLUGIN_GUID);

            if (aliveMessages.Count > 0)
            {
                Logger.LogDebug("Patching.... AliveMessagePatch");
                Harmony.PatchAll(typeof(AliveMessagePatch));
            }
            if (handsFullMessages.Count > 0)
            {
                Logger.LogDebug("Patching.... HandsFullMessagePatch");
                Harmony.PatchAll(typeof(HandsFullMessagePatch));
            }
            if (criticalInjuryMessages.Count > 0)
            {
                Logger.LogDebug("Patching.... CriticalInjuryPatch");
                Harmony.PatchAll(typeof(CriticalInjuryPatch));
            }
            if (deathMessages.Count > 0)
            {
                Logger.LogDebug("Patching.... DeathMessagePatch");
                Harmony.PatchAll(typeof(DeathMessagePatch));
            }
            if (gameOverMessages.Count > 0 || gameOverSubtitles.Count > 0)
            {
                Logger.LogDebug("Patching.... GameOverMessagePatch");
                Harmony.PatchAll(typeof(GameOverMessagePatch));
            }
            if (loadingTextMessages.Count > 0)
            {
                Logger.LogDebug("Patching.... LoadingLevelPatch");
                Harmony.PatchAll(typeof(LoadingLevelPatch));
            }

            Logger.LogDebug("Finished patching!");
        }

        internal static void Unpatch()
        {
            Logger.LogDebug("Unpatching...");

            Harmony?.UnpatchSelf();

            Logger.LogDebug("Finished unpatching!");
        }

        internal static List<string> StringToList(string s)
        {
            string[] sl = s.Trim().Split('|');
            if (sl.Length == 1)
            {
                if (sl[0] == "")
                {
                    return [];
                }
            }
            return [.. sl];
        }
    }
}
