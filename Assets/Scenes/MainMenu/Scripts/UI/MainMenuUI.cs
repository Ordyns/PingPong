using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private MenuCamera menuCamera;
    [Space]
    [SerializeField] private MenuPage mainPage;
    [Space]
    [SerializeField] private MenuPage difficultySelectionPage;
    [SerializeField] private DifficultySelectionView difficultySelectionView;

    private MainMenuViewModel _viewModel;

    public void Init(MainMenuViewModel viewModel) {
        _viewModel = viewModel;
        difficultySelectionView.Init(viewModel);

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