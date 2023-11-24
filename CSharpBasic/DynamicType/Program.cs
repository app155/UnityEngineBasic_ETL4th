namespace DynamicType
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var a = 1;      // 컴파일시 int 타입으로 결정
            dynamic b = 2;  // 런타임중 대입값에 따라 타입 결정 boxing처럼 heap영역에 객체 생성됨. (boxing)

            while (true)
            {
                string input = Console.ReadLine();

                switch (input)
                {
                    case "int":
                        b = 2;
                        break;
                    case "float":
                        b = 3.0f;
                        break;
                    case "string":
                        b = "asdf";
                        break;
                    default:
                        break;
                }

                Console.WriteLine(b.GetType());
            }
        }
    }
}