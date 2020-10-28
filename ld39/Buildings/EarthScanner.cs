using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld39.Buildings
{
    class EarthScanner : Building
    {
        public EarthScanner(int x, int y, ResourceGet ground) : base(x,y,Balance.earthScannerC,new System.Windows.Forms.PictureBox(),Resource.NONE,0,Balance.earthScannerU,ground)
        {
            if (ground.getResources().Equals(Resource.STONE))
            {
                sprite.Image = Properties.Resources.StoneEarthScanner;
            }
            else if (ground.getSwamp())
            {
                sprite.Image = Properties.Resources.SwampEarthScanner;
            }
            else
            {
                sprite.Image = Properties.Resources.GrassEarthScanner;
            }
        }
    }
}
