using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Evently.Modules.Ticketing.Domain.Orders;
using Evently.Modules.Ticketing.Domain.Events;

namespace Evently.Modules.Ticketing.Infrastructure.Orders;

internal sealed class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id).ValueGeneratedNever();

        builder.HasOne<TicketType>().WithMany().HasForeignKey(oi => oi.TicketTypeId);
    }
}
