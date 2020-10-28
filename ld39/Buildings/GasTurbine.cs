using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class GasTurbine : Building
    {

        public GasTurbine(int x, int y, ResourceGet ground) : base(x,y,Balance.gasTurbineC,new PictureBox(),Resource.NONE,0,0,ground)
        {
            if (ground.getResources().Equals(Resource.STONE))
            {
                sprite.Image = Properties.Resources.StoneGasTurbine;
            }
            else if (ground.getSwamp())
            {
                sprite.Image = Properties.Resources.SwampGasTurbine;
            }
            else
            {
                sprite.Image = Properties.Resources.GrassGasTurbine;
            }
        }

        public override void update(ResourceGet resourcePatch)
        {
            if (Program.gas < Balance.gasTurbineB)
            {
                Program.energyBalance += Program.gas * Balance.gasTurbinE;
                Program.dEnergy += Program.gas * Balance.gasTurbinE;
                Program.dGas -= Balance.gasTurbineB;
                Program.gas = 0;
            }
            else
            {
                Program.energyBalance += Balance.gasTurbineB * Balance.gasTurbinE; 
                Program.gas -= Balance.gasTurbineB;
                Program.dGas -= Balance.gasTurbineB;
                Program.dEnergy += Balance.gasTurbineB * Balance.gasTurbinE;
            }
        }

    }
}
