using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultySelectionView : MonoBehaviour
{
    [SerializeField] private BindableButton easyButton;
    [SerializeField] private BindableButton normalButton;
    [SerializeField] private BindableButton hardButton;

    public void Init(MainMenuViewModel viewModel){
        easyButton.Bind(viewModel.PlayEasyQuickGameCommand);
        normalButton.Bind(viewModel.PlayNormalQuickGameCommand);
        hardButton.Bind(viewModel.PlayHardQuickGameCommand);
    }
}
