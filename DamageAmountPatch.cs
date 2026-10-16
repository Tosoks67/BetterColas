using CustomPlayerEffects;
using HarmonyLib;
using JetBrains.Annotations;

namespace BetterColas;

[HarmonyPatch(typeof(Scp207), nameof(Scp207.Enabled))]
public static class DamageAmountPatch
{
    // ReSharper disable once InconsistentNaming
    [UsedImplicitly]
    private static void Postfix(ref Scp207 __instance)
    {
        if (!MainPlugin.Instance.PatchesEnabled)
            return;

        for (var i = 0; i < __instance.StackMultipliers.Length; i++)
        {
            var current = __instance.StackMultipliers[i];
            __instance.StackMultipliers[i] = current with
            {
                DamageAmount = current.DamageAmount / 2
            };
        }
    }
}
