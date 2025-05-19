using System;
using System.Threading;
using System.Threading.Tasks;

public class ChizelDonerPet : Pet
{
    private static readonly string _defaultAsciiArt = @"
 /\     /\
{  `---'  }
{  O   O  }
~~>  V  <~~
 \  \|/  /
  `-----'__
  /     \  `^\_
 {       }\ |\_\_   W
 |  \_/  |/ /  \_\_( )
  \__/  /(_E     \__/
    (  /
     MM
    ";
    
    private static readonly string _temporaryAsciiArt = @"
           .'\   /`.
         .'.-.`-'.-.`.
    ..._:   .-. .-.   :_...
  .'    '-.(o ) (o ).-'    `.
 :  _    _ _`~(_)~`_ _    _  :
:  /:   ' .-=_   _=-. `   ;\  :
:   :|-.._  '     `  _..-|:   :
 :   `:| |`:-:-.-:-:'| |:'   :
  `.   `.| | | | | | |.'   .'
    `.   `-:_| | |_:-'   .'
      `-._   ````    _.-'
          ``-------''
    ";
    
    private static readonly string[] _turkishFoods = new string[] 
    {
        "kebab",
        "baklava",
        "lahmacun",
        "döner",
        "pide",
        "börek",
        "mantı",
        "iskender",
        "simit",
        "köfte"
    };
    
    private Timer _messageTimer;
    private bool _showingTemporaryArt = false;
    
    public ChizelDonerPet(string name) : base(name, PetType.ChizelDoner, _defaultAsciiArt)
    {
        // Create timer for showing temporary message but don't start it yet
        _messageTimer = new Timer(ShowTemporaryMessage, null, Timeout.Infinite, Timeout.Infinite);
        
        // Start the timer with a random delay between 15-30 seconds
        Random random = new Random();
        int initialDelay = random.Next(15000, 30000);
        _messageTimer.Change(initialDelay, Timeout.Infinite);
    }
    
    private void ShowTemporaryMessage(object state)
    {
        if (!IsAlive) return;
        
        // If already showing temporary art, switch back
        if (_showingTemporaryArt)
        {
            AsciiArt = _defaultAsciiArt;
            _showingTemporaryArt = false;
            
            // Schedule next change
            Random random = new Random();
            int nextDelay = random.Next(15000, 30000);
            _messageTimer.Change(nextDelay, Timeout.Infinite);
            return;
        }
        
        // Get random Turkish food
        Random foodRandom = new Random();
        string randomFood = _turkishFoods[foodRandom.Next(_turkishFoods.Length)];
        
        // Change to temporary art
        AsciiArt = _temporaryAsciiArt;
        _showingTemporaryArt = true;
        
        // Show message
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\n{Name} says: Hello there fellow- kinda craving some {randomFood} right now...");
        Console.ResetColor();
        
        // Schedule change back to normal art after 5 seconds
        _messageTimer.Change(5000, Timeout.Infinite);
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