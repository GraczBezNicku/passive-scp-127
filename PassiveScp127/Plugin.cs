using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace PassiveScp127;

public class Plugin : Plugin<Config>
{
    public static Plugin? Instance { get; private set ;}

    public override string Name => "Passive SCP-127";

    public override string Description => "Adds voicelines to SCP-127 when it sits on the floor";

    public override string Author => "GBN";

    public override Version Version => new Version(1, 0, 1);

    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

    private PassiveVoiceLines? _passiveVoiceLines;

    public override void Disable()
    {
        LabApi.Events.Handlers.PlayerEvents.DroppingItem -= _passiveVoiceLines!.PopulateDataOnDrop;
        LabApi.Events.Handlers.PlayerEvents.PickedUpItem -= _passiveVoiceLines!.ReleaseDataOnPickup;

        LabApi.Events.Handlers.ServerEvents.RoundEnded -= _passiveVoiceLines!.KillCoroutineAtRoundEnd;
        LabApi.Events.Handlers.ServerEvents.RoundStarted -= _passiveVoiceLines!.StartCoroutineAtRoundStart;

        _passiveVoiceLines!.KillCoroutineAtRoundEnd(null);
        _passiveVoiceLines = null;

        Instance = null;
    }

    public override void Enable()
    {
        Instance = this;

        _passiveVoiceLines = new PassiveVoiceLines();

        LabApi.Events.Handlers.ServerEvents.RoundStarted += _passiveVoiceLines.StartCoroutineAtRoundStart;
        LabApi.Events.Handlers.ServerEvents.RoundEnded += _passiveVoiceLines.KillCoroutineAtRoundEnd;

        LabApi.Events.Handlers.PlayerEvents.PickedUpItem += _passiveVoiceLines.ReleaseDataOnPickup;
        LabApi.Events.Handlers.PlayerEvents.DroppingItem += _passiveVoiceLines.PopulateDataOnDrop;
    }
}
