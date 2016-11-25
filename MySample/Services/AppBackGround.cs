using Windows.UI.Xaml;
using System;
using Windows.ApplicationModel;
using Windows.UI.Notifications;

namespace MySample
{
    sealed partial class App : Application
    {
        bool isBackGround;
        partial void Init()
        {
            EnteredBackground += AppBack_Enter;
            LeavingBackground += AppBack_Leave;
            Suspending += App_Suspend;
            Resuming += App_Resume;
        }

        private void App_Resume(object sender, object e)
        {
            ShowToast("继续");
        }

        private void App_Suspend(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //可选内容：挂起前操作
            ShowToast("挂起");
            deferral.Complete();
        }

        private void AppBack_Leave(object sender, LeavingBackgroundEventArgs e)
        {
            ShowToast("前台");
            isBackGround = false;
        }

        private void AppBack_Enter(object sender, EnteredBackgroundEventArgs e)
        {
            ShowToast("后台");
            isBackGround = true;
        }

        public void ShowToast(string txt, string subtxt = "应用状态")
        {
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);
            var toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode(txt));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode(subtxt));
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}