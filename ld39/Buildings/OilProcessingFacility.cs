using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class OilProcessingFacility : Building
    {

        public OilProcessingFacility(int x, int y, ResourceGet ground) : base(x,y,Balance.oilProcessingFacilityC,new PictureBox(),Resource.NONE,0,Balance.oilProcessingFacilityU, ground)
        {
            if (ground.getResources().Equals(Resource.STONE))
            {
                sprite.Image = Properties.Resources.StoneOilProcessingFacility;
            }else if(ground.getSwamp())
            {
                sprite.Image = Properties.Resources.SwampOilProcessingFacility;
            }
            else
            {
                sprite.Image = Properties.Resources.GrassOilProcessingFacility;
            }
        }

        public override void update(ResourceGet resourcePatch)
        {
            if (Program.oil < Balance.oilProcessingFacilityB)
            {
                Program.energyBalance += Program.oil * Balance.oilProcessingFacilityE ;
                Program.dOil -= Balance.oilProcessingFacilityB;
                Program.dEnergy += Program.oil * Balance.oilProcessingFacilityE;
                Program.oil = 0;
            }
            else
            {
                Program.energyBalance += Balance.oilProcessingFacilityB * Balance.oilProcessingFacilityE;
                Program.oil -= Balance.oilProcessingFacilityB;
                Program.dOil -= Balance.oilProcessingFacilityB;
                Program.dEnergy += Balance.oilProcessingFacilityB * Balance.oilProcessingFacilityE;
            }
            Program.dEnergy += Balance.oilProcessingFacilityU;
            Program.energyBalance += Balance.oilProcessingFacilityU;
        }

    }
}
