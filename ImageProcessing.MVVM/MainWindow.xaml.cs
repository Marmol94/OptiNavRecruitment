namespace ImageProcessing.MVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();


            DataContext = new MainWindowViewModel(new Library.ImageProcessing());
        }
    }
}