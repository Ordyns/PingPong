using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void LoadQuickGame(QuickGameDifficulty difficulty){
        QuickGameSettings quickGameSettings = new QuickGameSettings(difficulty);
        ProjectContext.Instance.QuickGameSettingsContainer.SetQuickGameSettings(quickGameSettings);

        ScenesLoader.LoadQuickGame();
    }
}