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

        public bool WhiteTimerRun
        {
            get => _whiteTimerRun;
            private set
            {
                _whiteTimerRun = value;
                OnPropertyChanged();
            }
        }

        public bool BlackTimerRun
        {
            get => _blackTimerRun;
            private set
            {
                _blackTimerRun = value;
                OnPropertyChanged();
            }
        }

        private Color _blackBackgroundColor;
        private bool _blackIsEnabled;
        private bool _whiteIsEnabled;
        private Color _whiteBackgroundColor;
        private bool _pause;
        private string _whiteTimeSecondsShow;
        private string _blackTimeSecondsShow;

        #endregion

        #region Properties

        public Color NonCountDownRunningColor { get; } = Color.Gold;
        public Color NonCountDownClickedColor { get; } = Color.DarkGoldenrod;
        public Color CountDownRunningColor { get; } = Color.OrangeRed;
        public Color CountDownClickedColor { get; } = Color.Chocolate;
        public Color PausedColor { get; } = Color.Aquamarine;

        public bool BlackFinalCountDown
        {
            get => _blackFinalCountDown;
            private set
            {
                _blackFinalCountDown = value;
                if (_blackFinalCountDown)
                {
                    if (BlackTimerRun)
                    {
                        BlackBackgroundColor = CountDownRunningColor;
                    }
                    else
                    {
                        BlackBackgroundColor = CountDownClickedColor;
                    }
                }
                else
                {
                    if (BlackTimerRun)
                    {
                        BlackBackgroundColor = NonCountDownRunningColor;
                    }
                    else
                    {
                        BlackBackgroundColor = NonCountDownClickedColor;
                    }
                }
            }
        }

        public bool WhiteFinalCountDown
        {
            get => _whiteFinalCountDown;
            private set
            {
                _whiteFinalCountDown = value;
                if (_whiteFinalCountDown)
                {
                    if (WhiteTimerRun)
                    {
                        WhiteBackgroundColor = CountDownRunningColor;
                    }
                    else
                    {
                        WhiteBackgroundColor = CountDownClickedColor;
                    }
                }
                else
                {
                    if (WhiteTimerRun)
                    {
                        WhiteBackgroundColor = NonCountDownRunningColor;
                    }
                    else
                    {
                        WhiteBackgroundColor = NonCountDownClickedColor;
                    }
                }
                
            }
        }

        private int blackTimeProgress = 0;
        private int whiteTimeProgress = 0;
        private int _increment;
        private bool _whiteFinalCountDown;
        private bool _blackFinalCountDown;
        private bool _whiteTimerRun = true;
        private bool _blackTimerRun = true;


        //public int Increment
        //{
        //    get => _increment;
        //    set => _increment = value;
        //}

        public int WhiteTimeSeconds
        {
            get => _whiteTimeSeconds;
            set
            {
                _whiteTimeSeconds = value;

                //WhiteTimeSecondsShow = (_whiteTimeSeconds==0 || _whiteTimeSeconds == 15) ? "00" : _whiteTimeSeconds.ToString();

                //if (_whiteTimeSeconds == 60)
                //{
                //    WhiteTimeSecondsShow = "00";
                //}
                //else
                if (_whiteTimeSeconds < 10)
                {
                    WhiteTimeSecondsShow = "0" + _whiteTimeSeconds.ToString();

                    if (_whiteTimeSeconds == 0)
                    {
                        if (WhiteTimeMinutes !=0)
                        {
                            _whiteTimeSeconds = 60;
                        }
                        
                        //WhiteTimeSecondsShow = "00";
                    }
                        
                }
                else
                {
                    WhiteFinalCountDown = false;
                    if (_whiteTimeSeconds == 60)
                    {
                        WhiteTimeSecondsShow = "00";
                    }
                    else
                    {
                        WhiteTimeSecondsShow = _whiteTimeSeconds.ToString();
                    }
                    
                }

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

                //if (_blackTimeSeconds == 60)
                //{
                //    BlackTimeSecondsShow = "00";
                //}

                //else
                if (_blackTimeSeconds < 10)
                {
                    BlackTimeSecondsShow = "0" + _blackTimeSeconds.ToString();

                    if (_blackTimeSeconds==0)
                    {
                        if(BlackTimeMinutes != 0)
                            _blackTimeSeconds = 60;
                        //BlackTimeSecondsShow = "00";
                    }
                }
                else
                {
                    BlackFinalCountDown = false;
                    if (_blackTimeSeconds == 60)
                    {
                        BlackTimeSecondsShow = "00";
                    }
                    else
                    {
                        BlackTimeSecondsShow = _blackTimeSeconds.ToString();
                    }
                }
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
            set
            {
                _reset = value;
                OnPropertyChanged();
            }
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
            
            if (!WhiteTimerRun || Reset || Pause) 
            {
                //whiteTimeProgress = 0;
                //if (Reset || Pause)
                if (Reset)
                {
                    whiteTimeProgress = 0;
                }
                return false;
                
            }
            whiteTimeProgress++;

            if (whiteTimeProgress==10)
            {

                WhiteTimeSeconds--;

                if (WhiteTimeMinutes == 0)
                {
                    
                    //When the last few seconds arrives, highlight

                    if (WhiteTimeSeconds == 0)
                    {
                        whiteTimeProgress = 0;
                        //WhiteTimeSecondsShow = "00";
                        GameOverState();
                        return false;
                    }

                    if (WhiteTimeSeconds <= 10)
                    {
                        //WhiteBackgroundColor = Color.OrangeRed;
                        WhiteFinalCountDown = true;
                    }
                    

                }



                //else if(WhiteTimeSeconds<10)
                //{
                //    WhiteTimeSecondsShow = "0"+WhiteTimeSeconds;
                //}
                //else
                //{
                //    WhiteTimeSecondsShow = WhiteTimeSeconds.ToString();
                //}

                if (WhiteTimeSeconds == 59 && WhiteTimeMinutes !=0)
                {
                    WhiteTimeMinutes--;
                }

                whiteTimeProgress = 0;
                return true;

            }

            
            return true;
        }

        private void GameOverState()
        {
            Pause = true;
            //Reset = false;

            WhiteBackgroundColor = Color.DarkSeaGreen;
            WhiteIsEnabled = false;
            BlackIsEnabled = false;
            BlackBackgroundColor = Color.DarkSeaGreen;
            WhiteTimerRun = false;
            BlackTimerRun = false;
        }

        private bool HandleBlackTime()
        {
            
            if (!BlackTimerRun || Reset || Pause)
            {
                if (Reset)
                {
                    blackTimeProgress = 0;
                }
                return false;
            }
            blackTimeProgress++;
            if (blackTimeProgress == 10)
            {

                BlackTimeSeconds--;

                if (BlackTimeMinutes == 0)
                {
                    //return new Task<false>;
                    //When the last few seconds arrives, highlight

                    if (BlackTimeSeconds == 0)
                    {
                        blackTimeProgress = 0;
                        //BlackTimeSecondsShow = "00";
                        GameOverState();
                        return false;
                    }

                    if (BlackTimeSeconds <= 10)
                    {
                        //BlackBackgroundColor = Color.OrangeRed;
                        BlackFinalCountDown = true;
                    }



                }

                
                //if (BlackTimeSeconds == 0)
                //{
                //    BlackTimeSeconds = 60;
                //    //BlackTimeSecondsShow = "00";
                //}


                //else if (BlackTimeSeconds<10)
                //{
                //    BlackTimeSecondsShow = "0"+BlackTimeSeconds;
                //}
                //else
                //{
                //    BlackTimeSecondsShow = BlackTimeSeconds.ToString();
                //}

                if (BlackTimeSeconds == 59 && BlackTimeMinutes != 0)
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
            HandleWhiteIncrement();
            if (!WhiteFinalCountDown)
            {
                WhiteBackgroundColor = NonCountDownClickedColor;
            }
            else
            {
                WhiteBackgroundColor = CountDownClickedColor;
            }

            if (!BlackFinalCountDown)
            {
                BlackBackgroundColor = NonCountDownRunningColor;
            }
            else
            {
                BlackBackgroundColor = CountDownRunningColor;
            }

            WhiteIsEnabled = false;
            BlackIsEnabled = true;
            WhiteTimerRun = false;
            BlackTimerRun = true;
            //Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            //{
            //    Task.Run(async () =>
            //    {
            //        var time = await HandleBlackTime();

            //    });
            //    return true;
            //});
            //HandleIncrement(ref _whiteTimeSeconds, Increment);
            
            //WhiteTimeSecondsShow = WhiteTimeSeconds.ToString();
            Device.StartTimer(TimeSpan.FromMilliseconds(100),HandleBlackTime );
        }

        private void OnBlack_Tapped()
        {
            Pause = false;
            Reset = false;
            HandleBlackIncrement();

            if (!BlackFinalCountDown)
            {
                BlackBackgroundColor = NonCountDownClickedColor;
            }
            else
            {
                BlackBackgroundColor = CountDownClickedColor;
            }

            if (!WhiteFinalCountDown)
            {
                WhiteBackgroundColor = NonCountDownRunningColor;
            }
            else
            {
                WhiteBackgroundColor = CountDownRunningColor;
            }

            BlackIsEnabled = false;
            WhiteIsEnabled = true;
            BlackTimerRun = false;
            WhiteTimerRun = true;
            //Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            //{
            //    Task.Run(async () =>
            //    {
            //        var time = await HandleWhiteTime();

            //    });
            //    return true;
            //});

            //When Increment was generally handled for both colors
            //HandleIncrement(ref _blackTimeSeconds,Increment);

            

            //BlackTimeSecondsShow = BlackTimeSeconds.ToString();
            Device.StartTimer(TimeSpan.FromMilliseconds(100), HandleWhiteTime);
        }

        //private void HandleIncrement(ref int timeSecond,int increment)
        //{
        //     timeSecond += increment;
        //}

        private void HandleWhiteIncrement()
        {
            if ((WhiteTimeSeconds + SelectedTimeControl.Increment)>=60)
            {
                if (WhiteTimeSeconds!=60)
                {
                    WhiteTimeMinutes++;
                }
                WhiteTimeSeconds = WhiteTimeSeconds + SelectedTimeControl.Increment - 60;
                
            }
            else
            {
                WhiteTimeSeconds += SelectedTimeControl.Increment;
            }
            
        }

        private void HandleBlackIncrement()
        {
            
            if ((BlackTimeSeconds + SelectedTimeControl.Increment) >= 60)
            {
                if (BlackTimeSeconds != 60)
                {
                    BlackTimeMinutes++;
                }
                BlackTimeSeconds = BlackTimeSeconds + SelectedTimeControl.Increment - 60;
                
            }
            else
            {
                BlackTimeSeconds += SelectedTimeControl.Increment;
            }
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
                    //Description = "Bullet 1|0"
                    Description = "1|0"
                },
                new TimeControl
                {
                    Index = 1,
                    Limit = 1,
                    Increment = 1,
                    Description = "1|1"
                },
                new TimeControl
                {
                    Index = 2,
                    Limit = 3,
                    Increment = 0,
                    //Description = "Blitz 3|0"
                    Description = "3|0"
                },
                new TimeControl
                {
                    Index = 3,
                    Limit = 3,
                    Increment = 2,
                    Description = "3|2"
                },
                new TimeControl
                {
                    Index = 4,
                    Limit = 5,
                    Increment = 0,
                    Description = "5|0"
                },
                new TimeControl
                {
                    Index = 5,
                    Limit = 5,
                    Increment = 5,
                    Description = "5|5"
                },
                new TimeControl
                {
                    Index = 6,
                    Limit = 15,
                    Increment = 10,
                    //Description = "Rapid 15|10"
                    Description = "15|10"
                },
                new TimeControl
                {
                    Index = 7,
                    Limit = 25,
                    Increment = 0,
                    Description = "25|0"
                },
                new TimeControl
                {
                    Index = 8,
                    Limit = 25,
                    Increment = 10,
                    Description = "25|10"
                }
            };

            SelectedTimeControl = TimeControls[0];

            //For Real Time
            //Increment = SelectedTimeControl.Increment;

            WhiteTimeMinutes = SelectedTimeControl.Limit;
            WhiteTimeSeconds = 60;
            //WhiteTimeSecondsShow = "00";
            BlackTimeMinutes = SelectedTimeControl.Limit;
            BlackTimeSeconds = 60;
            //BlackTimeSecondsShow = "00";

            //For Last Few Seconds Testing
            //Increment = 2;

            //WhiteTimeMinutes = 0;
            //WhiteTimeSeconds = 15;
            ////WhiteTimeSecondsShow = "15";
            //BlackTimeMinutes = 0;
            //BlackTimeSeconds = 15;
            ////BlackTimeSecondsShow = "15";


            BlackBackgroundColor = NonCountDownRunningColor;
            WhiteBackgroundColor = NonCountDownRunningColor;
            WhiteIsEnabled = true;
            BlackIsEnabled = true;
            Reset = false;
            Pause = true;
            WhiteFinalCountDown = false;
            BlackFinalCountDown = false;


            SelectTimeControlCommand = new Command(
                (chosenTimeControl) =>
                {
                    var chosenTime = Int32.Parse(chosenTimeControl.ToString())==-1? SelectedTimeControl.Index:Int32
                        .Parse(chosenTimeControl.ToString());
                    SelectedTimeControl = this.TimeControls.First(x => x.Index == chosenTime);
                    //Increment = SelectedTimeControl.Increment;
                }

                );

            ResetTimeCommand = new Command(
                ResetTime);

            PauseTimeCommand= new Command(
                () =>
                {
                    Pause = true;
                    WhiteBackgroundColor= PausedColor;
                    BlackBackgroundColor= PausedColor;
                    WhiteIsEnabled = true;
                    BlackIsEnabled = true;
                });


            OnWhiteTappedCommand = new Command(() => OnWhite_Tapped());
            OnBlackTappedCommand = new Command(() => OnBlack_Tapped());

        }

        private void ResetTime()
        {
            //Real Time testing
            WhiteTimeMinutes = SelectedTimeControl.Limit;
            WhiteTimeSeconds = 60;
            whiteTimeProgress = 0;

            BlackTimeMinutes = SelectedTimeControl.Limit;
            BlackTimeSeconds = 60;
            blackTimeProgress = 0;




            //Last few seconds testing
            //WhiteTimeMinutes = 0;
            //WhiteTimeSeconds = 15;
            //whiteTimeProgress = 0;
            //WhiteTimeSecondsShow = "15";


            //BlackTimeMinutes = 0;
            //BlackTimeSeconds = 15;
            //blackTimeProgress = 0;
            //BlackTimeSecondsShow = "15";
            WhiteTimerRun = false;
            BlackTimerRun = false;


            //WhiteBackgroundColor = nonCountDownClickedColor;
            //BlackBackgroundColor = nonCountDownClickedColor;
            
            WhiteIsEnabled = true;
            BlackIsEnabled = true;

            whiteTimeProgress = 0;
            blackTimeProgress = 0;
            WhiteFinalCountDown = false;
            BlackFinalCountDown = false;

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
