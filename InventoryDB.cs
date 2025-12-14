/*
* Handles SQLite database operations for inventory items,
* including table creation and CRUD operations.
*/
using System.Data.SQLite;
using System.Collections.Generic;

public class InventoryDb {
    public static void CreateTable(SQLiteConnection conn) {
        string sql =
            "CREATE TABLE IF NOT EXISTS Inventory (" + "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "Name TEXT, " + "Category TEXT, " + "SubCategory TEXT, " + "Quantity INTEGER, " +
            "Condition TEXT, " + "Notes TEXT, " + "ItemType TEXT, " + "Extra TEXT);";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void AddItem(SQLiteConnection conn, Item item, string itemType, string extra) {
        string sql =
            "INSERT INTO Inventory (Name, Category, SubCategory, Quantity, Condition, Notes, ItemType, Extra) " +
            "VALUES (@name, @category, @subcategory, @quantity, @condition, @notes, @itemtype, @extra);";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        cmd.Parameters.AddWithValue("@name", item.Name);
        cmd.Parameters.AddWithValue("@category", item.Category);
        cmd.Parameters.AddWithValue("@subcategory", item.SubCategory);
        cmd.Parameters.AddWithValue("@quantity", item.Quantity);
        cmd.Parameters.AddWithValue("@condition", item.Condition);
        cmd.Parameters.AddWithValue("@notes", item.Notes);
        cmd.Parameters.AddWithValue("@itemtype", itemType);
        cmd.Parameters.AddWithValue("@extra", extra);

        cmd.ExecuteNonQuery();
    }

    public static List<Item> GetAllItems(SQLiteConnection conn) {
        List<Item> items = new List<Item>();
        string sql = "SELECT * FROM Inventory";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read()) {
            string itemType = rdr.GetString(7);
            string extra = rdr.GetString(8);

            if (itemType == "Card") {
                items.Add(new Card(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3),
                    rdr.GetInt32(4), rdr.GetString(5), rdr.GetString(6), extra));
            } else if (itemType == "Game") {
                items.Add(new Game( rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), 
                    rdr.GetInt32(4), rdr.GetString(5), rdr.GetString(6), extra));
            } else if (itemType == "Miniature") {
                items.Add(new Miniature(rdr.GetString(1), rdr.GetString(2), rdr.GetString(3),
                    rdr.GetInt32(4), rdr.GetString(5), rdr.GetString(6), bool.Parse(extra)));
            }
        }
        return items;
    }

    public static List<Collection> LoadCollections(SQLiteConnection conn) {
        Dictionary<string, Collection> collections = new Dictionary<string, Collection>();

        string sql = "SELECT * FROM Inventory";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read()) {
            string name = rdr.GetString(1);
            string category = rdr.GetString(2);
            string subCategory = rdr.GetString(3);
            int quantity = rdr.GetInt32(4);
            string condition = rdr.GetString(5);
            string notes = rdr.GetString(6);
            string itemType = rdr.GetString(7);
            string extra = rdr.GetString(8);

            string key = $"{category}:{subCategory}";
        
            if (!collections.ContainsKey(key)) {
                collections[key] = new Collection(category, subCategory);
            }

            Item item = null;

            if (itemType == "Card") {
                item = new Card(name, category, subCategory, quantity, condition, notes, extra);
            } else if (itemType == "Game") {
                item = new Game(name, category, subCategory, quantity, condition, notes, extra);
            } else if (itemType == "Miniature") {
                item = new Miniature(name, category, subCategory, quantity, condition, notes, bool.Parse(extra));
            }

            if (item != null) {
                collections[key].AddItem(item);
            }
        }

        return collections.Values.ToList();
    }
}
