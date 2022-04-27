using System;
using System.Linq;

namespace SudokuSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] puzzle = {{5,0,0,0,0,2,0,0,0},
                     {0,0,0,0,0,7,0,0,4},
                     {0,0,6,5,4,0,3,0,0},
                     {7,0,0,8,9,0,0,0,3},
                     {0,1,0,0,0,0,6,0,0},
                     {0,0,0,0,0,4,0,0,0},
                     {8,0,0,3,5,0,0,0,9},
                     {0,0,7,0,0,0,0,8,0},
                     {0,0,0,2,0,0,0,0,0}};

            PrintSudoku(puzzle);

            Console.WriteLine("Press 'enter' to solve.");
            Console.ReadLine();

            SolveSudoku(puzzle);

        }

        private static void SolveSudoku(int[,] puzzle)
        {
            //copy input-sudoku to solution-sudoku
            int[,] solution = new int[9, 9];
            Array.Copy(puzzle, 0, solution, 0, puzzle.Length);

            //loop through each cell
            for (int column = 0; column < 9; column++)
            {
                for (int row = 0; row < 9;)
                {
                    if (puzzle[column, row] == 0) //if cell != 0, dont change it.
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            //Increment cell with 1
                            solution[column, row]++;

                            //if cell = 10, it is a fail. Reset to 0 and go back to previous cell that has value 0
                            if (solution[column, row] == 10)
                            {
                                solution[column, row] = 0;

                                do
                                {
                                    if (row == 0)
                                    {
                                        row += 8;
                                        column -= 1;
                                    }
                                    else
                                    {
                                        row -= 1;
                                    }
                                } while (puzzle[column, row] != 0);

                                break;
                            }
                            //Else if number does not collide with any other row/column/square, continue with next cell
                            else if (ValidateNumber(solution, column, row))
                            {
                                row++;
                                break;
                            }
                            else //If cell != 10 and number validation failed, continue with the next number.
                            {
                                continue;
                            }
                        }
                    }
                    // original cell != 0, continue with next cell.
                    else
                    {
                        row++;
                    }

                }

            }

            PrintSudoku(solution);
            Console.ReadLine();

        }

        private static bool ValidateNumber(int[,] arr, int row, int column)
        {
            //Validate both 3x3 square and row/column
            if (ValidateSquare(arr, row, column) && ValidateDimension(arr, row, column))
            {
                return true;
            }
            return false;

        }

        private static bool ValidateDimension(int[,] arr, int row, int column)
        {
            //Check if the current cell match any other cell in the same row, if so return false
            for (int a = 0; a < 9; a++)
            {
                if (column == a) //don't compare the cell with itself
                {
                    continue;
                }
                if (arr[row, column] == arr[row, a])
                {
                    return false;
                }
            }

            //Check if the current cell match any other cell in the same column, if so return false
            for (int b = 0; b < 9; b++)
            {
                if (row == b)
                {
                    continue;
                }
                if (arr[row, column] == arr[b, column]) //don't compare the cell with itself
                {
                    return false;
                }
            }

            //if no other cell with same value, return true
            return true;

        }

        private static bool ValidateSquare(int[,] arr, int row, int column)
        {
            //Compare that the current cells 3x3 grid doesn't contain the same number as current cell. 
            int rowSquare = CurrentSquare(row);
            int columnSquare = CurrentSquare(column);

            for (int i = rowSquare; i < rowSquare + 3; i++)
            {
                for (int j = columnSquare; j < columnSquare + 3; j++)
                {
                    if (arr[i, j] == arr[row, column])
                    {
                        if (i == row && j == column) //don't compare the cell with itself
                        {
                            continue;
                        }
                        return false;
                    }
                }
            }

            return true;
        }

        private static int CurrentSquare(int coordinate)
        {
            //Give the coordinate of input dimension (row/column/depth), and defines which 3x3 square of the puzzle it belongs to in the 9x9 grid. 
            int square = 0;
            switch (coordinate)
            {
                case 0:
                case 1:
                case 2:
                    square = 0; //Square1 in grid, Square1 start at index 0
                    break;

                case 3:
                case 4:
                case 5:
                    square = 3; //Square2 in grid, Square2 start at index 3
                    break;

                case 6:
                case 7:
                case 8:
                    square = 6; //Square3 in grid, Square3 start at index 6
                    break;
            }
            return square;
        }

        private static void PrintSudoku(int[,] sudoku)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (j % 3 == 0 && j != 0)
                    {
                        Console.Write("| ");
                    }
                    Console.Write(sudoku[i, j] + " ");
                }

                if (i % 3 == 2 && i != 8)
                {
                    Console.WriteLine();
                    Console.Write("------+-------+------");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}