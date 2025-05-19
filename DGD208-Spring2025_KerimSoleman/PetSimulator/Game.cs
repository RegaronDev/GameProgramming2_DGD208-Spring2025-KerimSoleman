using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Game
{
    private bool _isRunning;
    private List<Pet> _adoptedPets;
    
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
        
        Console.WriteLine("Thanks for playing!");
    }
    
    private void Initialize()
    {
        Console.WriteLine("Welcome to the Animal Thriving Wild life!");
        Console.WriteLine("Click any key to start..");
        Console.ReadKey();
        Console.Clear();
    }
    
    private string GetUserInput()
    {
        // Create main menu items
        List<string> menuItems = new List<string>
        {
            "Own a Pet",
            "Seek Available Pets",
            "Use Item on Pet",
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
                
            case "Author Info":
                DisplayCreatorInfo();
                break;
                
            case "Exit":
                _isRunning = false;
                break;
        }
    }
    
    // Method to adopt a pet
    private async Task AdoptPet()
    {
        Console.Clear();
        Console.WriteLine("=== Own a Pet ===");
        
        // List of available pet types
        List<PetType> petTypes = Enum.GetValues(typeof(PetType)).Cast<PetType>().ToList();
        
        // Create and display pet type menu
        Menu<PetType> petTypeMenu = new Menu<PetType>("Choose Pet Type", petTypes, type => type.ToString());
        PetType selectedType = petTypeMenu.ShowAndGetSelection();
        
        if (selectedType == 0) return; // User canceled
        
        // Get pet name
        Console.Clear();
        Console.WriteLine($"You selected: {selectedType}");
        Console.Write("What should you call for the felow?: ");
        string petName = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(petName))
        {
            Console.WriteLine("Buddy, name your pet, by typing your keyboard >:L");
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
                
            case PetType.ProgrammerNerd:
                newPet = new ProgrammerNerdPet(petName);
                break;
        }
        
        // Add event handlers for the pet
        newPet.PetDied += OnPetDied;
        
        // Add the pet to the list
        _adoptedPets.Add(newPet);
        
        Console.WriteLine($"Congrats! {petName} the {selectedType}!");
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
            Console.WriteLine("You havent gotten any pets-");
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
            Console.WriteLine("You don't have any pets to use items on.");
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
            Console.WriteLine($"Hmm, no compatible items found for {selectedPet.Name}.");
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
        Console.WriteLine($"Using {selectedItem.Name} on {selectedPet.Name}...");
        
        // Wait for the item duration
        int progress = 0;
        int totalSteps = 20; // Number of progress bar steps
        
        for (int i = 0; i < totalSteps; i++)
        {
            await Task.Delay((int)(selectedItem.Duration * 1000 / totalSteps));
            progress = (i + 1) * 100 / totalSteps;
            
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write($"Progress: [{new string('#', i + 1)}{new string(' ', totalSteps - i - 1)}] {progress}%");
        }
        
        Console.WriteLine();
        
        // Apply the effect
        selectedPet.IncreaseStat(selectedItem.AffectedStat, selectedItem.EffectAmount);
        
        Console.WriteLine($"\n{selectedPet.Name} liked the {selectedItem.Name}!");
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
    
    // Method to display creator information
    private void DisplayCreatorInfo()
    {
        Console.Clear();
        Console.WriteLine("=== Creator Information ===");
        Console.WriteLine("Name: Kerim Soleman");
        Console.WriteLine("Student Number: 2305045033");
        Console.WriteLine("\nPress any key to continue");
        Console.ReadKey();
    }
    
    // Event handler for when a pet dies
    private void OnPetDied(object sender, PetEventArgs e)
    {
        Pet deadPet = e.Pet;
        
        Console.WriteLine($"\n---------------------------");
        Console.WriteLine($"AH SHIT! {deadPet.Name} has died!");
        Console.WriteLine($"---------------------------\n");
    }
} 