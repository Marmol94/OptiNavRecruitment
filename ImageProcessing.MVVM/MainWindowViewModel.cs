using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace ImageProcessing.MVVM
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object?, bool> _canExecute;
    
        public RelayCommand(Action<object> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (_ => true);
        }
    
        public bool CanExecute(object? parameter) => _canExecute(parameter);
    
        public void Execute(object? parameter)
        {
            if (parameter != null) _execute(parameter);
        }
    
    
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _selectedImagePath;
        private string _savedImagePath;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string SelectedImagePath
        {
            get => _selectedImagePath;
            set
            {
                if (value == _selectedImagePath) return;
                _selectedImagePath = value;
                OnPropertyChanged();
            }
        }

        public string SavedImagePath
        {
            get => _savedImagePath;
            set
            {
                if (value == _savedImagePath) return;
                _savedImagePath = value;
                OnPropertyChanged();
            }
        }
       
    }
}