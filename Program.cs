/* 
Create a Monte Carlos Simulation to find the probability that a given pattern will happen. 
*/

using System;

namespace BINGO_SIMULATION_C_SHARP{

    class Program{

        static void Main(string[] args){

            // Configurations
            int nRows = 5;
            int nCols = 5;

            // Bingo Card values and Number Ball Values
            int minVal = 1;
            int maxVal = 75;
            int nBalls = 30;

            // Number of games
            int nGames = 1000 ;
            // number of patterns found
            int nFound = 0;

            int[,] pattern = new int[,] {{1,0,0,0,1},
                                         {0,0,0,0,0},
                                         {0,0,0,0,0},
                                         {0,0,0,0,0},
                                         {1,0,0,0,1}};

            // Run Simulation
            for(int ii=0;ii <nGames;ii++){

                int[,] bingoCard = create_bingo_card(nRows,nCols,minVal,maxVal);
                int[] lstNumBalls = create_number_balls(minVal,maxVal,nBalls);
                bool isPatternFound = single_game(bingoCard,lstNumBalls,pattern);  

                if(isPatternFound){
                    nFound += 1;
                }
            }

            double prob = 100*(nFound/(double)nGames);
            Console.WriteLine($"Out of {nGames}, the pattern was found {nFound} times. Thus, there's {prob}% probability of getting this pattern. ");

        }

        static int[,] create_bingo_card(int nRows,int nCols, int minVal, int maxVal){

            Random randNum = new Random();
            int nSize = nRows * nCols;
            int[,] bingoCard = new int[nRows,nCols];
            
            for(int ii=0;ii<nRows;ii++){
                for(int jj=0;jj<nCols;jj++){
                    bingoCard[ii,jj] = randNum.Next(minVal,maxVal);
                }
            }

            return bingoCard;

        }

        static int[] create_number_balls(int minVal, int maxVal, int nBalls){

            Random randNum = new Random();
            int[] lstNumBalls = new int[nBalls];

            for(int ii=0;ii<nBalls;ii++){
                lstNumBalls[ii]= randNum.Next(minVal,maxVal);
            }

            return lstNumBalls;
        }

        static bool single_game(int[,] bingoCard, int[] lstNumBalls, int[,] pattern){

           bool isPatternFound = false;

            int[] selectedNums = new int[pattern.Length];

            // Flattens the array and adds all the elements
            int nTotalVals = pattern.Cast<int>().Sum();

            int nValsFound = 0;
           for(int ii=0;ii < bingoCard.GetLength(0); ii++){
               for(int jj=0; jj < bingoCard.GetLength(1); jj++){
                
                   if (pattern[ii,jj] == 1){

                       foreach(int num in lstNumBalls){

                           if (bingoCard[ii,jj] == num){
                               nValsFound += 1;
                               break;
                           }
                       }
                   }

               }

                if(nValsFound == nTotalVals){
                    isPatternFound = true;
                    break;
                }

            }

            return isPatternFound;
        }

    }
}
