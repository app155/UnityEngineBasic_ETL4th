// Attribute(특성)
// 메타데이터를 정의할 때 사용하는 클래스

#define test
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Attributes
{
    internal class A
    {
        // 더 이상 사용되지 않는다는 걸 표시하는 특성
        [Obsolete]
        public void OldMethod() => Console.WriteLine("Old");
        public void NewMethod() => Console.WriteLine("New");
    }

    internal class B
    {
        // conditional
        // #define 전처리기에서 정의된 문자열일때만 구현하는 특성.
        [Conditional("test")]
        public static void Log([CallerMemberName] string caller = default)
            => Console.WriteLine($"B called by {caller}");

    }
    
    internal class Program
    {
        static void Main(string[] args)
        {
            A a = new A();
            a.OldMethod();
            a.NewMethod();

            B.Log();

            LegoStore legoStore = new LegoStore();
            legoStore.PropertyChanged += (sender, args) =>
            {
                Console.WriteLine($"{args.PropertyName} of {sender} has changed");

                switch (args.PropertyName)
                {
                    case "CreatorTotal":
                        Console.WriteLine("gogo");
                        break;
                    case "StarwarsTotal":
                        {
                            if (legoStore.StarwarsTotal < 3)
                                Console.WriteLine("go");
                            else
                                Console.WriteLine("no");
                        }
                        break;
                    case "CityTotal":
                        Console.WriteLine("nono");
                        break;
                    default:
                        break;
                }
            };

            legoStore.StarwarsTotal = 3;
            legoStore.CreatorTotal = 3;
            legoStore.CityTotal = 3;

            GoldUI ui = new GoldUI();

            while (true)
            {
                string input = Console.ReadLine();

                GoldViewModel.Instance.Value = Int32.Parse(input);

                Console.WriteLine(ui.Text);
            }
        }
    }
}