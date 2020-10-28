using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class WoodFurnace : Building
    {

        public WoodFurnace(int x, int y, ResourceGet ground) : base(x,y,Balance.woodFurnaceC,new PictureBox(),Resource.NONE,0,0, ground)
        {
            if (ground.getResources().Equals(Resource.STONE))
            {
                sprite.Image = Properties.Resources.StoneWoodFurnace;
            }
            else if (ground.getSwamp())
            {
                sprite.Image = Properties.Resources.SwampWoodFurnace;
            }
            else
            {
                sprite.Image = Properties.Resources.GrassWoodFurnace;
            }
        }

        
        public override void update(ResourceGet resourcePatch)
        {
            if (Program.wood < Balance.woodFurnaceB)
            {
                Program.energyBalance += Program.wood *Balance.woodFurnaceE;
                Program.dEnergy += Program.wood * Balance.woodFurnaceE;
                Program.dWood -= Balance.woodFurnaceB;
                Program.wood = 0;
            }
            else
            {
                Program.energyBalance += Balance.woodFurnaceB * Balance.woodFurnaceE;
                Program.wood -= Balance.woodFurnaceB;
                Program.dWood -= Balance.woodFurnaceB;
                Program.dEnergy += Balance.woodFurnaceB * Balance.woodFurnaceE;
            }
        }

    }
}
