using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = nameof(BotDifficulty), menuName = "Bot/" + nameof(BotDifficulty))]
public class BotDifficulty : ScriptableObject
{
    [field:SerializeField] [field:Expandable] public BotState BotServingState { get; private set; }
    [field:SerializeField] [field:Expandable] public BotState PlayerServingState { get; private set; }
    [field:SerializeField] [field:Expandable] public BotState BallHittedByPlayerState { get; private set; }
    [field:SerializeField] [field:Expandable] public BotState BallHittedByBotState { get; private set; }
}
