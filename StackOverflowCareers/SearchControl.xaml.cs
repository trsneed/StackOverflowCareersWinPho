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
            InitializeComponent();
            DataContext = _mainViewModel = App.ViewModel;
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var criteria = new SearchCriteria();
            if (!string.IsNullOrWhiteSpace(txtWhere.Text))
            {
                criteria.Criteria. Add(new LocationCriteria(txtWhere.Text));
            }
        }

        private void MySlider_ValueChanged(object sender,
                                   RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).Value = Math.Round(((Slider)sender).Value/10.0) *10;
        }

        private void LocateMe_OnClick(object sender, RoutedEventArgs e)
        {
            _mainViewModel.GetLocation();
        }
    }
}
