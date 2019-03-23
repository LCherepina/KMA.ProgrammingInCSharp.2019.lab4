using System;
using System.Windows;
using KMA.ProgrammingInCSharp2019.lab4.Models;
using KMA.ProgrammingInCSharp2019.lab4.Tools;
using KMA.ProgrammingInCSharp2019.lab4.Tools.DataStorage;


namespace KMA.ProgrammingInCSharp2019.lab4.Managers
{
    public static class StationManager
    {

        private static IDataStorage _dataStorage;

        internal static User CurrentUser { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            MessageBox.Show("ShutDown");
            SerializationManager.Serialize(DataStorage.UsersList, FileFolderHelper.StorageFilePath);
            Environment.Exit(1);
        }
    }
}
