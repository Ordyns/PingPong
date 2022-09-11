using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadGame(BotDifficulty difficulty){
        ScenesLoader.LoadQuickGame();
    }
}