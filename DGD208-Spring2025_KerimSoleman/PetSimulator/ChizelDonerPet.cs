using System;
using System.Threading.Tasks;

public class ChizelDonerPet : Pet
{
    private static readonly string _defaultAsciiArt = @"
      /\__/\
     /      \
    |--o--o--|
     \  N   /
      |'''''''|
      | DONER |
      |_______|
    ";
    
    public ChizelDonerPet(string name) : base(name, PetType.ChizelDoner, _defaultAsciiArt)
    {
    }
    
    // Override the IncreaseStat method to give a boost to food-related stats
    public override void IncreaseStat(PetStat stat, int amount)
    {
        // Chizel Doner gets extra benefit from hunger stats (food)
        if (stat == PetStat.Hunger)
        {
            // 50% bonus for food
            amount = (int)(amount * 1.5);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{Name} is super happy with the food! (+{amount})");
            Console.ResetColor();
        }
        
        base.IncreaseStat(stat, amount);
    }
} 