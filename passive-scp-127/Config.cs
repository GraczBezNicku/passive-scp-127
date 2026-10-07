using InventorySystem.Items.Firearms.Modules.Scp127;

namespace passive_scp_127;

public class Config
{
    public float HumanCheckInterval { get; set; } = 0.25f;
    public float HumanDetectionRadius { get; set; } = 2.5f;

    public float ContinueConversationInterval { get; set; } = 15f;

    public List<Scp127VoiceLinesTranslation> GreetingVoiceLines { get; set; } = new List<Scp127VoiceLinesTranslation>()
    {
        Scp127VoiceLinesTranslation.DrawGoodToSeeYa,
        Scp127VoiceLinesTranslation.DrawHelloAgain,
        Scp127VoiceLinesTranslation.DrawHelloThere,
        Scp127VoiceLinesTranslation.DrawHello,
        Scp127VoiceLinesTranslation.DrawHeyHey,
        Scp127VoiceLinesTranslation.DrawHowsItGoin
    };

    public List<Scp127VoiceLinesTranslation> ContinueConversationVoiceLines { get; set; } = new List<Scp127VoiceLinesTranslation>()
    {
        Scp127VoiceLinesTranslation.IdleChatterBarDownInQueens,
        Scp127VoiceLinesTranslation.IdleChatterDifferentFacility,
        Scp127VoiceLinesTranslation.IdleChatterGettingPaidForThisRight,
        Scp127VoiceLinesTranslation.IdleChatterSoundedLikeDeath,
        Scp127VoiceLinesTranslation.IdleChatterThoughtIHeardSomething,
        Scp127VoiceLinesTranslation.IdleChatterWhatWentWrong,
        Scp127VoiceLinesTranslation.IdleChatterYouGotAnyPlans,
        Scp127VoiceLinesTranslation.PickupMtfAnyChanceICanGo,
        Scp127VoiceLinesTranslation.PickupTutorialHereToRescueMe
    };

    public List<Scp127VoiceLinesTranslation> AbandonVoiceLines { get; set; } = new List<Scp127VoiceLinesTranslation>()
    {
        Scp127VoiceLinesTranslation.DroppedDontForgetAboutMe,
        Scp127VoiceLinesTranslation.DroppedGuessIllWaitHere,
        Scp127VoiceLinesTranslation.DroppedIllCatchUp,
        Scp127VoiceLinesTranslation.DroppedSeeyaLaterBoss,
        Scp127VoiceLinesTranslation.DroppedTakeCareBoss,
        Scp127VoiceLinesTranslation.HolsterBeHereIfYouNeedMe,
        Scp127VoiceLinesTranslation.HolsterByeBye,
        Scp127VoiceLinesTranslation.HolsterGoodbye,
        Scp127VoiceLinesTranslation.HolsterYouKnowWhereToFindMeBoss,
        Scp127VoiceLinesTranslation.UserKilledNotAgain2,
        Scp127VoiceLinesTranslation.UserKilledNotAgain,
    };
}