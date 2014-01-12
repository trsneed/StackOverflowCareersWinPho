using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using StackOverflowCareers.ViewModels;

namespace StackOverflowCareers
{
    public partial class SearchControl : UserControl
    {
        private readonly MainViewModel _mainViewModel;

        public SearchControl()
        {
            DataContext = _mainViewModel = App.ViewModel;
            InitializeComponent();
        }


        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _mainViewModel.IsSearchOpen = false;
            await _mainViewModel.SearchCareers(_mainViewModel.BuildCriteria());
        }

        private void MySlider_ValueChanged(object sender,
            RoutedPropertyChangedEventArgs<double> e)
        {
            _mainViewModel.RoundDistance(((Slider) sender).Value);
        }

        private async void LocateMe_OnClick(object sender, RoutedEventArgs e)
        {
            await _mainViewModel.GetLocation();
        }

        private async void TxtWhat_OnKeyDown(object sender, KeyEventArgs e)
        {
            await CheckIfEnterPressed(e.Key);
        }

        private async Task CheckIfEnterPressed(Key key)
        {
            if (key == Key.Enter)
            {
                _mainViewModel.IsSearchOpen = false;
                await _mainViewModel.SearchCareers(_mainViewModel.BuildCriteria());
            }
        }

        private void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            // Update the binding source
            BindingExpression bindingExpr = textBox.GetBindingExpression(TextBox.TextProperty);
            bindingExpr.UpdateSource();
        }
    }
}