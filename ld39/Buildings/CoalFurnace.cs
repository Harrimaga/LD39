using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class CoalFurnace : Building
    {

        public CoalFurnace(int x, int y,ResourceGet ground) : base(x,y,Balance.coalFurnaceC,new PictureBox(),Resource.NONE,0,0, ground)
        {
            if (ground.getResources().Equals(Resource.STONE))
            {
                sprite.Image = Properties.Resources.StoneCoalFurnace;
            }
            else if (ground.getSwamp())
            {
                sprite.Image = Properties.Resources.SwampCoalFurnace;
            }
            else
            {
                sprite.Image = Properties.Resources.GrassCoalFurnace;
            }
        }

        public override void update(ResourceGet resourcePatch)
        {
            if (Program.coal < Balance.coalFurnaceB)
            {
                Program.energyBalance += Program.coal * Balance.coalFurnaceE;
                Program.dCoal -= Balance.coalFurnaceB;
                Program.dEnergy += Program.coal * Balance.coalFurnaceE;
                Program.coal = 0;
            }
            else
            {
                Program.energyBalance += Balance.coalFurnaceB * Balance.coalFurnaceE;
                Program.coal -= Balance.coalFurnaceB;
                Program.dCoal -= Balance.coalFurnaceB;
                Program.dEnergy += Balance.coalFurnaceB * Balance.coalFurnaceE;
            }
        }

    }
}
