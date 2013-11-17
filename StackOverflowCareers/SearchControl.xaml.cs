using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StackOverflowCareers.Model;
using StackOverflowCareers.Model.Criteria;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class SearchControl : UserControl
    {
        private SearchViewModel _viewModel;
        public SearchControl()
        {
            InitializeComponent();
            DataContext = _viewModel = new SearchViewModel();
        }


        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var criteria = new SearchCriteria();
            if (!string.IsNullOrWhiteSpace(txtWhere.Text))
            {
                criteria.Criteria. Add(new LocationCriteria(txtWhere.Text));
            }
        }
    }
}
