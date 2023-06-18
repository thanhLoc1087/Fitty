using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Fitty.iOS;
using Xamarin.Forms;

[assembly:Dependency(typeof(NotificationHelper))]
namespace Fitty.iOS
{
    class NotificationHelper : INotification
    {
        public void CreateNotification(string title, string message)
        {
            new NotificationDelegate().RegisterNotification(title, message);
        }
    }
}