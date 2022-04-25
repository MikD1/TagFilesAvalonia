using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TagFiles.Explorer.Models;

namespace TagFiles.Explorer.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<FileMetadata> FilesMetadata => Set<FileMetadata>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FileMetadata>().HasKey(x => x.Path);
        modelBuilder.Entity<FileMetadata>()
            .Property(x => x.Tags)
            .HasConversion(
                x => string.Join(';', x),
                x => x.Split(';', StringSplitOptions.None).ToList());

        // modelBuilder.Entity<FileMetadata>()
        //     .Property(x => x.Preview)
        //     .HasColumnType("BLOB");
    }
}