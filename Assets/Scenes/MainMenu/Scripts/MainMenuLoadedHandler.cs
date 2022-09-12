using UnityEngine;

public class MainMenuLoadedHandler : MonoBehaviour
{
    [SerializeField] private MainMenu mainMenu;
    [SerializeField] private MainMenuUI mainMenuUI;

    private void Awake() {
        MainMenuViewModel mainMenuViewModel = new MainMenuViewModel(mainMenu);
        mainMenuUI.Init(mainMenuViewModel);
    }
}
