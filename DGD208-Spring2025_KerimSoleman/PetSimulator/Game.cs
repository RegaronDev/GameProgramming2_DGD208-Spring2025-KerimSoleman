using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Game
{
    private bool _isRunning;
    private List<Pet> _adoptedPets;
    private int _zoomLevel = 1;  // Default zoom level
    
    // Constructor
    public Game()
    {
        _adoptedPets = new List<Pet>();
    }
    
    public async Task GameLoop()
    {
        // Initialize the game
        Initialize();
        
        // Main game loop
        _isRunning = true;
        while (_isRunning)
        {
            // Display menu and get player input
            string userChoice = GetUserInput();
            
            // Process the player's choice
            await ProcessUserChoice(userChoice);
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Thanks for playing!");
        Console.ResetColor();
    }
    
    private void Initialize()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Welcome to the Animal Thriving Wild life!");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Click any key to start..");
        Console.ResetColor();
        Console.ReadKey();
        Console.Clear();
        
        // Set initial console settings to a higher zoom level for better visibility
        SetConsoleZoom(3);  // Start with Large zoom (1.25x)
    }
    
    // Method to set console zoom by adjusting buffer and window size
    private void SetConsoleZoom(int zoomLevel)
    {
        try
        {
            // Store the zoom level
            _zoomLevel = zoomLevel;
            
            // Attempt to adjust console properties for better display
            try
            {
                // Set window size based on zoom level
                int width = 80;  // Base width
                int height = 25; // Base height
                
                switch (zoomLevel)
                {
                    case 1: // Small
                        width = 100;
                        height = 30;
                        break;
                    case 2: // Normal
                        width = 90;
                        height = 28;
                        break;
                    case 3: // Large
                        width = 80;
                        height = 25;
                        break;
                    case 4: // Extra Large
                        width = 70;
                        height = 22;
                        break;
                }
                
                // Adjust console window and buffer size
                Console.WindowWidth = Math.Min(width, Console.LargestWindowWidth);
                Console.WindowHeight = Math.Min(height, Console.LargestWindowHeight);
                Console.BufferWidth = Console.WindowWidth;
                Console.BufferHeight = Console.WindowHeight * 2;
                
                // Try to center console window
                Console.SetWindowPosition(0, 0);
            }
            catch (Exception)
            {
                // Some Windows environments might not support all console operations
                // Just continue if this fails
            }
            
            // Display zoom feedback
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Zoom level set to {GetZoomLevelName(_zoomLevel)}");
            Console.WriteLine("Window size adjusted for better visibility");
            Console.ResetColor();
            
            // Pause briefly to show message
            Task.Delay(1500).Wait();
            Console.Clear();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Could not adjust zoom: {ex.Message}");
        }
    }
    
    // Helper method to get zoom level name
    private string GetZoomLevelName(int zoomLevel)
    {
        switch (zoomLevel)
        {
            case 1: return "Small (0.75x)";
            case 2: return "Normal (1x)";
            case 3: return "Large (1.25x)";
            case 4: return "Extra Large (1.5x)";
            default: return $"Level {zoomLevel}";
        }
    }
    
    private string GetUserInput()
    {
        // Create main menu items
        List<string> menuItems = new List<string>
        {
            "Own a Pet",
            "Seek Available Pets",
            "Use Item on Pet",
            "Adjust Zoom",
            "Author Info",
            "Exit"
        };
        
        // Create and display menu
        Menu<string> mainMenu = new Menu<string>("Main Menu", menuItems, item => item);
        string choice = mainMenu.ShowAndGetSelection();
        
        return choice ?? "Exit"; // Default to Exit if null (user pressed 0)
    }
    
    private async Task ProcessUserChoice(string choice)
    {
        switch (choice)
        {
            case "Own a Pet":
                await AdoptPet();
                break;
                
            case "Seek Available Pets":
                ViewPets();
                break;
                
            case "Use Item on Pet":
                await UseItem();
                break;
                
            case "Adjust Zoom":
                AdjustZoom();
                break;
                
            case "Author Info":
                DisplayCreatorInfo();
                break;
                
            case "Exit":
                _isRunning = false;
                break;
        }
    }
    
    // Method to adjust zoom level
    private void AdjustZoom()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Adjust Zoom ===");
        Console.ResetColor();
        
        List<string> zoomOptions = new List<string>
        {
            "Small (0.75x)",
            "Normal (1x)",
            "Large (1.25x)",
            "Extra Large (1.5x)"
        };
        
        Menu<string> zoomMenu = new Menu<string>("Select Zoom Level", zoomOptions, option => option);
        string selectedOption = zoomMenu.ShowAndGetSelection();
        
        if (selectedOption == null) return;
        
        // Apply selected zoom
        switch (selectedOption)
        {
            case "Small (0.75x)":
                SetConsoleZoom(1);  // Smaller
                break;
            case "Normal (1x)":
                SetConsoleZoom(2);  // Normal
                break;
            case "Large (1.25x)":
                SetConsoleZoom(3);  // Larger
                break;
            case "Extra Large (1.5x)":
                SetConsoleZoom(4);  // Extra large
                break;
        }
    }
    
    // Method to adopt a pet
    private async Task AdoptPet()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("=== Own a Pet ===");
        Console.ResetColor();
        
        // List of available pet types
        List<PetType> petTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
        
        // Create and display pet type menu
        Menu<PetType> petTypeMenu = new Menu<PetType>("Choose Pet Type", petTypes, type => type.ToString());
        PetType selectedType = petTypeMenu.ShowAndGetSelection();
        
        if (selectedType == 0) return; // User canceled
        
        // Get pet name
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"You selected: {selectedType}");
        Console.ResetColor();
        Console.Write("What should you call for the felow?: ");
        string petName = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(petName))
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Buddy, name your pet, by typing your keyboard >:L");
            Console.ResetColor();
            Console.ReadKey();
            await AdoptPet();
            return;
        }
        
        // Create the appropriate pet type
        Pet newPet = null;
        switch (selectedType)
        {
            case PetType.ChizelDoner:
                newPet = new ChizelDonerPet(petName);
                break;
                
            case PetType.NonchalantRacoon:
                newPet = new NonchalantRacoonPet(petName);
                break;
                
            case PetType.ProgrammerNerdChimpmonk:
                newPet = new ProgrammerNerdChimpmonkPet(petName);
                break;
                
            case PetType.TonyTonyChopper:
                newPet = new TonyTonyChopperPet(petName);
                break;
        }
        
        // Add event handlers for the pet
        newPet.PetDied += OnPetDied;
        
        // Add the pet to the list
        _adoptedPets.Add(newPet);
        
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"Congrats! {petName} the {selectedType}!");
        Console.ResetColor();
        newPet.DisplayPet();
        
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    
    // Method to view pets
    private void ViewPets()
    {
        if (_adoptedPets.Count == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You havent gotten any pets-");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return;
        }
        
        // Create pet menu
        Menu<Pet> petMenu = new Menu<Pet>("Your Pets", _adoptedPets, pet => $"{pet.Name} ({pet.Type})");
        Pet selectedPet = petMenu.ShowAndGetSelection();
        
        if (selectedPet == null) return; // User canceled
        
        // Display the selected pet
        Console.Clear();
        selectedPet.DisplayPet();
        selectedPet.DisplayStats();
        
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    
    // Method to use an item on a pet
    private async Task UseItem()
    {
        if (_adoptedPets.Count == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You don't have any pets to use items on.");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            return;
        }
        
        // Select a pet
        Menu<Pet> petMenu = new Menu<Pet>("Select Pet", _adoptedPets, pet => $"{pet.Name} ({pet.Type})");
        Pet selectedPet = petMenu.ShowAndGetSelection();
        
        if (selectedPet == null) return; // User canceled
        
        // Get compatible items for the selected pet
        List<Item> compatibleItems = ItemDatabase.AllItems
            .Where(item => item.CompatibleWith.Contains(selectedPet.Type))
            .ToList();
        
        if (compatibleItems.Count == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Hmm, no compatible items found for {selectedPet.Name}.");
            Console.ResetColor();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            return;
        }
        
        // Display item menu
        Menu<Item> itemMenu = new Menu<Item>($"Select Item for {selectedPet.Name}", compatibleItems, 
            item => $"{item.Name} (Affects: {item.AffectedStat} +{item.EffectAmount})");
        Item selectedItem = itemMenu.ShowAndGetSelection();
        
        if (selectedItem == null) return; // User canceled
        
        // Use the item on the pet
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Using {selectedItem.Name} on {selectedPet.Name}...");
        Console.ResetColor();
        
        // Wait for the item duration
        int progress = 0;
        int totalSteps = 20; // Number of progress bar steps
        
        for (int i = 0; i < totalSteps; i++)
        {
            await Task.Delay((int)(selectedItem.Duration * 1000 / totalSteps));
            progress = (i + 1) * 100 / totalSteps;
            
            Console.SetCursorPosition(0, Console.CursorTop);
            
            // Colorful progress bar
            Console.Write("Progress: [");
            for (int j = 0; j < totalSteps; j++)
            {
                if (j < i + 1)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("#");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(" ");
                }
            }
            Console.ResetColor();
            Console.Write($"] {progress}%");
        }
        
        Console.WriteLine();
        
        // Apply the effect
        selectedPet.IncreaseStat(selectedItem.AffectedStat, selectedItem.EffectAmount);
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n{selectedPet.Name} liked the {selectedItem.Name}!");
        Console.ResetColor();
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    
    // Method to display creator information
    private void DisplayCreatorInfo()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Creator Information ===");
        Console.ResetColor();
        Console.WriteLine("Name: Kerim Soleman");
        Console.WriteLine("Student Number: 2305045033");
        Console.WriteLine("Note: I have only used AI to help me with the code, I have not used any other resources. Especially had trouble with exporting the build, thx for understanding!");
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
    }
    
    // Event handler for when a pet dies
    private void OnPetDied(object sender, PetEventArgs e)
    {
        Pet deadPet = e.Pet;
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n---------------------------");
        Console.WriteLine($"AH SHIT! {deadPet.Name} has died!");
        Console.WriteLine($"---------------------------\n");
        Console.ResetColor();
    }
} 