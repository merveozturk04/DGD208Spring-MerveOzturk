using System;
using System.Threading.Tasks;

namespace LittlePawsGame
{
    /// <summary>
    /// Main program entry point for Little Paws Pet Simulator
    /// Created by: Merve Ozturk (2305041043)
    /// Course: DGD208 - Game Programming 2, Spring 2025
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var game = new Game();
                await game.GameLoop();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
