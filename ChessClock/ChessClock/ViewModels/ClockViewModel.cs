using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace ChessClock.ViewModels
{
    public struct TimeControl
    {
        public int Index { get; set; }
        public int Limit { get; set; }
        public int Increment { get; set; }
    }



    class ClockViewModel :INotifyPropertyChanged
    {
        private TimeControl _selectedTimeControl;
        private List<TimeControl> _timeControls;


        public List<TimeControl> TimeControls
        {
            get
            {
                return _timeControls;
            }
            set { _timeControls = value; }
        }
        
        public TimeControl SelectedTimeControl
        {
            get
            {
                return _selectedTimeControl;
            }
            set
            {
                _selectedTimeControl = value;
                OnPropertyChanged();

            }
        }

        public ICommand SelectTimeControlCommand { get; set; }



        public ClockViewModel()
        {
            TimeControls = new List<TimeControl>()
            {
                new TimeControl
                {
                    Index = 0,
                    Limit = 3,
                    Increment = 2
                },
                new TimeControl
                {
                    Index = 1,
                    Limit = 5,
                    Increment = 3
                },
                new TimeControl
                {
                    Index = 2,
                    Limit = 10,
                    Increment = 0
                }
            };

            SelectedTimeControl = TimeControls[0];



            SelectTimeControlCommand = new Command(
                (chosenTimeControl) => { SelectedTimeControl = this.TimeControls.First(x=>x.Index==Int32.Parse(chosenTimeControl.ToString())); }

                );

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
