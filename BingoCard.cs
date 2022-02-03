using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aocd4
{
    internal class BingoCard
    {
        public BingoCard(int[,] bingoCard)
        {
            //inizializzo la scheda trasformando gli interi in BingoNumber
            Numbers = new BingoNumber[bingoCard.GetLength(0), bingoCard.GetLength(1)];

            for (int i = 0; i < bingoCard.GetLength(0); i++)
            {
                for (int j = 0; j < bingoCard.GetLength(1); j++)
                {
                    Numbers[i, j] = new BingoNumber(bingoCard[i, j]);
                }
            }
        }

        public BingoNumber[,] Numbers { get; set; }

        public bool ContainsNumber(int number)
        {
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                for (int j = 0; j < Numbers.GetLength(1); j++)
                {
                    if(Numbers[i,j].Value == number)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void SetNumberExtracted(int number)
        {
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                for (int j = 0; j < Numbers.GetLength(1); j++)
                {
                    if (Numbers[i, j].Value == number)
                    {
                        Numbers[i, j].Extracted = true;
                        return;
                    }
                }
            }
        }

        public bool VerifyIsWinnerCard()
        {
            //Verifico le righe
            //per ogni riga
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                int numEstrattiSuRiga = 0;
                //per ogni colonna
                for (int j = 0; j < Numbers.GetLength(1); j++)
                {
                    //se è stato estratto aumento il contatore
                    if (Numbers[i, j].Extracted)
                    {
                        numEstrattiSuRiga++;
                    }
                }
                //se il contatore equivale al numero delle colonne allora è vincente
                if(numEstrattiSuRiga == Numbers.GetLength(1))
                {
                    return true;
                }
            }

            //verifico le colonne
            //per ogni colonna
            for (int j = 0; j < Numbers.GetLength(1); j++)
            {
                int numEstrattiSuColonna = 0;
                //per ogni riga
                for (int i = 0; i < Numbers.GetLength(0); i++)
                {
                    //se è stato estratto aumento il contatore
                    if (Numbers[i, j].Extracted)
                    {
                        numEstrattiSuColonna++;
                    }
                }
                //se il contatore equivale al numero delle righe allora è vincente
                if (numEstrattiSuColonna == Numbers.GetLength(0))
                {
                    return true;
                }
            }

            //se non è mai uscito allora ritorno false;
            return false;
        }

        public List<int> GetAllNotExtractedNumbers()
        {
            List<int> returnValue = new List<int>();    
            //per ogni riga
            for (int i = 0; i < Numbers.GetLength(0); i++)
            {
                int numEstrattiSuRiga = 0;
                //per ogni colonna
                for (int j = 0; j < Numbers.GetLength(1); j++)
                {
                    //se non è stato estratto lo ritorno
                    if (!Numbers[i, j].Extracted)
                    {
                        returnValue.Add(Numbers[i, j].Value);
                    }
                }
            }

            return returnValue;
        }
    }
}
