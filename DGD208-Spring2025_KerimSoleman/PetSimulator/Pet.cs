using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Pet
{
    // Pet properties
    public string Name { get; private set; }
    public PetType Type { get; private set; }
    public Dictionary<PetStat, int> Stats { get; private set; }
    public bool IsAlive { get; private set; } = true;
    
    // ASCII art for the pet
    public string AsciiArt { get; protected set; }
    
    // Events
    public event EventHandler<PetStatEventArgs> StatChanged;
    public event EventHandler<PetEventArgs> PetDied;
    
    // Constructor
    public Pet(string name, PetType type, string asciiArt)
    {
        Name = name;
        Type = type;
        AsciiArt = asciiArt;
        
        // Initialize stats to 50
        Stats = new Dictionary<PetStat, int>
        {
            { PetStat.Hunger, 50 },
            { PetStat.Sleep, 50 },
            { PetStat.Fun, 50 }
        };
        
        // Start the stat decrease process
        StartStatDecrease();
    }
    
    // Method to decrease stats over time
    private async void StartStatDecrease()
    {
        while (IsAlive)
        {
            await Task.Delay(5000); // Decrease stats every 5 seconds
            
            if (!IsAlive) return; // Check if pet is still alive
            
            DecreaseStat(PetStat.Hunger, 1);
            DecreaseStat(PetStat.Sleep, 1);
            DecreaseStat(PetStat.Fun, 1);
        }
    }
    
    // Method to decrease a stat
    private void DecreaseStat(PetStat stat, int amount)
    {
        if (!IsAlive) return;
        
        Stats[stat] -= amount;
        
        // Make sure stat doesn't go below 0
        if (Stats[stat] < 0)
            Stats[stat] = 0;
        
        // Fire the stat changed event
        StatChanged?.Invoke(this, new PetStatEventArgs(stat, Stats[stat]));
        
        // Check if pet died
        if (Stats[stat] == 0)
        {
            IsAlive = false;
            PetDied?.Invoke(this, new PetEventArgs(this));
        }
    }
    
    // Method to increase a stat (used when feeding, playing, etc.)
    public virtual void IncreaseStat(PetStat stat, int amount)
    {
        if (!IsAlive) return;
        
        Stats[stat] += amount;
        
        // Make sure stat doesn't go above 100
        if (Stats[stat] > 100)
            Stats[stat] = 100;
        
        // Fire the stat changed event
        StatChanged?.Invoke(this, new PetStatEventArgs(stat, Stats[stat]));
    }
    
    // Display pet's ASCII art
    public void DisplayPet()
    {
        Console.WriteLine($"=== {Name} ({Type}) ===");
        Console.WriteLine(AsciiArt);
        Console.WriteLine();
    }
    
    // Display pet stats
    public void DisplayStats()
    {
        Console.WriteLine($"=== {Name}'s Stats ===");
        Console.WriteLine($"Hunger: {Stats[PetStat.Hunger]}");
        Console.WriteLine($"Sleep: {Stats[PetStat.Sleep]}");
        Console.WriteLine($"Fun: {Stats[PetStat.Fun]}");
        Console.WriteLine();
    }
}

// Event arguments for stat change events
public class PetStatEventArgs : EventArgs
{
    public PetStat StatType { get; private set; }
    public int NewValue { get; private set; }
    
    public PetStatEventArgs(PetStat statType, int newValue)
    {
        StatType = statType;
        NewValue = newValue;
    }
}

// Event arguments for pet events
public class PetEventArgs : EventArgs
{
    public Pet Pet { get; private set; }
    
    public PetEventArgs(Pet pet)
    {
        Pet = pet;
    }
} 