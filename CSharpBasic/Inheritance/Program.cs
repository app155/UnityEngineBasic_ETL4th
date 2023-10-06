namespace Inheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Warrior warrior = new Warrior();
            warrior.NickName = "asdf";
            Console.WriteLine($"{warrior.NickName}, {warrior.Exp}");

            Goblin goblin = new Goblin();

            // 공변성
            // 하위타입을 기반타입으로 참조할 수 있는 성질
            Character player = warrior;
            IHp target = goblin;
            player.Attack(target);

        }
    }
}