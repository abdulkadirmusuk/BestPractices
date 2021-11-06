using System;
using System.Collections.Generic;
using System.Threading;

namespace Delegates
{
    class Program
    {
        public delegate void MyDelegate(); //void ve parametre almayan metodlara delegelik yapabilir
        public delegate void MyDelegate2(string text);//void ve parametre alan metodlara delegasyon
        public delegate int MyDelegate3(int number1, int number2);

        static void Main(string[] args)
        {
            //DelegateUsingTutorial(); //Standart Delege kullanımı


            //Action Delege Kullanımı (Geriye değer döndürmeyen ve belli kod bloklarını çalıştıran yapılardır.)
            //ActionDelegateUsingTutorial();


            //Func Delegasyon kullanımı 
            FuncUsingTutorial();

            Console.ReadLine();
        }

        private static void ActionDelegateUsingTutorial()
        {
            HandleException(() =>
            {
                Find();
            });
        }

        private static void FuncUsingTutorial()
        {
            //Func action dan farklı olarak geriye bir dönüş tipin döndürür.
            Func<int, int, int> add = Topla;
            Console.WriteLine("Toplama sonucu " + add(3, 4));

            Func<int> getRandomNumber = delegate ()
            {
                Random random = new Random();
                return random.Next(1, 100);
            };
            Thread.Sleep(500);
            Func<int> getRandomNumber2 = () => new Random().Next(1, 100);

            Console.WriteLine("Parametre almayan ve int döndüren delegsayon func metodu için sonuc :" + getRandomNumber());
            Console.WriteLine("getRandomNumber2 sonuc " + getRandomNumber2());
        }

        private static int Topla(int x, int y)
        {
            return x + y;
        }

        private static void DelegateUsingTutorial()
        {
            //Delege Kullanım Amacı : yapılacak işlemler önce belirli şartlara göre önce toplanır. yapılacak bir sıra oluşturulur ve sırayla o işler çalışır
            CustomerManager customerManager = new CustomerManager();
            Matematik matematik = new Matematik();

            MyDelegate myDelegate = customerManager.SendMessage;
            myDelegate += customerManager.ShowAlert;//önce send sonra aler işi delegasyon üzerine yüklendi.
            myDelegate -= customerManager.SendMessage;//send message işlemi iptal edildi
            myDelegate();//delege çağırma


            MyDelegate2 myDelegate2 = customerManager.SendMessage2;
            myDelegate2 += customerManager.ShowAlert2;
            myDelegate2("this is message"); // burada ikisine de aynı mesaj gider. Ayrı ayrı göndermek için farklı yollar izlenmelidir.

            MyDelegate3 myDelegate3 = matematik.Topla;
            myDelegate3 += matematik.Carp;
            var sonuc = myDelegate3(2, 3); //dönüş tipi olan delegasyonda birden fazla dönüş değeri en son olan işlemin olur
            Console.WriteLine("3.delegasyon sonucu " + sonuc);
        }

        private static void HandleException(Action action)
        {
            try
            {
                action.Invoke(); //metodu çalıştır
            }
            catch (Exception ex) when (ex != null)
            {

                Console.WriteLine(ex.Message);
            }
        }

        private static void Find()
        {
            List<string> students = new List<string>
            {
                "Ogrenci1", "Ogrenci2"
            };
            if (!students.Contains("Test"))
            {
                throw new RecordNotFoundException("Record not found");
            }
            else
            {
                Console.WriteLine("Record found");
            }
        }
    }

    
    public class RecordNotFoundException : Exception
    {
        public RecordNotFoundException(string errorMessage)
        {
            Console.WriteLine(errorMessage);
        }
    }

    public class CustomerManager
    {
        public void SendMessage()
        {
            Console.WriteLine("Hello");
        }

        public void ShowAlert()
        {
            Console.WriteLine("Be Careful!");
        }

        //parametreli method delegasyonu için örnek metodlar
        public void SendMessage2(string message)
        {
            Console.WriteLine("Hello {0}",message);
        }

        public void ShowAlert2(string alertMessage)
        {
            Console.WriteLine("Be Careful! {0}",alertMessage);
        }
    }

    public class Matematik
    {
        public int Topla(int sayi1, int sayi2)
        {
            return sayi1 + sayi2;
        }

        public int Carp(int sayi1, int sayi2)
        {
            return sayi1 * sayi2;
        }
    }
}
