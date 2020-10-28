using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ld39
{
    [Serializable]
    public class Options
    {
        public enum Difficulty { SANDBOX, LINEAR, EXPONENTIAL };
        private static Options instance;
        private Difficulty d; //Difficulty
        private bool cb; //ColourblindMode
        public Keys advanceMine, coalFurnace, coalMine, earthScanner, fusionReactor, gasTurbine, gasWell, oilProcessingfacility, oilPump, stoneMine, woodFurnace, woodMill;

        private Options()
        {
            d = Difficulty.LINEAR;
            cb = false;
            advanceMine = Keys.O;
            coalFurnace = Keys.D4;
            coalMine = Keys.D3;
            earthScanner = Keys.D8;
            fusionReactor = Keys.P;
            gasTurbine = Keys.D0;
            gasWell = Keys.D9;
            oilProcessingfacility = Keys.D6;
            oilPump = Keys.D5;
            stoneMine = Keys.D7;
            woodFurnace = Keys.D2;
            woodMill = Keys.D1;

        }

        public void setDifficulty(Difficulty d)
        {
            this.d = d;
        }

        public Difficulty getDifficulty()
        {
            return d;
        }

        public static Options getInstance()
        {
            if(instance==null)
            {
                instance = new Options();
            }
            return instance;
        }

        public string getDifficultyAsString()
        {
            return d.ToString();
        }

        public void setColourBlindMode(bool set)
        {
            if(set)
            {
                Colour.POSITIVE = Color.Blue;
            }
            else
            {
                Colour.POSITIVE = Color.DarkGreen;
            }
            cb = set;
        }

        public void updateColours()
        {
            if (cb)
                Colour.POSITIVE = Color.Blue;
        }

        public Keys magic(Keys kd)
        {
            if(woodMill.Equals(kd))
            {
                return Keys.D1;
            }
            else if(woodFurnace.Equals(kd))
            {
                return Keys.D2;
            }else if (coalFurnace.Equals(kd))
            {
                return Keys.D3;
            }
            else if (coalMine.Equals(kd))
            {
                return Keys.D4;
            }
            else if (oilPump.Equals(kd))
            {
                return Keys.D5;
            }
            else if (oilProcessingfacility.Equals(kd))
            {
                return Keys.D6;
            }
            else if (stoneMine.Equals(kd))
            {
                return Keys.D7;
            }
            else if (earthScanner.Equals(kd))
            {
                return Keys.D8;
            }
            else if (gasTurbine.Equals(kd))
            {
                return Keys.D9;
            }
            else if (gasWell.Equals(kd))
            {
                return Keys.D0;
            }
            else if (advanceMine.Equals(kd))
            {
                return Keys.OemMinus;
            }
            else if (fusionReactor.Equals(kd))
            {
                return Keys.P;
            }

            return kd;
        }

        public void setHotkey(Keys key)
        {
            if(advanceMine.Equals(key) || coalMine.Equals(key) || coalFurnace.Equals(key) || earthScanner.Equals(key) || fusionReactor.Equals(key) || gasTurbine.Equals(key) || gasWell.Equals(key) || oilProcessingfacility.Equals(key) || oilPump.Equals(key) || stoneMine.Equals(key) || woodFurnace.Equals(key) || woodMill.Equals(key) )
            {
                return;
            }
            switch (MainMenu.changing)
            {
                case (1):
                    woodMill = key;
                    break;
                case (2):
                    woodFurnace = key;
                    break;
                case (3):
                    coalMine = key;
                    break;
                case (4):
                    coalFurnace = key;
                    break;
                case (5):
                    oilPump = key;
                    break;
                case (6):
                    oilProcessingfacility = key;
                    break;
                case (7):
                    stoneMine = key;
                    break;
                case (8):
                    earthScanner = key;
                    break;
                case (9):
                    gasWell = key;
                    break;
                case (10):
                    gasTurbine = key;
                    break;
                case (11):
                    advanceMine = key;
                    break;
                case (12):
                    fusionReactor = key;
                    break;
            }
            MainMenu.hotKey = false;
            FileHandler.WriteToBinaryFile<Options>("files\\options.file", MainMenu.o);
            MainMenu.options();

        }

    }
}
