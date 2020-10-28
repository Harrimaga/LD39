using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class StoneMine : Building
    {

        public StoneMine(int x, int y, ResourceGet ground) : base(x,y,Balance.stoneMineC,new PictureBox(),Resource.STONE,Balance.stoneMineY,Balance.stoneMineU, ground)
        {
            sprite.Image = Properties.Resources.StoneMine;
        }

    }
}
