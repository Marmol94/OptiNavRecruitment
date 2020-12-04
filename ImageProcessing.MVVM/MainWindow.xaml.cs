using System.IO;
using System.Windows;
using Microsoft.Win32;

namespace ImageProcessing.MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static readonly DependencyProperty FilePathProperty = DependencyProperty.Register(
            nameof(FilePath), typeof(string), typeof(MainWindow), new PropertyMetadata(default(string)));

        public string FilePath
        {
            get => (string) GetValue(FilePathProperty);
            set => SetValue(FilePathProperty, value);
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        // private void ConvertButtonClick(object sender, RoutedEventArgs e)
        // {
        //     var imageService = new ImageProcessingService();
        //     ConvertedImage.Source = imageService.ConvertToMainColors(LoadedImage.Source);
        // }
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            var imageService = new ImageProcessingService();
            var dialog = new SaveFileDialog {Filter = "PNG File(*.PNG)|*.PNG|JPG File(*.JPG)|*.JPG|BMP File(*.BMP)|*.BMP",};
            if (dialog.ShowDialog() != true) return;
            File.WriteAllBytes(dialog.FileName, imageService.ConvertToStream(ConvertedImage.Source).ToArray());
        }
    }
}