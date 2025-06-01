using System.Collections.Generic;
using LittlePawsGame;

// Store product class  
public class Product(string title, ItemTypeForLittlePaws classification, List<PetTypeForLittlePaws> allowedAnimals, PetStatForLittlePaws modifiedAttribute, int modificationValue, float processingDuration)
{
    // Product title - displayed in game  
    public required string Title { get; set; } = title;

    // Product classification - determines usage purpose  
    public required ItemTypeForLittlePaws Classification { get; set; } = classification;

    // Compatible animal types  
    public required List<LittlePawsGame.PetTypeForLittlePaws> AllowedAnimals { get; set; } = allowedAnimals;

    // Affected animal attribute  
    public required PetStatForLittlePaws ModifiedAttribute { get; set; } = modifiedAttribute;

    // Modification value  
    public required int ModificationValue { get; set; } = modificationValue;

    // Processing duration (for async usage)  
    public required float ProcessingDuration { get; set; } = processingDuration;
}

