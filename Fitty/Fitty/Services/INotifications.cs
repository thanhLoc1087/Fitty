using System;
using System.Collections.Generic;
using System.Text;

namespace Fitty_Xamarin
{
    public interface INotification
    {
        void CreateNotification(String title, String message);
    }
}