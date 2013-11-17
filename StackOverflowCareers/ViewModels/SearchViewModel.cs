using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StackOverflowCareers.Annotations;

namespace StackOverflowCareers.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        public SearchViewModel()
        {
            
        }

        private string _Location;

        public string Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                OnPropertyChanged("Location");
            }
        }

        private string _Keyword;

        public string Keyword
        {
            get { return _Keyword; }
            set
            {
                _Keyword = value;
                OnPropertyChanged("Keyword");
            }
        }

        private bool _Remote = false;

        public bool Remote
        {
            get { return _Remote; }
            set
            {
                _Remote = value;
                OnPropertyChanged("Remote");
            }
        }

        private bool _Relocation = false;

        public bool Relocation
        {
            get { return _Relocation; }
            set
            {
                _Relocation = value;
                OnPropertyChanged("Relocation");
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
