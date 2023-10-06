namespace Boxing
{
    class Dummy { }

    internal class Program
    {
        static void Main(string[] args)
        {
            object obj1 = 5;
            object obj2 = 1.4;
            object obj3 = "asdf";
            object obj4 = new Dummy();

            int k = (int)obj1;
        }
    }
}