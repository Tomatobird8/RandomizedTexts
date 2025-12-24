using GameNetcodeStuff;
using HarmonyLib;
using TMPro;
using UnityEngine;
using RandomizedTexts.Extensions;

namespace RandomizedTexts.Patches
{
    internal class DeathMessagePatch
    {
        [HarmonyPatch(typeof(PlayerControllerB), "KillPlayer")]
        [HarmonyPrefix]
        private static void KillPlayerPrefix(PlayerControllerB __instance)
        {
            string selectedText = "";
            GameObject deathTextObject = GameObject.Find("Systems/UI/Canvas/DeathScreen/GameOverText");
            if (deathTextObject != null)
            {
                TextMeshProUGUI component = deathTextObject.GetComponent<TextMeshProUGUI>();
                if (RandomizedTexts.seedBasedRandom)
                {
                    System.Random rand = new(StartOfRound.Instance.randomMapSeed + 420);
                    selectedText = rand.NextItem(RandomizedTexts.deathMessages);
                }
                else
                {
                    System.Random rand = new();
                    selectedText = rand.NextItem(RandomizedTexts.deathMessages);
                }
                component.text = selectedText;
                component.fontSize = RandomizedTexts.deathFontSize;
            }
        }
    }
}
