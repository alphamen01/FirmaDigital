using System;
using System.Collections.Generic;
using AccesoDatos.Models;
using Microsoft.EntityFrameworkCore;

namespace AccesoDatos.Context;

public partial class PruebaContext : DbContext
{
    

    public PruebaContext(DbContextOptions<PruebaContext> options) : base(options)
    {
    }

    public virtual DbSet<Firmadigital> Firmadigitals { get; set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseNpgsql("Host=postgresql.cttk8gyj8bcf.us-east-1.rds.amazonaws.com;Database=prueba;Username=postgres;Password=Lesg2022");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Firmadigital>(entity =>
        {
            entity.HasKey(e => e.IdFirma).HasName("firmadigital_pkey");

            entity.ToTable("firmadigital");

            entity.Property(e => e.IdFirma).HasColumnName("id_firma");
            entity.Property(e => e.CertificadoDigital).HasColumnName("certificado_digital");
            entity.Property(e => e.EmpresaAcreditadora)
                .HasMaxLength(200)
                .HasColumnName("empresa_acreditadora");
            entity.Property(e => e.FechaEmision)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_emision");
            entity.Property(e => e.FechaVencimiento)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_vencimiento");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(200)
                .HasColumnName("razon_social");
            entity.Property(e => e.RepresentanteLegal)
                .HasMaxLength(200)
                .HasColumnName("representante_legal");
            entity.Property(e => e.RutaRubrica).HasColumnName("ruta_rubrica");
            entity.Property(e => e.TipoFirma)
                .HasMaxLength(1)
                .HasColumnName("tipo_firma");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
