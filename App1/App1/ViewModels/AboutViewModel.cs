using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            //Title = "About";
            var lib = new ClassLibrary1.Class1();
            Title = lib.HelloWorld();
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://xamarin.com"));
        }

        public ICommand OpenWebCommand { get; }
    }
}