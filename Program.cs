using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gunman
{
    class Gunman
    {
        public int x { get; set; }
        public int y { get; set; }
    }

    class Solution
    {
        public List<Gunman> Gunmen = new List<Gunman>();
    }

    class Program
    {
        static List<Solution> Solutions = new List<Solution>();
        static List<int[,]> MapSolutions = new List<int[,]>();
        static List<int[,]> MapCombinations = new List<int[,]>();
        static int nMaxGunMen = 0;
        static int[,] MaxMapSolution;
        static int nCombination = 0;

        static void PrintSolution(int[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                    Console.Write(" " + map[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        static void PrintSolutionWithMaxGunmen()
        {
            foreach (int[,] map in MapSolutions)
            {
                int i, j, nGunmen = 0;
                for (i = 0; i < map.GetLength(0); i++)
                {
                    for (j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == 2)
                        {
                            nGunmen++;
                        }
                    }
                }
                if (nMaxGunMen == nGunmen)
                {
                    MaxMapSolution = map;
                    PrintSolution(map);
                    nCombination++;
                }
            }
        }

        static void SaveSolution(int[,] map)
        {
            Solution thisSol = new Solution();
            int i, j;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2)
                    {
                        thisSol.Gunmen.Add(new Gunman() { x = i, y = j });
                    }
                }
            }
            Solutions.Add(thisSol);
        }

        static bool IsSolutionExist(int[,] map)
        {
            bool isExist = false;
            Solution thisSol = new Solution();
            int i, j;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2)
                    {
                        thisSol.Gunmen.Add(new Gunman() { x = i, y = j });
                    }
                }
            }
            // Find in the existing solutions
            foreach (var sol in Solutions)
            {
                int nSimilar = 0;
                foreach (var gunman in thisSol.Gunmen)
                {
                    //if(Solutions.Any(p=>p.Gunmen))
                    if (sol.Gunmen.Any(p => p.x == gunman.x && p.y == gunman.y))
                    {
                        nSimilar++;
                    }
                }
                if (nSimilar == sol.Gunmen.Count)
                {
                    isExist = true;
                    break;
                }
            }

            return isExist;
        }

        static void SaveMap(int[,] map)
        {
            MapCombinations.Add(map);
        }

        static bool IsMapExist(int[,] map)
        {
            int i, j;
            foreach (var mapComb in MapCombinations)
            {
                int count = 0, nBox = mapComb.GetLength(0) * mapComb.GetLength(1);
                for (i = 0; i < map.GetLength(0); i++)
                {
                    for (j = 0; j < map.GetLength(1); j++)
                    {
                        if (map[i, j] == mapComb[i, j])
                            count++;
                    }
                }
                if (count == nBox)
                {
                    return true;
                }
            }

            return false;
        }

        static bool IsSafe(int[,] map, int row, int col)
        {
            int i;
            bool isSafe = true;

            // Check on the left
            for (i = col - 1; i >= 0; i--)
            {
                if (map[row, i] == 2)
                {
                    isSafe = false;
                    break;
                }
                else if (map[row, i] == 1)
                {
                    isSafe = true;
                    break;
                }
            }
            if (!isSafe)
                return isSafe;

            // Check on the right
            for (i = col + 1; i < map.GetLength(0); i++)
            {
                if (map[row, i] == 2)
                {
                    isSafe = false;
                    break;
                }
                else if (map[row, i] == 1)
                {
                    isSafe = true;
                    break;
                }
            }
            if (!isSafe)
                return isSafe;

            // Check on the bottom
            for (i = row + 1; i < map.GetLength(0); i++)
            {
                if (map[i, col] == 2)
                {
                    isSafe = false;
                    break;
                }
                else if (map[i, col] == 1)
                {
                    isSafe = true;
                    break;
                }
            }
            if (!isSafe)
                return isSafe;

            // Check on the top
            for (i = row - 1; i >= 0; i--)
            {
                if (map[i, col] == 2)
                {
                    isSafe = false;
                    break;
                }
                else if (map[i, col] == 1)
                {
                    isSafe = true;
                    break;
                }
            }
            if (!isSafe)
                return isSafe;

            return isSafe;
        }

        static bool IsSolutionComplete(int[,] map)
        {
            int i, j, nGunmen = 0;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 0 && IsSafe(map, i, j))
                    {
                        return false;
                    }
                    else if (map[i, j] == 2)
                    {
                        nGunmen++;
                    }
                }
            }
            if (nMaxGunMen < nGunmen)
            {
                nMaxGunMen = nGunmen;
                MapSolutions.Add(map);
            }
            return true;
        }

        static bool SolveUntilTopLeft(int[,] map, int row, int col)
        {
            if (col == map.GetLength(0))
            {
                SolveUntilTopLeft(map, row + 1, 0);
                return false;
            }
            else if (IsSolutionComplete(map))
            {
                PrintSolution(map);
                return true;
            }

            for (int i = row; i < map.GetLength(0); i++)
            {
                if (IsSafe(map, i, col) && map[i, col] == 0)
                {
                    map[i, col] = 2;

                    SolveUntilTopLeft(map, row, col + 1);

                    map[i, col] = 0; // BACKTRACK
                }
            }

            return false;

        }

        static bool SolveUntilBottomRight(int[,] map, int row, int col)
        {
            if (col == -1)
            {
                SolveUntilBottomRight(map, row - 1, map.GetLength(1) - 1);
                return false;
            }
            else if (IsSolutionComplete(map))
            {
                PrintSolution(map);
                return true;
            }

            for (int i = map.GetLength(0) - 1; i >= 0; i--)
            {
                if (IsSafe(map, i, col) && map[i, col] == 0)
                {
                    map[i, col] = 2;

                    SolveUntilBottomRight(map, row, col - 1);

                    if (!IsSolutionComplete(map))
                    {
                        SolveUntilBottomRight(map, row, col - 1);
                    }
                    else
                    {
                        map[i, col] = 0; // BACKTRACK
                    }
                }
            }

            return false;
        }

        static bool SolveUntilTopRight(int[,] map, int row, int col)
        {
            if (col == -1)
            {
                SolveUntilTopRight(map, row + 1, map.GetLength(1) - 1);
                return false;
            }
            else if (IsSolutionComplete(map))
            {
                PrintSolution(map);
                return true;
            }

            for (int i = row; i < map.GetLength(0); i++)
            {
                for (int j = col; j < map.GetLength(1); j++)
                {
                    if (IsSafe(map, i, j) && map[i, j] == 0)
                    {
                        map[i, j] = 2;

                        SolveUntilTopRight(map, row, col - 1);

                        if (!IsSolutionComplete(map))
                        {
                            SolveUntilTopRight(map, row, col - 1);
                        }
                        else
                        {
                            map[i, j] = 0; // BACKTRACK
                        }
                    }
                }
            }

            return false;
        }

        static void SolveBottomRightToTop(int[,] map)
        {
            int row, col;
            for (col = map.GetLength(1) - 1; col >= 0; col--)
            {
                for (row = map.GetLength(0) - 1; row >= 0; row--)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveBottomRightToLeft(int[,] map)
        {
            int row, col;
            for (row = map.GetLength(1) - 1; row >= 0; row--)
            {
                for (col = map.GetLength(0) - 1; col >= 0; col--)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveTopRightToDown(int[,] map)
        {
            int row, col;
            for (col = map.GetLength(1) - 1; col >= 0; col--)
            {
                for (row = 0; row < map.GetLength(0); row++)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveTopRightToLeft(int[,] map)
        {
            int row, col;
            for (row = 0; row < map.GetLength(0); row++)
            {
                for (col = map.GetLength(1) - 1; col >= 0; col--)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveBottomLeftToTop(int[,] map)
        {
            int row, col;
            for (col = map.GetLength(1) - 1; col >= 0; col--)
            {
                for (row = map.GetLength(0) - 1; row >= 0; row--)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveBottomLeftToRight(int[,] map)
        {
            int row, col;
            for (row = map.GetLength(0) - 1; row >= 0; row--)
            {
                for (col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveTopLeftToRight(int[,] map)
        {
            int row, col;
            for (row = 0; row < map.GetLength(0); row++)
            {
                for (col = 0; col < map.GetLength(1); col++)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void SolveTopLeftToBottom(int[,] map)
        {
            int row, col;
            for (col = 0; col < map.GetLength(1); col++)
            {
                for (row = 0; row < map.GetLength(0); row++)
                {
                    if (map[row, col] == 0 && IsSafe(map, row, col))
                    {
                        // Put a gunman
                        map[row, col] = 2;
                    }
                }
            }
            if (IsSolutionComplete(map))
                Console.WriteLine("Done...");
        }

        static void FindDifferentWaysOnTheLeft(int[,] map)
        {
            int i, j, k;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2)
                    {

                        // Check left
                        bool isReplaced = false;
                        map[i, j] = 0;
                        for (k = j - 1; k >= 0; k--)
                        {
                            if (IsSafe(map, i, k) && map[i, k] == 0)
                            {
                                map[i, k] = 2;
                                isReplaced = true;
                                break;
                            }
                        }

                        if (!isReplaced)
                        {
                            map[i, j] = 2;
                        }
                        else
                        {
                            PrintSolution(map);
                            nCombination++;
                        }

                    }
                }
            }
        }

        static void FindDifferentWaysOnTheRight(int[,] map)
        {
            int i, j, k;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2)
                    {
                        // Check right
                        bool isReplaced = false;
                        map[i, j] = 0;
                        for (k = j + 1; k < map.GetLength(1); k++)
                        {
                            if (IsSafe(map, i, k) && map[i, k] == 0)
                            {
                                map[i, k] = 2;
                                isReplaced = true;
                                break;
                            }
                        }

                        if (!isReplaced)
                        {
                            map[i, j] = 2;
                        }
                        else
                        {
                            PrintSolution(map);
                            nCombination++;
                        }
                    }
                }
            }
        }

        static void FindDifferentWaysOnTheTop(int[,] map)
        {
            int i, j, k;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2)
                    {
                        // Check top
                        bool isReplaced = false;
                        map[i, j] = 0;
                        for (k = i - 1; k >= 0; k--)
                        {
                            if (IsSafe(map, k, j) && map[k, j] == 0)
                            {
                                map[k, j] = 2;
                                isReplaced = true;
                                break;
                            }
                        }

                        if (!isReplaced)
                        {
                            map[i, j] = 2;
                        }
                        else
                        {
                            PrintSolution(map);
                            nCombination++;
                        }
                    }
                }
            }
        }

        static void FindDifferentWaysOnTheBottom(int[,] map)
        {
            int i, j, k;
            for (i = 0; i < map.GetLength(0); i++)
            {
                for (j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] == 2)
                    {
                        // Check bottom
                        bool isReplaced = false;
                        map[i, j] = 0;
                        for (k = i + 1; k < map.GetLength(0); k++)
                        {
                            if (IsSafe(map, k, j) && map[k, j] == 0)
                            {
                                map[k, j] = 2;
                                isReplaced = true;
                                break;
                            }
                        }

                        if (!isReplaced)
                        {
                            map[i, j] = 2;
                        }
                        else
                        {
                            PrintSolution(map);
                            nCombination++;
                        }
                    }
                }
            }
        }

        static int[,] RevertMap1()
        {
            return new int[4, 4] {
                { 1, 1, 1, 0},
                { 0, 0, 0, 0},
                { 0, 1, 0, 0},
                { 1, 1, 1, 0}};
        }

        static int[,] RevertMap2()
        {
            return new int[8, 8]
            {
                {0,1,0,1,0,1,0,1 },
                {0,0,0,0,0,1,0,0 },
                {1,0,1,0,0,1,0,1 },
                {0,0,0,0,0,0,0,0 },
                {0,0,0,1,0,0,0,0 },
                {0,0,0,0,0,1,0,1 },
                {0,1,0,0,0,0,0,0 },
                {0,0,0,0,1,0,1,0 }
            };
        }

        static void Main(string[] args)
        {
            /*
            Map assumption: 1 is a wall, 0 is an empty space, and 2 is a gunman
            */
            int[,] map1 = RevertMap1();

            int[,] map2 = RevertMap2();

            /*SolveUntilTopLeft(map2, 0, 0);
            SolveUntilBottomRight(map2, map2.GetLength(0) - 1, map2.GetLength(1) - 1);
            SolveUntilTopRight(map1, 0, map1.GetLength(1) - 1);
            SolveUntilTopRight(map2, 0, map2.GetLength(1) - 1);*/

            /*Map1's Solutions*/
            //SolveBottomRightToTop(map1); map1 = RevertMap1();
            //SolveBottomRightToLeft(map1); map1 = RevertMap1();
            //SolveTopRightToDown(map1); map1 = RevertMap1();
            //SolveTopRightToLeft(map1); map1 = RevertMap1();
            //SolveBottomLeftToTop(map1); map1 = RevertMap1();
            //SolveBottomLeftToRight(map1); map1 = RevertMap1();
            //SolveTopLeftToRight(map1); map1 = RevertMap1();
            //SolveTopLeftToBottom(map1); map1 = RevertMap1();

            /*Map2's Solutions*/
            SolveBottomRightToTop(map2); map2 = RevertMap2();
            SolveBottomRightToLeft(map2); map2 = RevertMap2();
            SolveTopRightToDown(map2); map2 = RevertMap2();
            SolveTopRightToLeft(map2); map2 = RevertMap2();
            SolveBottomLeftToTop(map2); map2 = RevertMap2();
            SolveBottomLeftToRight(map2); map2 = RevertMap2();
            SolveTopLeftToRight(map2); map2 = RevertMap2();
            SolveTopLeftToBottom(map2); map2 = RevertMap2();

            Console.WriteLine("Complete ...");

            PrintSolutionWithMaxGunmen();
            FindDifferentWaysOnTheBottom(MaxMapSolution);
            FindDifferentWaysOnTheTop(MaxMapSolution);
            FindDifferentWaysOnTheLeft(MaxMapSolution);
            FindDifferentWaysOnTheRight(MaxMapSolution);

            Console.WriteLine("Max gunmen: " + nMaxGunMen);
            Console.WriteLine("Combination found: " + nCombination);
            Console.ReadLine();
        }
    }
}
