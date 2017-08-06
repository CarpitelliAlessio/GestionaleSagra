using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SagraElCoda
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          
            //form per serate "regolari" senza eventi 
            Application.Run(new Form2());

            //form con serata in presenza di menu
            Application.Run(new FormMENU());
           
            //form per festa unità 
         //   Application.Run(new FormFU());
          }
    }
}
