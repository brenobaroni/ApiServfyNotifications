using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiServfyNotifications.Domain;

namespace ApiServfyNotifications.Data.Context.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(e => e.Id).HasName("User_pkey");

            user.ToTable("User");

            user.HasIndex(e => e.Email, "User_email_key").IsUnique();

            user.HasIndex(e => e.Id, "User_id_key").IsUnique();

            user.HasIndex(e => e.Idpid, "User_idpid_key").IsUnique();

            user.Property(e => e.Id).HasColumnName("id");
            user.Property(e => e.Active)
                .HasDefaultValue(true)
                .HasColumnName("active");
            user.Property(e => e.Avatar).HasColumnName("avatar");
            user.Property(e => e.ContactNumber)
                .HasDefaultValueSql("''::text")
                .HasColumnName("contactNumber");
            user.Property(e => e.ContactNumberSecondary)
                .HasDefaultValueSql("''::text")
                .HasColumnName("contactNumberSecondary");
            user.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("created_at");
            user.Property(e => e.EditedAt)
                .HasColumnType("timestamp(3) without time zone")
                .HasColumnName("edited_at");
            user.Property(e => e.Email).HasColumnName("email");
            user.Property(e => e.Idpid)
                .HasDefaultValueSql("''::text")
                .HasColumnName("idpid");
            user.Property(e => e.Name).HasColumnName("name");
            user.Property(e => e.PersonalIdNumber)
                .HasDefaultValueSql("''::text")
                .HasColumnName("personalIdNumber");
        }
    }
}
