using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39
{
    class WoodMill : Building
    {

        public WoodMill(int x, int y, ResourceGet ground) : base(x,y,Balance.woodMillC,new PictureBox(), Resource.WOOD, Balance.woodMillY, Balance.woodMillU, ground)
        {
            if (ground.getSwamp())
                sprite.Image = Properties.Resources.SwampWoodMill;
            else
                sprite.Image = Properties.Resources.WoodMill;
        }

        
    }
}
