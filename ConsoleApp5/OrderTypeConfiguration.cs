
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using KeramikTracker.Shared.DatabaseModels;

public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> b)
    {
        b.HasMany(v=>v.Tables).WithMany(t => t.Orders);
        b.HasMany(v => v.Guests).WithMany(v=>v.Orders);
        b.Property(v => v.State).HasColumnName("orderstate");
        
    }
}