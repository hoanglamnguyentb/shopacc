using Hinet.Model.Entities;
using System.Collections.Generic;

namespace Hinet.Web.Models
{
    public class ItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsChecked { get; set; }
    }

    public class ItemListViewModel
    {
        public List<ItemModel> Items { get; set; }
    }
}