using HarmonyLib;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace RandomizedTexts.Patches;

internal class CriticalInjuryPatch
{
    [HarmonyPatch(typeof(HUDManager), nameof(HUDManager.UpdateHealthUI))]
    [HarmonyPostfix]
    private static void UpdateHealthUIPostFix(HUDManager __instance, ref int health, ref bool hurtPlayer)
    {
        if (hurtPlayer && health < 20)
        {
            RandomizedTexts.ChangeText("Systems/UI/Canvas/IngamePlayerHUD/SpecialHUDGraphics/CriticalInjury/TipLeft1", "Critical Injury", RandomizedTexts.criticalInjuryMessages, RandomizedTexts.criticalInjuryFontSize, 75);
        }
    }
}
