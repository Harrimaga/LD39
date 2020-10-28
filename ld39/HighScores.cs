using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ld39.Options;

namespace ld39
{
    [Serializable]
    public class HighScores
    {

        double[] ezScores;
        double[] lineScores;
        double[] realScores;

        public HighScores()
        {
            ezScores = new double[10];
            for(int i = 0; i<10;i++)
            {
                ezScores[i] = 0;
            }
            lineScores = new double[10];
            for (int i = 0; i < 10; i++)
            {
                lineScores[i] = 0;
            }
            realScores = new double[10];
            for (int i = 0; i < 10; i++)
            {
                realScores[i] = 0;
            }
        }

        public void commit(double score, Difficulty d)
        {
            switch (d)
            {
                case (Difficulty.SANDBOX):
                    for (int i = 0; i < 10; i++)
                    {
                        if (score > ezScores[i])
                        {
                            for (int j = 9; j > i; j--)
                            {
                                ezScores[j] = ezScores[j - 1];
                            }
                            ezScores[i] = score;
                            break;
                        }
                    }
                    break;
                case (Difficulty.LINEAR):
                    for (int i = 0; i < 10; i++)
                    {
                        if (score > lineScores[i])
                        {
                            for (int j = 9; j > i; j--)
                            {
                                lineScores[j] = lineScores[j - 1];
                            }
                            lineScores[i] = score;
                            break;
                        }
                    }
                    break;
                case (Difficulty.EXPONENTIAL):
                    for (int i = 0; i < 10; i++)
                    {
                        if (score > realScores[i])
                        {
                            for (int j = 9; j > i; j--)
                            {
                                realScores[j] = realScores[j - 1];
                            }
                            realScores[i] = score;
                            break;
                        }
                    }
                    break;
            }

            FileHandler.WriteToBinaryFile<HighScores>("files\\highscores.file", MainMenu.hs);
            
        }

        public double[] getList(Difficulty d)
        {
            switch (d)
            {
                case (Difficulty.SANDBOX):
                    return ezScores;
                case (Difficulty.LINEAR):
                    return lineScores;
                case (Difficulty.EXPONENTIAL):
                    return realScores;
            }
            return ezScores;

        }

    }
}
