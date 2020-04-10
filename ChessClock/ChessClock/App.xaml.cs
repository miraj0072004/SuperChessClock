using System;
using ChessClock.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChessClock
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.MaterialModule())
                                  .With(new Plugin.Iconize.Fonts.MaterialDesignIconsModule())
                                  .With(new Plugin.Iconize.Fonts.FontAwesomeRegularModule());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            var viewModel = Application.Current.Resources["ClockViewModel"] as ClockViewModel;
            if (viewModel != null)
            {
                viewModel.PauseTimeCommand.Execute(null);
            }
        }

        protected override void OnResume()
        {
        }
    }
}
