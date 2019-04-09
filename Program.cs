using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Animal
{
    static class Program
    {
        // main entrance to game program
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AnimalMain());
        }
    }
}
