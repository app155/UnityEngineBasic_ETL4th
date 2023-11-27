namespace Homeworks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ObservableCollection<string> collection = new ObservableCollection<string>();

            collection.Add(0, "A");
            collection.Add(1, "B");
            collection.Change(5, "F");
            collection.Add(2, "C");
            collection.Add(3, "D");
            collection.Remove(2);
        }
    }
}