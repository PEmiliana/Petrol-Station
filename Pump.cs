using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PetrolStation4
{
    public class Pump
    {
        public bool isFueling;
        public int counter;
        public int name;
        private int counterLimit;
        public int fueledCars; 

        public Pump(int name, int counterLimit)
        {
            this.name = name;
            this.isFueling = false;
            this.counter = 0;
            this.fueledCars = 0;
            this.counterLimit = counterLimit;
        }

        public void StartFueling()
        {
            this.isFueling = true;
            this.counter = 0;
        }

        public bool GetIsFueling()
        {
            return isFueling;
        }


        public bool UpdatePump(int ms)
        {
            if (isFueling)
            {
                counter += ms;
                if (counter >= counterLimit)
                {
                    isFueling = false;
                    fueledCars++;
                    counter = 0;
                    return true;
                }
            }
            return false;
        }

        /* public double CalculateFueled()
         {
             double fueledToday =+ fueledCars * pumpCapacity;
              this.totatFueled =+ fueledToday * fuelPrice;
             double totatFueled2 =+ this.totalFueled;
             Console.WriteLine($"Total fueled today: £ {totatFueled}");
             return totalFueled;
         }*/
    }

}
