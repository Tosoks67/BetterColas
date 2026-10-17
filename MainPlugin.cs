using CustomPlayerEffects;
using JetBrains.Annotations;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Handlers;
using LabApi.Features;
using LabApi.Features.Console;
using LabApi.Loader.Features.Plugins;
using LabApi.Loader.Features.Plugins.Enums;
using PlayerStatsSystem;

namespace BetterColas;

[UsedImplicitly]
public class MainPlugin : Plugin
{
    [UsedImplicitly] public static MainPlugin Instance { get; private set; } = null!;

    // this is here only so that other plugins (e.g. Wylom)
    // may toggle the patches
    [UsedImplicitly] public bool PatchesEnabled { get; set; } = true;

    public override string Name => nameof(BetterColas);
    public override string Description => "extra SCP-207 speed and 0.5x damage";
    public override string Author => "Tosoks67";
    public override Version Version => new(1, 0, 0);
    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

    // highest as to prevent plugins bundling this plugin from enabling before this
    public override LoadPriority Priority => LoadPriority.High;

    public override void Enable()
    {
        Instance = this;

        PlayerEvents.Hurting += OnHurting;
        PlayerEvents.UpdatedEffect += OnUpdatedEffect;
    }

    public override void Disable()
    {
        Instance = null!;

        PlayerEvents.Hurting -= OnHurting;
        PlayerEvents.UpdatedEffect -= OnUpdatedEffect;
    }

    private void OnUpdatedEffect(PlayerEffectUpdatedEventArgs ev)
    {
        if (!PatchesEnabled || ev.Effect is not Scp207)
            return;

        var intensity = ev.Intensity;

        if (intensity is 0)
        {
            ev.Player.DisableEffect<MovementBoost>();
            return;
        }

        ev.Player.EnableEffect<MovementBoost>(
            (byte)Math.Min(intensity * 10, byte.MaxValue)
        );
    }

    private void OnHurting(PlayerHurtingEventArgs ev)
    {
        if (!PatchesEnabled
            || ev.DamageHandler is not UniversalDamageHandler universal
            || universal.TranslationId != DeathTranslations.Scp207.Id)
            return;

        universal.Damage /= 2;
    }
}
