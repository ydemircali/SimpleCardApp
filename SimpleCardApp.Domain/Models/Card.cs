using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCardApp.Domain.Models
{
    public class Card
    {
        public int UserId { get; set; }
        public List<CardProduct> CardProducts { get; set; }
    }

    public class CardProduct
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
