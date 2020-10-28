using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39
{
    static class Program
    {
        public static int energyBalance, tileW, tileH, wood, oil, coal, stone, w, h, gas;
        public static int dWood, dOil, dStone, dCoal, dEnergy, dGas; //Delta resources
        static Timer t;
        public static Feiv f;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            t = new Timer();
            t.Interval = (int)1;
            t.Tick += tick;
            t.Start();
            f = new Feiv();
            Application.Run(f);
        }

        static void tick(object sender, EventArgs e)
        {
            f.update();
        }

        public static void clearDelta()
        {
            dWood = 0;
            dOil = 0;
            dCoal = 0;
            dStone = 0;
            dEnergy = 0;
            dGas = 0;
        }

    }
}
