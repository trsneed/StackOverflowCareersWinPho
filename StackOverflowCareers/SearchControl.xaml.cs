using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Windows.Phone.ApplicationModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StackOverflowCareers.Model;
using StackOverflowCareers.Model.Criteria;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class SearchControl : UserControl
    {
        private MainViewModel _mainViewModel;
        public SearchControl()
        {
            DataContext = _mainViewModel = App.ViewModel;
            InitializeComponent();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _mainViewModel.SearchCareers(_mainViewModel.BuildCriteria());
        }

        private void MySlider_ValueChanged(object sender,
                                   RoutedPropertyChangedEventArgs<double> e)
        {
            _mainViewModel.RoundDistance(((Slider)sender).Value);
        }

        private void LocateMe_OnClick(object sender, RoutedEventArgs e)
        {
            _mainViewModel.GetLocation();
        }
    }
}
