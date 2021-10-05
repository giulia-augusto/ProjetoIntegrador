using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CadastroImoveis.Models
{
    public partial class BDContexto : DbContext
    {
        public BDContexto()
        {
        }

        public BDContexto(DbContextOptions<BDContexto> options)
            : base(options)
        {
        }

        public virtual DbSet<Diferencial> Diferencial { get; set; }
        public virtual DbSet<Estado> Estado { get; set; }
        public virtual DbSet<Imovel> Imovel { get; set; }
        public virtual DbSet<ImovelDiferencial> ImovelDiferencial { get; set; }
        public virtual DbSet<Municipio> Municipio { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=localhost;port=3306;user=root;password=root;database=imobiliaria");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diferencial>(entity =>
            {
                entity.HasKey(e => e.IdDiferencial)
                    .HasName("PRIMARY");

                entity.ToTable("diferencial");

                entity.Property(e => e.IdDiferencial).HasColumnName("idDiferencial");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.ToTable("estado");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Imovel>(entity =>
            {
                entity.HasKey(e => e.CodImovel)
                    .HasName("PRIMARY");

                entity.ToTable("imovel");

                entity.HasIndex(e => e.IdMunicipio)
                    .HasName("idMunicipio");

                entity.Property(e => e.CodImovel).HasColumnName("codImovel");

                entity.Property(e => e.Ano)
                    .HasColumnName("ano")
                    .HasColumnType("year");

                entity.Property(e => e.DataAquisicao)
                    .HasColumnName("dataAquisicao")
                    .HasColumnType("date");

                entity.Property(e => e.IdMunicipio).HasColumnName("idMunicipio");

                entity.Property(e => e.Proprietario)
                    .IsRequired()
                    .HasColumnName("proprietario")
                    .HasMaxLength(50);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasColumnName("tipo")
                    .HasColumnType("enum('Novo','Usado')");

                entity.HasOne(d => d.IdMunicipioNavigation)
                    .WithMany(p => p.Imovel)
                    .HasForeignKey(d => d.IdMunicipio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("imovel_ibfk_1");
            });

            modelBuilder.Entity<ImovelDiferencial>(entity =>
            {
                entity.HasKey(e => new { e.CodImovel, e.IdDiferencial })
                    .HasName("PRIMARY");

                entity.ToTable("imovel_diferencial");

                entity.HasIndex(e => e.IdDiferencial)
                    .HasName("idDiferencial");

                entity.Property(e => e.CodImovel).HasColumnName("codImovel");

                entity.Property(e => e.IdDiferencial).HasColumnName("idDiferencial");

                entity.HasOne(d => d.CodImovelNavigation)
                    .WithMany(p => p.ImovelDiferencial)
                    .HasForeignKey(d => d.CodImovel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("imovel_diferencial_ibfk_1");

                entity.HasOne(d => d.IdDiferencialNavigation)
                    .WithMany(p => p.ImovelDiferencial)
                    .HasForeignKey(d => d.IdDiferencial)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("imovel_diferencial_ibfk_2");
            });

            modelBuilder.Entity<Municipio>(entity =>
            {
                entity.HasKey(e => e.IdMunicipio)
                    .HasName("PRIMARY");

                entity.ToTable("municipio");

                entity.HasIndex(e => e.IdEstado)
                    .HasName("idEstado");

                entity.Property(e => e.IdMunicipio).HasColumnName("idMunicipio");

                entity.Property(e => e.IdEstado).HasColumnName("idEstado");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnName("nome")
                    .HasMaxLength(50);

                entity.Property(e => e.Populacao).HasColumnName("populacao");

                entity.Property(e => e.Porte)
                    .IsRequired()
                    .HasColumnName("porte")
                    .HasColumnType("enum('Metrópole','Grande','Médio','Pequeno')");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Municipio)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("municipio_ibfk_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
