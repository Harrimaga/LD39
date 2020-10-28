using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class AdvancedMine : Building
    {
        public AdvancedMine(int x, int y, ResourceGet ground) : base(x,y,Balance.advanceMineC,new PictureBox(),Resource.STONE,Balance.advanceMineY,Balance.advanceMineU, ground)
        {
            sprite.Image = Properties.Resources.AdvancedStoneMine;
        }
    }
}
