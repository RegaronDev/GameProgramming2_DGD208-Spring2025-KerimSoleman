using System;
using System.Threading.Tasks;

public class NonchalantRacoonPet : Pet
{
    private static readonly string _defaultAsciiArt = @"
      /\___/\
     /       \
    |  -   -  |  
    >\   v   /<
      \     /
       \___/
      /   \
    ";
    
    public NonchalantRacoonPet(string name) : base(name, PetType.NonchalantRacoon, _defaultAsciiArt)
    {
    }
    
    // Override the IncreaseStat method for this pet type
    public override void IncreaseStat(PetStat stat, int amount)
    {
        // Nonchalant Racoon is chill and gets benefits for sleep
        if (stat == PetStat.Sleep)
        {
            // 30% bonus for sleep
            amount = (int)(amount * 1.3);
            Console.WriteLine($"{Name} is peacefully resting... (+{amount})");
        }
        
        base.IncreaseStat(stat, amount);
    }
} 