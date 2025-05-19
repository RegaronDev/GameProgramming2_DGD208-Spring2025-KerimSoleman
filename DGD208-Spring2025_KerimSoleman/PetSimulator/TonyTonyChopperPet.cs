using System;
using System.Threading;
using System.Threading.Tasks;

public class TonyTonyChopperPet : Pet
{
    private static readonly string _defaultAsciiArt = @"
   ┌───────┐   
   |       |   
#  |  \/   |  #
 ##|  /\   |## 
┌──┘       └──┐
───────────────
  \ @    @  /  
   |'_+_'  |   
    ───────    
     /     \   
    |       |  
    |       |  
    ";
    
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
    
    private Timer _quoteTimer;
    
    public TonyTonyChopperPet(string name) : base(name, PetType.TonyTonyChopper, _defaultAsciiArt)
    {
        _quoteTimer = new Timer(SpeakChopperQuote, null, 8000, 18000);
    }
    
    private void SpeakChopperQuote(object state)
    {
        if (!IsAlive) return;
        
        Random random = new Random();
        int quoteIndex = random.Next(_chopperQuotes.Length);
        
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
            
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line)) continue;
            
            // For line 3 with "\/", print characters individually with specific colors
            if (i == 3)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    // Color the # symbols brown (tags)
                    if (line[j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Brown
                    }
                    // Color the \/ symbols white (eyes)
                    else if (j >= 5 && j <= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    // Color the rest of the hat red
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(line[j]);
                }
                Console.WriteLine();
            }
            // For line 4 with "/\", print characters individually with specific colors
            else if (i == 4)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    // Color the ## symbols brown (tags)
                    if (line[j] == '#')
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow; // Brown
                    }
                    // Color the /\ symbols white (eyes)
                    else if (j >= 5 && j <= 10)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    // Color the rest of the hat red
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.Write(line[j]);
                }
                Console.WriteLine();
            }
            // Hat part (red)
            else if (i >= 1 && i <= 5 && i != 3 && i != 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(line);
            }
            // Face and body (brown/dark yellow)
            else if (i > 5)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow; // Brown
                Console.WriteLine(line);
            }
        }
        
        Console.ResetColor();
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