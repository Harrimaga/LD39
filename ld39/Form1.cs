using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using ld39.Buildings;
using System.Diagnostics;
using static ld39.Options;

namespace ld39
{
    public partial class Feiv : Form
    {

        private int w, h, tileW, tileH, onScreenX = 40, onScreenY = 23, mapW, mapH, ticksC;
        private Tilegrid tGrid;
        private ArrayList buildings, mineBuildings, prossBuildings;
        private int cameraX, cameraY, camXS = 0, camYS = 0, walkenergytimer = 0, direction = 0, batteryLim = 250;
        private double delta, camProg, updateTick = 0, exponent = 1;
        private bool movingCam, moving, atGrid2, settedimg, buildedScanner = false, startedGame = false, movedButtons = false, movedPanel = false, updateNeeded = false, updatingScreen = false;
        private GUI gui;
        private Stopwatch stopwatch;
        private int px, py, pxs, pys;
        private PictureBox pSprite, overlay = null;
        private Boolean GameOver = false;
        private Difficulty d;
        private double score;
        private ArrayList notifications;
        private bool[] notificationsActive = null;
        private Label l = null;

        public Feiv()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            w = Width;
            h = Height;
            Program.w = w;
            Program.h = h;
            MainMenu.buildMainMenu();
        }
        
        private void grid_MouseHover(object sender, EventArgs e){} //EMPTY

        public void toMainMenu(object sender, EventArgs e)
        {
            MainMenu.startup = true;
            MainMenu.sp.Stop();
            MainMenu.buildMainMenu();
            MainMenu.sp.Open(new Uri("Resources\\AudioFiles\\Theme.wav", UriKind.Relative));
            MainMenu.sp.Play();
            GameOver = false;
            startedGame = false;
        }

