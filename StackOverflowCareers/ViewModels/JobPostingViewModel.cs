using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using StackOverflowCareers.Annotations;
using StackOverflowCareers.Model;

namespace StackOverflowCareers.ViewModels
{
    public class JobPostingViewModel : INotifyPropertyChanged
    {

        public JobPostingViewModel(JobPosting post)
        {
            JobPosting = post;
        }


        private JobPosting _jobPosting;
        public JobPosting JobPosting
        {
            get
            {
                return _jobPosting;
            }
            set
            {
                if (value != _jobPosting)
                {
                    _jobPosting = value;
                    OnPropertyChanged("JobPosting");
                }
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
