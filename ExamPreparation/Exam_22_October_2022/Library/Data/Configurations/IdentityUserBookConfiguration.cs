using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configurations
{
    public class IdentityUserBookConfiguration : IEntityTypeConfiguration<IdentityUserBook>
    {
        public void Configure(EntityTypeBuilder<IdentityUserBook> builder)
        {
            builder.HasKey(iub => new { iub.CollectorId, iub.BookId });
        }
    }
}
