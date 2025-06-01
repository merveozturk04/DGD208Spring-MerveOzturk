using System;
using System.Collections.Generic;

namespace LittlePaws
{
    /// <summary>
    /// Generic menu class for displaying and selecting items
    /// </summary>
    /// <typeparam name="T">Type of items to display</typeparam>
    public class MenuForLittlePaws<T>
    {
        private readonly List<T> items;
        private readonly string title;
        private readonly Func<T, string> displaySelector;

        public MenuForLittlePaws(string title, List<T> items, Func<T, string> displaySelector)
        {
            this.title = title ?? "Menu";
            this.items = items ?? new List<T>();
            this.displaySelector = displaySelector ?? (item => item?.ToString() ?? "Unknown");
        }

        public T ShowAndGetSelection()
        {
            if (items.Count == 0)
            {
                Console.WriteLine($"No items available in {title}.");
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                return default(T)!;
            }

            while (true)
            {
                DisplayMenu();
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int selection))
                {
                    if (selection == 0)
                        return default(T)!; // Back option

                    if (selection > 0 && selection <= items.Count)
                        return items[selection - 1];
                }

                Console.WriteLine("Invalid selection. Press any key to try again...");
                Console.ReadKey();
            }
        }

        private void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(new string('=', title.Length));
            Console.WriteLine();

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {displaySelector(items[i])}");
            }

            Console.WriteLine("0. Back");
            Console.WriteLine();
            Console.Write("Select an option: ");
        }
    }
}