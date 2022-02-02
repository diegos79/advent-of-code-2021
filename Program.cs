using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace aocd4
{
    internal class Program
    {
        static class AocD4
        {
            /* ------------------------------------------------------------- */
            //Get matrix from text file and convert it in 2D Array
            public static int[,] AcquireMatrix(string s, int lenght) 
            {
                string[] arrayStrings = s.Split('\n');
                for (int a = 0; a < arrayStrings.Length; a++) 
                    //riduce spazi duplici in singoli spazi
                {
                    arrayStrings[a] = arrayStrings[a].Replace("  ", " ");
                }
                int[,] result = new int[arrayStrings.Length, lenght];


                int i = 0, j;
                foreach (var row in arrayStrings)
                {
                    j = 0;
                    foreach (var col in row.Trim().Split(' '))
                    {
                        if (col == "") break;
                        result[i, j] = int.Parse(col.Trim('\r'));
                        j++;
                    }
                    i++;
                }
                return result;
            }

            /* ------------------------------------------------------------- */

            //Print a matrix 
            public static void PrintMatrix(int[,] mat)
            {
                Console.WriteLine("\n");
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        Console.Write(" " + mat[i, j]);
                    }
                    Console.WriteLine("");
                }
            }
            /* ------------------------------------------------------------- */

            //Sum digits of a matrix
            public static int SumMatrixDigits (int [,] mat)
            {
                int sum = 0;
                for (int i = 0; i < mat.GetLength(0); i++)
                    for (int j = 0; j < mat.GetLength(1); j++)
                        sum += mat[i, j];
                    return sum;
            }
            /* ------------------------------------------------------------- */

            //split great matrix to square matrix of row 
            public static List<int[,]> SplitSquareMatrix (int [,] mat, int row) 


            {
                
                List<int[,]> result = new List<int[,]>();
                int tempcont = 5;
                int a = 0;
                int[,] tempmatrix = new int[row, row];
                for (int i = 0; i < mat.GetLength(0); i++)
                {
                    if (i == tempcont)
                    {
                        result.Add((int[,])tempmatrix.Clone()); //per evitare il reference problem, è necessario copiare la matrice
                        a = 0; 
                        continue;
                    }
                    for (int j = 0; j < mat.GetLength(1); j++)
                    {
                        tempmatrix[a, j] = mat[i,j];
                    } 
                    a++;
                    if (i > tempcont) tempcont += 6; 

                }
                return result;
            }
            
            /* ------------------------------------------------------------- */
            //add number of row to matrix (last row) with value to fill
            public static int[,] AddRowToMatrix (int[,] matrix, int rowToAdd, int valueToFill) 
            {
                int row = matrix.GetLength(0)+rowToAdd;
                int[,] result = new int[row, matrix.GetLength(1)];
                /* First copy the original matrix to preserve it */
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        result[r, c] = matrix[r, c];
                    }
                }

                /*Insert Values into Main Matrix
                --------------------------------------------------------------------------------*/
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        result[row-1, c] = valueToFill;
                    }
                return result;
            }
           
            /* ------------------------------------------------------------- */
            //add number of col to matrix (last col) with value to fill
            public static int[,] AddColToMatrix(int[,] matrix, int colToAdd, int valueToFill)
            {
                int col = matrix.GetLength(1) + colToAdd;
                int[,] result = new int[matrix.GetLength(0), col];
                /* First copy the original matrix to preserve it*/
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        result[r, c] = matrix[r, c];
                    }
                }

                /* Then Insert Values into Main Matrix
                --------------------------------------------------------------------------------*/
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    result[r, col-1] = valueToFill;
                }
                return result;
            }
            /* ------------------------------------------------------------- */


            /* ------------------------------------------------------------- */
            //convert string of numbers with comma -> in list
            public static List<int> GetNumbersInList (string s)
            {
                List<int> list = new List<int>();
                var a = s.Split(',').ToList();
                for (int i = 0; i < a.Count; i++)
                {
                    list.Add(int.Parse(a[i].ToString()));
                }
                return list;
            }
            /* ------------------------------------------------------------- */

            /* ------------------------------------------------------------- */
            //print a list
            public static void PrintList (List<int> list)
            {
                foreach (var item in list)
                {
                    Console.Write(" " + item);
                }
            }
            /* ------------------------------------------------------------- */

            // search number (by list of numbers) in matrix and return true/false
            public static bool MatrixContainsNumber (int[,] matrix, int number)
            {
                bool res = false;
                for (int r = 0; r < matrix.GetLength(0); r++) 
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[r, c] == number)
                        {
                            res = true; 
                            break;
                        }
                    }
                    if (res == true) break;
                } 
                return res;
            } 
            /* ------------------------------------------------------------- */

            public static int GetRowOfItem (int[,] matrix, int item)
            {
                int row=0;
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[r, c] == item)
                        {
                            row = r;break;
                        }
                    }
                } 
                return row;
            }

            public static int GetColOfItem(int[,] matrix, int item)
            {
                int col = 0;
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[r, c] == item)
                        {
                            col = c; break;
                        }
                    }
                }
                return col;
            }

            public static void CountItemFoundInMatrix (int[,] matrix, int row, int col)
                //for item found decrement the last row/col
            {
                matrix[row, 5]--;
                matrix[5, col]--;
            }

            public static bool MatrixIsWinner (int[,] matrix)
            {
                bool winner = false;
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    if (matrix[r, 5] == -5)
                    {
                        winner = true; break;
                    }
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[5, c] == -5)
                        {
                            winner = true; break;
                        }
                    }
                }
                return winner;
            }
                //Verify if last row or col is -5 for declaring winner matrix

        }


        static void Main(string[] args)
        {

            /* -------------------SETUP --------------------------  */

            string matrixstring = File.ReadAllText(@"c:\input.txt"); 
                //read all the file and convert in string
            string numbersextracted = File.ReadAllText(@"c:\input-numbers.txt"); 
                //read all the numbers extracted
            int[,] res = AocD4.AcquireMatrix(matrixstring, 5);
                //convert string in one maxi matrix 
            List<int[,]> listOfMatrix = AocD4.SplitSquareMatrix(res, 5);
                //split maxi matrix in list of matrices by 5 row and col
            List<int> listOfNumbers = AocD4.GetNumbersInList(numbersextracted);
            //convert string of extracted numbers in list


            /* -----------------------------------------------------  */
            //adding -1 to all matrix splitted
            for (int i = 0; i < listOfMatrix.Count; i++)
            {
                listOfMatrix[i] = AocD4.AddRowToMatrix(listOfMatrix[i], 1, -1);
                listOfMatrix[i] = AocD4.AddColToMatrix(listOfMatrix[i], 1, -1);
            }
            /* -----------------------------------------------------  */

            /* -------------------END SETUP ------------------------  */


                var winner = new int[6,6];
            for (int i = 0; i < listOfMatrix.Count; i++)
            {
            while (!AocD4.MatrixIsWinner(listOfMatrix[i]))
                {
                    foreach (var item in listOfNumbers)
                    {
                        if (AocD4.MatrixContainsNumber(listOfMatrix[i], item))
                        {
                            AocD4.CountItemFoundInMatrix(listOfMatrix[i],
                                AocD4.GetColOfItem(listOfMatrix[i], item),
                                AocD4.GetRowOfItem(listOfMatrix[i], item));
                        }
                    }
                }
                winner = listOfMatrix[i];
            }
            Console.Write($"Winner matrix is: {AocD4.MatrixIsWinner(winner)}");


            //stampa di tutte le matrici per verificare le liste di matrici
            //foreach (var item in lista)
            //{
            //    AocD4.PrintMatrix(item);
            //}
            /*     -------------------     START TEST AREA -------------------------   
            //    AocD4.GetRowOfItem(lista[0], listOfNumbers[0]),
            //    AocD4.GetColOfItem(lista[0], listOfNumbers[0]));
            //AocD4.MatrixIsWinner(lista[0]);
            //AocD4.PrintMatrix(lista[0]);
            //bool contain = AocD4.MatrixContainsNumber(lista[0], 22);
            //Console.WriteLine(contain);
            //Console.WriteLine(numbersextracted);
            AocD4.PrintList(listOfNumbers);
            //var List = new List<int[,]>(); //si possono creare liste di matrici
            //AocD4.SumMatrixDigits(res);
            //Console.WriteLine(matrixstring);
            
            AocD4.PrintMatrix(res);
            Console.WriteLine("\n\n");
            int[,] res2 = AocD4.AddRowToMatrix(res, 1, -1);
            AocD4.PrintMatrix(res2);
            Console.WriteLine("\n\n");
            int[,] res3 = AocD4.AddColToMatrix(res2, 1, -1);
            AocD4.PrintMatrix(res3);
            List<int[,]> lista = AocD4.SplitSquareMatrix(res, 5);
            AocD4.PrintMatrix(lista[0]);

            /*     -------------------     END TEST AREA -------------------------   */




            Console.ReadKey();

        }

    }
    
}



