using System;
using System.Collections.Generic;

namespace LittlePaws
{
    /// <summary>
    /// Displays a menu of items of any type and returns the user's selection.
    /// </summary>
    /// <typeparam name="T">Menu item type</typeparam>
    public class Menu<T>
    {
        private readonly List<T> _items;
        private readonly string _title;
        private readonly Func<T, string> _displaySelector;
        /// <summary>
        /// Initializes a new instance of the menu class with the specified items and display format.
        /// </summary>
        /// <param name="title">The title to display at the top of the menu</param>
        /// <param name="items">The list of items to display in the menu</param>
        /// <param name="displaySelector">A function that determines how each item is displayed</param>
        public Menu(string title, List<T> items, Func<T, string> displaySelector)
        {
            _title = title ?? string.Empty;
            _items = items ?? new List<T>();
            _displaySelector = displaySelector ?? (item => item?.ToString() ?? string.Empty);
        }

        /// <summary>
        /// Shows the menu to the user and gets their selection.
        /// </summary>
        /// <returns>
        /// The item selected by the user, or default(T) if the user opts to go back.
        /// </returns>
        public T ShowAndGetSelection()
        {
            if (_items.Count == 0)
            {
                Console.WriteLine($"There are no items in {_title}. Press any key to continue...");
                Console.ReadKey();
                return default;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine(new string('=', _title.Length + 8));
                Console.WriteLine($"   {_title}   ");
                Console.WriteLine(new string('=', _title.Length + 8));
                Console.WriteLine();

                // Display menu items with numbers
                for (int i = 0; i < _items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_displaySelector(_items[i])}");
                }

                Console.WriteLine();
                Console.WriteLine("0. Back");
                Console.WriteLine();
                Console.Write("Please enter your selection: ");

                // Get user input
                string input = Console.ReadLine();

                // Try to parse the input
                if (int.TryParse(input, out int selection))
                {
                    // Check for "Back" option
                    if (selection == 0)
                        return default; // Return default value of T to indicate backing out

                    // Check if selection is valid
                    if (selection > 0 && selection <= _items.Count)
                    {
                        return _items[selection - 1];
                    }
                }

                Console.WriteLine("Oops! Selection is not valid.Please press any key to try again.");
                Console.ReadKey();
            }
        }
    }
}
