using System;
using System.Collections.Generic;

namespace LittlePaws
{
    /// <summary>
    /// Displays a menu of items of any type and returns the user's selection.
    /// </summary>
    /// <typeparam name="T">Menu item type</typeparam>
    public class MenuForLittlePaws<T>
    {
        private readonly List<T> items;
        private readonly string title;
        private readonly Func<T, string> displaySelector;

        // Project creator info
        private const string projectCreator = "Project Creator: Merve Ozturk (2305041043)";

        /// <summary>
        /// Initializes a new instance of the menu class with the specified items and display format.
        /// </summary>
        /// <param name="title">The title to display at the top of the menu</param>
        /// <param name="items">The list of items to display in the menu</param>
        /// <param name="displaySelector">A function that determines how each item is displayed</param>
        public MenuForLittlePaws(string title, List<T> items, Func<T, string> displaySelector)
        {
            this.title = title ?? string.Empty;
            this.items = items ?? new List<T>();
            this.displaySelector = displaySelector ?? (item => item?.ToString() ?? string.Empty);
        }

        /// <summary>
        /// Shows the menu to the user and gets their selection.
        /// </summary>
        /// <returns>
        /// The item selected by the user, or default(T) if the user opts to go back.
        /// </returns>
        public T ShowAndGetSelection()
        {
            if (items.Count == 0)
            {
                Console.WriteLine($"There are no items in {title}. Press any key to continue...");
                Console.ReadKey();
                return default!;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine(new string('=', title.Length + 8));
                Console.WriteLine($"   {title}   ");
                Console.WriteLine(new string('=', title.Length + 8));
                Console.WriteLine();

                // Display project creator info
                Console.WriteLine(ProjectCreator);
                Console.WriteLine();

                // Display menu items with numbers
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {displaySelector(items[i])}");
                }

                Console.WriteLine();
                Console.WriteLine("A. Show stats for all pets");
                Console.WriteLine("0. Back");
                Console.WriteLine();
                Console.Write("Please enter your selection: ");

                // Get user input
                string? input = Console.ReadLine();

                // Show stats for all pets if 'A' or 'a' is entered
                if (string.Equals(input, "A", StringComparison.OrdinalIgnoreCase))
                {
                    ShowAllStats();
                    continue;
                }

                // Try to parse the input
                if (int.TryParse(input, out int selection))
                {
                    // Check for "Back" option
                    if (selection == 0)
                        return default!; // Return default value of T to indicate backing out

                    // Check if selection is valid
                    if (selection > 0 && selection <= items.Count)
                    {
                        return items[selection - 1];
                    }
                }

                Console.WriteLine("Oops! selection is not valid. Please press any key to try again.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Shows the stats for all pets in the menu.
        /// </summary>
        private void ShowAllStats()
        {
            Console.Clear();
            Console.WriteLine("Current Stats for All Pets:");
            Console.WriteLine(new string('-', 30));
            foreach (var item in items)
            {
                Console.WriteLine(displaySelector(item));
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey();
        }

        private static string ProjectCreator => projectCreator;
    }
}