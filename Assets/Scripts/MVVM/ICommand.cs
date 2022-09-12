using System;

namespace MVVM
{
    public interface ICommand
    {
        event Action CanExecuteChanged;
    
        bool CanExecute();
        void Execute();
        
        void InvokeCanExecuteChanged();
    }
    
    public interface ICommand<T>
    {
        event Action CanExecuteChanged;
    
        bool CanExecute(T parameter);
        void Execute(T parameter);
    
        void InvokeCanExecuteChanged();
    }
}