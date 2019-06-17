using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapping
{
    public class RouteMap : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> builder)
        {
            builder.Property(p => p.CountryCode)
                .HasMaxLength(3)
                .IsRequired();

            builder.Property(p => p.Code)
                .HasMaxLength(2)
                .IsRequired();

            builder.Property(p => p.Name)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(p => p.Color)
                .IsRequired();

            builder.Property(p => p.RowStatus)
                .IsRowVersion();
        }
    }
}