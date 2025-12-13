/*
* Card class extends the Item base class and represents a
* collectible card within the inventory. It adds card-specific
* properties and overrides base functionality to display
* relevant card details.
*/
public class Card : Item
{
    public string CardSet { get; set; }

    public Card(
        string name,
        string category,
        string subCategory,
        int quantity,
        string condition,
        string notes,
        string cardSet)
        : base(name, category, subCategory, quantity, condition, notes)
    {
        CardSet = cardSet;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine("=== Card Item ===");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"SubCategory: {SubCategory}");
        Console.WriteLine($"Set: {CardSet}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Condition: {Condition}");
        Console.WriteLine($"Notes: {Notes}");
        Console.WriteLine();
    }
}