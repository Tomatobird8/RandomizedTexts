using HarmonyLib;
using UnityEngine;
using TMPro;
using RandomizedTexts.Extensions;

namespace RandomizedTexts.Patches
{
    internal class AliveMessagePatch
    {
        [HarmonyPatch(typeof(StartOfRound), "Start")]
        [HarmonyPostfix]
        private static void StartPatch(StartOfRound __instance)
        {
            string selectedText = "";
            GameObject aliveTextObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/BottomMiddle/SystemsOnline/TipLeft1");
            if (aliveTextObject != null)
            {
                TextMeshProUGUI component = aliveTextObject.GetComponent<TextMeshProUGUI>();
                if (RandomizedTexts.seedBasedRandom)
                {
                    System.Random rand = new(StartOfRound.Instance.randomMapSeed + 29);
                    selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
                }
                else
                {
                    System.Random rand = new();
                    selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
                }
                component.text = selectedText;
                component.fontSize = RandomizedTexts.aliveFontSize;
            }
        }

        [HarmonyPatch(typeof(HUDManager), "RemoveSpectateUI")]
        [HarmonyPostfix]
        private static void RemoveSpectateUIPatch(HUDManager __instance)
        {
            string selectedText = "";
            GameObject aliveTextObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/BottomMiddle/SystemsOnline/TipLeft1");
            if (aliveTextObject != null)
            {
                TextMeshProUGUI component = aliveTextObject.GetComponent<TextMeshProUGUI>();
                if (RandomizedTexts.seedBasedRandom)
                {
                    System.Random rand = new(StartOfRound.Instance.randomMapSeed + 39);
                    selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
                }
                else
                {
                    System.Random rand = new();
                    selectedText = rand.NextItem(RandomizedTexts.aliveMessages);
                }
                component.text = selectedText;
                component.fontSize = RandomizedTexts.aliveFontSize;
            }
        }
    }
}
