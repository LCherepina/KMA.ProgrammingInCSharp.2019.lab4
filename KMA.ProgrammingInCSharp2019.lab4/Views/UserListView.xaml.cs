

using KMA.ProgrammingInCSharp2019.lab4.ViewModels;

namespace KMA.ProgrammingInCSharp2019.lab4.Views
{
    /// <summary>
    /// Interaction logic for UserListView.xaml
    /// </summary>
    public partial class UserListView
    {
        public UserListView()
        {
            InitializeComponent();
            DataContext = new UserListViewModel();
        }
    }
}
