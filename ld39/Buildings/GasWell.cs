using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class GasWell : Building
    {

        public GasWell(int x, int y, ResourceGet ground) : base(x,y,Balance.gasWellC,new PictureBox(),Resource.GAS,Balance.gasWellY,Balance.gasWellU,ground)
        {
            sprite.Image = Properties.Resources.GasPump;
        }

    }
}
