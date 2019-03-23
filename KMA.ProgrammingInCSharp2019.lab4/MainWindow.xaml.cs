
using System.ComponentModel;
using System.Windows.Controls;
using KMA.ProgrammingInCSharp2019.lab4.Managers;
using KMA.ProgrammingInCSharp2019.lab4.Tools;
using KMA.ProgrammingInCSharp2019.lab4.Tools.DataStorage;
using KMA.ProgrammingInCSharp2019.lab4.ViewModels;

namespace KMA.ProgrammingInCSharp2019.lab4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IContentWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var navigationModel = new NavigationModel(this);
            NavigationManager.Instance.Initialize(navigationModel);
            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel();

            // DataContext = mainWindowViewModel;
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Navigate(ModesEnum.Info);
            mainWindowViewModel.StartApplication();
        }
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            StationManager.CloseApp();
        }
    }
}
