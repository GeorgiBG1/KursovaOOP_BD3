using System;
using System.Collections.Generic;

#nullable disable

namespace KursovaNet5BD.Models
{
    public partial class Item
    {
        public Item()
        {
            GameTypeForbiddenItems = new HashSet<GameTypeForbiddenItem>();
            UserGameItems = new HashSet<UserGameItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int ItemTypeId { get; set; }
        public int StatisticId { get; set; }
        public decimal Price { get; set; }
        public int MinLevel { get; set; }

        public virtual ItemType ItemType { get; set; }
        public virtual Statistic Statistic { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<GameTypeForbiddenItem> GameTypeForbiddenItems { get; set; }
        public virtual ICollection<UserGameItem> UserGameItems { get; set; }
        public override string ToString()
        {
            return $"Item information:\n" +
                $"Id: {Id}\n" +
                $"Name: {Name}\n" +
                $"Price: {Price:F2}\n" +
                $"Min level: {MinLevel}\n";
        }
    }
}
