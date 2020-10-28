using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class CoalMine : Building
    {

        public CoalMine(int x, int y, ResourceGet ground) : base(x,y,Balance.coalMineC,new PictureBox(),Resource.COAL,Balance.coalMineY,Balance.coalMineU, ground)
        {
            sprite.Image = Properties.Resources.CoalMine;
        }

    }
}
