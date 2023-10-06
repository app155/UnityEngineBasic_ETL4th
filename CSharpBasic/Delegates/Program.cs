namespace Delegates
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Player player = new Player();
            PlayerUI playerUI = new PlayerUI();
            PlayerUI2 playerUI2 = new PlayerUI2();

            player.onHpChanged += playerUI.Refresh;

            player.onHpChanged += playerUI2.Refresh;

            player.onHpChanged -= playerUI2.Refresh;

            while (true)
            {
                
            }
        }
    }
}