using UnityEngine.UI;
using MVVM;

public class BindableButton : Button
{
    private ICommand _bindedCommand;

    public void Bind(ICommand command){
        _bindedCommand = command;
        onClick.AddListener(_bindedCommand.Execute);
        command.CanExecuteChanged += () => interactable = command.CanExecute();
        interactable = command.CanExecute();
    }

    public void Unbind(){
        _bindedCommand = null;
        onClick.RemoveListener(_bindedCommand.Execute);
        _bindedCommand.CanExecuteChanged += OnCanExecuteChanged;
    }

    private void OnCanExecuteChanged(){
        interactable = _bindedCommand.CanExecute();
    }
}