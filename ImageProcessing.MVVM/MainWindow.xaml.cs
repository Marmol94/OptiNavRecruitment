using System.Windows;
using System.Windows.Controls;
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


        private void OnButtonClick(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog {Filter = "Image Files(*.BMP;*.JPG;*.PNG)|*.BMP;*.JPG;*.PNG"};
            if (dialog.ShowDialog() != true) return;

            FilePathTextBox.Text = dialog.FileName;
            FilePathTextBox.GetBindingExpression(TextBox.TextProperty)?.UpdateSource();
        }
    }
}