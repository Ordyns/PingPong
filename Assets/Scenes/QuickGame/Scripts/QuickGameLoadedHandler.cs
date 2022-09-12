using UnityEngine;

public class QuickGameLoadedHandler : MonoBehaviour
{
    [SerializeField] private Bot bot;
    [SerializeField] private BotDifficultyFactory botDifficultyFactory;
    [Space]
    [SerializeField] private QuickGameUI quickGameUI;
    [Space]
    [SerializeField] private ServeManager serveManager;

    private ScoreViewModel _scoreViewModel;
    private ScoreManager _scoreManager;

    private void Awake() {
        _scoreViewModel = new ScoreViewModel();
        
        _scoreManager = new ScoreManager(_scoreViewModel, serveManager);

        quickGameUI.Init(_scoreViewModel);

        InitBot();
    }

    private void InitBot(){
        if(ProjectContext.Instance == null)
            return;

        QuickGameSettings quickGameSettings = ProjectContext.Instance.QuickGameSettingsContainer.QuickGameSettings;
        BotDifficulty botDifficulty = botDifficultyFactory.Get(quickGameSettings.Difficulty);
        bot.SetDifficulty(botDifficulty);
    }
}
