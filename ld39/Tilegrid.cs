using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Collections;

namespace ld39
{
    class Tilegrid
    {

        private ResourceGet[,] mapRes;
        private Bitmap map;
        private int tilex, tiley, onScreenX = 20, onScreenY = 12, w, h;
        private bool makingImage = false;

        public Tilegrid(int tilex, int tiley, int w, int h, int osx, int osy, int bx, int by)
        {
            this.tilex = tilex;
            this.tiley = tiley;
            onScreenX = osx;
            onScreenY = osy;
            this.w = w;
            this.h = h;
            mapRes = new ResourceGet[w,h];
            Random r = new Random();
            for(int i=0;i<w;i++)
            {
                for(int j=0;j<h;j++)
                {
                    /*
                    int ran = r.Next(100);
                    if(ran < 25)
                    {
                        if(r.Next(2) == 0)
                        {
                            mapRes[i, j] = new ResourceGet(Resource.NONE, 0, false);
                        }
                        else
                        {
                            mapRes[i, j] = new ResourceGet(Resource.NONE, 0, true);
                        }
                    }
                    else if(ran < 40)
                    {
                        if (r.Next(2) == 0)
                        {
                            mapRes[i, j] = new ResourceGet(Resource.WOOD, 175 + r.Next(50), false);
                        }
                        else
                        {
                            mapRes[i, j] = new ResourceGet(Resource.WOOD, 175 + r.Next(50), true);
                        }
                    }
                    else if(ran < 46)
                    {
                        mapRes[i, j] = new ResourceGet(Resource.STONE, 800 + r.Next(400), false);
                    }
                    else if(ran < 49)
                    {
                        mapRes[i, j] = new ResourceGet(Resource.COAL, 100 + r.Next(40), false);
                    }
                    else if(ran < 50)
                    {
                        if (r.Next(2) == 0)
                        {
                            mapRes[i, j] = new ResourceGet(Resource.OIL, 350 + r.Next(200), false);
                        }
                        else
                        {
                            mapRes[i, j] = new ResourceGet(Resource.OIL, 350 + r.Next(200), true);
                        }
                    }
                    else if(ran < 75)
                    {
                        if(i > 0)
                        {
                            int am = 0;
                            switch(mapRes[i - 1, j].getResources())
                            {
                                case (Resource.OIL):
                                    am = 350 + r.Next(300);
                                    break;
                                case (Resource.STONE):
                                    am = 800 + r.Next(400);
                                    break;
                                case (Resource.GAS):
                                    am = 100 + r.Next(100);
                                    break;
                                case (Resource.NONE):
                                    am = 0;
                                    break;
                                case (Resource.COAL):
                                    am = 100 + r.Next(40);
                                    break;
                                case (Resource.WOOD):
                                    am = 175 + r.Next(50);
                                    break;
                            }
                            mapRes[i, j] = new ResourceGet(mapRes[i-1, j].getResources(), am, mapRes[i-1, j].getSwamp());
                        }
                        else
                        {
                            if (r.Next(2) == 0)
                            {
                                mapRes[i, j] = new ResourceGet(Resource.NONE, 0, false);
                            }
                            else
                            {
                                mapRes[i, j] = new ResourceGet(Resource.NONE, 0, true);
                            }
                        }
                    }
                    else
                    {
                        if (j > 0)
                        {
                            int am = 0;
                            switch (mapRes[i, j - 1].getResources())
                            {
                                case (Resource.OIL):
                                    am = 350 + r.Next(200);
                                    break;
                                case (Resource.STONE):
                                    am = 800 + r.Next(400);
                                    break;
                                case (Resource.GAS):
                                    am = 100 + r.Next(100);
                                    break;
                                case (Resource.NONE):
                                    am = 0;
                                    break;
                                case (Resource.COAL):
                                    am = 100 + r.Next(40);
                                    break;
                                case (Resource.WOOD):
                                    am = 175 + r.Next(50);
                                    break;
                            }
                            mapRes[i, j] = new ResourceGet(mapRes[i, j - 1].getResources(), am, mapRes[i, j - 1].getSwamp());
                        }
                        else
                        {
                            if (r.Next(2) == 0)
                            {
                                mapRes[i, j] = new ResourceGet(Resource.NONE, 0, false);
                            }
                            else
                            {
                                mapRes[i, j] = new ResourceGet(Resource.NONE, 0, true);
                            }
                        }
                    }
                    */
                    mapRes[i, j] = new ResourceGet(Resource.NONE, 0, false);
                }
            }
            int am = 10;
            for(int i=0;i<am;i++)
            {
                int chance = 100;
                int cMin = 2;
                ArrayList good = new ArrayList();
                for(int k=0;k<w;k++)
                {
                    for(int j=0;j<h;j++)
                    {
                        if(mapRes[k, j].getResources().Equals(Resource.NONE))
                        {
                            good.Add(new Point(k, j));
                        }
                    }
                }
                Point rp = (Point)good[r.Next(good.Count)];
                int plx = rp.X;
                int ply = rp.Y;
                if(mapRes[plx, ply].getResources().Equals(Resource.NONE))
                {
                    chance = 80;
                    ArrayList changed = new ArrayList();
                    ArrayList nchanged = new ArrayList();
                    mapRes[plx, ply].setResource(Resource.STONE);
                    changed.Add(new Point(plx, ply));
                    mapRes[plx, ply].setAmount(r.Next(400) + 800);
                    while (changed.Count > 0)
                    {
                        foreach(Point p in changed)
                        {
                            if(p.X + 1 < w && r.Next(100) < chance && mapRes[p.X + 1, p.Y].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X + 1, p.Y].setResource(Resource.STONE);
                                mapRes[p.X + 1, p.Y].setAmount(r.Next(400) + 800);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X + 1, p.Y));
                            }
                            if (p.X - 1 >= 0 && r.Next(100) < chance && mapRes[p.X - 1, p.Y].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X - 1, p.Y].setResource(Resource.STONE);
                                mapRes[p.X - 1, p.Y].setAmount(r.Next(400) + 800);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X - 1, p.Y));
                            }
                            if (p.Y + 1 < h && r.Next(100) < chance && mapRes[p.X, p.Y + 1].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X, p.Y + 1].setResource(Resource.STONE);
                                mapRes[p.X, p.Y + 1].setAmount(r.Next(400) + 800);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y + 1));
                            }
                            if (p.Y - 1 >= 0 && r.Next(100) < chance && mapRes[p.X, p.Y - 1].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X, p.Y - 1].setResource(Resource.STONE);
                                mapRes[p.X, p.Y - 1].setAmount(r.Next(400) + 800);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y - 1));
                            }
                        }
                        changed = nchanged;
                        nchanged = new ArrayList();
                    }
                }
                else
                {
                    i--;
                }

            }
            am = 6;
            for (int i = 0; i < am; i++)
            {
                int chance = 100;
                int cMin = 6;
                ArrayList good = new ArrayList();
                for (int k = 0; k < w; k++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (mapRes[k, j].getResources().Equals(Resource.STONE))
                        {
                            good.Add(new Point(k, j));
                        }
                    }
                }
                Point rp = (Point)good[r.Next(good.Count)];
                int plx = rp.X;
                int ply = rp.Y;
                if (mapRes[plx, ply].getResources().Equals(Resource.STONE))
                {
                    chance = 90;
                    ArrayList changed = new ArrayList();
                    ArrayList nchanged = new ArrayList();
                    mapRes[plx, ply].setResource(Resource.COAL);
                    mapRes[plx, ply].setAmount(r.Next(80) + 200);
                    changed.Add(new Point(plx, ply));
                    while (changed.Count > 0)
                    {
                        foreach (Point p in changed)
                        {
                            if (p.X + 1 < w && r.Next(100) < chance && mapRes[p.X + 1, p.Y].getResources().Equals(Resource.STONE))
                            {
                                mapRes[p.X + 1, p.Y].setResource(Resource.COAL);
                                mapRes[p.X + 1, p.Y].setAmount(r.Next(80) + 200);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X + 1, p.Y));
                            }
                            if (p.X - 1 >= 0 && r.Next(100) < chance && mapRes[p.X - 1, p.Y].getResources().Equals(Resource.STONE))
                            {
                                mapRes[p.X - 1, p.Y].setResource(Resource.COAL);
                                mapRes[p.X - 1, p.Y].setAmount(r.Next(80) + 200);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X - 1, p.Y));
                            }
                            if (p.Y + 1 < h && r.Next(100) < chance && mapRes[p.X, p.Y + 1].getResources().Equals(Resource.STONE))
                            {
                                mapRes[p.X, p.Y + 1].setResource(Resource.COAL);
                                mapRes[p.X, p.Y + 1].setAmount(r.Next(80) + 200);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y + 1));
                            }
                            if (p.Y - 1 >= 0 && r.Next(100) < chance && mapRes[p.X, p.Y - 1].getResources().Equals(Resource.STONE))
                            {
                                mapRes[p.X, p.Y - 1].setResource(Resource.COAL);
                                mapRes[p.X, p.Y - 1].setAmount(r.Next(80) + 200);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y - 1));
                            }
                        }
                        changed = nchanged;
                        nchanged = new ArrayList();
                    }
                }
                else
                {
                    i--;
                }

            }
            am = 10;
            for (int i = 0; i < am; i++)
            {
                int chance = 93;
                int cMin = 1;
                ArrayList good = new ArrayList();
                for (int k = 0; k < w; k++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (mapRes[k, j].getResources().Equals(Resource.NONE))
                        {
                            good.Add(new Point(k, j));
                        }
                    }
                }
                Point rp = (Point)good[r.Next(good.Count)];
                int plx = rp.X;
                int ply = rp.Y;
                if (mapRes[plx, ply].getResources().Equals(Resource.NONE))
                {
                    chance = 80;
                    ArrayList changed = new ArrayList();
                    ArrayList nchanged = new ArrayList();
                    mapRes[plx, ply].setResource(Resource.WOOD);
                    mapRes[plx, ply].setAmount(r.Next(50) + 175);
                    changed.Add(new Point(plx, ply));
                    while (changed.Count > 0)
                    {
                        foreach (Point p in changed)
                        {
                            if (p.X + 1 < w && r.Next(100) < chance && mapRes[p.X + 1, p.Y].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X + 1, p.Y].setResource(Resource.WOOD);
                                mapRes[p.X + 1, p.Y].setAmount(r.Next(50) + 175);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X + 1, p.Y));
                            }
                            if (p.X - 1 >= 0 && r.Next(100) < chance && mapRes[p.X - 1, p.Y].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X - 1, p.Y].setResource(Resource.WOOD);
                                mapRes[p.X - 1, p.Y].setAmount(r.Next(50) + 175);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X - 1, p.Y));
                            }
                            if (p.Y + 1 < h && r.Next(100) < chance && mapRes[p.X, p.Y + 1].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X, p.Y + 1].setResource(Resource.WOOD);
                                mapRes[p.X, p.Y + 1].setAmount(r.Next(50) + 175);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y + 1));
                            }
                            if (p.Y - 1 >= 0 && r.Next(100) < chance && mapRes[p.X, p.Y - 1].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X, p.Y - 1].setResource(Resource.WOOD);
                                mapRes[p.X, p.Y - 1].setAmount(r.Next(50) + 175);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y - 1));
                            }
                        }
                        changed = nchanged;
                        nchanged = new ArrayList();
                    }
                }
                else
                {
                    i--;
                }

            }
            am = 4;
            for (int i = 0; i < am; i++)
            {
                int chance = 60;
                int cMin = 12;
                ArrayList good = new ArrayList();
                for (int k = 0; k < w; k++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (mapRes[k, j].getResources().Equals(Resource.NONE))
                        {
                            good.Add(new Point(k, j));
                        }
                    }
                }
                Point rp = (Point)good[r.Next(good.Count)];
                int plx = rp.X;
                int ply = rp.Y;
                if (mapRes[plx, ply].getResources().Equals(Resource.NONE))
                {
                    chance = 60;
                    ArrayList changed = new ArrayList();
                    ArrayList nchanged = new ArrayList();
                    mapRes[plx, ply].setResource(Resource.OIL);
                    mapRes[plx, ply].setAmount(r.Next(200) + 350);
                    changed.Add(new Point(plx, ply));
                    while (changed.Count > 0)
                    {
                        foreach (Point p in changed)
                        {
                            if (p.X + 1 < w && r.Next(100) < chance && mapRes[p.X + 1, p.Y].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X + 1, p.Y].setResource(Resource.OIL);
                                mapRes[p.X + 1, p.Y].setAmount(r.Next(200) + 350);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X + 1, p.Y));
                            }
                            if (p.X - 1 >= 0 && r.Next(100) < chance && mapRes[p.X - 1, p.Y].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X - 1, p.Y].setResource(Resource.OIL);
                                mapRes[p.X - 1, p.Y].setAmount(r.Next(200) + 350);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X - 1, p.Y));
                            }
                            if (p.Y + 1 < h && r.Next(100) < chance && mapRes[p.X, p.Y + 1].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X, p.Y + 1].setResource(Resource.OIL);
                                mapRes[p.X, p.Y + 1].setAmount(r.Next(200) + 350);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y + 1));
                            }
                            if (p.Y - 1 >= 0 && r.Next(100) < chance && mapRes[p.X, p.Y - 1].getResources().Equals(Resource.NONE))
                            {
                                mapRes[p.X, p.Y - 1].setResource(Resource.OIL);
                                mapRes[p.X, p.Y - 1].setAmount(r.Next(200) + 350);
                                chance -= cMin;
                                nchanged.Add(new Point(p.X, p.Y - 1));
                            }
                        }
                        changed = nchanged;
                        nchanged = new ArrayList();
                    }
                }
                else
                {
                    i--;
                }

            }
            am = 2;
            for (int i = 0; i < am; i++)
            {
                double chance = 90;
                double cMin = 5;
                ArrayList good = new ArrayList();
                for (int k = 0; k < w; k++)
                {
                    for (int j = 0; j < h; j++)
                    {
                        if (mapRes[k, j].getResources().Equals(Resource.NONE) || mapRes[k, j].getResources().Equals(Resource.WOOD) || mapRes[k, j].getResources().Equals(Resource.OIL))
                        {
                            good.Add(new Point(k, j));
                        }
                    }
                }
                Point rp = (Point)good[r.Next(good.Count)];
                int plx = rp.X;
                int ply = rp.Y;
                if (mapRes[plx, ply].getResources().Equals(Resource.NONE) || mapRes[plx, ply].getResources().Equals(Resource.WOOD) || mapRes[plx, ply].getResources().Equals(Resource.OIL))
                {
                    chance = 100;
                    ArrayList changed = new ArrayList();
                    ArrayList nchanged = new ArrayList();
                    mapRes[plx, ply].setSwamp(true);
                    changed.Add(new Point(plx, ply));
                    while (changed.Count > 0)
                    {
                        foreach (Point p in changed)
                        {
                            if (p.X + 1 < w && r.Next(100) < chance && (mapRes[p.X + 1, p.Y].getResources().Equals(Resource.NONE) || mapRes[p.X + 1, p.Y].getResources().Equals(Resource.WOOD) || mapRes[p.X + 1, p.Y].getResources().Equals(Resource.OIL)))
                            {
                                mapRes[p.X + 1, p.Y].setSwamp(true);
                                nchanged.Add(new Point(p.X + 1, p.Y));
                            }
                            if (p.X - 1 >= 0 && r.Next(100) < chance && (mapRes[p.X - 1, p.Y].getResources().Equals(Resource.NONE) || mapRes[p.X - 1, p.Y].getResources().Equals(Resource.WOOD) || mapRes[p.X - 1, p.Y].getResources().Equals(Resource.OIL)))
                            {
                                mapRes[p.X - 1, p.Y].setSwamp(true);
                                nchanged.Add(new Point(p.X - 1, p.Y));
                            }
                            if (p.Y + 1 < h && r.Next(100) < chance && (mapRes[p.X, p.Y + 1].getResources().Equals(Resource.NONE) || mapRes[p.X, p.Y + 1].getResources().Equals(Resource.WOOD) || mapRes[p.X, p.Y + 1].getResources().Equals(Resource.OIL)))
                            {
                                mapRes[p.X, p.Y + 1].setSwamp(true);
                                nchanged.Add(new Point(p.X, p.Y + 1));
                            }
                            if (p.Y - 1 >= 0 && r.Next(100) < chance && (mapRes[p.X, p.Y - 1].getResources().Equals(Resource.NONE) || mapRes[p.X, p.Y - 1].getResources().Equals(Resource.WOOD) || mapRes[p.X, p.Y - 1].getResources().Equals(Resource.OIL)))
                            {
                                mapRes[p.X, p.Y - 1].setSwamp(true);
                                nchanged.Add(new Point(p.X, p.Y - 1));
                            }
                        }
                        chance -= cMin;
                        changed = nchanged;
                        nchanged = new ArrayList();
                    }
                }
                else
                {
                    i--;
                }

            }
            createImage(bx, by);
        }

        public void createNewImage(int x, int y)
        {
            makingImage = true;
            Task t = new Task(() => { createImage(x, y); });
            t.Start();
        }

        public void createImage(int x, int y)
        {
            Bitmap b = new Bitmap(tilex * (2 + onScreenX), tiley * (2 + onScreenY));
            Graphics g = Graphics.FromImage(b);
            for (int i = -1; i < onScreenX + 1; i++)
            {
                for (int j = -1; j < onScreenY + 2; j++)
                {
                    int xx = x + i;
                    int yy = y + j;
                    if (xx < 0 || yy < 0 || xx >= w || yy >= h)
                    {

                    }
                    else
                    {
                        Image img = null;
                        if(mapRes[xx, yy].getResources().Equals(Resource.COAL))
                        {
                            img = ld39.Properties.Resources.Coal;
                        }
                        else if(mapRes[xx, yy].getResources().Equals(Resource.NONE))
                        {
                            if (mapRes[xx, yy].getSwamp())
                            {
                                img = ld39.Properties.Resources.SwampGrass;
                            }
                            else
                            {
                                img = ld39.Properties.Resources.Grass;
                            }
                        }
                        else if (mapRes[xx, yy].getResources().Equals(Resource.OIL))
                        {
                            if(mapRes[xx, yy].getSwamp())
                            {
                                img = ld39.Properties.Resources.SwampOil;
                            }
                            else
                            {
                                img = ld39.Properties.Resources.Oil;
                            }
                        }
                        else if (mapRes[xx, yy].getResources().Equals(Resource.STONE))
                        {
                            img = ld39.Properties.Resources.Stone;
                        }
                        else if (mapRes[xx, yy].getResources().Equals(Resource.WOOD))
                        {
                            if (mapRes[xx, yy].getSwamp())
                            {
                                img = ld39.Properties.Resources.SwampForrest;
                            }
                            else
                            {
                                img = ld39.Properties.Resources.Forrest;
                            }
                        }
                        else if (mapRes[xx, yy].getResources().Equals(Resource.NONES))
                        {
                            img = ld39.Properties.Resources.StoneDepleted;
                        }
                        else if (mapRes[xx, yy].getResources().Equals(Resource.GAS))
                        {
                            img = ld39.Properties.Resources.Gas;
                        }
                        else
                        {
                            img= ld39.Properties.Resources.Tile;
                        }
                        g.DrawImage(img, tilex * (i+1), tiley * (j+1), tilex, tiley);
                    }
                }
            }
            map = b;
            makingImage = false;
        }

        public Bitmap getBox()
        {
            return map;
        }

        public bool getMakingImg()
        {
            return makingImage;
        }

        public ResourceGet getResource(Building b)
        {
            int x = b.getX()-1;
            int y = b.getY()-1;
            return mapRes[x, y];
        }

        public ResourceGet getResource(int x, int y)
        {
            return mapRes[x, y];
        }

        public void createGas()
        {
            Random r = new Random();
            for(int i=0;i<w;i++)
            {
                for(int j=0;j<h;j++)
                {
                    if(mapRes[i, j].getResources().Equals(Resource.STONE))
                    {
                        if(r.Next(100) < 10)
                        {
                            mapRes[i, j].setResource(Resource.GAS);
                        }
                    }
                }
            }
            ArrayList buildings = Program.f.getBuildings();
            foreach(Building b in buildings)
            {
                if(mapRes[b.getX() - 1, b.getY() - 1].getResources().Equals(Resource.GAS))
                {
                    mapRes[b.getX() - 1, b.getY() - 1].setResource(Resource.STONE);
                }
            }
        }

    }
}
