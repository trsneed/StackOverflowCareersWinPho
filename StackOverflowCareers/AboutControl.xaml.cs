using System.Windows;
using System.Windows.Controls;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class AboutControl : UserControl
    {
        private readonly MainViewModel _mainViewModel;

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