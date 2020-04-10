using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using ChessClock.Droid.DependencyServices;
using ChessClock.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(Audio))]
namespace ChessClock.Droid.DependencyServices
{
    public class Audio : IAudio
    {
        private MediaPlayer _mediaPlayer;

        public  bool PlayAudio()
        {
            _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.click);
            _mediaPlayer.Start();
            return true;
        }
    }
}