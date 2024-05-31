using FinaFlow.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinaFlow.Api.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable("Transaction");

            builder.HasKey(c => c.Id);

            builder.Property(x => x.Type)
                   .IsRequired()
                   .HasColumnType("SMALLINT");

            builder.Property(x => x.Amount)
                   .IsRequired()
                   .HasColumnType("MONEY");

            builder.Property(x => x.CreatedAt)
                   .IsRequired();

            builder.Property(x => x.PaidOrReceivedAt)
                   .IsRequired(false);
            
            builder.Property(x => x.UserId)
                   .IsRequired(false)
                   .HasColumnType("VARCHAR")
                   .HasMaxLength(160);
        }
    }
}
