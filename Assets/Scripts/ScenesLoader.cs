using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private ScenesTransitions scenesTransitions;

    private const string MainMenuSceneName = "MainMenu";
    private const string QuickGameSceneName = "MainScene";

    private static ScenesLoader _instance;

    private void Awake() {
        _instance = this;
    }

    public static void LoadMainMenu(){
        _instance.scenesTransitions.CreateNewTransition(() => {
            SceneManager.LoadSceneAsync(MainMenuSceneName).completed += SceneLoaded;
        });
    }

    public static void LoadQuickGame(){
        _instance.scenesTransitions.CreateNewTransition(() => {
            SceneManager.LoadSceneAsync(QuickGameSceneName).completed += SceneLoaded;
        });
    }

    private static void SceneLoaded(AsyncOperation asyncOperation){
        _instance.scenesTransitions.CloseCurrentTransition();
    }
}
