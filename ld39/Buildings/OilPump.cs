using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ld39.Buildings
{
    class OilPump : Building
    {

        public OilPump(int x, int y, ResourceGet ground) : base(x, y, Balance.oilPumpC, new PictureBox(), Resource.OIL, Balance.oilPumpY, Balance.oilPumpU, ground)
        {
            if (ground.getSwamp())
                sprite.Image = Properties.Resources.SwampOilWell;
            else
                sprite.Image = Properties.Resources.OilWell;
        }

    }
}
