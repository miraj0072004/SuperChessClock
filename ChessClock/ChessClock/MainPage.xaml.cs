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
        

        public MainPage()
        {
            InitializeComponent();

            
            //WhiteTimeMinutes = 5;

            

        }

        

        

        private void SettingsButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SettingsPage());
        }
    }
}
