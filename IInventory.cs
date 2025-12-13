/*
* IInventory interface defines the common properties and behavior
* required for all inventory-related objects. It enforces a shared
* structure by requiring identifying fields and a method for
* displaying item details.
*/
public interface IInventory
{
    string Name { get; set; }
    string Category { get; set; }
    string SubCategory { get; set; }

    void DisplayDetails();
}