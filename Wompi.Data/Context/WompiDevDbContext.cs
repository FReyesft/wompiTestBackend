using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Wompi.Core.EntityModels;

namespace Wompi.Data.Context;

public partial class WompiDevDbContext : DbContext
{
    public WompiDevDbContext()
    {
    }

    public WompiDevDbContext(DbContextOptions<WompiDevDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GeLink> GeLinks { get; set; }

    public virtual DbSet<GeTransaction> GeTransactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=NEYRON_GEUS\\SQLEXPRESS; database=WompiDevDB; user=sa; password=123456789; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GeLink>(entity =>
        {
            entity.HasKey(e => e.IdGeLink).HasName("PK_LinksHistory_IdGeLink");

            entity.ToTable("GeLink");

            entity.Property(e => e.IdLinkWompi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JsonRequest)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.JsonWompiResponse)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.TransactionState)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeTransaction>(entity =>
        {
            entity.HasKey(e => e.IdGeTransaction).HasName("PK_TransactionHistory_IdGeTransaction");

            entity.ToTable("GeTransaction");

            entity.Property(e => e.IdLinkWompi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IdTransactionWompi)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.JsonWompiResponse)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.TransactionState)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
