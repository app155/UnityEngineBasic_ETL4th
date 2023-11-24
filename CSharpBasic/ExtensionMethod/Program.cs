using System.Linq;

namespace ExtensionMethod
{
    // 확장 메서드
    // 어떤 객체참조를 대상으로 기능을 확장할 때 사용하는 함수
    // static 클래스 내에서 static 메서드를 만들고 기능을 확장해야하는 객체참조를 파라미터로 받는다.

    internal class Program
    {
        static void Main(string[] args)
        {
            string name = "asdf qwer";
            Console.WriteLine(name.WordCount());

            List<int> numbers = new List<int>()
            {
                1,2,3,6,2,3,4,10,44,22,33,55,22,77,42,12
            };

            IEnumerable<string> filtered =
            from number in numbers
            where number > 5
            orderby number descending
            select $"number : {number}";

            foreach (string number in filtered)
            {
                Console.WriteLine(number);
            }

            IEnumerable<string> filtered2 =
            numbers.Where(x => x > 5)
                   .OrderByDescending(x => x)
                   .Select(x => $"number : {x}");

            IEnumerable<int> numbersFiltered =
            numbers.Where(x => x > 5);

        }
    }
}