/*
* Game class extends the Item base class and represents a
* video game within the inventory system. It adds platform-
* specific information and overrides display behavior to
* reflect game-related details.
*/
public class Game : Item
{
    public string Platform { get; set; }

    public Game(
        string name,
        string category,
        string subCategory,
        int quantity,
        string condition,
        string notes,
        string platform)
        : base(name, category, subCategory, quantity, condition, notes)
    {
        Platform = platform;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine("=== Game Item ===");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Category: {Category}");
        Console.WriteLine($"SubCategory: {SubCategory}");
        Console.WriteLine($"Platform: {Platform}");
        Console.WriteLine($"Quantity: {Quantity}");
        Console.WriteLine($"Condition: {Condition}");
        Console.WriteLine($"Notes: {Notes}");
        Console.WriteLine();
    }
}