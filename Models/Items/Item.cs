using System;

namespace Boomerang.Models
{
    public class Item
    {
        public int ItemId { get; set; }
        //Stores User ID
        public int BelongsTo { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
