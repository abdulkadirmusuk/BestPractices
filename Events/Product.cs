namespace Events
{
    public delegate void StockControl();

    public class Product
    {
        private int _stock;

        public Product(int stock)
        {
            _stock = stock;
        }

        public event StockControl StockControlEvent;
        public string ProductName { get; set; }
        public int Stock
        {
            get {
                return _stock;
            }
            set {
                _stock = value; //value : kişinin verdiği değer demektir. Setleme değeri yani
                if (value <= 15 && StockControlEvent != null)
                {
                    //Stok 15 den aşağı düşmüşse ve bu stockControlEvent e abone olunmuş ise bu blok çalışır
                    StockControlEvent();
                }
            }
        }

        public void Sell(int amount)
        {
            if (Stock <= 0)
            {
                System.Console.WriteLine("{0} Ürünü bittiği için satış yapılamaz!", ProductName);
                return;
            }
            Stock -= amount;
            System.Console.WriteLine("Ürün için 10 adet satış yapıldı...");
            System.Console.WriteLine("{0} Stock amount : {1}\n",ProductName, Stock);
        }
    }
}
