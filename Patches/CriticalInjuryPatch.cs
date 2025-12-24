using HarmonyLib;
using RandomizedTexts.Extensions;
using TMPro;
using UnityEngine;

namespace RandomizedTexts.Patches
{
    internal class CriticalInjuryPatch
    {
        [HarmonyPatch(typeof(HUDManager), "UpdateHealthUI")]
        [HarmonyPostfix]
        private static void UpdateHealthUIPostFix(HUDManager __instance, ref int health, ref bool hurtPlayer)
        {
            if (hurtPlayer && health < 20)
            {
                string selectedText = "";
                GameObject criticalInjuryTextObject = GameObject.Find("Systems/UI/Canvas/IngamePlayerHUD/SpecialHUDGraphics/CriticalInjury/TipLeft1");
                if (criticalInjuryTextObject != null)
                {
                    TextMeshProUGUI component = criticalInjuryTextObject.GetComponent<TextMeshProUGUI>();
                    if (RandomizedTexts.seedBasedRandom)
                    {
                        System.Random rand = new(StartOfRound.Instance.randomMapSeed + 75);
                        selectedText = rand.NextItem(RandomizedTexts.criticalInjuryMessages);
                    }
                    else
                    {
                        System.Random rand = new();
                        selectedText = rand.NextItem(RandomizedTexts.criticalInjuryMessages);
                    }
                    component.text = selectedText;
                    component.fontSize = RandomizedTexts.criticalInjuryFontSize;
                }
            }
        }
    }
}
