using Library.Data.Common.Constants;
using Library.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Library.Data.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);

            builder
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .IsRequired();

            builder.Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(ValidationalConstants.BookConstants.BookTitleMaxLength);

            builder.Property(b => b.Author)
                .IsRequired()
                .HasMaxLength(ValidationalConstants.BookConstants.BookAuthorMaxLength);

            builder.Property(b => b.Description)
                .IsRequired()
                .HasMaxLength(ValidationalConstants.BookConstants.BookDescriptionMaxLength);

            builder.Property(b => b.Rating)
                .IsRequired()
                .HasPrecision(18, 2);
        }
    }
}
