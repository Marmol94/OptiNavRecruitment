using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;
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
        private ImageSource _convertedImage;
        private ImageSource? _convertedImage1;
        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainWindowViewModel()
        {
            LoadFileCommand = new RelayCommand(_ => ExecuteLoadFile(), o => true);
            SaveFileCommand = new RelayCommand(_=>ExecuteSaveFile(), o=> true);
            ConvertCommand = new RelayCommand(_=>ExecuteConvert(), o=> true);
            service = new ImageProcessingService();
        }

        public Stream? LoadedImage { get; set; }

        public ImageSource? ConvertedImage
        {
            get => _convertedImage1;
            set
            {
                if (Equals(value, _convertedImage1)) return;
                _convertedImage1 = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand LoadFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand ConvertCommand { get; }
        public ImageProcessingService service { get; }

        private void ExecuteSaveFile()
        {
            var imageService = new ImageProcessingService();
            var dialog = new SaveFileDialog {Filter = "PNG File(*.PNG)|*.PNG|JPG File(*.JPG)|*.JPG|BMP File(*.BMP)|*.BMP",};
            if (dialog.ShowDialog() != true) return;
            File.WriteAllBytes(dialog.FileName, imageService.ConvertToStream(ConvertedImage).ToArray());

        }
        private void ExecuteLoadFile()
        {
            var dialog = new OpenFileDialog {Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"};
            if (dialog.ShowDialog() == true)
            {
                LoadedImagePath = dialog.FileName;
                LoadedImage = File.OpenRead(LoadedImagePath);
            }
        }

        private void ExecuteConvert()
        {
            var wpfImage = service.ConvertGdiToWpf(System.Drawing.Image.FromStream(LoadedImage));
            ConvertedImage = service.ConvertToMainColors(wpfImage);
            
        }
        
    }
}