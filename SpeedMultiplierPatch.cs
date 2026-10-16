using CustomPlayerEffects;
using HarmonyLib;
using JetBrains.Annotations;

namespace BetterColas;

[UsedImplicitly]
[HarmonyPatch(typeof(Scp207Stack), nameof(Scp207Stack.SpeedMultiplier), MethodType.Getter)]
public static class SpeedMultiplierPatch
{
    // ReSharper disable once InconsistentNaming
    [UsedImplicitly]
    private static void Postfix(ref float __result)
    {
        if (!MainPlugin.Instance.PatchesEnabled)
            return;

        __result *= 2;
    }
}
