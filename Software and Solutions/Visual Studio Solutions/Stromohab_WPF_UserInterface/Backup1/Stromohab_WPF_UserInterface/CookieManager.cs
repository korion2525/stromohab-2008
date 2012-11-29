using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Stromohab_WPF_UserInterface
{
    public static class CookieManager
    {
     
        public static string UserName
        {
            get
            {
                return Application.GetCookie(new Uri(Environment.CurrentDirectory)).Split('=')[1];
            }
            set
            {
                Application.SetCookie(new Uri(Environment.CurrentDirectory), "userName=" + value);
            }
        }
        


    }
}
