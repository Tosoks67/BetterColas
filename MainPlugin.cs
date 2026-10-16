using HarmonyLib;
using JetBrains.Annotations;
using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace BetterColas;

[UsedImplicitly]
public class MainPlugin : Plugin
{
    private Harmony? _harmony;

    public static MainPlugin Instance { get; private set; } = null!;

    // this is here only so that other plugins (e.g. Wylom)
    // may toggle the patches
    public bool PatchesEnabled { get; set; } = true;

    public override string Name => "BetterColas";
    public override string Description => "2x SCP-207 speed and 0.5x damage";
    public override string Author => "Tosoks67";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

    public override void Enable()
    {
        _harmony = new Harmony("Tosoks67.BetterColas");
        _harmony.PatchAll();

        Instance = this;
    }

    public override void Disable()
    {
        Instance = null!;
    }
}
