using System;
using System.ComponentModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();

            bool isExpired = Preferences.Get("isExpired", false);
            IsExpiredSwitch.IsToggled = isExpired;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void IsExpiredSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            Preferences.Set("isExpired", IsExpiredSwitch.IsToggled);
        }
    }
}