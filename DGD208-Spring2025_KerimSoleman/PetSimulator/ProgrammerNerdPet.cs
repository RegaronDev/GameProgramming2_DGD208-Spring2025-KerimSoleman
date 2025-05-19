using System;
using System.Threading;
using System.Threading.Tasks;

public class ProgrammerNerdPet : Pet
{
    private static readonly string _defaultAsciiArt = @"
       _____
      /     \
     | -   - |
     |   O   |
     |  \_/  |
      \_____/
      /|   |\
     / |___| \
    ";
    
    // Array of nerdy facts to randomly spout
    private static readonly string[] _nerdyFacts = new string[]
    {
        "Um well first of all~ The first computer bug was an actual bug, a moth found in 1947.",
        "Actually, according to my calculations, tihi~ The average programmer writes 10-50 lines of code per day.",
        "Um well first of all~ In binary code, the number 42 is 101010.",
        "Actually, according to my calculations, tihi~ The most common password is '123456'.",
        "Um well first of all~ The first computer programmer was a woman, Ada Lovelace.",
        "Actually, according to my calculations, tihi~ The term 'bug' was popularized by Grace Hopper in 1947.",
        "Um well first of all~ The term 'spam' comes from a Monty Python sketch.",
        "Actually, according to my calculations, tihi~ A 'jiffy' is an actual unit of time: 1/100th of a second.",
        "Um well first of all~ The first computer mouse was made of wood.",
        "Actually, according to my calculations, tihi~ There are approximately 700 programming languages.",
        "I Just did a fartie HAHHAHAHA xD"
    };
    
    // Timer for spewing facts
    private Timer _factTimer;
    
    // Constructor
    public ProgrammerNerdPet(string name) : base(name, PetType.ProgrammerNerd, _defaultAsciiArt)
    {
        // Start the fact spewing timer (every 4 seconds)
        _factTimer = new Timer(SpeakNerdyFact, null, 4000, 4000);
    }
    
    // Method to have the pet speak a nerdy fact
    private void SpeakNerdyFact(object state)
    {
        if (!IsAlive) return;
        
        // Get a random fact
        Random random = new Random();
        int factIndex = random.Next(_nerdyFacts.Length);
        
        // Display the fact
        Console.WriteLine($"\n{Name} says: {_nerdyFacts[factIndex]}\n");
    }
    
    // Override the IncreaseStat method for this pet type
    public override void IncreaseStat(PetStat stat, int amount)
    {
        // Programmer Nerd gets an extra benefit for fun stats
        if (stat == PetStat.Fun)
        {
            // 40% bonus for fun
            amount = (int)(amount * 1.4);
            Console.WriteLine($"{Name} is having a blast with intellectual stimulation! (+{amount})");
        }
        
        base.IncreaseStat(stat, amount);
    }
} 