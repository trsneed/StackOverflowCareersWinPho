using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Microsoft.Phone.Shell;

namespace StackOverflowCareers
{
    public static class Indicators
    {
        public static void SetIndicators(DependencyObject element, object bindingSource)
        {
            var indicator = new ProgressIndicator();
            SystemTray.SetProgressIndicator(element, indicator);

            Binding binding = new Binding("IsLoading") { Source = bindingSource };
            BindingOperations.SetBinding(
                indicator, ProgressIndicator.IsVisibleProperty, binding);

            binding = new Binding("IsLoading") { Source = bindingSource };
            BindingOperations.SetBinding(
                indicator, ProgressIndicator.IsIndeterminateProperty, binding);


            Binding textBinding = new Binding("LoadingText") { Source = bindingSource };
            BindingOperations.SetBinding(
                indicator, ProgressIndicator.IsVisibleProperty, binding);
            BindingOperations.SetBinding(indicator, ProgressIndicator.TextProperty, textBinding);
        }
    }
}
