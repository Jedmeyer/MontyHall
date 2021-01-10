using System;
using System.Collections.Generic;

namespace MontyHallSim
{
    internal class Program
    {
        // This is a little 'proof' I did for myself to validate the MontyHall Problem programmatically.
        // Check out a short introduction here: https://www.youtube.com/watch?v=mhlc7peGlGg

        private static int Main(string[] args)
        {
            System.Random random = new System.Random();
            int CorrectChoices = 0;
            if (args.Length == 0)
            {
                Console.WriteLine("Please enter the number of simulations as an integer and whether or not to always change the guess\n\tUsage: ./program int bool");
                return 1;
            }
            int cycles = Int32.Parse(args[0]);
            bool AlwaysChangeTheGuess = bool.Parse(args[1]);

            for (int i = 0; i < cycles; i++)
            {
                //Create 3 doors -
                List<Door> doors = new List<Door>();
                int prizeValue = random.Next(0, 3);
                for (int j = 0; j < 3; j++)
                {
                    if (j == prizeValue)
                        doors.Add(new Door()
                        {
                            Prize = true,
                            Choosable = true
                        });
                    else
                        doors.Add(new Door()
                        {
                            Prize = false,
                            Choosable = true
                        });
                }

                //Simulate choosing door -
                int chosenValue = random.Next(0, 3);

                //Make the first door found as un-chooseable if:
                // - it isn't the door you chose
                // - it isn't the prize door.
                for (int j = 0; j < 3; j++)
                {
                    if (j != chosenValue && doors[j].Choosable == true && doors[j].Prize == false)
                    {
                        doors[j].Choosable = false;
                        break;
                    }
                }

                if (AlwaysChangeTheGuess)
                {
                    int secondGuess = random.Next(0, 3);
                    while (doors[secondGuess].Choosable == false)
                    {
                        //If you can't choose the door - re-guess.
                        secondGuess = random.Next(0, 3);
                    }
                    chosenValue = secondGuess;
                }
                if (doors[chosenValue].Prize)
                {
                    CorrectChoices++;
                }
            }

            double successRate = (double)CorrectChoices / cycles;
            Console.WriteLine($"Accuracy after {cycles} cycles - {successRate}");
            return 0;
        }

        public class Door
        {
            public bool Prize { get; set; }
            public bool Choosable { get; set; }
        }
    }
}