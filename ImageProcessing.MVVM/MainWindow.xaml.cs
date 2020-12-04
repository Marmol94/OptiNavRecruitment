using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ImageProcessing = ImageProcessingLibrary.ImageProcessing;

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


        private void LoadButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"};
            if (dialog.ShowDialog() != true) return;
            
            FilePathTextBox.Text = dialog.FileName;
            FilePathTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
        }

        private void ConvertButtonClick(object sender, RoutedEventArgs e)
        {
            var imageService = new ImageProcessingService();
            ConvertedImage.Source = imageService.ConvertToMainColors(LoadedImage.Source);
        }
        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            
            var imageService = new ImageProcessingService();
            var dialog = new SaveFileDialog{Filter = "PNG File(*.PNG)|*.PNG|JPG File(*.JPG)|*.JPG|BMP File(*.BMP)|*.BMP", };
            if (dialog.ShowDialog() != true) return;
            File.WriteAllBytes(dialog.FileName, imageService.ConvertToStream(ConvertedImage.Source).ToArray() );
        }
        
        
    }
}