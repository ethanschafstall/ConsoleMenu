

namespace ConsoleMenu
{
    internal class Program
    {
        static void Main(string[] args) 
        {
            Console.CursorVisible= false;
            
            Menu firstMenu = new Menu();
            firstMenu.MenuOptions.Add("option 1");
            firstMenu.MenuOptions.Add("option 2");
            firstMenu.MenuOptions.Add("option 3");
            firstMenu.MenuPrompt = "My first menu";
            firstMenu.MenuPosition = new int[] { 1, 0 };
            firstMenu.Run();

        }
    }
}
