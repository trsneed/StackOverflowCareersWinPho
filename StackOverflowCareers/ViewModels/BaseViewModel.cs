using System.ComponentModel;
using System.Runtime.CompilerServices;
using StackOverflowCareers.Annotations;

namespace StackOverflowCareers.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private string _LoadingText;
        private bool _isLoading;


        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }

        public string LoadingText
        {
            get { return _LoadingText; }
            set
            {
                _LoadingText = value;
                OnPropertyChanged("LoadingText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}