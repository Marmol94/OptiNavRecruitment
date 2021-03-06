﻿using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using ImageProcessing.Library;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace ImageProcessing.WPF
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(Library.ImageProcessing imageProcessingService)
        {
            _imageProcessingService = imageProcessingService;
            LoadFileCommand = new RelayCommand(_ => ExecuteLoadFile());
            SaveFileCommand = new RelayCommand(_ => ExecuteSaveFile());
            ConvertCommand = new RelayCommand(_ => ExecuteConvert());
            ConvertAsyncCommand = new RelayCommand(async _ => await ExecuteConvertAsync());
        }

        public ICommand LoadFileCommand { get; }
        public ICommand SaveFileCommand { get; }
        public ICommand ConvertCommand { get; }
        public ICommand ConvertAsyncCommand { get; }

        private Stream? _loadedImage;

        public Stream? LoadedImage
        {
            get => _loadedImage;
            set
            {
                if (Equals(value, _loadedImage)) return;
                _loadedImage = value;
                OnPropertyChanged();
            }
        }

        private Stream? _convertedImage;

        public Stream? ConvertedImage
        {
            get => _convertedImage;
            set
            {
                if (Equals(value, _convertedImage)) return;
                _convertedImage = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan? _conversionDuration;


        public TimeSpan? ConversionDuration
        {
            get => _conversionDuration;
            set
            {
                if (Nullable.Equals(value, _conversionDuration)) return;
                _conversionDuration = value;
                OnPropertyChanged();
            }
        }


        private void ExecuteSaveFile()
        {
            var dialog = new SaveFileDialog {Filter = "PNG File(*.PNG)|*.PNG|JPG File(*.JPG)|*.JPG|BMP File(*.BMP)|*.BMP",};
            if (dialog.ShowDialog() != true) return;
            using var fileToWrite = File.OpenWrite(dialog.FileName);
            ConvertedImage?.Seek(0, SeekOrigin.Begin);
            ConvertedImage?.CopyTo(fileToWrite);
        }

        private void ExecuteLoadFile()
        {
            var dialog = new OpenFileDialog {Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"};
            if (dialog.ShowDialog() == true) LoadedImage = File.OpenRead(dialog.FileName);
        }

        private void ExecuteConvert()
        {
            var image = new Image(LoadedImage);
            var timedConversion = Timer.Measure(() => _imageProcessingService.ToMainColors(image));
            ConversionDuration = timedConversion.Duration;
            ConvertedImage = timedConversion.Result.Value;
        }

        private async Task ExecuteConvertAsync()
        {
            var image = new Image(LoadedImage);
            var timedConversion = await Timer.MeasureAsync(_imageProcessingService.ToMainColorsAsync(image));
            ConversionDuration = timedConversion.Duration;
            ConvertedImage = timedConversion.Result.Value;
        }
        
        #region PropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        
        private readonly Library.ImageProcessing _imageProcessingService;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion
    }
}