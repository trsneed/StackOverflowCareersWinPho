using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StackOverflowCareers.Resources;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ProgressIndicator indicator;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
            indicator = new ProgressIndicator();
            Binding binding = new Binding("IsLoading") { Source = DataContext };
            Binding textBinding = new Binding("LoadingText"){Source = DataContext};
            BindingOperations.SetBinding(
                indicator, ProgressIndicator.IsVisibleProperty, binding);
            BindingOperations.SetBinding(indicator, ProgressIndicator.TextProperty, textBinding);
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (JobPostingSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (JobPostingSelector.SelectedItem as JobPostingViewModel).JobPosting.OrderId, UriKind.Relative));

            // Reset selected item to null (no selection)
            JobPostingSelector.SelectedItem = null;
        }


    }
}