using Microsoft.AspNetCore.Routing.Template;
using System.Net.Mime;
using Homework_5.Week;
///
namespace Homework_5.Catalogue
{
    public interface ICatalogue
    {
        public bool Create(Item item);
        public KeyValuePair<Guid, Item> Read(Guid key);
        public KeyValuePair<Guid, Item>[] ReadAll();
        public KeyValuePair<Guid, Item> Read(IWeek weekDiscount, Guid key);
        public KeyValuePair<Guid, Item>[] ReadAll(IWeek weekDiscount);
        public bool Update(Guid key, Item Value);
        public bool Delete(Guid key);
        public void Clear();
    }
}
