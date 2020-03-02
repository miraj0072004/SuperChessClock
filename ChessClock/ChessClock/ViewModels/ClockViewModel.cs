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
        #region Backing Fields
        private TimeControl _selectedTimeControl;
        private List<TimeControl> _timeControls;
        private bool _reset;
        private int _whiteTimeSeconds;
        private int _whiteTimeMinutes;
        private int _whiteTimeHours;
        private int _blackTimeSeconds;
        private int _blackTimeMinutes;
        private int _blackTimeHours;
        private bool _whiteTimerRun = true;
        private bool _blackTimerRun = true;
        private Color _blackBackgroundColor;
        private bool _blackIsEnabled;
        private bool _whiteIsEnabled;
        private Color _whiteBackgroundColor;
        private bool _pause;

        #endregion

        #region Properties
        public int WhiteTimeSeconds
        {
            get => _whiteTimeSeconds;
            set
            {
                _whiteTimeSeconds = value;
                OnPropertyChanged();
            }
        }

        public int WhiteTimeMinutes
        {
            get => _whiteTimeMinutes;
            set
            {
                _whiteTimeMinutes = value;
                OnPropertyChanged();
            }
        }

        public int WhiteTimeHours
        {
            get => _whiteTimeHours;
            set => _whiteTimeHours = value;
        }

        public int BlackTimeSeconds
        {
            get => _blackTimeSeconds;
            set
            {
                _blackTimeSeconds = value;
                OnPropertyChanged();
            }
        }

        public int BlackTimeMinutes
        {
            get => _blackTimeMinutes;
            set
            {
                _blackTimeMinutes = value;
                OnPropertyChanged();
            }
        }

        public int BlackTimeHours
        {
            get => _blackTimeHours;
            set
            {
                _blackTimeHours = value;
                OnPropertyChanged();
            }
        }

        public Color BlackBackgroundColor
        {
            get => _blackBackgroundColor;
            set
            {
                _blackBackgroundColor = value;
                OnPropertyChanged();
            }
        }

        public bool BlackIsEnabled
        {
            get => _blackIsEnabled;
            set
            {
                _blackIsEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool WhiteIsEnabled
        {
            get => _whiteIsEnabled;
            set
            {
                _whiteIsEnabled = value;
                OnPropertyChanged();
            }
        }

        public Color WhiteBackgroundColor
        {
            get => _whiteBackgroundColor;
            set
            {
                _whiteBackgroundColor = value;
                OnPropertyChanged();
            }
        }

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

        public bool Reset
        {
            get => _reset;
            set => _reset = value;
        }

        public bool Pause
        {
            get => _pause;
            set
            {
                _pause = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Methods
        private bool HandleWhiteTime()
        {
            if (Reset || Pause)
                return false;

            if (WhiteTimeSeconds == 0)
                WhiteTimeSeconds = 60;

            WhiteTimeSeconds--;

            if (WhiteTimeSeconds == 59)
            {
                WhiteTimeMinutes--;
            }

            if (WhiteTimeMinutes == 0)
            {
                //return new Task<false>;
                return false;

            }

            if (_whiteTimerRun)
            {

                return true;
            }


            return false;
        }

        private bool HandleBlackTime()
        {

            if (Reset || Pause)
                return false;

            if (BlackTimeSeconds == 0)
                BlackTimeSeconds = 60;

            BlackTimeSeconds--;

            if (BlackTimeSeconds == 59)
            {
                BlackTimeMinutes--;
            }

            if (BlackTimeMinutes == 0)
            {
                //return new Task<false>;
                return false;

            }

            if (_blackTimerRun)
            {
                return true;
            }


            return false;
        }

        private void OnWhite_Tapped()
        {
            Pause = false;
            Reset = false;
            Device.StartTimer(TimeSpan.FromSeconds(1), HandleBlackTime);
            WhiteBackgroundColor = Color.Bisque;
            WhiteIsEnabled = false;
            BlackIsEnabled = true;
            BlackBackgroundColor = Color.Beige;
            _whiteTimerRun = false;
            _blackTimerRun = true;
        }

        private void OnBlack_Tapped()
        {
            Pause = false;
            Reset = false;
            Device.StartTimer(TimeSpan.FromSeconds(1), HandleWhiteTime);
            BlackBackgroundColor = Color.Bisque;
            WhiteBackgroundColor = Color.Beige;
            BlackIsEnabled = false;
            WhiteIsEnabled = true;
            _blackTimerRun = false;
            _whiteTimerRun = true;
        }


        #endregion

        #region Commands

        public ICommand SelectTimeControlCommand { private set; get; }
        public ICommand ResetTimeCommand { private set; get; }
        public ICommand PauseTimeCommand { private set; get; }
        public ICommand OnBlackTappedCommand { get; set; }
        public ICommand OnWhiteTappedCommand { get; set; }
        #endregion

        #region Constructor
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

            WhiteTimeMinutes = SelectedTimeControl.Limit;
            WhiteTimeSeconds = 0;
            BlackTimeMinutes = SelectedTimeControl.Limit;
            BlackTimeSeconds = 0;
            BlackBackgroundColor = Color.Beige;
            WhiteBackgroundColor = Color.Beige;
            WhiteIsEnabled = true;
            BlackIsEnabled = true;
            Reset = false;
            Pause = false;


            SelectTimeControlCommand = new Command(
                (chosenTimeControl) => { SelectedTimeControl = this.TimeControls.First(x => x.Index == Int32.Parse(chosenTimeControl.ToString())); }

                );

            ResetTimeCommand = new Command(
                () =>
                {
                    WhiteTimeMinutes = SelectedTimeControl.Limit;
                    WhiteTimeSeconds = 0;

                    BlackTimeMinutes = SelectedTimeControl.Limit;
                    BlackTimeSeconds = 0;

                    Reset = true;
                });

            PauseTimeCommand= new Command(
                () =>
                {
                    Pause = true;
                    WhiteBackgroundColor=Color.Aquamarine;
                    BlackBackgroundColor=Color.Aquamarine;
                    WhiteIsEnabled = true;
                    BlackIsEnabled = true;
                });

            OnWhiteTappedCommand = new Command(() => OnWhite_Tapped());
            OnBlackTappedCommand = new Command(() => OnBlack_Tapped());

        }

        #endregion


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
