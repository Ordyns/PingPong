public class QuickGameSettings
{
    public QuickGameSettings(QuickGameDifficulty difficulty){
        Difficulty = difficulty;
    }

    public QuickGameDifficulty Difficulty { get; }
}

public enum QuickGameDifficulty
{
    Easy,
    Normal,
    Hard
}