using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace PetrolStation4
    


{
    public class App
    {
         Timer loopTimer;
         Random rnd = new Random();
         List<Pump> pumps = new List<Pump>();
         int carInQueue = 0;
         int carLeft = 0;
         int carWaitTime = 0;
         int currentTimer = 0;
         int maxTimer;
         int pumpCapacity = 27;
         double fuelPrice = 4.5;
         double totalFueled = 0.0;
         double percent = 1.0 / 100;
         double commission = 0.0;
         double payRate = 5.9;


         bool isRunning = true;
         bool exit = false;
         int hours = 8;
         double fueledToday = 0.0;
         double employeePayment = 0.0;

        public App()
        {
        }
        public void Run()
        {

        
            maxTimer = rnd.Next(1500, 2200);
            for (int i = 1; i <= 9; ++i)
            {
                pumps.Add(new Pump(i, 18000));
            }

            loopTimer = new Timer();
            loopTimer.Interval = 100;
            loopTimer.AutoReset = false;
            loopTimer.Elapsed += programloop;
            loopTimer.Enabled = true;
            loopTimer.Start();

            while (!exit)
            {

                Console.Clear();
                Console.WriteLine("Press [ESC] at any time to stop the application.");
                Console.WriteLine("Select a number from 1 - 9 to assign a car to a pump.");
                
                if (carInQueue < 5)
                {
                    Console.Write($"Number of cars in queue: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(carInQueue);
                    Console.ForegroundColor = ConsoleColor.Gray;
                }

                else
                {
                    Console.Write($"Number of cars in queue: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(carInQueue);
                    Console.WriteLine("Maximum number of cars in queue. Please assign a car if possible.");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    if (carInQueue == 5)
                    {
                        carWaitTime++;
                        if (carWaitTime == 25)
                        {
                            carInQueue --;
                            carLeft++;
                            carWaitTime = 0;
                            Console.Beep();

                        }
                    }
                    
                }
                

                if (carInQueue > 0)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = GetKeyInput();
                        SelectMenuOptions(key);

                        while (Console.KeyAvailable)
                        {
                            Console.ReadKey(false);
                        }
                    }
                }
                int sumCars = 0;
                foreach (var pump in pumps)
                {

                    if (pump.name == 1 || pump.name == 4 || pump.name == 7)
                    {
                        if (pump.name == 1)
                        {
                            Console.WriteLine($"\n Line number: 1");
                        }

                        if (pump.name == 4)
                        {
                            Console.WriteLine($"\n Line number: 2");
                        }

                        if (pump.name == 7)
                        {
                            Console.WriteLine($"\n Line number: 3");
                        }

                    }




                    if (pump.GetIsFueling())
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("|Pump " + pump.name + " BUSY     |" + "       |CARS FUELED : " + pump.fueledCars + "|");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }

                    else 
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("|Pump " + pump.name + " AVAILABLE|" + "       |CARS FUELED : " + pump.fueledCars + "|");
                        Console.ForegroundColor = ConsoleColor.Gray;
                    }
                    sumCars += pump.fueledCars;
                }

                    Console.WriteLine($"\nTOTAL FUELED: {sumCars}");
                Console.WriteLine($"Number of cars that left the petrol station: {carLeft}");
                Thread.Sleep(100);


            }

        }

         private void programloop(object sender, ElapsedEventArgs e)
        {
            if (isRunning)
            {
                loopTimer = new Timer();
                loopTimer.Interval = 100;
                loopTimer.AutoReset = false;
                loopTimer.Elapsed += programloop;
                loopTimer.Enabled = true;
                loopTimer.Start();
            }

            if (currentTimer <= maxTimer && carInQueue != 5)
            {
                currentTimer += 100;
            }
            else
            {
                if (carInQueue < 5)
                {
                    carInQueue++;
                    currentTimer = 0;
                    maxTimer = rnd.Next(1500, 2200);
                }
            }

            foreach (var pump in pumps)
            {
                pump.UpdatePump(100);
            }

        }

         private ConsoleKeyInfo GetKeyInput()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            return key;
        }

         private double TotalFueled(List<Pump> pumps)
        {

            foreach (Pump currentPump in pumps)
            {

                fueledToday = currentPump.fueledCars * pumpCapacity;
                totalFueled += fueledToday * fuelPrice;
            }
            return totalFueled;


        }

         private double FueledToday(List<Pump> pumps)
        {

            foreach (Pump currentPump in pumps)
            {
                fueledToday += currentPump.fueledCars * pumpCapacity;
            }
            return fueledToday;
        }

         private double TotalPay()
        {
            commission += totalFueled * percent;
            employeePayment += hours * payRate;
            employeePayment += commission;
            return employeePayment;
        }

         private void SelectMenuOptions(ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.D1:
                    if (pumps[0].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[0].StartFueling();
                        carInQueue--;
                    }

                    break;
                case ConsoleKey.D2:
                    if (pumps[1].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[1].StartFueling();
                        carInQueue--;
                    }

                    break;
                case ConsoleKey.D3:
                    if (pumps[2].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[2].StartFueling();
                        carInQueue--;
                    }

                    break;
                case ConsoleKey.D4:
                    if (pumps[3].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[3].StartFueling();
                        carInQueue--;
                    }

                    break;

                case ConsoleKey.D5:
                    if (pumps[4].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[4].StartFueling();
                        carInQueue--;
                    }

                    break;

                case ConsoleKey.D6:
                    if (pumps[5].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[5].StartFueling();
                        carInQueue--;
                    }

                    break;

                case ConsoleKey.D7:
                    if (pumps[6].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[6].StartFueling();
                        carInQueue--;
                    }

                    break;

                case ConsoleKey.D8:
                    if (pumps[7].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[7].StartFueling();
                        carInQueue--;
                    }

                    break;

                case ConsoleKey.D9:
                    if (pumps[8].GetIsFueling())
                    {
                        Console.WriteLine("Pump already fueling");
                    }
                    else
                    {
                        pumps[8].StartFueling();
                        carInQueue--;
                    }

                    break;
                case ConsoleKey.Escape:
                    {
                        Console.WriteLine($"\nTotal fueled today: £ {TotalFueled(pumps)}");
                        Console.WriteLine($"Employee payment for today is: £ {Math.Round(TotalPay(),2)}");
                        Console.WriteLine($"Number of litres: {FueledToday(pumps)}");
                        Console.WriteLine($"Your commission is : £ {Math.Round(commission,2)}\n");

                        isRunning = false;
                        exit = true;
                        break;
                    }
                    
                default:
                    break;
            }
        }
    }
}
