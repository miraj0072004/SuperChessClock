using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChessClock.ViewModels;
using Xamarin.Forms;

namespace ChessClock
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage,INotifyPropertyChanged
    {
        private int _whiteTimeSeconds;
        private int _whiteTimeMinutes;
        private int _whiteTimeHours;
        private int _blackTimeSeconds;
        private int _blackTimeMinutes;
        private int _blackTimeHours;
        private bool _whiteTimerRun=true;
        private bool _blackTimerRun = true;



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
            set => _blackTimeHours = value;
        }

        public MainPage()
        {
            InitializeComponent();

            
            //WhiteTimeMinutes = 5;

            

        }

        

        private bool HandleWhiteTime()
        {

            WhiteTimeMinutes = Int32.Parse(whiteMinuteLabel.Text);
            WhiteTimeSeconds = Int32.Parse(whiteSecondLabel.Text);
            if (WhiteTimeSeconds == 0)
                WhiteTimeSeconds = 60;

            WhiteTimeSeconds--;

            if (WhiteTimeSeconds==59)
            {
                WhiteTimeMinutes--;
            }

            if(WhiteTimeMinutes==0)
            {
                //return new Task<false>;
                return false;

            }

            if (_whiteTimerRun)
            {
                whiteMinuteLabel.Text = WhiteTimeMinutes.ToString();
                whiteSecondLabel.Text = WhiteTimeSeconds.ToString();

                return true;
            }
                

            return false;
        }

        private bool HandleBlackTime()
        {
            BlackTimeMinutes = Int32.Parse(blackMinuteLabel.Text);
            BlackTimeSeconds = Int32.Parse(blackSecondLabel.Text);
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
                blackMinuteLabel.Text = BlackTimeMinutes.ToString();
                blackSecondLabel.Text = BlackTimeSeconds.ToString();
                return true;
            }
                

            return false;
        }

        private void OnWhite_Tapped(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), HandleBlackTime);
            WhiteFrame.BackgroundColor = Color.Bisque;
            WhiteFrame.IsEnabled = false;
            BlackFrame.IsEnabled = true;
            BlackFrame.BackgroundColor = Color.Beige;
            _whiteTimerRun = false;
            _blackTimerRun = true;
        }
        private void OnBlack_Tapped(object sender, EventArgs e)
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), HandleWhiteTime);
            BlackFrame.BackgroundColor = Color.Bisque;
            WhiteFrame.BackgroundColor = Color.Beige;
            BlackFrame.IsEnabled = false;
            WhiteFrame.IsEnabled = true;
            _blackTimerRun = false;
            _whiteTimerRun = true;
        }

        private void SettingsButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SettingsPage());
        }
    }
}
