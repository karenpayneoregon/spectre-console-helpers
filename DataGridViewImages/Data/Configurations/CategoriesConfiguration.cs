﻿using DataGridViewImages.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataGridViewImages.Data.Configurations;

public partial class CategoriesConfiguration : IEntityTypeConfiguration<Categories>
{
    public void Configure(EntityTypeBuilder<Categories> entity)
    {
        entity.HasKey(e => e.CategoryId);

        entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

        entity.Property(e => e.CategoryName)
            .IsRequired()
            .HasMaxLength(15);

        entity.Property(e => e.Description).HasColumnType("ntext");

        entity.Property(e => e.Picture).HasColumnType("image");

        OnConfigurePartial(entity);
    }

    partial void OnConfigurePartial(EntityTypeBuilder<Categories> entity);
}