using LabApi.Features;
using LabApi.Loader.Features.Plugins;

namespace passive_scp_127;

public class Plugin : Plugin<Config>
{
    public override string Name => "Passive SCP-127";

    public override string Description => "Adds voicelines to SCP-127 when it sits on the floor";

    public override string Author => "GBN";

    public override Version RequiredApiVersion => LabApiProperties.CurrentVersion;

    public override void Disable()
    {
        throw new NotImplementedException();
    }

    public override void Enable()
    {
        throw new NotImplementedException();
    }
}
