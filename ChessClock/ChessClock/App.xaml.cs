using System;
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
        }

        protected override void OnResume()
        {
        }
    }
}
