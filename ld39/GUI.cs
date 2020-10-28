using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;

namespace ld39
{
    class GUI
    {

        private int width = Program.w;
        private int height = Program.h;
        public String selected;
        private Label amount;

        public ArrayList Buttons = new ArrayList();
        public ArrayList textBoxes = new ArrayList();

        public GUI()
        {
            Button WoodMill = new Button();
            WoodMill.Height = 64;
            WoodMill.Width = 64;
            WoodMill.Top = height - 150;
            WoodMill.Left = 10;
            WoodMill.BackgroundImage = Properties.Resources.WoodMill;
            WoodMill.BackgroundImageLayout = ImageLayout.Zoom;
            WoodMill.Click += (s, e) =>
            {
                selected = "WoodMill";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));

            };

            Button WoodFurnace = new Button();
            WoodFurnace.Height = 64;
            WoodFurnace.Width = 64;
            WoodFurnace.Top = height - 76;
            WoodFurnace.Left = 10;
            WoodFurnace.BackgroundImage = Properties.Resources.GrassWoodFurnace;
            WoodFurnace.BackgroundImageLayout = ImageLayout.Zoom;
            WoodFurnace.Click += (s, e) =>
            {
                selected = "WoodFurnace";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button CoalMine = new Button();
            CoalMine.Height = 64;
            CoalMine.Width = 64;
            CoalMine.Top = height - 150;
            CoalMine.Left = 84;
            CoalMine.BackgroundImage = Properties.Resources.CoalMine;
            CoalMine.BackgroundImageLayout = ImageLayout.Zoom;
            CoalMine.Click += (s, e) =>
            {
                selected = "CoalMine";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button CoalFurnace = new Button();
            CoalFurnace.Height = 64;
            CoalFurnace.Width = 64;
            CoalFurnace.Top = height - 76;
            CoalFurnace.Left = 84;
            CoalFurnace.BackgroundImage = Properties.Resources.GrassCoalFurnace;
            CoalFurnace.BackgroundImageLayout = ImageLayout.Zoom;
            CoalFurnace.Click += (s, e) =>
            {
                selected = "CoalFurnace";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button OilPump = new Button();
            OilPump.Height = 64;
            OilPump.Width = 64;
            OilPump.Top = height - 150;
            OilPump.Left = 158;
            OilPump.BackgroundImage = Properties.Resources.OilWell;
            OilPump.BackgroundImageLayout = ImageLayout.Zoom;
            OilPump.Click += (s, e) =>
            {
                selected = "OilPump";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button OilProcessingFacility = new Button();
            OilProcessingFacility.Height = 64;
            OilProcessingFacility.Width = 64;
            OilProcessingFacility.Top = height - 76;
            OilProcessingFacility.Left = 158;
            OilProcessingFacility.BackgroundImage = Properties.Resources.GrassOilProcessingFacility;
            OilProcessingFacility.BackgroundImageLayout = ImageLayout.Zoom;
            OilProcessingFacility.Click += (s, e) =>
            {
                selected = "OilProcessingFacility";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };


            Button StoneMine = new Button();
            StoneMine.Height = 64;
            StoneMine.Width = 64;
            StoneMine.Top = height - 150;
            StoneMine.Left = 232;
            StoneMine.BackgroundImage = Properties.Resources.StoneMine;
            StoneMine.BackgroundImageLayout = ImageLayout.Zoom;
            StoneMine.Click += (s, e) =>
            {
                selected = "StoneMine";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button EarthScanner = new Button();
            EarthScanner.Height = 64;
            EarthScanner.Width = 64;
            EarthScanner.Top = height - 76;
            EarthScanner.Left = 232;
            EarthScanner.BackgroundImage = Properties.Resources.GrassEarthScanner;
            EarthScanner.BackgroundImageLayout = ImageLayout.Zoom;
            EarthScanner.Click += (s, e) =>
            {
                selected = "EarthScanner";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button GasPump = new Button();
            GasPump.Height = 64;
            GasPump.Width = 64;
            GasPump.Top = height - 150;
            GasPump.Left = 306;
            GasPump.BackgroundImage = Properties.Resources.GasPump;
            GasPump.BackgroundImageLayout = ImageLayout.Zoom;
            GasPump.Click += (s, e) =>
            {
                selected = "GasPump";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button GasTurbine = new Button();
            GasTurbine.Height = 64;
            GasTurbine.Width = 64;
            GasTurbine.Top = height - 76;
            GasTurbine.Left = 306;
            GasTurbine.BackgroundImage = Properties.Resources.GrassGasTurbine;
            GasTurbine.BackgroundImageLayout = ImageLayout.Zoom;
            GasTurbine.Click += (s, e) =>
            {
                selected = "GasTurbine";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button AdvancedMine = new Button();
            AdvancedMine.Height = 64;
            AdvancedMine.Width = 64;
            AdvancedMine.Top = height - 150;
            AdvancedMine.Left = 380;
            AdvancedMine.BackgroundImage = Properties.Resources.AdvancedStoneMine;
            AdvancedMine.BackgroundImageLayout = ImageLayout.Zoom;
            AdvancedMine.Click += (s, e) =>
            {
                selected = "AdvancedMine";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Button FusionReactor = new Button();
            FusionReactor.Height = 64;
            FusionReactor.Width = 64;
            FusionReactor.Top = height - 76;
            FusionReactor.Left = 380;
            FusionReactor.BackgroundImage = Properties.Resources.GrassFusionReactor;
            FusionReactor.BackgroundImageLayout = ImageLayout.Zoom;
            FusionReactor.Click += (s, e) =>
            {
                selected = "FusionReactor";
                MainMenu.playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));
            };

            Panel p = new Panel();
            p.Left = width - 300;
            p.Top = height - 50;
            p.Width = 300;
            p.Height = 50;
            p.BackColor = Colour.PANEL;
            p.BorderStyle = BorderStyle.FixedSingle;
            Buttons.Add(p);

            Label coalAm = new Label();
            coalAm.Height = 20;
            coalAm.Width = 90;
            coalAm.Text = "Coal: " + Program.coal;
            coalAm.Top = 5;
            coalAm.Left = 5;
            coalAm.BackColor = Color.Transparent;
            coalAm.Parent = p;
            textBoxes.Add(coalAm);

            Label stoneAm = new Label();
            stoneAm.Height = 20;
            stoneAm.Width = 95;
            stoneAm.Text = "Stone: " + Program.stone;
            stoneAm.Top = 5;
            stoneAm.Left = 100;
            stoneAm.BackColor = Color.Transparent;
            stoneAm.Parent = p;
            stoneAm.BringToFront();
            textBoxes.Add(stoneAm);

            Label energyAm = new Label();
            energyAm.Height = 20;
            energyAm.Width = 105;
            energyAm.Text = "Energy: " + Program.energyBalance;
            energyAm.Top = 5;
            energyAm.Left = 195;
            energyAm.BackColor = Color.Transparent;
            energyAm.Parent = p;
            energyAm.BringToFront();
            textBoxes.Add(energyAm);

            Label woodAm = new Label();
            woodAm.Height = 20;
            woodAm.Width = 90;
            woodAm.Text = "Wood: " + Program.wood;
            woodAm.Top = 30;
            woodAm.Left = 5;
            woodAm.BackColor = Color.Transparent;
            woodAm.Parent = p;
            woodAm.BringToFront();
            textBoxes.Add(woodAm);

            Label oilAm = new Label();
            oilAm.Height = 20;
            oilAm.Width = 90;
            oilAm.Text = "Oil: " + Program.wood;
            oilAm.Top = 30;
            oilAm.Left = 100;
            oilAm.BackColor = Color.Transparent;
            oilAm.Parent = p;
            oilAm.BringToFront();
            textBoxes.Add(oilAm);

            Label gasAm = new Label();
            gasAm.Height = 20;
            gasAm.Width = 90;
            gasAm.Text = "Gas: " + Program.gas;
            gasAm.Top = 30;
            gasAm.Left = 195;
            gasAm.BackColor = Color.Transparent;
            gasAm.Parent = p;
            gasAm.BringToFront();
            textBoxes.Add(gasAm);

            // \/\/ Tooltips down here \/\/

            ToolTip woodmillTt = new ToolTip();
            woodmillTt.OwnerDraw = true;
            woodmillTt.BackColor = Colour.NEUTRALTHEME;
            woodmillTt.ForeColor = Colour.LIGHTTEXT;
            woodmillTt.AutoPopDelay = 30000;
            woodmillTt.SetToolTip(WoodMill, "Sawmill: \r\n Cost: " + Balance.woodMillC + " Stone \r\n Effects: \r\n +" +Balance.woodMillY+ " Wood \r\n "+Balance.woodMillU+" Energy \r\n Can be placed on \r\n -Forest");
            woodmillTt.Draw += (object s, DrawToolTipEventArgs e) => 
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip CoalMineTt = new ToolTip();
            CoalMineTt.OwnerDraw = true;
            CoalMineTt.BackColor = Colour.NEUTRALTHEME;
            CoalMineTt.ForeColor = Colour.LIGHTTEXT;
            CoalMineTt.AutoPopDelay = 30000;
            CoalMineTt.SetToolTip(CoalMine, "Coal Mine: \r\n Cost: "+Balance.coalMineC+" Stone \r\n Effects: \r\n +"+Balance.coalMineY+" Coal \r\n "+Balance.coalMineU+" Energy \r\n Can be placed on \r\n -Coal");
            CoalMineTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip CoalFurnaceTt = new ToolTip();
            CoalFurnaceTt.OwnerDraw = true;
            CoalFurnaceTt.BackColor = Colour.NEUTRALTHEME;
            CoalFurnaceTt.ForeColor = Colour.LIGHTTEXT;
            CoalFurnaceTt.AutoPopDelay = 30000;
            CoalFurnaceTt.SetToolTip(CoalFurnace, "Coal Furnace: \r\n Cost: "+Balance.coalFurnaceC+" Stone \r\n Effects: \r\n "+Balance.coalFurnaceE+" Energy p/Coal \r\n (Max "+Balance.coalFurnaceB+" Coal) \r\n Can be placed on \r\n -Grass, Stone");
            CoalFurnaceTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip OilProcessingFacilityTt = new ToolTip();
            OilProcessingFacilityTt.OwnerDraw = true;
            OilProcessingFacilityTt.BackColor = Colour.NEUTRALTHEME;
            OilProcessingFacilityTt.ForeColor = Colour.LIGHTTEXT;
            OilProcessingFacilityTt.AutoPopDelay = 30000;
            OilProcessingFacilityTt.SetToolTip(OilProcessingFacility, "Oil Processing Facility: \r\n Cost: " + Balance.oilProcessingFacilityC + " Stone \r\n Effects: \r\n +" + Balance.oilProcessingFacilityE + " Energy p/Oil \r\n (Max " + Balance.oilProcessingFacilityB +" Oil) \r\n (Passively drains "+Balance.oilProcessingFacilityU+" energy) \r\n Can be placed on \r\n -Grass, Stone");
            OilProcessingFacilityTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip OilPumpTt = new ToolTip();
            OilPumpTt.OwnerDraw = true;
            OilPumpTt.BackColor = Colour.NEUTRALTHEME;
            OilPumpTt.ForeColor = Colour.LIGHTTEXT;
            OilPumpTt.AutoPopDelay = 30000;
            OilPumpTt.SetToolTip(OilPump, "Pumpjack: \r\n Cost: "+Balance.oilPumpC+" Stone \r\n Effects: \r\n +"+Balance.oilPumpY+" Oil \r\n "+Balance.oilPumpU+" Energy \r\n Can be placed on \r\n -Oil");
            OilPumpTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip StoneMineTt = new ToolTip();
            StoneMineTt.OwnerDraw = true;
            StoneMineTt.BackColor = Colour.NEUTRALTHEME;
            StoneMineTt.ForeColor = Colour.LIGHTTEXT;
            StoneMineTt.AutoPopDelay = 30000;
            StoneMineTt.SetToolTip(StoneMine, "Stone Mine: \r\n Cost: "+Balance.stoneMineC+" Stone \r\n Effects: \r\n +"+Balance.stoneMineY+" Stone \r\n "+Balance.stoneMineU+" Energy \r\n Can be placed on \r\n -Stone");
            StoneMineTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip WoodFurnaceTt = new ToolTip();
            WoodFurnaceTt.OwnerDraw = true;
            WoodFurnaceTt.BackColor = Colour.NEUTRALTHEME;
            WoodFurnaceTt.ForeColor = Colour.LIGHTTEXT;
            WoodFurnaceTt.AutoPopDelay = 30000;
            WoodFurnaceTt.SetToolTip(WoodFurnace, "Wood Furnace: \r\n Cost: "+Balance.woodFurnaceC+" Stone \r\n Effects: \r\n +"+Balance.woodFurnaceE+" Energy p/Wood \r\n (Max "+Balance.woodFurnaceB+" Wood) \r\n Can be placed on \r\n -Grass, Stone");
            WoodFurnaceTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip EarthScannerTt = new ToolTip();
            EarthScannerTt.OwnerDraw = true;
            EarthScannerTt.BackColor = Colour.NEUTRALTHEME;
            EarthScannerTt.ForeColor = Colour.LIGHTTEXT;
            EarthScannerTt.AutoPopDelay = 30000;
            EarthScannerTt.SetToolTip(EarthScanner, "Earth Scanner: \r\n Cost: "+Balance.earthScannerC+" \r\n Scans the earth for gas \r\n Effects: \r\n "+Balance.earthScannerU+" Energy \r\n(Reveals Gas) \r\n (Can be built once) \r\n Can be placed on \r\n -Grass, Stone");
            EarthScannerTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip GasPumpTt = new ToolTip();
            GasPumpTt.OwnerDraw = true;
            GasPumpTt.BackColor = Colour.NEUTRALTHEME;
            GasPumpTt.ForeColor = Colour.LIGHTTEXT;
            GasPumpTt.AutoPopDelay = 30000;
            GasPumpTt.SetToolTip(GasPump, "Gas Well: \r\n Cost: "+Balance.gasWellC+" \r\n Effects: \r\n +"+Balance.gasWellY+" Gas \r\n "+Balance.gasWellU+" Energy \r\n Can be placed on \r\n -Gas");
            GasPumpTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip GasTurbineTt = new ToolTip();
            GasTurbineTt.OwnerDraw = true;
            GasTurbineTt.BackColor = Colour.NEUTRALTHEME;
            GasTurbineTt.ForeColor = Colour.LIGHTTEXT;
            GasTurbineTt.AutoPopDelay = 30000;
            GasTurbineTt.SetToolTip(GasTurbine, "Gas Turbine: \r\n Cost: "+Balance.gasTurbineC+" \r\n Effects: \r\n +"+Balance.gasTurbinE+" Energy p/Gas \r\n (Max "+Balance.gasTurbineB+" Gas) \r\n Can be placed on \r\n -Grass, Stone");
            GasTurbineTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip AdvancedMineTt = new ToolTip();
            AdvancedMineTt.OwnerDraw = true;
            AdvancedMineTt.BackColor = Colour.NEUTRALTHEME;
            AdvancedMineTt.ForeColor = Colour.LIGHTTEXT;
            AdvancedMineTt.AutoPopDelay = 30000;
            AdvancedMineTt.SetToolTip(AdvancedMine, "Advanced Mine: \r\n Cost: " + Balance.advanceMineC + " \r\n Effects: \r\n +" + Balance.advanceMineY +" Stone \r\n "+Balance.advanceMineU+" Energy \r\n Can be placed on \r\n -Stone");
            AdvancedMineTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };

            ToolTip FusionReactorTt = new ToolTip();
            FusionReactorTt.OwnerDraw = true;
            FusionReactorTt.BackColor = Colour.NEUTRALTHEME;
            FusionReactorTt.ForeColor = Colour.LIGHTTEXT;
            FusionReactorTt.AutoPopDelay = 30000;
            FusionReactorTt.SetToolTip(FusionReactor, "Fusion Reactor: \r\n Cost: "+Balance.fusionReactorC+" \r\n Effects: \r\n Win the game \r\n Can be placed on \r\n -Grass, Stone");
            FusionReactorTt.Draw += (object s, DrawToolTipEventArgs e) =>
            {
                e.DrawBackground();
                e.DrawBorder();
                e.DrawText(TextFormatFlags.HorizontalCenter);
            };


            //Amount Display
            amount = new Label { Top = 10, Left = 10, Text = "NaN",  BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, TextAlign = ContentAlignment.MiddleCenter};
            Buttons.Add(amount);

            Buttons.Add(WoodMill);
            Buttons.Add(WoodFurnace);
            Buttons.Add(CoalMine);
            Buttons.Add(CoalFurnace);
            Buttons.Add(OilPump);
            Buttons.Add(OilProcessingFacility);
            Buttons.Add(StoneMine);
            Buttons.Add(EarthScanner);
            Buttons.Add(GasPump);
            Buttons.Add(GasTurbine);
            Buttons.Add(AdvancedMine);
            Buttons.Add(FusionReactor);
        }

        public void update(int amount)
        {
            Label t = (Label)(textBoxes[0]);
            t.Text = "Coal: " + Program.coal + " (" + Program.dCoal + ")" ;
            if(Program.dCoal > 0)
            {
                t.ForeColor = Colour.POSITIVE;
            }
            else if(Program.dCoal < 0)
            {
                t.ForeColor = Colour.WARNING;
            }
            else
            {
                t.ForeColor = Colour.NEUTRALTHEME;
            }
            t = (Label)(textBoxes[1]);
            t.Text = "Stone: " + Program.stone + " (" + Program.dStone + ")";
            if (Program.dStone > 0)
            {
                t.ForeColor = Colour.POSITIVE;
            }
            else if (Program.dStone < 0)
            {
                t.ForeColor = Colour.WARNING;
            }
            else
            {
                t.ForeColor = Colour.NEUTRALTHEME;
            }
            t = (Label)(textBoxes[2]);
            t.Text = "Energy: " + Program.energyBalance + " (" + Program.dEnergy + ")";
            if (Program.dEnergy > 0)
            {
                t.ForeColor = Colour.POSITIVE;
            }
            else if (Program.dEnergy < 0)
            {
                t.ForeColor = Colour.WARNING;
            }
            else
            {
                t.ForeColor = Colour.NEUTRALTHEME;
            }
            t = (Label)(textBoxes[3]);
            t.Text = "Wood: " + Program.wood + " (" + Program.dWood + ")";
            if (Program.dWood > 0)
            {
                t.ForeColor = Colour.POSITIVE;
            }
            else if (Program.dWood < 0)
            {
                t.ForeColor = Colour.WARNING;
            }
            else
            {
                t.ForeColor = Colour.NEUTRALTHEME;
            }
            t = (Label)(textBoxes[4]);
            t.Text = "Oil: " + Program.oil + " (" + Program.dOil + ")";
            if (Program.dOil > 0)
            {
                t.ForeColor = Colour.POSITIVE;
            }
            else if (Program.dOil < 0)
            {
                t.ForeColor = Colour.WARNING;
            }
            else
            {
                t.ForeColor = Colour.NEUTRALTHEME;
            }
            t = (Label)(textBoxes[5]);
            t.Text = "Gas: " + Program.gas + " (" + Program.dGas + ")";
            if (Program.dGas > 0)
            {
                t.ForeColor = Colour.POSITIVE;
            }
            else if (Program.dGas < 0)
            {
                t.ForeColor = Colour.WARNING;
            }
            else
            {
                t.ForeColor = Colour.NEUTRALTHEME;
            }
            this.amount.Text = amount.ToString();
        }

        public ArrayList getButtons()
        {
            return Buttons;
        }

        public void moveButtons()
        {
            foreach(Control c in Buttons)
            {
                if(c.GetType() == typeof(Button))
                {
                    c.Left = width - c.Left - 64;
                    c.Top = height - c.Top - 64;
                    c.BringToFront();
                }
            }
        }

        public void movePanel()
        {
            foreach(Control c in Buttons)
            {
                if(c.GetType() == typeof(Panel))
                {
                    c.Top = height - c.Top - 50;
                }
            }
        }

    }
}
