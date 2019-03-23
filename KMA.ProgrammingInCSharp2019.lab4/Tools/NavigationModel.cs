using System;
using KMA.ProgrammingInCSharp2019.lab4.Views;

namespace KMA.ProgrammingInCSharp2019.lab4.Tools
{
    internal enum ModesEnum
    {
        Main,
        Info
    }
    internal class NavigationModel
    {
        private readonly IContentWindow _contentWindow;

        internal NavigationModel(IContentWindow contentWindow)
        {
            _contentWindow = contentWindow;
        }

        internal void Navigate(ModesEnum mode)
        {
            switch (mode)
            {
                case ModesEnum.Main:
                    _contentWindow.ContentControl.Content = new MainView();
                    break;
                case ModesEnum.Info:
                    _contentWindow.ContentControl.Content = new UserListView();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
} 

