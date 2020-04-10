using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ChessClock.Helpers;
using ChessClock.ViewModels;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms;

namespace ChessClock
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage,INotifyPropertyChanged
    {
        //IAudio audio;
        //private Stream audioStream;

        public MainPage()
        {
            InitializeComponent();
            //audio = DependencyService.Get<IAudio>();


            //var assembly = typeof(App).GetTypeInfo().Assembly;
            //audioStream = assembly.GetManifestResourceStream("ChessClock." + "click.mp3");
        }
        private void SettingsButton_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new SettingsPage());
        }


        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            //audio = DependencyService.Get<IAudio>();
            //audio.PlayAudio();

            ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(GetStreamFromFile("click.mp3"));
            player.Play();
        }

        Stream GetStreamFromFile(string filename)
        {
            var assembly = typeof(App).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream("ChessClock." + filename);
            return stream;
        }
    }
}
