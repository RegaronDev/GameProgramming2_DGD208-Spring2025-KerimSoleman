using System.Collections.Generic;

public static class ItemDatabase
{
    public static List<Item> AllItems = new List<Item>
    {
        // Foods for Chizel Doner
        new Item { 
            Name = "Kebab", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ChizelDoner }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 30,
            Duration = 3.0f  // Takes 3 seconds to eat
        },
        new Item { 
            Name = "Lahmacun", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ChizelDoner }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 25,
            Duration = 2.5f
        },
        new Item { 
            Name = "Baklava", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ChizelDoner }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 15,
            Duration = 1.5f
        },
        
        // Foods for Nonchalant Racoon
        new Item { 
            Name = "Trash Delicacy", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.NonchalantRacoon }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 20,
            Duration = 2.0f
        },
        new Item { 
            Name = "Stolen Sandwich", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.NonchalantRacoon }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 25,
            Duration = 2.5f
        },
        
        // Foods for Programmer Nerd Chimpmonk
        new Item { 
            Name = "Energy Drink", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ProgrammerNerdChimpmonk }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 10,
            Duration = 1.0f
        },
        new Item { 
            Name = "Pizza", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ProgrammerNerdChimpmonk }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 30,
            Duration = 3.0f
        },
        new Item { 
            Name = "Instant Noodles", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ProgrammerNerdChimpmonk }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 15,
            Duration = 2.0f
        },
        
        // Foods for Tony Tony Chopper
        new Item { 
            Name = "Cotton Candy", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 25,
            Duration = 2.0f
        },
        new Item { 
            Name = "Rumble Ball", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 35,
            Duration = 3.0f
        },
        new Item { 
            Name = "Medical Herbs", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 30,
            Duration = 2.5f
        },
        
        // Universal Foods
        new Item { 
            Name = "Cookies", 
            Type = ItemType.Food, 
            CompatibleWith = new List<PetType> { PetType.ChizelDoner, PetType.ProgrammerNerdChimpmonk, PetType.NonchalantRacoon, PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Hunger, 
            EffectAmount = 10,
            Duration = 1.0f
        },
        
        // Toys
        new Item { 
            Name = "Persian Carpet", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.ChizelDoner }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 20,
            Duration = 3.0f
        },
        new Item { 
            Name = "Shiny Object", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.NonchalantRacoon }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 25,
            Duration = 3.0f
        },
        new Item { 
            Name = "Mechanical Keyboard", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.ProgrammerNerdChimpmonk }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 30,
            Duration = 4.0f
        },
        new Item { 
            Name = "Doctor's Kit", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 30,
            Duration = 3.0f
        },
        new Item { 
            Name = "Straw Hat", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Fun, 
            EffectAmount = 25,
            Duration = 2.0f
        },
        
        // Sleep Items
        new Item { 
            Name = "Turkish Hammam", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.ChizelDoner }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 25,
            Duration = 4.0f
        },
        new Item { 
            Name = "Cozy Hideout", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.NonchalantRacoon }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 30,
            Duration = 5.0f
        },
        new Item { 
            Name = "Dark Mode IDE", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.ProgrammerNerdChimpmonk }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 20,
            Duration = 3.0f
        },
        new Item { 
            Name = "Infirmary Bed", 
            Type = ItemType.Toy, 
            CompatibleWith = new List<PetType> { PetType.TonyTonyChopper }, 
            AffectedStat = PetStat.Sleep, 
            EffectAmount = 35,
            Duration = 3.0f
        }
    };
} 