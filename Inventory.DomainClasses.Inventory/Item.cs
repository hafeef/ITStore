namespace Inventory.DomainClasses.Inventory
{
    public class Item
    {
        public int ItemID { get; private set; }
        public string Description { get; private set; }
        public string PartNumber { get; private set; }
    }
}
