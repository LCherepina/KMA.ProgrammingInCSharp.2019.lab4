using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using KMA.ProgrammingInCSharp2019.lab4.Annotations;
using KMA.ProgrammingInCSharp2019.lab4.Managers;
using KMA.ProgrammingInCSharp2019.lab4.Models;
using KMA.ProgrammingInCSharp2019.lab4.Tools;

namespace KMA.ProgrammingInCSharp2019.lab4.ViewModels
{
    class NewUserViewModel:INotifyPropertyChanged
    {

        #region Fields

        private string _firstname;
        private string _lastname;
        private string _email;
        private DateTime _birthdate = DateTime.Now.Date;
       

   
        #region Commands
        private ICommand _calculateCommand;
        private ICommand _backCommand;
        #endregion

        #endregion

        #region Properties


        public DateTime BirthDate
        {
            get
            {
                return _birthdate;
            }
            set
            {
                _birthdate = value;
                
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get
            {
                return _firstname;
            }
            set
            {
                _firstname = value; 
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get
            {
                return _lastname;
            }
            set
            {
                _lastname = value; 
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }
        #region Commands
        public ICommand CalculateCommand
        {
            get
            {
                return _calculateCommand ?? (_calculateCommand = new RelayCommand<KeyEventArgs>(Calculate, CanCalculate));
            }
        }
        public ICommand BackCommand
        {
            get
            {
                return _backCommand ?? (_backCommand = new RelayCommand<KeyEventArgs>(Back));
            }
        }
        #endregion

        #endregion

        public NewUserViewModel(User user)
        {
            if (user != null)
            {
                LastName = user.Surname;
                FirstName = user.Name;
                Email = user.Email;
                BirthDate = user.BirthDate;
            }
        }

        private async void Calculate(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    
                    var user = new User(_firstname, _lastname, _email, _birthdate);
                    StationManager.DataStorage.AddUser(user);
                    StationManager.CurrentUser = user;

                    if(user.IsBirthday)
                      MessageBox.Show("!!!!!!!!Happy Birthday!!!!!!!!!", "Greeting!", MessageBoxButton.OK);

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
            {
                _firstname = "";
                _lastname = "";
                _email = "";
                _birthdate = DateTime.Now.Date;
                OnPropertyChanged(FirstName);
                OnPropertyChanged(LastName);
                OnPropertyChanged(Email);
                OnPropertyChanged(nameof(BirthDate));
                NavigationManager.Instance.Navigate(ModesEnum.Info);
            }

        }

        private async void Back(Object obj)
        {
            await Task.Run(() =>
            {
                return true;

            });
            NavigationManager.Instance.Navigate(ModesEnum.Info);
        }

        private bool CanCalculate(object obj)
        {
            return !String.IsNullOrEmpty(_firstname) &&
                   !String.IsNullOrEmpty(_lastname) &&
                   !String.IsNullOrEmpty(_email);
           
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
