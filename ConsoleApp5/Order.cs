using System.ComponentModel.DataAnnotations.Schema;

namespace KeramikTracker.Shared.DatabaseModels;

public class Order
    {
        public int Id { get; set; }
        public int ExternalId { set; get; }
        public OrderState State { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public ICollection<Table> Tables { get; set; } 
        public override string ToString()
        {
            string tables = "";
            foreach (Table table in Tables)
                tables += table.ToString();
            return $"Order: {Id} - Tables: '{tables}'";
        }
    
        
        public ICollection<Guest> Guests { get; set; }
}


public class OrderTable
{
    int OrderId { get; set; }

}