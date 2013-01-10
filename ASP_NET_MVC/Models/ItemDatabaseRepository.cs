using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASP_NET_MVC.Models
{
    public class ItemDatabaseRepository : ItemRepository
    {
        private ItemDBContext _db = new ItemDBContext();

        public IEnumerable<Item> GetAllItems()
        {
            return _db.Items.ToList();
        }

        public void CreateNewItem(Item item)
        {
            _db.Items.Add(item);
            _db.SaveChanges();
        }
    }
}
