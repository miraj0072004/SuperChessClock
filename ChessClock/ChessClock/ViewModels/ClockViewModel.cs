using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public string Description { get; set; }


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
        private string _whiteTimeSecondsShow;
        private string _blackTimeSecondsShow;

        #endregion

        #region Properties

        private int blackTimeProgress = 0;
        private int whiteTimeProgress = 0;
        public int WhiteTimeSeconds
        {
            get => _whiteTimeSeconds;
            set
            {
                _whiteTimeSeconds = value;

                //WhiteTimeSecondsShow = (_whiteTimeSeconds==0 || _whiteTimeSeconds == 60) ? "00" : _whiteTimeSeconds.ToString();

                OnPropertyChanged();
            }
        }

        public string WhiteTimeSecondsShow
        {
            get => _whiteTimeSecondsShow;
            set
            {
                _whiteTimeSecondsShow = value;
                OnPropertyChanged();
            } 
        }

        public string BlackTimeSecondsShow
        {
            get => _blackTimeSecondsShow;
            set
            {
                _blackTimeSecondsShow = value;
                //BlackTimeSecondsShow = (_blackTimeSeconds == 0 || _blackTimeSeconds == 60) ? "00" : _blackTimeSeconds.ToString();
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
                ResetTime();
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
            whiteTimeProgress ++;
            if (!_whiteTimerRun || Reset || Pause) 
            {
                
                return false;
                
            }

            if (whiteTimeProgress==10)
            {
                

                if (WhiteTimeMinutes == 0 && WhiteTimeSeconds == 0)
                {
                    //return new Task<false>;
                    whiteTimeProgress = 0;
                    return false;

                }

                WhiteTimeSeconds--;
                if (WhiteTimeSeconds == 0)
                {
                    WhiteTimeSeconds = 60;
                    WhiteTimeSecondsShow = "00";
                }
                else if(WhiteTimeSeconds<10)
                {
                    WhiteTimeSecondsShow = "0"+WhiteTimeSeconds;
                }
                else
                {
                    WhiteTimeSecondsShow = WhiteTimeSeconds.ToString();
                }

                if (WhiteTimeSeconds == 59)
                {
                    WhiteTimeMinutes--;
                }

                whiteTimeProgress = 0;
                return true;

            }

            
            return true;
        }

        private bool HandleBlackTime()
        {
            blackTimeProgress ++;
            if (!_blackTimerRun || Reset || Pause)
            {
                blackTimeProgress = 0;
                return false;
                

            }

            if (blackTimeProgress == 10)
            {


                if (BlackTimeMinutes == 0 && BlackTimeSeconds == 0)
                {
                    //return new Task<false>;
                    blackTimeProgress = 0;
                    return false;

                }

                BlackTimeSeconds--;
                if (BlackTimeSeconds == 0)
                {
                    BlackTimeSeconds = 60;
                    BlackTimeSecondsShow = "00";
                }
                else if (BlackTimeSeconds<10)
                {
                    BlackTimeSecondsShow = "0"+BlackTimeSeconds;
                }
                else
                {
                    BlackTimeSecondsShow = BlackTimeSeconds.ToString();
                }

                if (BlackTimeSeconds == 59)
                {
                    BlackTimeMinutes--;
                }

                blackTimeProgress = 0;
                return true;

            }

            
            return true;
        }

        private void OnWhite_Tapped()
        {
            Pause = false;
            Reset = false;
            
            WhiteBackgroundColor = Color.Bisque;
            WhiteIsEnabled = false;
            BlackIsEnabled = true;
            BlackBackgroundColor = Color.Beige;
            _whiteTimerRun = false;
            _blackTimerRun = true;
            //Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            //{
            //    Task.Run(async () =>
            //    {
            //        var time = await HandleBlackTime();

            //    });
            //    return true;
            //});
            Device.StartTimer(TimeSpan.FromMilliseconds(100),HandleBlackTime );
        }

        private void OnBlack_Tapped()
        {
            Pause = false;
            Reset = false;
            
            BlackBackgroundColor = Color.Bisque;
            WhiteBackgroundColor = Color.Beige;
            BlackIsEnabled = false;
            WhiteIsEnabled = true;
            _blackTimerRun = false;
            _whiteTimerRun = true;
            //Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            //{
            //    Task.Run(async () =>
            //    {
            //        var time = await HandleWhiteTime();

            //    });
            //    return true;
            //});
            Device.StartTimer(TimeSpan.FromMilliseconds(100), HandleWhiteTime);
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
                    Limit = 1,
                    Increment = 0,
                    Description = "Bullet 1|0"
                },
                new TimeControl
                {
                    Index = 1,
                    Limit = 3,
                    Increment = 0,
                    Description = "Blitz 3|0"
                },
                new TimeControl
                {
                    Index = 2,
                    Limit = 5,
                    Increment = 0,
                    Description = "Blitz 5|0"
                },
                new TimeControl
                {
                    Index = 3,
                    Limit = 10,
                    Increment = 0,
                    Description = "Rapid 15|0"
                },
                new TimeControl
                {
                    Index = 4,
                    Limit = 3,
                    Increment = 2,
                    Description = "Blitz 3|2"
                },
                new TimeControl
                {
                    Index = 5,
                    Limit = 5,
                    Increment = 5,
                    Description = "Blitz 5|5"
                },
                new TimeControl
                {
                    Index = 6,
                    Limit = 15,
                    Increment = 10,
                    Description = "Rapid 15|10"
                }
            };

            SelectedTimeControl = TimeControls[0];

            WhiteTimeMinutes = SelectedTimeControl.Limit;
            WhiteTimeSeconds = 60;
            WhiteTimeSecondsShow = "00";
            BlackTimeMinutes = SelectedTimeControl.Limit;
            BlackTimeSeconds = 60;
            BlackTimeSecondsShow = "00";
            BlackBackgroundColor = Color.Beige;
            WhiteBackgroundColor = Color.Beige;
            WhiteIsEnabled = true;
            BlackIsEnabled = true;
            Reset = false;
            Pause = true;


            SelectTimeControlCommand = new Command(
                (chosenTimeControl) => { SelectedTimeControl = this.TimeControls.First(x => x.Index == Int32.Parse(chosenTimeControl.ToString())); }

                );

            ResetTimeCommand = new Command(
                ResetTime);

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

        private void ResetTime()
        {
            WhiteTimeMinutes = SelectedTimeControl.Limit;
            WhiteTimeSeconds = 60;
            whiteTimeProgress = 0;
            WhiteTimeSecondsShow = "00";
            

            BlackTimeMinutes = SelectedTimeControl.Limit;
            BlackTimeSeconds = 60;
            blackTimeProgress = 0;
            BlackTimeSecondsShow = "00";
            

            WhiteBackgroundColor = Color.Beige;
            BlackBackgroundColor = Color.Beige;
            WhiteIsEnabled = true;
            BlackIsEnabled = true;

            Pause = true;
            Reset = true;
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
