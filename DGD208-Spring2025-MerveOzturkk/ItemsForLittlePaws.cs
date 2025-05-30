using System.Collections.Generic;

//Store product class
public class product
{
    //Product tittle - displayed in game
    public string Title {get; set; }

    //Product classification - determines usage purpose
    public ItemType Classification { get; set; }

    // Compatible animal types
    public List<PetType> AllowedAnimals { get; set; }

    // Affected animal attribute
    public PetStat ModifiedAttribute { get; set; }

    // Modification value
    public int ModificationValue { get; set; }

    // Processing duration (for async usage)
    public float ProcessingDuration { get; set; }
}
