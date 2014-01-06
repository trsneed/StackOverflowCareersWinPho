using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using StackOverflowCareers.Model;
using StackOverflowCareers.Resources;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ProgressIndicator indicator;
        private MainViewModel _mainViewModel;
        int _offsetKnob = 5;

        // Constructor
        public MainPage()
        {

            DataContext = _mainViewModel = App.ViewModel;
            Indicators.SetIndicators(this, _mainViewModel);
            InitializeComponent();

        }

        // Load data for the ViewModel Items
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                await _mainViewModel.SearchCareers();
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (JobPostingSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            var jobPosting = JobPostingSelector.SelectedItem as JobPosting;
            if (jobPosting != null)
                NavigationService.Navigate(
                    new Uri(
                        "/DetailsPage.xaml?selectedItem=" + jobPosting.OrderId,
                        UriKind.Relative));

            // Reset selected item to null (no selection)
            JobPostingSelector.SelectedItem = null;
        }

        private void SearchClicked(object sender, EventArgs e)
        {

            SearchControl.Height = Application.Current.Host.Content.ActualHeight;
            SearchControl.Width = Application.Current.Host.Content.ActualWidth;
            _mainViewModel.IsSearchOpen = true;


        }

        private async void main_Reailized(object sender, ItemRealizationEventArgs e)
        {
            if (!_mainViewModel.IsLoading && JobPostingSelector.ItemsSource != null &&
                JobPostingSelector.ItemsSource.Count >= _offsetKnob)
            {
                if (e.ItemKind == LongListSelectorItemKind.Item)
                {
                    if (
                        (e.Container.Content as JobPosting).Equals(
                            JobPostingSelector.ItemsSource[JobPostingSelector.ItemsSource.Count - _offsetKnob]) &&
                        JobPostingSelector.ItemsSource.Count > _mainViewModel.Offset)
                    {
                        Debug.WriteLine("Searching for {0}", _mainViewModel.Offset);
                        await _mainViewModel.LoadCareersOffsetAsync();
                    }
                }
            }
        }

        private void AboutClicked(object sender, EventArgs e)
        {
            AboutControl.Height = Application.Current.Host.Content.ActualHeight;
            AboutControl.Width = Application.Current.Host.Content.ActualWidth;
            _mainViewModel.IsAboutOpen = true;
        }
    }
}