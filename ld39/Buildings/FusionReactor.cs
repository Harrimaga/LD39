using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class FusionReactor : Building
    {
        public FusionReactor(int x, int y, ResourceGet ground) : base(x,y,Balance.fusionReactorC,new PictureBox(),Resource.NONE,0,0, ground)
        {
            if (ground.getResources().Equals(Resource.STONE))
            {
                sprite.Image = Properties.Resources.StoneFusionReactor;
            }
            else if (ground.getSwamp())
            {
                sprite.Image = Properties.Resources.SwampFusionReactor;
            }
            else
            {
                sprite.Image = Properties.Resources.GrassFusionReactor;
            }
        }
    }
}
