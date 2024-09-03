/*************************************************
 * FileName     = Program.cs
 * 
 * Author       = Komarabathina Srujana Jasmine
 * 
 * Product      = SoftwareDesignPatterns
 * 
 * Project      = ChainOfResponsibility
 * 
 * Description  = Main program to run the project
 *************************************************/
using UI;
namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var ui = new UserInterface();       // initiating the project
            ui.Start();
        }
    }
}