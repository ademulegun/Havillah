using Havillah.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Havillah.Persistense.DbConfigurations
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {

        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasColumnType("nvarchar(300)").IsRequired(true);
            builder.Property(x => x.Expenditure).HasPrecision(18, 2).IsRequired(true);
            builder.Property(x => x.ExpenditureDate).HasColumnType("Datetime2").IsRequired(true);
            builder.Property(x => x.ContractedBy).HasColumnType("nvarchar").IsRequired(false);
            builder.Property(x => x.Description).HasColumnType("nvarchar(1000)").IsRequired(true);
            builder.HasMany(x => x.Entities).WithOne("Datetime2").HasForeignKey(x => x.ExpenseId);
        }
    }
}
