using System;
using System.Threading;
using System.Windows.Forms;
using BcProjekt.View;

namespace BcProjekt
{
    class MainClass
    {
        private static Form main;

        public static void Main(string[] args)
        { 

            main = new MainWindowView();
            Application.Run(main);



        }
    }
}
