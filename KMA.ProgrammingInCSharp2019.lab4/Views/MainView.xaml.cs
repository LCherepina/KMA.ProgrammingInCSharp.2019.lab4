using System.Windows;
using KMA.ProgrammingInCSharp2019.lab4.Managers;
using KMA.ProgrammingInCSharp2019.lab4.ViewModels;

namespace KMA.ProgrammingInCSharp2019.lab4.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView
    {
        private NewUserViewModel _newUserViewModel;
        public MainView()
        {
            InitializeComponent();
            Initialize();

        }

        private void Initialize()
        {
            Visibility = Visibility.Visible;
            _newUserViewModel = new NewUserViewModel(StationManager.CurrentUser);
            DataContext = _newUserViewModel;
        }
    }
}
