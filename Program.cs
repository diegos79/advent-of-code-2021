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
            //Sum digits contained in a matrix
            public static int SumMatrixDigits (int [,] mat, int row, int col)
            {
                int sum = 0;
                for (int i = 0; i < row; i++)
                    for (int j = 0; j < col; j++)
                        sum += mat[i, j];
                    return sum;
            }
            /* ------------------------------------------------------------- */
            //Split single matrix to square matrix of row/col 
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
            //print a list
            public static void PrintList (List<int> list)
            {
                foreach (var item in list)
                {
                    Console.Write(" " + item);
                }
            }
            /* ------------------------------------------------------------- */

            // search matrix for containing number 
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
            // get row of an item found 
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

            /* ------------------------------------------------------------- */
            // get col of an item found 
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

            /* ------------------------------------------------------------- */
            //for every item found, decrease the last row/col
            public static void CountItemFoundInMatrix (int[,] matrix, int row, int col)
                
            {
                matrix[row, 5]--;
                matrix[5, col]--;
            }

            /* ------------------------------------------------------------- */
            //Verify if last row or col is -6 for declaring winner matrix
            public static bool MatrixIsWinner (int[,] matrix)
            {
                bool winner = false;
                for (int r = 0; r < matrix.GetLength(0); r++)
                {
                    if (matrix[r, 5] == -6)
                    {
                        winner = true; break;
                    }
                    for (int c = 0; c < matrix.GetLength(1); c++)
                    {
                        if (matrix[5, c] == -6)
                        {
                            winner = true; break;
                        }
                    }
                }
                return winner;
            }
            /* ------------------------------------------------------------- */

        }


        static void Main(string[] args)
        {

            /* -------------------SETUP --------------------------  */

            string matrixstring = File.ReadAllText(@"c:\input3.txt"); 
                //read all the file and convert in string
            string numbersextracted = File.ReadAllText(@"c:\input-numbers3.txt"); 
                //read all the numbers extracted
            int[,] res = AocD4.AcquireMatrix(matrixstring, 5);
                //convert string in one maxi matrix 
            List<int[,]> listOfMatrix = AocD4.SplitSquareMatrix(res, 5);
                //split maxi matrix in list of matrices by 5 row and col
            List<int> listOfNumbers = AocD4.GetNumbersInList(numbersextracted);
                //convert string of extracted numbers in list

            /* -----------------------------------------------------  */
            //adding -1 to all matrix splitted to track extracted num on row and col
            for (int i = 0; i < listOfMatrix.Count; i++)
            {
                listOfMatrix[i] = AocD4.AddRowToMatrix(listOfMatrix[i], 1, -1);
                listOfMatrix[i] = AocD4.AddColToMatrix(listOfMatrix[i], 1, -1);
            }

            /* -------------------END SETUP ------------------------  */


            List<int> extractedNums = new List<int>(); 
                //containing all the extracted numbers
            List<int> extractedInWinnerMatrix = new List<int>(); 
                //containing extracted numbers in the winner matrix
            Console.WriteLine("---------- ADVENT OF CODE ------------ ");
            Console.WriteLine("---------- DAY 4 // PART 1 ------------ ");
            Console.WriteLine("Press a key when ready to process input data");
            Console.ReadKey();
            int j;
            int countExtracted = 0;
            foreach (var item in listOfNumbers) //loop over all numbers extracted
            {
                countExtracted++;
                for (j = 0; j < listOfMatrix.Count; j++) //loop over all matrix
                {
                    Console.WriteLine($"\nChecking number... {item} in matrix nr. {j} \n");
                    if (AocD4.MatrixContainsNumber(listOfMatrix[j], item)) //check matrix for item
                    {
                    Console.WriteLine($"\n------------------------------------------\n" +
                        $"Found number {item} in matrix number {j} !" +
                        $"\n------------------------------------------\n");
                    AocD4.CountItemFoundInMatrix(listOfMatrix[j],
                        AocD4.GetRowOfItem(listOfMatrix[j], item),
                            AocD4.GetColOfItem(listOfMatrix[j], item));
                    extractedNums.Add(item); //all the extracted numbers 
                        
                       

                        if (AocD4.MatrixIsWinner(listOfMatrix[j])) //check if matrix is winner
                        {
                            foreach (var num in listOfMatrix[j]) //loop for check the winner matrix for extracted numbers
                            {
                                if (extractedNums.Contains(num))
                                {
                                    extractedInWinnerMatrix.Add(num);
                                }
                            }
                            Console.WriteLine($"\n---------------------------\nWe have a winner matrix!\n---------------------------\n");
                            AocD4.PrintMatrix(listOfMatrix[j]);
                            Console.WriteLine($"\nWinner number: {item}, extracted after {countExtracted} times");
                            Console.WriteLine($"Sum of extracted numbers: {extractedNums.Sum()}");
                            Console.WriteLine($"Sum of extracted numbers in winner matrix: {extractedInWinnerMatrix.Sum()}");
                            Console.WriteLine($"Sum of all numbers in winner matrix: {AocD4.SumMatrixDigits(listOfMatrix[j],5,5)}");
                            Console.WriteLine($"Sum of all unmarked extracted: {AocD4.SumMatrixDigits(listOfMatrix[j], 5, 5)- extractedInWinnerMatrix.Sum()}");
                            Console.WriteLine($"Final result is (Unmarked numbers * Last extracted): {(AocD4.SumMatrixDigits(listOfMatrix[j], 5, 5) - extractedInWinnerMatrix.Sum())*item}");
                            Console.ReadKey();
                            return;
                        }
                    }
                } 
            }
        }
    }
}



