using System;

namespace Events
{
    class Program
    {
        static void Main(string[] args)
        {
            Product harddisk = new(50);
            harddisk.ProductName = "Hard Disk";

            Product gsm = new(50);
            gsm.ProductName = "Telefon";
            gsm.StockControlEvent += Gsm_StockControlEvent;//GSM Product ı için event e abone olma

            for (int i = 0; i < 10; i++)
            {
                harddisk.Sell(10);
                gsm.Sell(10);
                Console.WriteLine("---------------------\n");
                Console.ReadLine();
            }

            Console.ReadLine();
        }

        private static void Gsm_StockControlEvent()
        {
            //Event çalıştığında yapılacak işler
            Console.WriteLine("--------------ALERT!!!! GSM(Telephone) Product about to finish----------------");
        }
    }
}
