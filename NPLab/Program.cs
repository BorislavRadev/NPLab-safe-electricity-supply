using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NPLab.Models;

namespace NPLab
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        // #недовършено Да се добави Stat class
        
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Home());
        }
    }
}
