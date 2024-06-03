﻿using Evently.Modules.Ticketing.Domain.Payments;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Evently.Modules.Ticketing.Domain.Orders;

namespace Evently.Modules.Ticketing.Infrastructure.Payments;


internal sealed class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasOne<Order>().WithMany().HasForeignKey(p => p.OrderId);

        builder.HasIndex(p => p.TransactionId).IsUnique();
    }
}
