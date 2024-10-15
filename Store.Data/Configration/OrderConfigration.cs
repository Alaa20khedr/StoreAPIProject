using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.OrderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Data.Configration
{
    public class OrderConfigration : IEntityTypeConfiguration<Orders>
    {
        public void Configure(EntityTypeBuilder<Orders> builder)
        {
            builder.OwnsOne(order => order.ShippingAddress,
                x =>
                {
                    x.WithOwner();
                });
            builder.HasMany(order=>order.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }

    }
}