        public bool startGame()
        {
                try {
                score = 0;
                notifications = new ArrayList();
                d = MainMenu.o.getDifficulty();
                Controls.Clear();
                grid = new PictureBox();
                Controls.Add(grid);
                Controls.Add(grid2);
                grid.Click += grid_Click;
                grid2.Click += grid_Click;
                tileW = w / 40;
                tileH = (int)(h / 23);
                Program.tileW = tileW;
                Program.tileH = tileH;
                grid.Width = w + tileW * 2;
                grid.Height = h + tileH * 2;
                grid.Left = -tileW;
                grid.Top = -tileH;
                mapW = 40;
                mapH = 24;
                px = mapW / 2;
                py = mapH / 2;
                cameraX = 0;
                cameraY = 0;
                tGrid = new Tilegrid(tileW, tileH, mapW, mapH, onScreenX, onScreenY, cameraX, cameraY);
                grid.Image = tGrid.getBox();
                buildings = new ArrayList();
                movingCam = false;
                grid2.Width = w + tileW * 2;
                grid2.Height = h + tileH * 2;
                grid2.Left = -tileW;
                grid2.Top = -tileH;
                grid2.Visible = false;
                atGrid2 = false;
                gui = new GUI();
                ArrayList a = gui.getButtons();
                foreach (Control c in a)
                {
                    Controls.Add(c);
                    c.BringToFront();
                }
                pSprite = new PictureBox();
                pSprite.Image = ld39.Properties.Resources.Sprite;
                pSprite.Left = (px - cameraX) * tileW;
                pSprite.Top = (py - cameraY) * tileH;
                pSprite.Width = tileW;
                pSprite.Height = tileH;
                pSprite.SizeMode = PictureBoxSizeMode.Zoom;
                pSprite.Parent = grid;
                pSprite.BackColor = Color.Transparent;
                startedGame = true;
                mineBuildings = new ArrayList();
                prossBuildings = new ArrayList();
                Program.wood = 10;
                Program.oil = 0;
                Program.coal = 0;
                Program.stone = 150;
                Program.gas = 0;
                Program.energyBalance = 75;
                exponent = 1;
                ticksC = 0;
                score = 0;
                batteryLim = 250;
                direction = 0;
                if (overlay != null)
                {
                    overlay.Parent = null;
                }
                if(l != null)
                {
                    l.Parent = null;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                return false;
            }
            return true;
        }

        public void updateEnergy()
        {
            switch (d)
            {
                case (Difficulty.SANDBOX):
                    Program.dEnergy -= 1;
                    Program.energyBalance -= 1;
                    score += 5;
                    break;
                case (Difficulty.LINEAR):
                    ticksC++;
                    if(ticksC>=25)
                    {
                        exponent += 2;
                        ticksC = 0;
                    }
                    Program.dEnergy -= (int)exponent;
                    Program.energyBalance -= (int)exponent;
                    score += 5 * (int)exponent;
                    break;
                case (Difficulty.EXPONENTIAL):
                    exponent *= 1.0175;
                    ticksC++;
                    Program.dEnergy -= (int)exponent + (ticksC/20);
                    Program.energyBalance -= (int)exponent + (ticksC / 20);
                    score += ((int)exponent + (ticksC / 20)) * 5;
                    break;

            }
        }

        public void update()
        {
            if (!GameOver && startedGame)
            {
                if(updateNeeded && !updatingScreen)
                {
                    updatingScreen = true;
                    updateNeeded = false;
                    tGrid.createNewImage(cameraX, cameraY);
                }
                if(updatingScreen && !tGrid.getMakingImg())
                {
                    updatingScreen = false;
                    if(atGrid2)
                    {
                        grid2.Image = tGrid.getBox();
                    }
                    else
                    {
                        grid.Image = tGrid.getBox();
                    }
                }
                int x = (MousePosition.X-this.Left) / tileW;
                int y = (MousePosition.Y-this.Top) / tileH;
                int realX = x + cameraX;
                int realY = y + cameraY;

                if (realX < 0 || realY < 0 || realX >= mapW  || realY >= mapH)
                {
                    gui.update(0);
                }
                else
                {
                    ResourceGet r = tGrid.getResource(realX, realY);
                    gui.update(r.getAmount());
                }
                
                stopwatch.Stop();
                delta = stopwatch.ElapsedMilliseconds * 60 / 1000.0;
                stopwatch.Reset();
                stopwatch.Start();

                if (updateTick >= 180)
                {
                    updateTick = 0;
                    Program.clearDelta();
                    updateEnergy();
                    ArrayList d = new ArrayList();
                    ArrayList dm = new ArrayList();
                    ArrayList dp = new ArrayList();
                    foreach (Building b in mineBuildings)
                    {
                        b.update(tGrid.getResource(b));
                        if (b.getDead())
                        {
                            d.Add(b);
                            dm.Add(b);
                        }
                    }
                    foreach (Building b in prossBuildings)
                    {
                        b.update(tGrid.getResource(b));
                        if (b.getDead())
                        {
                            d.Add(b);
                            dp.Add(b);
                        }
                    }
                    if (d.Count > 0)
                    {
                        foreach (Building b in d)
                        {
                            buildings.Remove(b);
                            b.getPictureBox().Parent = null;
                            ResourceGet res = tGrid.getResource(b);
                            if (res.getResources().Equals(Resource.WOOD) || res.getResources().Equals(Resource.OIL))
                            {
                                res.setResource(Resource.NONE);
                            }
                            else if (res.getResources().Equals(Resource.STONE) || res.getResources().Equals(Resource.GAS) || res.getResources().Equals(Resource.COAL))
                            {
                                res.setResource(Resource.NONES);
                            }
                        }
                        updateNeeded = true;
                    }
                    if (dm.Count > 0)
                    {
                        foreach (Building b in dm)
                        {
                            mineBuildings.Remove(b);
                            b.getPictureBox().Parent = null;
                            ResourceGet res = tGrid.getResource(b);
                            if (res.getResources().Equals(Resource.WOOD) || res.getResources().Equals(Resource.OIL))
                            {
                                res.setResource(Resource.NONE);
                            }
                            else if (res.getResources().Equals(Resource.STONE) || res.getResources().Equals(Resource.GAS) || res.getResources().Equals(Resource.COAL))
                            {
                                res.setResource(Resource.NONES);
                            }
                        }
                        updateNeeded = true;
                    }
                    if (dp.Count > 0)
                    {
                        foreach (Building b in dp)
                        {
                            prossBuildings.Remove(b);
                            b.getPictureBox().Parent = null;
                            ResourceGet res = tGrid.getResource(b);
                            if (res.getResources().Equals(Resource.WOOD) || res.getResources().Equals(Resource.OIL))
                            {
                                res.setResource(Resource.NONE);
                            }
                            else if (res.getResources().Equals(Resource.STONE) || res.getResources().Equals(Resource.GAS) || res.getResources().Equals(Resource.COAL))
                            {
                                res.setResource(Resource.NONES);
                            }
                        }
                        updateNeeded = true;
                    }
                    foreach (Control c in notifications)
                    {
                        Controls.Remove(c);
                    }
                    notifications = new ArrayList();
                    checkNotifications();
                    foreach(Control c in notifications)
                    {
                        Controls.Add(c);
                        c.BringToFront();
                    }

                }
                else
                {
                    updateTick += delta;
                }
                if (movingCam)
                {
                    camProg += delta;
                    int camspeed = 30;
                    if (camProg >= camspeed)
                    {
                        camProg = 0;
                        int ml = 0;
                        int mu = 0;
                        if (camXS > 0)
                        {
                            cameraX++;
                            ml -= tileW;
                        }
                        else if (camXS < 0)
                        {
                            cameraX--;
                            ml += tileW;
                        }
                        else if (camYS > 0)
                        {
                            cameraY++;
                            mu -= tileH;
                        }
                        else
                        {
                            cameraY--;
                            mu += tileH;
                        }
                        if (!settedimg)
                        {
                            while (tGrid.getMakingImg())
                            {
                                Thread.Sleep(0);
                            }
                            if (atGrid2)
                            {
                                grid.Image = tGrid.getBox();
                                grid.Width = w + tileW * 2;
                                grid.Height = h + tileH * 2;
                                grid.Left = -tileW;
                                grid.Top = -tileH;
                            }
                            else
                            {
                                grid2.Image = tGrid.getBox();
                                grid2.Width = w + tileW * 2;
                                grid2.Height = h + tileH * 2;
                                grid2.Left = -tileW;
                                grid2.Top = -tileH;
                            }

                        }
                        if (atGrid2)
                        {
                            grid.Visible = true;
                            grid2.Visible = false;
                            pSprite.Parent = grid;
                        }
                        else
                        {
                            grid2.Visible = true;
                            grid.Visible = false;
                            pSprite.Parent = grid2;
                        }
                        pSprite.Left = (px - cameraX) * tileW;
                        pSprite.Top = (py - cameraY) * tileH;
                        foreach (Building b in buildings)
                        {
                            if (atGrid2)
                            {
                                b.getPictureBox().Parent = grid;
                                b.getPictureBox().Left = (b.getX() - cameraX) * tileW;
                                b.getPictureBox().Top = (b.getY() - cameraY) * tileH;
                            }
                            else
                            {
                                b.getPictureBox().Parent = grid2;
                                b.getPictureBox().Left = (b.getX() - cameraX) * tileW;
                                b.getPictureBox().Top = (b.getY() - cameraY) * tileH;
                            }
                        }
                        atGrid2 = !atGrid2;
                        movingCam = false;
                    }
                    else
                    {
                        if (!settedimg && !tGrid.getMakingImg())
                        {
                            if (atGrid2)
                            {
                                grid.Image = tGrid.getBox();
                                grid.Width = w + tileW * 2;
                                grid.Height = h + tileH * 2;
                                grid.Left = -tileW;
                                grid.Top = -tileH;
                                Button restart = new Button { Text = "Restart", Top = h / 2 + 125, Left = w / 2 - 100, Width = 200, Height = 75, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid2, TextAlign = ContentAlignment.MiddleCenter };
                                restart.Click += toMainMenu;
                            }
                            else
                            {
                                grid2.Image = tGrid.getBox();
                                grid2.Width = w + tileW * 2;
                                grid2.Height = h + tileH * 2;
                                grid2.Left = -tileW;
                                grid2.Top = -tileH;
                                Button restart = new Button { Text = "Restart", Top = h / 2 + 125, Left = w / 2 - 100, Width = 200, Height = 75, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid, TextAlign = ContentAlignment.MiddleCenter };
                                restart.Click += toMainMenu;
                            }
                        }
                        if (camXS != 0)
                        {
                            if (atGrid2)
                            {
                                grid2.Left = (int)(-tileW + (-camProg * camXS * tileW) / camspeed);
                            }
                            else
                            {
                                grid.Left = (int)(-tileW + (-camProg * camXS * tileW) / camspeed);
                            }
                        }
                        else
                        {
                            if (atGrid2)
                            {
                                grid2.Top = (int)(-tileH + (-camProg * camYS * tileH) / camspeed);
                            }
                            else
                            {
                                grid.Top = (int)(-tileH + (-camProg * camYS * tileH) / camspeed);
                            }
                        }
                    }
                }
                else if (moving)
                {
                    camProg += delta;
                    int speed = 30;
                    if (camProg > speed)
                    {
                        camProg = 0;
                        moving = false;
                        pSprite.Left = (px - cameraX) * tileW;
                        pSprite.Top = (py - cameraY) * tileH;
                        if(px < 11 && py > 19 && !movedButtons)
                        {
                            gui.moveButtons();
                            movedButtons = true;
                        }
                        else if((px >= 11 || py <= 19) && movedButtons)
                        {
                            gui.moveButtons();
                            movedButtons = false;
                        }
                        if(px > 32 && py > 21 && !movedPanel)
                        {
                            gui.movePanel();
                            movedPanel = true;
                        }
                        else if((px <= 32 || py <= 21) && movedPanel)
                        {
                            gui.movePanel();
                            movedPanel = false;
                        }
                    }
                    else
                    {
                        if (pxs != 0)
                        {
                            pSprite.Left = (px - cameraX) * tileW + (int)((camProg-speed) * pxs * tileW / speed);
                        }
                        else
                        {
                            pSprite.Top = (py - cameraY) * tileH + (int)((camProg - speed) * pys * tileH / speed);
                        }
                    }
                }
                
                if (Program.energyBalance < 0)
                {
                    MainMenu.hs.commit(score,d);
                    FileHandler.WriteToBinaryFile<HighScores>("files\\highscores.file",MainMenu.hs);

                    if (atGrid2)
                    {
                        grid.Visible = true;
                        grid.Image = tGrid.getBox();
                        overlay = new PictureBox();
                        overlay.Width = w + tileW;
                        overlay.Height = h + tileH;
                        overlay.Parent = grid;
                        overlay.Image = Properties.Resources.GameOver;
                        overlay.Top = 0;
                        overlay.Left = 0;

                        foreach (Building b in buildings)
                        {
                            b.getPictureBox().Parent = grid;
                        }

                        grid.BringToFront();

                        overlay.SizeMode = PictureBoxSizeMode.StretchImage;
                        overlay.BackColor = Color.Transparent;
                        l = new Label { Text = score.ToString(), Top = h / 2 - 25, Left = w / 2 - 100, Width = 200, Height = 50, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid, TextAlign = ContentAlignment.MiddleCenter };
                        Button restart = new Button { Text = "Restart", Top = h / 2 + 125, Left = w / 2 - 100, Width = 200, Height = 75, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid, TextAlign = ContentAlignment.MiddleCenter };
                        restart.Click += toMainMenu;
                        restart.BringToFront();
                        l.BringToFront();
                    }
                    else
                    {
                        grid2.Visible = true;
                        grid2.Image = tGrid.getBox();
                        overlay = new PictureBox();
                        overlay.Width = w + tileW;
                        overlay.Height = h + tileH;
                        overlay.Parent = grid2;
                        overlay.Image = Properties.Resources.GameOver;
                        overlay.Top = 0;
                        overlay.Left = 0;

                        /*
                        foreach (Building b in buildings)
                        {
                            b.getPictureBox().Parent = grid2;
                        }
                        */

                        grid2.BringToFront();

                        overlay.SizeMode = PictureBoxSizeMode.StretchImage;
                        overlay.BackColor = Color.Transparent;
                        l = new Label { Text = score.ToString(),Top = h / 2 - 25, Left = w / 2 - 100, Width = 200, Height = 50, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid2, TextAlign = ContentAlignment.MiddleCenter};
                        Button restart = new Button { Text = "Restart", Top = h / 2 + 125, Left = w / 2 - 100, Width = 200, Height = 75, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid2, TextAlign = ContentAlignment.MiddleCenter };
                        restart.Click += toMainMenu;
                        restart.BringToFront();
                        l.BringToFront();
                    }
                   
                    //Controls.Add(l);
                    //l.BringToFront();
         
                    GameOver = true;

                }
                else if(Program.energyBalance > batteryLim)
                {
                    Program.energyBalance = batteryLim;
                }
            }
        }

        private void grid_Click(object sender, EventArgs e)
        {
            int x = (MousePosition.X-this.Left) / tileW;
            int y = (MousePosition.Y-this.Top) / tileH;
            int realX = x + 1 + cameraX;
            int realY = y + 1 + cameraY;
            if(realX + 1 == px && realY == py || realX - 1 == px && realY == py || realX == px && realY == py + 1 || realX == px && realY == py - 1 || realX-1 == px && realY-1 == py || realX-1 == px && realY+1 == py || realX+1 == px && realY-1 == py || realX+1 == px && realY+1 == py)
            {

            }
            else
            {
                return;
            }
            bool bb = false;
            foreach(Building b in buildings)
            {
                if(b.getX() + cameraX == x+1 && b.getY() + cameraY == y+1)
                {
                    bb = true;
                    break;
                }
            }
            if(!bb)
            {
                Building nb = null;
                x++;
                y++;
                bool pross = false;
                switch (gui.selected)
                {
                    case ("WoodMill"):
                        if(!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.WOOD))
                        {
                            break;
                        }
                        nb = new WoodMill(x, y, tGrid.getResource(x-1 + cameraX, y-1 + cameraY));
                        pross = false;
                        break;
                    case ("WoodFurnace"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.NONE) && !tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            break;
                        }
                        nb = new WoodFurnace(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = true;
                        break;
                    case ("OilProcessingFacility"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.NONE) && !tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            break;
                        }
                        nb = new OilProcessingFacility(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = true;
                        break;
                    case ("OilPump"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.OIL))
                        {
                            break;
                        }
                        nb = new OilPump(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = false;
                        break;
                    case ("StoneMine"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            break;
                        }
                        nb = new StoneMine(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = false;
                        break;
                    case ("CoalFurnace"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.NONE) && !tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            break;
                        }
                        nb = new CoalFurnace(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = true;
                        break;
                    case ("CoalMine"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.COAL))
                        {
                            break;
                        }
                        nb = new CoalMine(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = false;
                        break;
                    case ("EarthScanner"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.NONE) && !tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE) || buildedScanner)
                        {
                            break;
                        }
                        nb = new EarthScanner(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = false;
                        break;
                    case ("GasPump"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.GAS))
                        {
                            break;
                        }
                        nb = new GasWell(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = false;
                        break;
                    case ("GasTurbine"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.NONE) && !tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            break;
                        }
                        nb = new GasTurbine(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = true;
                        break;
                    case ("AdvancedMine"):
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            break;
                        }
                        nb = new AdvancedMine(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        pross = false;
                        break;
                    case ("FusionReactor"):
                        nb = new FusionReactor(x, y, tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY));
                        if (nb.getCost() > Program.stone)
                        {
                            MainMenu.playSFX(new Uri("Resources\\AudioFiles\\NoStone.wav", UriKind.Relative));
                            return;
                        }
                        if (!tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.NONE) && !tGrid.getResource(x - 1 + cameraX, y - 1 + cameraY).getResources().Equals(Resource.STONE))
                        {
                            return;
                        }
                        Program.stone -= nb.getCost();
                        GameOver = true;

                        score = 1000000 - score;
                        MainMenu.hs.commit(score, d);

                        if (atGrid2)
                        {
                            grid.Visible = true;
                            grid.Image = tGrid.getBox();
                            overlay = new PictureBox();
                            overlay.Width = w + tileW;
                            overlay.Height = h + tileH;
                            overlay.Parent = grid;
                            overlay.Image = Properties.Resources.GameWon;
                            overlay.Top = 0;
                            overlay.Left = 0;

                            foreach (Building b in buildings)
                            {
                                b.getPictureBox().Parent = grid;
                            }

                            grid.BringToFront();

                            overlay.SizeMode = PictureBoxSizeMode.StretchImage;
                            overlay.BackColor = Color.Transparent;
                            overlay.Parent = grid2;
                            l = new Label { Text = score.ToString(), Top = h / 2 - 25, Left = w / 2 - 100, Width = 200, Height = 50, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid, TextAlign = ContentAlignment.MiddleCenter };
                            Button restart = new Button { Text = "Restart", Top = h / 2 + 125, Left = w / 2 - 100, Width = 200, Height = 75, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid, TextAlign = ContentAlignment.MiddleCenter };
                            restart.Click += toMainMenu;
                            l.BringToFront();
                        }
                        else
                        {
                            grid2.Visible = true;
                            grid2.Image = tGrid.getBox();
                            overlay = new PictureBox();
                            overlay.Width = w + tileW;
                            overlay.Height = h + tileH;
                            overlay.Parent = grid2;
                            overlay.Image = Properties.Resources.GameWon;
                            overlay.Top = 0;
                            overlay.Left = 0;

                            foreach (Building b in buildings)
                            {
                                b.getPictureBox().Parent = grid2;
                            }

                            grid2.BringToFront();

                            overlay.SizeMode = PictureBoxSizeMode.StretchImage;
                            overlay.BackColor = Color.Transparent;
                            overlay.Parent = grid2;
                            l = new Label { Text = score.ToString(), Top = h / 2 - 25, Left = w / 2 - 100, Width = 200, Height = 50, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid2, TextAlign = ContentAlignment.MiddleCenter };
                            Button restart = new Button { Text = "Restart", Top = h / 2 + 125, Left = w / 2 - 100, Width = 200, Height = 75, Font = new Font("Arial", 24, FontStyle.Bold), ForeColor = Colour.LIGHTTEXT, BackColor = Colour.NEUTRALTHEME, Parent = grid2, TextAlign = ContentAlignment.MiddleCenter };
                            restart.Click += toMainMenu;
                            restart.BringToFront();
                            l.BringToFront();

                        }
                        break;
                }
                if(nb == null)
                {
                    return;
                }
                if(nb.getCost() > Program.stone)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\NoStone.wav", UriKind.Relative));
                    return;
                }
                Program.stone -= nb.getCost();
                if(gui.selected.Equals("EarthScanner"))
                {
                    buildedScanner = true;
                    tGrid.createGas();
                    updateNeeded = true;

                }
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Building.wav", UriKind.Relative));
                buildings.Add(nb);
                if(pross)
                {
                    prossBuildings.Add(nb);
                }
                else
                {
                    mineBuildings.Add(nb);
                }
                if (atGrid2)
                {
                    nb.getPictureBox().Parent = grid2;
                }
                else
                {
                    nb.getPictureBox().Parent = grid;
                }
                nb.setX(nb.getX() + cameraX);
                nb.setY(nb.getY() + cameraY);
            }
        }

        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (!movingCam && !moving && startedGame)
            {
                //Cheatsy magic
                keyData = MainMenu.o.magic(keyData);

                switch (keyData)
                {
                    case (Keys.Escape):
                        Close();
                        break;
                        /*
                    case (Keys.Up):
                        if (cameraY > 0)
                        {
                            camXS = 0;
                            camYS = -1;
                            tGrid.createNewImage(cameraX, cameraY - 1);
                            movingCam = true;
                            settedimg = false;
                        }
                        break;
                    case (Keys.Left):
                        if (cameraX > 0)
                        {
                            camXS = -1;
                            camYS = 0;
                            tGrid.createNewImage(cameraX - 1, cameraY);
                            movingCam = true;
                            settedimg = false;
                        }
                        break;
                    case (Keys.Down):
                        if (cameraY + onScreenY < mapH - 1)
                        {
                            camXS = 0;
                            camYS = 1;
                            tGrid.createNewImage(cameraX, cameraY + 1);
                            movingCam = true;
                            settedimg = false;
                        }
                        break;
                    case (Keys.Right):
                        if (cameraX + onScreenX < mapW)
                        {
                            camXS = 1;
                            camYS = 0;
                            tGrid.createNewImage(cameraX + 1, cameraY);
                            movingCam = true;
                            settedimg = false;
                        }
                        break;
                        */
                    case (Keys.W):
                        switch (0 - direction)
                        {
                            case (-3):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case (-2):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case (-1):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                        pSprite.Invalidate();
                        direction = 0;
                        if (py <= 1)
                        {
                            return true;
                        }
                        foreach (Building b in buildings)
                        {
                            if (b.getX() == px && b.getY() == py - 1)
                            {
                                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Thump.wav", UriKind.Relative));
                                return true;
                            }
                        }
                        moving = true;
                        pxs = 0;
                        pys = -1;
                        py--;
                        walkenergytimer++;
                        if(walkenergytimer >= 2)
                        {
                            Program.energyBalance--;
                            walkenergytimer = 0;
                        }
                        break;
                    case (Keys.A):
                        switch (3 - direction)
                        {
                            case (1):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case (2):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case (3):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                        }
                        pSprite.Invalidate();
                        direction = 3;
                        if (px <= 1)
                        {
                            return true;
                        }
                        foreach (Building b in buildings)
                        {
                            if (b.getX() == px - 1 && b.getY() == py)
                            {
                                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Thump.wav", UriKind.Relative));
                                return true;
                            }
                        }
                        moving = true;
                        pxs = -1;
                        pys = 0;
                        px--;
                        walkenergytimer++;
                        if (walkenergytimer >= 2)
                        {
                            Program.energyBalance--;
                            walkenergytimer = 0;
                        }
                        break;
                    case (Keys.S):
                        switch (2 - direction)
                        {
                            case (-1):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                            case (1):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                            case (2):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                        }
                        pSprite.Invalidate();
                        direction = 2;
                        if (py >= mapH-1)
                        {
                            return true;
                        }
                        foreach (Building b in buildings)
                        {
                            if (b.getX() == px && b.getY() == py + 1)
                            {
                                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Thump.wav", UriKind.Relative));
                                return true;
                            }
                        }
                        moving = true;
                        pxs = 0;
                        pys = 1;
                        py++;
                        walkenergytimer++;
                        if (walkenergytimer >= 2)
                        {
                            Program.energyBalance--;
                            walkenergytimer = 0;
                        }
                        break;
                    case (Keys.D):
                        switch (1 - direction)
                        {
                            case (-2):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                break;
                            case (-1):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                break;
                            case (1):
                                pSprite.Image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                break;
                        }
                        pSprite.Invalidate();
                        direction = 1;
                        if (px >= mapW)
                        {
                            return true;
                        }
                        foreach (Building b in buildings)
                        {
                            if (b.getX() == px + 1 && b.getY() == py)
                            {
                                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Thump.wav", UriKind.Relative));
                                return true;
                            }
                        }
                        moving = true;
                        pxs = 1;
                        pys = 0;
                        px++;
                        walkenergytimer++;
                        if (walkenergytimer >= 2)
                        {
                            Program.energyBalance--;
                            walkenergytimer = 0;
                        }
                        break;
                    case (Keys.D1):
                        gui.selected = "WoodMill";
                        break;
                    case (Keys.D2):
                        gui.selected = "WoodFurnace";
                        break;
                    case (Keys.D3):
                        gui.selected = "CoalMine";
                        break;
                    case (Keys.D4):
                        gui.selected = "CoalFurnace";
                        break;
                    case (Keys.D5):
                        gui.selected = "OilPump";
                        break;
                    case (Keys.D6):
                        gui.selected = "OilProcessingFacility";
                        break;
                    case (Keys.D7):
                        gui.selected = "StoneMine";
                        break;
                    case (Keys.D8):
                        gui.selected = "EarthScanner";
                        break;
                    case (Keys.D9):
                        gui.selected = "GasPump";
                        break;
                    case (Keys.D0):
                        gui.selected = "GasTurbine";
                        break;
                    case (Keys.O):
                        gui.selected = "AdvancedMine";
                        break;
                    case (Keys.P):
                        gui.selected = "FusionReactor";
                        break;
                }
            }
            if(MainMenu.hotKey)
            {
                if(keyData.Equals(Keys.Q) || keyData.Equals(Keys.F) || keyData.Equals(Keys.E) || keyData.Equals(Keys.R) || keyData.Equals(Keys.T) || keyData.Equals(Keys.Y) || keyData.Equals(Keys.U) || keyData.Equals(Keys.I) || keyData.Equals(Keys.O) || keyData.Equals(Keys.P) || keyData.Equals(Keys.G) || keyData.Equals(Keys.H) || keyData.Equals(Keys.J) || keyData.Equals(Keys.K) || keyData.Equals(Keys.L) || keyData.Equals(Keys.Z) || keyData.Equals(Keys.X) || keyData.Equals(Keys.C) || keyData.Equals(Keys.V) || keyData.Equals(Keys.B) || keyData.Equals(Keys.N) || keyData.Equals(Keys.M) || keyData.Equals(Keys.D1) || keyData.Equals(Keys.D2) || keyData.Equals(Keys.D3) || keyData.Equals(Keys.D4) || keyData.Equals(Keys.D5) || keyData.Equals(Keys.D6) || keyData.Equals(Keys.D7) || keyData.Equals(Keys.D8) || keyData.Equals(Keys.D9) || keyData.Equals(Keys.D0))
                {
                    MainMenu.o.setHotkey(keyData);
                }

            }
            return true;
        }

        public ArrayList getBuildings()
        {
            return buildings;
        }

        public bool ToCall(ArrayList bts)
        {
            try
            {
                Controls.Clear();
                foreach (Control c in bts)
                {
                    Controls.Add(c);
                    c.BringToFront();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
            return true;
        }
        
        private void checkNotifications()
        {
            if(notificationsActive == null)
            {
                notificationsActive = new bool[8];
                for(int i=0;i<8;i++)
                {
                    notificationsActive[i] = false;
                }
            }


            int c = 0;
            if(Program.energyBalance + (Program.dEnergy * 5) < 0)
            {
                notifications.Add(new Label { Text = "You are running out of power!", Top = 40 + c * 30, Left = 10, BackColor = Colour.WARNING, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[0] == false)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\RedAlert.wav", UriKind.Relative));
                }
                notificationsActive[0] = true;
                c++;
            }
            else
            {
                notificationsActive[0] = false;
            }
            if(Program.energyBalance >= 250)
            {
                notifications.Add(new Label { Text = "You are capped on energy!", Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                c++;
            }
            if(Program.stone < 20)
            {
                notifications.Add(new Label { Text = "Low on stone!", Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[2] == false)
                {
                    //MainMenu.playSFX(new Uri("Resources\\AudioFiles\\BlackAlert.wav", UriKind.Relative));
                }
                notificationsActive[2] = true;
                c++;
            }
            else
            {
                notificationsActive[2] = false;
            }
            if (Program.coal + Program.dCoal < 0)
            {
                notifications.Add(new Label { Text = "Coal shortage!" , Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[3] == false)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\BlackAlert.wav", UriKind.Relative));
                }
                notificationsActive[3] = true;
                c++;
            }
            else
            {
                notificationsActive[3] = false;
            }
            if (Program.oil + Program.dOil < 0)
            {
                notifications.Add(new Label { Text = "Oil shortage!", Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[4] == false)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\BlackAlert.wav", UriKind.Relative));
                }
                notificationsActive[4] = true;
                c++;
            }
            else
            {
                notificationsActive[4] = false;
            }
            if (Program.wood + Program.dWood < 0)
            {
                notifications.Add(new Label { Text = "Wood shortage!", Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[5] == false)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\BlackAlert.wav", UriKind.Relative));
                }
                notificationsActive[5] = true;
                c++;
            }
            else
            {
                notificationsActive[5] = false;
            }
            if (Program.gas + Program.dGas < 0)
            {
                notifications.Add(new Label { Text = "Gas shortage!", Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[6] == false)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\BlackAlert.wav", UriKind.Relative));
                }
                notificationsActive[6] = true;
                c++;
            }
            else
            {
                notificationsActive[6] = false;
            }
            if (Program.energyBalance < 25)
            {
                notifications.Add(new Label { Text = "Low Energy!", Top = 40 + c * 30, Left = 10, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Width = 160, Height = 20, TextAlign = ContentAlignment.MiddleCenter });
                if (notificationsActive[7] == false)
                {
                    MainMenu.playSFX(new Uri("Resources\\AudioFiles\\BlackAlert.wav", UriKind.Relative));
                }
                notificationsActive[7] = true;
                c++;
            }
            else
            {
                notificationsActive[7] = false;
            }
        }

    }

}
