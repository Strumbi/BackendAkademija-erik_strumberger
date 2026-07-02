using BackendAkademija.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackendAkademija.Infrastructure.Persistence.Configurations;

public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshTokenEntity>
{
    public void Configure(EntityTypeBuilder<RefreshTokenEntity> entity)
    {
        entity.HasKey(e => e.Id);
        
        entity.Property(e => e.Token)
            .IsRequired()
            .HasMaxLength(512);
        
        entity.HasIndex(e => e.Token)
            .IsUnique();
        
        entity.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(100);
        
        entity.Property(e => e.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
    }
}