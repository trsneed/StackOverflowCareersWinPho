using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StackOverflowCareers.Resources;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        private JobPostingViewModel _vm;
       
       
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
            
        }

        // When page is navigated to set data context to view modell from selected item in list
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {

                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = _vm = new JobPostingViewModel(App.ViewModel.JobPostings[index]);
                    Indicators.SetIndicators(this, DataContext);
                    await _vm.ScrapeThatScreenAsync(_vm.JobPosting.Id);
                }
            }
        }

    }
}