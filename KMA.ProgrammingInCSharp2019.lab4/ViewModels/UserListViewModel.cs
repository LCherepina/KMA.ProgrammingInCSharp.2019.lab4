

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using KMA.ProgrammingInCSharp2019.lab4.Annotations;
using KMA.ProgrammingInCSharp2019.lab4.Managers;
using KMA.ProgrammingInCSharp2019.lab4.Models;
using KMA.ProgrammingInCSharp2019.lab4.Tools;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace KMA.ProgrammingInCSharp2019.lab4.ViewModels
{
    class UserListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private ObservableCollection<User> _users;
        private User _selectedUser;


        #region Commands

        private ICommand _addExecute;
        private ICommand _deleteExecute;
        private ICommand _editExecute;
        #endregion
        #endregion

        #region Properties
        public User SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                if (_selectedUser == null) return;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<User> Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged();
            }
        }
        #region Commands
        public ICommand AddCommand
        {
            get
            {
                return _addExecute ?? (_addExecute = new RelayCommand<KeyEventArgs>(AddExecuteAsync));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return _deleteExecute ?? (_deleteExecute = new RelayCommand<KeyEventArgs>(DeleteExecuteAsync, CanExecute));
            }
        }
        public ICommand EditCommand
        {
            get
            {
                return _editExecute ?? (_editExecute = new RelayCommand<KeyEventArgs>(EditExecuteAsync, CanExecute));
            }
        }


        #endregion

        #endregion



        internal UserListViewModel()
        {
            if (StationManager.DataStorage.UsersList.Count == 0)
            {
                AddUsers();
            }
            _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);

        }

        private async void AddExecuteAsync(Object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
                NavigationManager.Instance.Navigate(ModesEnum.Main);
        }
        private async void EditExecuteAsync(Object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    StationManager.CurrentUser = SelectedUser;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return false;
                }

                return true;
            });
            LoaderManager.Instance.HideLoader();
            if (result)
                NavigationManager.Instance.Navigate(ModesEnum.Main);
        }
        private async void DeleteExecuteAsync(Object obj)
        {
            LoaderManager.Instance.ShowLoader();
            await Task.Run(() =>
            {
                try
                {
                    MessageBox.Show($"You are deleting user {SelectedUser.Name + " " + SelectedUser.Surname}");

                    StationManager.DataStorage.DeleteUser(SelectedUser);
                    _users = new ObservableCollection<User>(StationManager.DataStorage.UsersList);
                    OnPropertyChanged(nameof(Users));
                    return true;
                }
                catch (Exception e)
                {
                     MessageBox.Show(e.Message);
                    return false;
                }
            });
            LoaderManager.Instance.HideLoader();
            
        }

        private bool CanExecute(object obj)
        {
            return SelectedUser != null;

        }


        private void AddUsers()
        {
            for(int i=0; i<50; i++)
            {
                char a = RandomLetter.GetLetter();
                char b = RandomLetter.GetLetter();
                StationManager.DataStorage.AddUser(new User(b + "" + a, b + "" + a, b + "@" + a, DateTime.Now.AddYears(-i).AddMonths(-i)));
            }

        }




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    static class RandomLetter
    {
        static Random _random = new Random();
        public static char GetLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            return let;
        }
    }
}
