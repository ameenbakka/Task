    namespace abc
    {
        internal class Program
        {
            static void Main(string[] args)
            {
               Fruit apple = new Fruit("red", "apple");
            apple.Display();
           
            }
        }
        public class Fruit
        {
            public string Color;
            public string Name;
            public Fruit( string FruitColor, string FruitName)
            {
                Color = FruitColor;
                Name = FruitName;
            }
            public void Display()
            {
                Console.WriteLine(Name);
                Console.WriteLine(Color);
            }

       
        }
    }
