using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ld39
{
    class ResourceGet
    {
        private Resource resource;
        private int amount;
        private bool isSwamp;

        public ResourceGet(Resource resource, int amount, bool isSwamp)
        {
            this.resource = resource;
            this.amount = amount;
            this.isSwamp = isSwamp;
        }

        public Resource getResources()
        {
            return resource;
        }
        public void setResource(Resource r)
        {
            resource = r;
        }

        public int getAmount()
        {
            return amount;
        }

        public void setAmount(int amount)
        {
            this.amount = amount;
        }

        public void setSwamp(bool swamp)
        {
            isSwamp = swamp;
        }

        public bool getSwamp()
        {
            return isSwamp;
        }

        public void removeAmount(int delta)
        {
            this.amount -= delta;
        }
    }
}
