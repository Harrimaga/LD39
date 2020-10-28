using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace ld39
{
    public static class MainMenu
    {
        public static Options o;
        public static HighScores hs;
        public static System.Windows.Media.MediaPlayer sp;
        public static System.Windows.Media.MediaPlayer sfx;
        public static bool startup = true;
        public static bool playing = false;
        public static bool hotKey = false;
        public static int changing = 0;

        public static int tutNum = 1;
        public static PictureBox image = new PictureBox { Top = 0, Left = 0, Width = Program.w, Height = Program.h, SizeMode = PictureBoxSizeMode.StretchImage };
        public static String[] i = new String[] { "TutorialThisIsYou", "TutorialThisIsYou", "TutorialThisIsYou", "TutorialThisIsYou", "TutorialLosingEnergy", "TutorialMine", "TutorialMine", "TutorialMine",
                                        "TutorialMine", "TutorialGainingStoneLosingMoreEnergy", "TutorialGainingStoneLosingMoreEnergy", "TutorialGainingStoneLosingMoreEnergy", "TutorialWoodMill", "TutorialWoodFurnace",
                                        "TutorialGainingEnergyLosingWood", "TutorialGainingEnergyLosingWood", "TutorialHowToWin", "GameWon" };

        public static void ended(object sender, EventArgs e)
        {
            sp.Position = TimeSpan.Zero;
            sp.Play();
        }
        
        public static void buildMainMenu()
        {
            if (startup)
            {
                sp = new System.Windows.Media.MediaPlayer();
                sp.Open(new Uri("Resources\\AudioFiles\\Theme.wav", UriKind.Relative));
                sp.Play();
                sp.MediaEnded += ended;
                sfx = new System.Windows.Media.MediaPlayer();
                startup = false;
            }

            PictureBox background = new PictureBox { Top = 0, Left = 0, Width = Program.w, Height = Program.h, Image = Properties.Resources.BackGround, SizeMode = PictureBoxSizeMode.StretchImage };
            

            ArrayList b = new ArrayList();

            Button start = new Button { Text = "Start a new game", Top = Program.h / 2 - 340, Left = (Program.w / 2 - 200), Width = 400, Height = 100, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Font = new Font("Arial", 24, FontStyle.Bold) };
            start.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative));  startGame(); };
            Button boptions = new Button { Text = "Options", Top = Program.h / 2 - 100, Left = (Program.w / 2 - 200), Width = 400, Height = 100, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Font = new Font("Arial", 24, FontStyle.Bold) };
            Button tutorial = new Button { Text = "Tutorial", Top = Program.h / 2 - 220, Left = (Program.w / 2 - 200), Width = 400, Height = 100, BackColor = Color.Black, ForeColor = Color.White, Font = new Font("Arial", 24, FontStyle.Bold) };
            tutorial.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); startTutorial(); };
            boptions.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); options(); };
            Button lb = new Button { Text = "Show Leaderboards", Top = Program.h / 2 + 20, Left = (Program.w / 2 - 200), Width = 400, Height = 100, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Font = new Font("Arial", 24, FontStyle.Bold) };
            lb.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); showHighscore(); };
            Button bquit = new Button { Text = "Quit?", Top = Program.h /2 + 140, Left = (Program.w / 2 - 200), Width = 400, Height = 100, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT, Font = new Font( "Arial", 24, FontStyle.Bold) };
            bquit.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); quit(); };

            b.Add(background);
            b.Add(start);
            b.Add(tutorial);
            b.Add(boptions);
            b.Add(lb);
            b.Add(bquit);

            Program.f.ToCall(b);
        }

        public static void startGame()
        {

            sp.Stop();
            sp.Open(new Uri("Resources\\AudioFiles\\GitaarLied.wav", UriKind.Relative));

            sp.Play();

            if(o==null)
            try
            {
                o = FileHandler.ReadFromBinaryFile<Options>("files\\options.file");
            }
            catch(Exception ex)
            {
                o = Options.getInstance();
            }

            o.updateColours();

            try
            {
                hs = FileHandler.ReadFromBinaryFile<HighScores>("files\\highscores.file");
            }
            catch (Exception ex)
            {
                hs = new HighScores();
            }
            Program.f.startGame();
        }

        public static void tutEnded(object sender, EventArgs e)
        {
            if (tutNum < 19)
            {
                System.Threading.Thread.Sleep(200);
                image.ImageLocation = "Resources\\" + i[tutNum - 1] + ".png";
                sfx.Open(new Uri("Resources\\AudioFiles\\Tutorial\\" + tutNum + ".wav", UriKind.Relative));
                sfx.Play();
                tutNum++;
            }

            if (tutNum == 19)
            {
                image.ImageLocation = "Resources\\GameWon.png";
                System.Threading.Thread.Sleep(2000);
                MainMenu.buildMainMenu();
                Program.f.Controls.Remove(image);
                sfx.MediaEnded -= tutEnded;
            }
        }

        public static void startTutorial()
        {

            sfx.Stop();

            ArrayList t = new ArrayList();

            image.ImageLocation = "Resources\\" + i[tutNum - 1] + ".png";
            
            t.Add(image);
            Program.f.ToCall(t);
            sfx.Open(new Uri("Resources\\AudioFiles\\Tutorial\\"+ tutNum +".wav", UriKind.Relative));
            sfx.Play();

            tutNum++;

            sfx.MediaEnded += tutEnded; 
        }

        public static void options()
        {
            ArrayList c = new ArrayList();
            PictureBox background = new PictureBox { Top = 0, Left = 0, Width = Program.w, Height = Program.h, Image = Properties.Resources.BackGround2, SizeMode = PictureBoxSizeMode.StretchImage };
            c.Add(background);
            try
            {
                o = FileHandler.ReadFromBinaryFile<Options>("files\\options.file");
            } catch (Exception e)
            {
                o = Options.getInstance();
                try
                {
                    FileHandler.WriteToBinaryFile<Options>("files\\options.file", o);
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message, "Exception occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            o.updateColours();

            //Difficulty
            Label difficulty = new Label { Top = 50, Left = Program.w / 2 - 100, Width = 200, Height = 40, Font = new Font("Arial", 16), Text = "  Set the difficulty", BackColor = Color.Transparent, Parent = background };
            Button sandBox = new Button { Text = "No Challenge", Top = 100, Left = (Program.w / 2 - 115), Width = 70, Height = 35, BackColor = Colour.POSITIVE, ForeColor = Colour.LIGHTTEXT };
            sandBox.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); o.setDifficulty(Options.Difficulty.SANDBOX); save(); };
            Button linear = new Button { Text = "Easy", Top = 100, Left = (Program.w / 2 - 35), Width = 70, Height = 35, BackColor = Colour.YELLOW, ForeColor = Colour.NEUTRALTHEME };
            linear.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); o.setDifficulty(Options.Difficulty.LINEAR); save(); };
            Button exponential = new Button { Text = "Hard", Top = 100, Left = (Program.w / 2 + 45), Width = 70, Height = 35, BackColor = Colour.WARNING, ForeColor = Colour.LIGHTTEXT};
            exponential.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); o.setDifficulty(Options.Difficulty.EXPONENTIAL); save(); };

            //Colourblind mode
            Label colourblindMode = new Label { Top = 150, Left = Program.w / 2 - 135, Width = 180, Height = 40, Font = new Font("Arial", 16), Text = "Colourblindmode :", BackColor = Color.Transparent, Parent = background };
            Button cbOn = new Button { Text = "ON", Top = 150, Left = (Program.w / 2 + 55), Width = 35, Height = 35, BackColor = Color.Blue, ForeColor = Colour.LIGHTTEXT };
            cbOn.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); o.setColourBlindMode(true); save(); };
            Button cbOff = new Button { Text = "OFF", Top = 150, Left = (Program.w / 2 + 100), Width = 35, Height = 35, BackColor = Color.Blue, ForeColor = Colour.LIGHTTEXT };
            cbOff.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); o.setColourBlindMode(false); save(); };

            //Hotkeys
            Label hotkeys = new Label { Top = 210, Left = Program.w / 2 - 90, Width = 200, Height = 40, Font = new Font("Arial", 16), Text = "Set your hotkeys:", BackColor = Color.Transparent, Parent = background };
            Button bwoodMill = new Button { Top = 260, Left = Program.w / 2 - 449, Width = 64, Height = 64, BackgroundImage = Properties.Resources.WoodMill, BackgroundImageLayout = ImageLayout.Zoom };
            bwoodMill.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 1; };
            Label woodMill = new Label { Top = 325, Left = Program.w / 2 - 449, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.woodMill.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bwoodFurnace = new Button { Top = 260, Left = Program.w / 2 - 375, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GrassWoodFurnace, BackgroundImageLayout = ImageLayout.Zoom };
            bwoodFurnace.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 2; };
            Label woodfurnace = new Label { Top = 325, Left = Program.w / 2- 375, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.woodFurnace.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bCoalMine = new Button { Top = 260, Left = Program.w / 2 - 301, Width = 64, Height = 64, BackgroundImage = Properties.Resources.CoalMine, BackgroundImageLayout = ImageLayout.Zoom };
            bCoalMine.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 3; };
            Label coalMine = new Label { Top = 325, Left = Program.w / 2 - 301, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.coalMine.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bcoalFurnace = new Button { Top = 260, Left = Program.w / 2 - 227, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GrassCoalFurnace, BackgroundImageLayout = ImageLayout.Zoom };
            bcoalFurnace.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 4; };
            Label coalfurnace = new Label { Top = 325, Left = Program.w / 2 - 227, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.coalFurnace.ToString(), BackColor = Color.Transparent, Parent = background };
            Button boilPump = new Button { Top = 260, Left = Program.w / 2 - 153, Width = 64, Height = 64, BackgroundImage = Properties.Resources.OilWell, BackgroundImageLayout = ImageLayout.Zoom };
            boilPump.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 5; };
            Label oilPump = new Label { Top = 325, Left = Program.w / 2 - 153, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.oilPump.ToString(), BackColor = Color.Transparent, Parent = background };
            Button boilProcess = new Button { Top = 260, Left = Program.w / 2 - 79, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GrassOilProcessingFacility, BackgroundImageLayout = ImageLayout.Zoom };
            boilProcess.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 6; };
            Label oilProcess = new Label { Top = 325, Left = Program.w / 2 - 79, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.oilProcessingfacility.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bstoneMine = new Button { Top = 260, Left = Program.w / 2 + 5, Width = 64, Height = 64, BackgroundImage = Properties.Resources.StoneMine, BackgroundImageLayout = ImageLayout.Zoom };
            bstoneMine.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 7; };
            Label stoneMine = new Label { Top = 325, Left = Program.w / 2 + 5, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.stoneMine.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bearth = new Button { Top = 260, Left = Program.w / 2 + 79, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GrassEarthScanner, BackgroundImageLayout = ImageLayout.Zoom };
            bearth.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 8; };
            Label earth = new Label { Top = 325, Left = Program.w / 2 + 79, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.earthScanner.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bgasWell = new Button { Top = 260, Left = Program.w / 2 + 153, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GasPump, BackgroundImageLayout = ImageLayout.Zoom };
            bgasWell.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 9; };
            Label gasWell = new Label { Top = 325, Left = Program.w / 2 + 153, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.gasWell.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bgasTurbine = new Button { Top = 260, Left = Program.w / 2 + 227, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GrassGasTurbine, BackgroundImageLayout = ImageLayout.Zoom };
            bgasTurbine.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 10; };
            Label gasTurbine = new Label { Top = 325, Left = Program.w / 2 + 227, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.gasTurbine.ToString(), BackColor = Color.Transparent, Parent = background };
            Button badvanceMine = new Button { Top = 260, Left = Program.w / 2 + 301, Width = 64, Height = 64, BackgroundImage = Properties.Resources.AdvancedStoneMine, BackgroundImageLayout = ImageLayout.Zoom };
            badvanceMine.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 11; };
            Label advanceMine = new Label { Top = 325, Left = Program.w / 2 + 301, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.advanceMine.ToString(), BackColor = Color.Transparent, Parent = background };
            Button bfusion = new Button { Top = 260, Left = Program.w / 2 + 375, Width = 64, Height = 64, BackgroundImage = Properties.Resources.GrassFusionReactor, BackgroundImageLayout = ImageLayout.Zoom };
            bfusion.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); hotKey = true; changing = 12; };
            Label fusion = new Label { Top = 325, Left = Program.w / 2 + 375, Width = 64, Height = 30, Font = new Font("Arial", 16), Text = o.fusionReactor.ToString(), BackColor = Color.Transparent, Parent = background };

            Button quit = new Button { Text = "Quit to main menu", Top = 500, Left = (Program.w / 2 - 100), Width = 200, Height = 35, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT };
            quit.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); buildMainMenu(); };
            c.Add(sandBox); c.Add(linear); c.Add(exponential);  c.Add(quit); c.Add(cbOn); c.Add(cbOff);
            c.Add(bwoodMill); c.Add(bwoodFurnace); c.Add(bCoalMine); c.Add(bcoalFurnace);
            c.Add(boilPump); c.Add(boilProcess); c.Add(bstoneMine); c.Add(bearth);
            c.Add(bgasTurbine); c.Add(bgasWell); c.Add(badvanceMine); c.Add(bfusion);
            Program.f.ToCall(c); 

        }

        private static void save()
        {
            FileHandler.WriteToBinaryFile<Options>("files\\options.file", o);
        }

        public static void showHighscore()
        {
            Button quit = new Button { Text = "Quit to main menu", Top = 100, Left = (Program.w / 2 - 100), Width = 200, Height = 35, BackColor = Colour.NEUTRALTHEME, ForeColor = Colour.LIGHTTEXT };
            quit.Click += (s, e) => { playSFX(new Uri("Resources\\AudioFiles\\Click.wav", UriKind.Relative)); buildMainMenu(); };
            ArrayList a = new ArrayList();
            PictureBox background = new PictureBox { Top = 0, Left = 0, Width = Program.w, Height = Program.h, Image = Properties.Resources.BackGround2, SizeMode = PictureBoxSizeMode.StretchImage };
            a.Add(background);
            if (hs == null)
            {
                try
                {
                    hs = FileHandler.ReadFromBinaryFile<HighScores>("files\\highscores.file");
                }
                catch (Exception ex)
                {
                    hs = new HighScores();
                }
            }

            double[] A = hs.getList(Options.Difficulty.SANDBOX);
            double[] B = hs.getList(Options.Difficulty.LINEAR);
            double[] C = hs.getList(Options.Difficulty.EXPONENTIAL);

            new Label { Text = "No Challenge", Font = new Font("Arial", 24, FontStyle.Bold), Top = 150, Left = (Program.w / 2 - 400), Width = 250, Height = 40, Parent = background, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleCenter };
            new Label { Text = "Easy", Font = new Font("Arial", 24, FontStyle.Bold), Top = 150, Left = (Program.w / 2 - 100), Width = 200, Height = 40, Parent = background, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleCenter };
            new Label { Text = "Hard", Font = new Font("Arial", 24, FontStyle.Bold), Top = 150, Left = (Program.w / 2 + 200), Width = 200, Height = 40, Parent = background, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleCenter };



            for (int i = 0; i<10;i++)
            {
                new Label { Text = A[i].ToString(), Font = new Font("Arial", 24, FontStyle.Bold), Top = 200 + i*50, Left = (Program.w / 2 - 400), Width = 200, Height = 40, Parent = background, BackColor = Color.Transparent,TextAlign = ContentAlignment.MiddleCenter };
            }
            for (int i = 0; i < 10; i++)
            {
                new Label { Text = B[i].ToString(), Font = new Font("Arial", 24, FontStyle.Bold), Top = 200 + i * 50, Left = (Program.w / 2 - 100), Width = 200, Height = 40, Parent = background, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleCenter };
            }
            for (int i = 0; i < 10; i++)
            {
                new Label { Text = C[i].ToString(), Font = new Font("Arial", 24, FontStyle.Bold), Top = 200 + i * 50, Left = (Program.w / 2 + 200), Width = 200, Height = 40, Parent = background, BackColor = Color.Transparent, TextAlign = ContentAlignment.MiddleCenter };
            }
            a.Add(quit);
            Program.f.ToCall(a);
        }
        
        public static void quit()
        {
            Program.f.Close();
        }

        public static void playSFX(Uri u)
        {
            sfx.Open(u);
            sfx.Play();
        }

    }
}
