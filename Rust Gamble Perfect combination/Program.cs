using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
namespace Rust_Gamble_Perfect_combination
{
    class Program
    {
        static TimeSpan MTTime;
        static List<int> Roullete = new List<int>() { 5, 1, 3, 1, 10, 1, 3, 1, 5, 1, 5, 3, 1, 10, 1, 3, 1, 5, 1, 3, 1, 20, 1, 3, 1 };
        static void Main(string[] args)
        {
            /*Console.WriteLine("How many scraps do you have:");
            int scraps = Convert.ToInt32(Console.ReadLine());*/
            //Console.WriteLine("Bet on 1:");
            //int bestBet1 = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Bet on 3:");
            //int Bet3 = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Bet on 5:");
            //int Bet5 = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Bet on 10:");
            //int Bet10 = Convert.ToInt32(Console.ReadLine());
            //Console.WriteLine("Bet on 20:");
            //int Bet20 = Convert.ToInt32(Console.ReadLine());
            //int srap = bestBet1 + Bet3 + Bet5 + Bet10 + Bet20;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            int taskN = 10;
            Task[] tasks = new Task[taskN];
            for (int i = 0; i < taskN; i++)
            {
                int iCopy = i;
                /*tasks[iCopy] = Task.Factory.StartNew(() => Game(srap, bestBet1, Bet3, Bet5, Bet10, Bet20), CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);*/

                tasks[iCopy] = Task.Factory.StartNew(() => Finding(), CancellationToken.None,
                TaskCreationOptions.LongRunning,
                TaskScheduler.Default);
                //Loop(10);
                Thread.Sleep(1);
            }
            //Loop(10);
            Task.WaitAll(tasks);
            stopwatch.Stop();
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("");
            MTTime = stopwatch.Elapsed;
            Console.WriteLine("Time: " + MTTime);
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
        }


//        public static void Loop(int scraps)
//        {
//            int high = 0;
//            for (int i = 0; i <= scraps; i++)
//            {
//                //Console.WriteLine("bet1: " + bet1);
//                for (int ii = 0; ii <= scraps - i; ii++)
//                {
//                    //Console.WriteLine("bet3: " + bet3);
//                    for (int iii = 0; iii <= scraps - i - ii; iii++)
//                    {
//                        //Console.WriteLine("bet5: " + bet5);
//                        for (int iiii = 0; iiii <= scraps - i - ii - iii; iiii++)
//                        {
//                            //Console.WriteLine("bet10: " + bet10);
//                            for (int iiiii = 0; iiiii <= scraps - i - ii - iii - iiii; iiiii++)
//                            {
//                                //Console.WriteLine("bet20: " + bet20);
//                                List<int> Bets = new List<int>() { i, ii, iii, iiii, iiiii};
//                                high++;
//                                //Console.WriteLine(Convert.ToString(Bets[0]) + Convert.ToString(Bets[1]) + Convert.ToString(Bets[2]) + Convert.ToString(Bets[3]) + Convert.ToString(Bets[4]));
//                            }
//                        }
//                    }
//                }
//            }
//            Console.WriteLine(high);
//        }

        public static void Finding()
        {
            int HighestPercent = 0;
            int bestBet1 = 0;
            int bestBet3 = 0;
            int bestBet5 = 0;
            int bestBet10 = 0;
            int bestBet20 = 0;
            float range = 10;
            for (int bet1 = 0; bet1 <= range; bet1++)
            {
                for (int bet3 = 0; bet3 <= range - bet1; bet3++)
                {
                    for (int bet5 = 0; bet5 <= range - bet1 - bet3; bet5++)
                    {
                        for (int bet10 = 0; bet10 <= range - bet1 - bet3 - bet5; bet10++)
                        {
                            for (int bet20 = 0; bet20 <= range - bet1 - bet3 - bet5 - bet10; bet20++)
                            {
                                //Console.WriteLine(Convert.ToString(bet1) + Convert.ToString(bet3) + Convert.ToString(bet5) + Convert.ToString(bet10) + Convert.ToString(bet20));
                                int srap = bet1 + bet3 + bet5 + bet10 + bet20;
                                int Per = Game(srap, bet1, bet3, bet5, bet10, bet20);
                                if (Per > HighestPercent)
                                {
                                    HighestPercent = Per;
                                    bestBet1 = bet1;
                                    bestBet3 = bet3;
                                    bestBet5 = bet5;
                                    bestBet10 = bet10;
                                    bestBet20 = bet20;
                                }
                            }
                        }
                    }
                }
            }
            Console.WriteLine(Convert.ToString(bestBet1) + "-" + Convert.ToString(bestBet3) + "-" + Convert.ToString(bestBet5) + "-" + Convert.ToString(bestBet10) + "-" + Convert.ToString(bestBet20));
            Console.WriteLine(HighestPercent + "%");
            Console.WriteLine("");
        }

        public static int Game(int scraps, int Bet1, int Bet3, int Bet5, int Bet10, int Bet20)
        {
            int attempts = 10000;
            /*int HighestScrap = 0;
            float Highestcount = 0;*/
            int ProfitCount = 0;
            Random rnd = new Random();
            for (int i = 0; i < attempts; i++)
            {
                int current = scraps;
                int count = 0;
                bool isProfit = false;
                while (current > 0 && Bet1 <= current && Bet3 <= current && Bet5 <= current && Bet10 <= current && Bet20 <= current && count < 60)
                {
                    int slot = Roullete[rnd.Next(0, 25)];
                    current -= Bet1 + Bet3 + Bet5 + Bet10 + Bet20;
                    switch (slot)
                    {
                        case 1:
                            current += 2 * Bet1;
                            break;
                        case 3:
                            current += 4 * Bet3;
                            break;
                        case 5:
                            current += 6 * Bet5;
                            break;
                        case 10:
                            current += 11 * Bet10;
                            break;
                        case 20:
                            current += 21 * Bet20;
                            break;
                        default:
                            break;
                    }
                    /*if (HighestScrap < current)
                    {
                        HighestScrap = current;
                        Highestcount = count;
                    }*/
                    count++;
                    if (current >= scraps * 20)
                    {
                        isProfit = true;
                    }
                    /*Console.WriteLine("Bet3: " + Bet3);
                    Console.WriteLine("slot: " + slot);
                    Console.WriteLine("current: " + current);*/
                }
                if (isProfit)
                {
                    ProfitCount++;
                }
            }
            int Profit = (ProfitCount * 100) / attempts;
            //Console.WriteLine("Profit: " + Profit + "%");
            /*Console.WriteLine("Highest Scrap: " + HighestScrap);
            Console.WriteLine("Highest count: " + Highestcount);*/
            //Console.WriteLine("");
            return Profit; 
        }
    }
}
