namespace Homework_2023_10_14.Models
{
    public interface ICatalogue
    {
        public bool Create(Item item);
        public KeyValuePair<Guid, Item> Read(Guid key);
        public KeyValuePair<Guid, Item>[] ReadAll();
        public bool Update(Guid key, Item Value);
        public bool Delete(Guid key);
        public void Clear();
    }
}
