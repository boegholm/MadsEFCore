using System.Xml;

using KeramikTracker.Shared.DatabaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Table
    {
        public int Id { get; set; }
        public int ExternalId { get; set; }
        [StringLength(45)]
        [Column($"{nameof(Table)}_name")]
        public string? Name { get; set; }
        public ICollection<Order> Orders { get; set; }
        public override string ToString()
        {
return $"Id: {Id} - Name: {Name}";
}
}
