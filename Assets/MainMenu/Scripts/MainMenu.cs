using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private MenuCamera menuCamera;
    [SerializeField] private MainMenuUI ui;
    [Space]
    [SerializeField] private string gameSceneName;

    public void MoveToDefault(){
        menuCamera.MoveToDefault();
        ui.CloseAll();
    }

    public void OpenDifficultySelection(){
        menuCamera.MoveToDifficultySelection();
        ui.OpenDifficultySelection();
    }

    public void LoadGame(BotDifficulty difficulty){
        SceneManager.LoadScene(gameSceneName);
    }
}