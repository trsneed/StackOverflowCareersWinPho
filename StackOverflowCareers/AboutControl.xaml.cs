using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class AboutControl : UserControl
    {
        private MainViewModel _mainViewModel;

        public AboutControl()
        {
            InitializeComponent();
            DataContext = _mainViewModel = App.ViewModel;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _mainViewModel.IsAboutOpen = false;
        }
    }
}
