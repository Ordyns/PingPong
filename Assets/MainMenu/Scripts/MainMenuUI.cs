using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private MenuCamera menuCamera;
    [Space]
    [SerializeField] private MenuPage mainPage;
    [SerializeField] private MenuPage difficultySelectionPage;

    private MainMenuViewModel _viewModel;

    private void Awake() {
        mainPage.OpenInstantly();
        difficultySelectionPage.CloseInstantly();   
    }

    public void OpenDifficultySelection(){
        difficultySelectionPage.Open();
        mainPage.Close();

        menuCamera.MoveToDifficultySelection();
    }

    public void MoveToMainPage(){
        mainPage.Open();
        difficultySelectionPage.Close();

        menuCamera.MoveToDefault();
    }
}