using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace HaberinMerkezi.Admin.App_Classes
{
    public static class boyutCeken
    {

        public static Size buyukBoyutCek
        {
            get
            {
                Size temp = new Size();
                temp.Width = Convert.ToInt32(ConfigurationManager.AppSettings["BuyukBoyutWidth"]);
                temp.Height = Convert.ToInt32(ConfigurationManager.AppSettings["BuyukBoyutHeight"]);
                return temp;
            }
        }
        public static Size ortaBoyutCek
        {
            get
            {
                Size temp = new Size();
                temp.Width = Convert.ToInt32(ConfigurationManager.AppSettings["OrtaBoyutWidth"]);
                temp.Height = Convert.ToInt32(ConfigurationManager.AppSettings["OrtaBoyutHeight"]);
                return temp;
            }
        }
    }
}