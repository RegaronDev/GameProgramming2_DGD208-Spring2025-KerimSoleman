using System;
using System.Collections.Generic;

/// <summary>
/// A generic menu system that can display a list of items and get user selection.
/// </summary>
/// <typeparam name="T">The type of items in the menu</typeparam>
public class Menu<T>
{
    private readonly List<T> _items;
    private readonly string _title;
    private readonly Func<T, string> _displaySelector;
    private readonly Dictionary<int, ConsoleColor> _itemColors;

    /// <summary>
    /// Creates a new menu with the specified items and display format.
    /// </summary>
    /// <param name="title">The title to display at the top of the menu</param>
    /// <param name="items">The list of items to display in the menu</param>
    /// <param name="displaySelector">A function that determines how each item is displayed</param>
    public Menu(string title, List<T> items, Func<T, string> displaySelector)
    {
        _title = title;
        _items = items ?? new List<T>();
        _displaySelector = displaySelector ?? (item => item?.ToString() ?? "");
        _itemColors = new Dictionary<int, ConsoleColor>();
        
        // Set default colors for common menu items
        SetDefaultColors();
    }
    
    /// <summary>
    /// Set a specific color for a menu item by its index (1-based)
    /// </summary>
    /// <param name="index">The 1-based index of the item</param>
    /// <param name="color">The color to use for the item</param>
    public void SetItemColor(int index, ConsoleColor color)
    {
        _itemColors[index] = color;
    }
    
    /// <summary>
    /// Set default colors for common menu items
    /// </summary>
    private void SetDefaultColors()
    {
        // Exit/last option is usually red
        if (_items.Count > 0)
        {
            _itemColors[_items.Count] = ConsoleColor.Red;
        }
        
        // First option is usually yellow
        if (_items.Count > 0)
        {
            _itemColors[1] = ConsoleColor.Yellow;
        }
        
        // Second option is green
        if (_items.Count > 1)
        {
            _itemColors[2] = ConsoleColor.Green;
        }
        
        // Third option is cyan
        if (_items.Count > 2)
        {
            _itemColors[3] = ConsoleColor.Cyan;
        }
        
        // Fourth option is magenta
        if (_items.Count > 3)
        {
            _itemColors[4] = ConsoleColor.Magenta;
        }
    }

    /// <summary>
    /// Displays the menu and gets the user's selection.
    /// </summary>
    /// <returns>The selected item, or default(T) if the user chooses to go back</returns>
    public T ShowAndGetSelection()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine($"No items available in {_title}. Press any key to continue...");
            Console.ReadKey();
            return default;
        }

        while (true)
        {
            Console.Clear();
            
            // Display title in blue
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"=== {_title} ===");
            Console.ResetColor();
            Console.WriteLine();

            // Display menu items with numbers and colors
            for (int i = 0; i < _items.Count; i++)
            {
                // Check if this item has a custom color
                if (_itemColors.TryGetValue(i + 1, out ConsoleColor itemColor))
                {
                    Console.ForegroundColor = itemColor;
                }
                
                Console.WriteLine($"{i + 1}. {_displaySelector(_items[i])}");
                Console.ResetColor();
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("0. Go Back");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Enter selection: ");

            // Get user input
            string input = Console.ReadLine();
            
            // Try to parse the input
            if (int.TryParse(input, out int selection))
            {
                // Check for "Go Back" option
                if (selection == 0)
                    return default; // Return default value of T to indicate backing out
                
                // Check if selection is valid
                if (selection > 0 && selection <= _items.Count)
                {
                    return _items[selection - 1];
                }
            }
            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hmm, Something's not right, maybe invalid Input, try again.");
            Console.ResetColor();
            Console.ReadKey();
        }
    }
} 