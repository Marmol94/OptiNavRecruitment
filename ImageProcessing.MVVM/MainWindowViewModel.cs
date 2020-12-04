using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace ImageProcessing.MVVM
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Func<object?, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object? parameter) => _canExecute(parameter);

        public void Execute(object? parameter)
        {
           _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }

    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string? _loadedImagePath;
        private string? _savedImagePath;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            LoadFileCommand = new RelayCommand(_ => ExecuteLoadFile(), o => true);
        }

        public Stream? LoadedImage { get; set; }

        public string? LoadedImagePath
        {
            get => _loadedImagePath;
            set
            {
                if (value == _loadedImagePath) return;
                _loadedImagePath = value;
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

        public ICommand LoadFileCommand { get; }

        private void ExecuteLoadFile()
        {
            var dialog = new OpenFileDialog {Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"};
            if (dialog.ShowDialog() == true)
            {
                LoadedImagePath = dialog.FileName;
                LoadedImage = File.OpenRead(LoadedImagePath);
            }
        }
    }
}