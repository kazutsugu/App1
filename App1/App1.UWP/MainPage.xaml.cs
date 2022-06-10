using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.Foundation.Collections;
using Windows.UI.Core.Preview;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel;
using Xamarin.Essentials;

namespace App1.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            SystemNavigationManagerPreview mgr =
                SystemNavigationManagerPreview.GetForCurrentView();
            mgr.CloseRequested += SystemNavigationManager_CloseRequested;

            LoadApplication(new App1.App());
        }

        private async void SystemNavigationManager_CloseRequested(object sender, SystemNavigationCloseRequestedPreviewEventArgs e)
        {
            Deferral deferral = e.GetDeferral();
            ConfirmCloseDialog dlg = new ConfirmCloseDialog();
            ContentDialogResult result = await dlg.ShowAsync();
            if (result == ContentDialogResult.Secondary)
            {
                // user cancelled the close operation
                e.Handled = true;
                deferral.Complete();
            }
            else
            {
                switch (dlg.Result)
                {
                    case CloseAction.Terminate:
                        e.Handled = false;
                        deferral.Complete();
                        break;

                    case CloseAction.Systray:
                        if (ApiInformation.IsApiContractPresent(
                             "Windows.ApplicationModel.FullTrustAppContract", 1, 0))
                        {
                            bool isExpired = Preferences.Get("isExpired", false);
                            if(!isExpired)
                                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync();
                            else
                                await FullTrustProcessLauncher.LaunchFullTrustProcessForCurrentAppAsync("expired");
                        }
                        e.Handled = false;
                        deferral.Complete();
                        break;
                }
            }
        }
    }
}
