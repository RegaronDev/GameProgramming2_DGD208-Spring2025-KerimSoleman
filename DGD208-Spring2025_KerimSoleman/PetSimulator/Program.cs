using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.Title = "Pet Simulator";
        
        // Create a new game instance
        Game game = new Game();
        
        // Start the game loop
        await game.GameLoop();
    }
}
