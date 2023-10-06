namespace CSharpBasic
{
    internal struct Coord
    {
        public float x;
        public float y;
        public float z;

        // 구조체 생성자
        // 구조체 데이터를 초기화 및 추가 연산 정의 가능
        public Coord()
        {
            x = 0;
            y = 0;
            z = 0;
        }
    }

    // C# 구조체 및 클래스는 캡슐화 컨셉
    // -> 외부에서 함부로 멤버에 접근하면 안되기 때문에 기본적으로 접근제한자는 private

    internal class CoordClass
    {
        // 싱글톤
        // 인스턴스를 참조할 static 변수를 사용하는 형태.
        // 일반적으로 인스턴스가 힙 영역에 하나만 존재할 경우 객체 참조 구조를 간단하게 하기 위해 사용함
        // 모든 데이터 혹은 클래스를 static으로 하게 될 경우, 사용되지 않아도 리소스가 메모리를 잡아먹기 때문에
        // 사용하는 경우에만 리소스를 로드 / 생성하여 메모리에 올려놓기 위한 형태

        public static CoordClass instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CoordClass();

                return _instance;
            }
        }

        private static CoordClass _instance;
         
        static public float x;
        public float y;
        public float z;

        // 클래스 생성자
        // Manage Heap 영역에 클래스의 멤버 변수들 크기만큼의 메모리를 할당 후 해당 객체의 주소를 반환
        public CoordClass()
        {

        }

        // this 키워드
        // 인스턴스 멤버 함수는 어떤 인스턴스의 데이터를 읽고 써야할지 정해주기 위해
        // 파라미터 대상이 될 인스턴스에 대한 참조가 필요한데, 이를 코드에서 생략하고 this 키워드로 제공.
        // this 키워드도 인스턴스 대상으로는 생략할 수 있음.
        public float Magnitude()
        {
            float result = x * x + y * y + z * z;
            return result;
        }
    }

    internal class Dummy
    {
        public int Poo1;
        public int Poo2;
    }

    internal class Program
    {
        // 값 vs 참조
        // 값 타입 : 메모리의 데이터를 직접 읽거나 쓰는 형태
        // 참조 타입 : 메모리의 주소를 통해 간접적으로 읽거나 쓰는 형태

        // 구조체 - 값
        // 클래스 - 참조
        // 값을 자주 읽고 쓰면서, 멤버변수들의 메모리가 총 16byte 이하면서, 확장성에 대해 닫혀있을 때 구조체 사용.

        static void Main(string[] args)
        {
            int[] arr = new int[2];
            Swap(ref arr[0], ref arr[1]);

            Coord coord = new Coord();
            CoordClass coordClass = new CoordClass();

            Dummy dummy = new Dummy();
            // dummy = new CoordClass();
            // 주소참조변수는 모두 똑같이 주소를 저장하기 위한 4byte만 필요하지만,
            // 간접참조시 얼마만큼의 데이터를 어떻게 읽어야 하는지 알아야 하기 때문에 참조변수 타입은 동일해야한다.
        }

        static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
    }
}