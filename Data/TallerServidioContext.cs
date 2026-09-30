using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using TallerServidio.Models;

namespace TallerServidio.Data;

public partial class TallerServidioContext : DbContext
{
    public TallerServidioContext(DbContextOptions<TallerServidioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Turno> Turnos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Turno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Turnos__3214EC07D7CE5881");

            entity.Property(e => e.Estado).HasDefaultValue("Pendiente");
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("(sysdatetime())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
