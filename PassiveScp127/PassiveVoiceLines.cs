using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules.Scp127;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.Arguments.ServerEvents;
using LabApi.Features.Console;
using LabApi.Features.Wrappers;
using MEC;

namespace PassiveScp127;

public class PassiveVoiceLines
{
    Dictionary<ushort, float> Scp127ToConversationClock = new Dictionary<ushort, float>();
    Dictionary<ushort, List<Player>> Scp127ToPlayersNearby = new Dictionary<ushort, List<Player>>();
    Dictionary<ushort, Scp127VoiceLineManagerModule> Scp127ToVoiceModule = new Dictionary<ushort, Scp127VoiceLineManagerModule>();

    private CoroutineHandle _voiceCoroutine;

    public void StartCoroutineAtRoundStart()
    {
        _voiceCoroutine = Timing.RunCoroutine(HandlePassiveVoiceLines());
    }

    public void KillCoroutineAtRoundEnd(RoundEndedEventArgs? ev)
    {
        Timing.KillCoroutines(_voiceCoroutine);
    }

    public void ReleaseDataOnPickup(PlayerPickedUpItemEventArgs ev)
    {
        if (ev.Item.Type != ItemType.GunSCP127)
            return;
        
        Scp127ToConversationClock.Remove(ev.Item.Serial);
        Scp127ToPlayersNearby.Remove(ev.Item.Serial);
        Scp127ToVoiceModule.Remove(ev.Item.Serial);
    }

    public void PopulateDataOnDrop(PlayerDroppingItemEventArgs ev)
    {
        if (ev.Item.Type != ItemType.GunSCP127)
            return;

        try
        {            
            Scp127ToConversationClock.Add(ev.Item.Serial, 0f);
            Scp127ToPlayersNearby.Add(ev.Item.Serial, new List<Player>());
            Scp127ToVoiceModule.Add(ev.Item.Serial, ((ev.Item.Base as Firearm)!.Modules.First(x => x is Scp127VoiceLineManagerModule) as Scp127VoiceLineManagerModule)!);
        }
        catch (Exception ex)
        {
            Logger.Error($"Failed populating dictionaries!\n{ex}");
        }
    }

    public IEnumerator<float> HandlePassiveVoiceLines()
    {
        while (true)
        {
            yield return Timing.WaitForSeconds(Plugin.Instance!.Config.HumanCheckInterval);

            foreach (ushort key in Scp127ToVoiceModule.Keys)
            {
                if (!Pickup.TryGet(key, out Pickup? scp127Pickup))
                    continue;

                int currentHumansNearby = Scp127ToPlayersNearby[key].Count;

                Scp127ToPlayersNearby[key] = scp127Pickup.Position.GetPlayersWithinRange(Plugin.Instance!.Config.HumanDetectionRadius)
                    .Where(x => x.IsHuman)
                    .ToList();

                // Greet first human nearby
                if (currentHumansNearby <= 0)
                {
                    if (Scp127ToPlayersNearby[key].Count > 0)
                    {                        
                        if (Scp127ToVoiceModule[key].TryFindVoiceLine(Plugin.Instance!.Config.GreetingVoiceLines.RandomItem(), out var trigger, out var clip))
                            Scp127ToVoiceModule[key].ServerSendVoiceLine(trigger, null, clip, (byte)Scp127VoiceTriggerBase.VoiceLinePriority.Normal);
                    }
                    
                    continue;
                }

                if (currentHumansNearby > 0)
                {
                    // Call out to abandoning players and reset conversation counter
                    if (Scp127ToPlayersNearby[key].Count <= 0)
                    {                        
                        if (Scp127ToVoiceModule[key].TryFindVoiceLine(Plugin.Instance!.Config.AbandonVoiceLines.RandomItem(), out var trigger, out var clip))
                            Scp127ToVoiceModule[key].ServerSendVoiceLine(trigger, null, clip, (byte)Scp127VoiceTriggerBase.VoiceLinePriority.Normal);
                        
                        Scp127ToConversationClock[key] = 0f;
                    }
                    // Conversation clock
                    else
                    {
                        Scp127ToConversationClock[key] += Plugin.Instance!.Config.HumanCheckInterval;

                        // Call out to players standing for a while and reset clock
                        if (Scp127ToConversationClock[key] >= Plugin.Instance!.Config.ContinueConversationInterval)
                        {
                            if (Scp127ToVoiceModule[key].TryFindVoiceLine(Plugin.Instance!.Config.ContinueConversationVoiceLines.RandomItem(), out var trigger, out var clip))
                                Scp127ToVoiceModule[key].ServerSendVoiceLine(trigger, null, clip, (byte)Scp127VoiceTriggerBase.VoiceLinePriority.Normal);

                            Scp127ToConversationClock[key] = 0f;
                        }
                    }
                }
            }
        }
    }
}