﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Library.MVVM;
using Microsoft.Win32;

namespace Library.TreeView
{
    public class TreeViewModel : INotifyPropertyChanged
    {
        #region constructors
        public TreeViewModel()
        {
            HierarchicalAreas = new ObservableCollection<TreeViewItem>();
            Click_Button = new RelayCommand(LoadDLL);
            Click_Browse = new RelayCommand(Browse);
        }
        #endregion

        #region DataContext
        public ObservableCollection<TreeViewItem> HierarchicalAreas { get; set; }
        public string PathVariable { get; set; }
        public Visibility ChangeControlVisibility { get; set; } = Visibility.Hidden;
        public ICommand Click_Browse { get; }
        public ICommand Click_Button { get; }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }
        #endregion

        #region private
        private void LoadDLL()
        {
            if (PathVariable.Substring(PathVariable.Length - 4) == ".dll")
                TreeViewLoaded();
        }
        private void TreeViewLoaded()
        {
            TreeViewItem rootItem = new TreeViewItem { Name = PathVariable.Substring(PathVariable.LastIndexOf('\\') + 1) };
            HierarchicalAreas.Add(rootItem);
        }
        private void Browse()
        {
            OpenFileDialog test = new OpenFileDialog()
            {
                Filter = "Dynamic Library File(*.dll)| *.dll"
            };
            test.ShowDialog();
            if (test.FileName.Length == 0)
                MessageBox.Show("No files selected");
            else
            {
                PathVariable = test.FileName;
                ChangeControlVisibility = Visibility.Visible;
                RaisePropertyChanged("ChangeControlVisibility");
                RaisePropertyChanged("PathVariable");
            }
        }
        #endregion
    }
}
