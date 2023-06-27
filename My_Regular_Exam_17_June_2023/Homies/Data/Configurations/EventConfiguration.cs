using Homies.Data.Common.Constants;
using Homies.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homies.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(ValidationalConstants.EventConstants.EventNameMaxLength);

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(ValidationalConstants.EventConstants.EventDescriptionMaxLength);
        }
    }
}
