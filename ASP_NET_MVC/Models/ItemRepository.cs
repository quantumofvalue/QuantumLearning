using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP_NET_MVC.Models
{
    public interface ItemRepository
    {
        IEnumerable<Item> GetAllItems();
        void CreateNewItem(Item item);
    }
}
