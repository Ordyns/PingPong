using MVVM;

public class MainMenuViewModel : ViewModel
{
    public ICommand PlayEasyQuickGameCommand { get; }
    public ICommand PlayNormalQuickGameCommand { get; }
    public ICommand PlayHardQuickGameCommand { get; }

    public MainMenuViewModel(MainMenu mainMenu){
        PlayEasyQuickGameCommand = new DelegateCommand(() => mainMenu.LoadQuickGame(QuickGameDifficulty.Easy));
        PlayNormalQuickGameCommand = new DelegateCommand(() => mainMenu.LoadQuickGame(QuickGameDifficulty.Normal));
        PlayHardQuickGameCommand = new DelegateCommand(() => mainMenu.LoadQuickGame(QuickGameDifficulty.Hard));
    }
}
