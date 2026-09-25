using GameNetcodeStuff;
using HarmonyLib;

namespace RandomizedTexts.Patches;

internal class HandsFullMessagePatch
{
    [HarmonyPatch(typeof(PlayerControllerB), nameof(PlayerControllerB.SwitchToItemSlot))]
    [HarmonyPostfix]
    private static void SwitchToItemSlotPostFix(PlayerControllerB __instance)
    {
        if (__instance.IsOwner && HUDManager.Instance.holdingTwoHandedItem.enabled)
        {
            RandomizedTexts.ChangeText("Systems/UI/Canvas/IngamePlayerHUD/HandsFullText", "Hands Full", RandomizedTexts.handsFullMessages, RandomizedTexts.handsFullFontSize, 38);
        }
    }
}
