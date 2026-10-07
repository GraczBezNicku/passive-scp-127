# Passive SCP-127
This plugin allows SCP-127 to talk to nearby humans when not picked up. It will greet oncoming humans, converse with them if they stand nearby long enough, and call out to them if they leave without SCP-127.
## Configuration
The configuration file will be generated on first boot with the plugin installed.
| Name                           | Type                            | Description                                                                      |
|--------------------------------|---------------------------------|----------------------------------------------------------------------------------|
| HumanCheckInterval             | float                           | How often (in seconds) will human presence be checked.                |
| HumanDetectionRadius           | float                           | Radius of the human presence check.                                      |
| ContinueConversationInterval   | float                           | How long humans have to be around SCP-127 to initiate a conversation.        |
| GreetingVoiceLines             | List<[Scp127VoiceLinesTranslation](https://github.com/GraczBezNicku/passive-scp-127/blob/main/passive-scp-127/Scp127VoiceLinesTranslation.txt)>                             | List of all possible voicelines to be played when a human approaches SCP-127. |
| ContinueConversationVoiceLines | List<[Scp127VoiceLinesTranslation](https://github.com/GraczBezNicku/passive-scp-127/blob/main/passive-scp-127/Scp127VoiceLinesTranslation.txt)>                             | List of all possible voicelines to be played when a human stands around SCP-127.     |
| AbandonVoiceLines              | List<[Scp127VoiceLinesTranslation](https://github.com/GraczBezNicku/passive-scp-127/blob/main/passive-scp-127/Scp127VoiceLinesTranslation.txt)>                             | List of all possible voicelines to be played when a human abandons SCP-127.
## Demonstration
Here's a [video](https://youtu.be/e8juoWucf2A) demonstrating how the plugin works in practice
