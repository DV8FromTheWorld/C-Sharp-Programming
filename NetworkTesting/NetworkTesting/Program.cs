using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace NetworkTesting
{
    static class Program
    {
        public static Base program;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            program = new Base();
            Application.Run(program);
        }
    }
}
