using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace ld39
{
    class Building
    {

        protected int x, y, cost, pSpeed, deltaE;
        protected PictureBox sprite;
        protected Resource mines;
        protected ResourceGet ground;
        private bool dead = false;

        protected Building(int x, int y, int cost, PictureBox sprite, Resource mines, int pSpeed, int deltaE, ResourceGet ground)
        {
            this.x = x;
            this.y = y;
            this.cost = cost;
            this.sprite = sprite;
            this.mines = mines;
            this.pSpeed = pSpeed;
            this.sprite.Top = y * Program.tileH;
            this.sprite.Left = x * Program.tileW;
            this.sprite.Width = Program.tileW;
            this.sprite.Height = Program.tileH;
            this.sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            this.deltaE = deltaE;
            this.ground = ground;
        }

        public int getX()
        {
            return x;
        }

        public int getY()
        {
            return y;
        }

        public int getCost()
        {
            return cost;
        }

        public PictureBox getPictureBox()
        {
            return sprite;
        }

        public virtual void update(ResourceGet resourcePatch)
        {
            energyUpdate();
            resourceUpdate(resourcePatch);
        }

        private void resourceUpdate(ResourceGet patch)
        {
            int r = 0;
            if(patch.getResources().Equals(mines))
            {
                if(patch.getAmount()<pSpeed)
                {
                    r = patch.getAmount();
                    patch.setAmount(0);
                    dead = true;
                }
                else
                {
                    r = pSpeed;
                    patch.removeAmount(pSpeed);
                }
            }
            switch (mines)
            {
                case(Resource.WOOD):
                    Program.wood += r;
                    Program.dWood += r;
                    break;
                case (Resource.COAL):
                    Program.coal += r;
                    Program.dCoal += r;
                    break;
                case (Resource.OIL):
                    Program.oil += r;
                    Program.dOil += r;
                    break;
                case (Resource.STONE):
                    Program.stone += r;
                    Program.dStone += r;
                    break;
                case (Resource.GAS):
                    Program.gas += r;
                    Program.dGas += r;
                    break;
            }
        }

        private void energyUpdate()
        {
            Program.energyBalance += deltaE;
            Program.dEnergy += deltaE;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public bool locatedAt(int x , int y)
        {
            return this.x == x && this.y == y;
        }

        public bool getDead()
        {
            return dead;
        }

    }
}
