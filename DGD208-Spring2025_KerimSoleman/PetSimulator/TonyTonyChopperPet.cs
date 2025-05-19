using System;
using System.Threading;
using System.Threading.Tasks;

public class TonyTonyChopperPet : Pet
{
    private static readonly string _defaultAsciiArt = @"
⠀⠀⠀⠀⢀⣀⣀⣀⣀⡀⠀⠀⠀⠀⠀
⢀⠌⡇⠉⠁⠀⣀⠀⣀⠈⠁⢲⢰⠆⠀
⠀⠀⠃⡆⠀⠈⡦⠉⠡⠓⠀⢀⢸⣼⠸
⠘⢤⠼⠃⠐⠚⠒⠓⠒⠃⠀⠼⡆⡡⠂
⠀⢐⠊⠡⠒⣮⡏⠉⢹⣾⢫⠒⠠⡅⠀
⠀⠀⠈⠐⠓⠭⣠⣻⣎⡩⠃⠀⠒⠁⠀
⠀⠀⠀⢀⠔⠟⠀⠀⠈⢷⠄⠀⠀⠀⠀
⠀⠀⠀⠈⠊⠆⠀⠀⠀⠜⠶⠃⠀⠀⠀
⠀⠀⠀⠀⠀⣽⣫⠉⣗⣥⠀⠀⠀⠀⠀
    ";
    
    // Array of One Piece quotes Chopper might say
    private static readonly string[] _chopperQuotes = new string[]
    {
        "I'm not happy you called me cute, you jerk!~ tihi",
        "I'm a monster, but I'm a monster who wants to help people!",
        "I'm not a raccoon dog, I'm a reindeer! See? I have horns!",
        "Being alone is more painful than getting hurt!",
        "When do you think people die? When they're shot? No. When they're ravaged by disease? No. It's when they're forgotten!",
        "I won't be a monster that hurts people... I want to be a monster that helps people!",
        "I will become a doctor who can cure anything!",
        "Cotton candy~! I love cotton candy~!",
        "I'm not a pet! I'm a pirate!",
        "I'm gonna be the best doctor in the world!"
    };
    
    // Timer for speaking quotes
    private Timer _quoteTimer;
    
    // Constructor
    public TonyTonyChopperPet(string name) : base(name, PetType.TonyTonyChopper, _defaultAsciiArt)
    {
        // Start the quote timer (every 18 seconds)
        _quoteTimer = new Timer(SpeakChopperQuote, null, 8000, 18000);
    }
    
    // Method to have Chopper speak a quote
    private void SpeakChopperQuote(object state)
    {
        if (!IsAlive) return;
        
        // Get a random quote
        Random random = new Random();
        int quoteIndex = random.Next(_chopperQuotes.Length);
        
        // Display the quote with color
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n{Name} says: {_chopperQuotes[quoteIndex]}\n");
        Console.ResetColor();
    }
    
    // Override the display method to add colored hat
    public override void DisplayPet()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"=== {Name} ({Type}) ===");
        
        // Split ASCII art lines to colorize specific parts
        string[] lines = AsciiArt.Split('\n');
        for (int i = 0; i < lines.Length; i++)
        {
            string line = lines[i];
            
            // Color the hat (first few lines) red
            if (i >= 1 && i <= 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            // Color the body brown
            else if (i > 4)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
            }
            
            Console.WriteLine(line);
            Console.ResetColor();
        }
        
        Console.WriteLine();
    }
    
    // Override GetPetColor to return brown as default
    protected override ConsoleColor GetPetColor()
    {
        return ConsoleColor.DarkYellow;  // Brown color for Chopper
    }
    
    // Override the IncreaseStat method for this pet type
    public override void IncreaseStat(PetStat stat, int amount)
    {
        // Chopper gets extra benefit for fun and health stats
        if (stat == PetStat.Fun)
        {
            Random random = new Random();
            
            if (random.Next(10) > 6)  // 30% chance
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{Name} dances happily doing his chopsticks dance!");
                Console.ResetColor();
            }
        }
        
        // Special response to cotton candy
        if (stat == PetStat.Hunger && amount >= 20)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"{Name} exclaims: Is this cotton candy?! I love cotton candy!");
            Console.ResetColor();
            
            // Extra bonus if it's cotton candy (we can pretend)
            amount = (int)(amount * 1.3);
        }
        
        base.IncreaseStat(stat, amount);
    }
} 