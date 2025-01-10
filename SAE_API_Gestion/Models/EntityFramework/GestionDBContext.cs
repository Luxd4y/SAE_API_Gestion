using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

public partial class GestionDBContext : DbContext
{
    public GestionDBContext()
    {
    }

    public GestionDBContext(DbContextOptions<GestionDBContext> options)
        : base(options)
    {

    }

    public virtual DbSet<ParametreCapteur> ParametreCapteur { get; set; }

    public virtual DbSet<Batiment> Batiments { get; set; }

    public virtual DbSet<Capteur> Capteurs { get; set; }

    public virtual DbSet<CapteurInstalle> CapteurInstalles { get; set; }

    public virtual DbSet<Equipement> Equipements { get; set; }

    public virtual DbSet<EquipementInstalle> EquipementInstalles { get; set; }

    public virtual DbSet<MarqueCapteur> MarqueCapteurs { get; set; }

    public virtual DbSet<PositionSurface> PositionSurfaces { get; set; }

    public virtual DbSet<Salle> Salles { get; set; }

    public virtual DbSet<Surface> Surfaces { get; set; }

    public virtual DbSet<TypeEquipement> TypeEquipements { get; set; }

    public virtual DbSet<TypeSalle> TypeSalles { get; set; }

    public virtual DbSet<UniteMesurer> UniteMesurers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Name=SaeGestionRemoteDev");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParametreCapteur>(entity =>
        {
            entity.HasKey(e => new { e.UniteMesurerId, e.CapteurId }).HasName("pk_t_a_capteur_unitemesurer_ac");

            entity.HasOne(d => d.Capteur).WithMany(p => p.ParametreCapteur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_a_capt_t_a_capte_t_e_capt");

            entity.HasOne(d => d.UniteMesurer).WithMany(p => p.ParametreCapteur)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_a_capt_t_a_capte_t_e_unit");
        });

        modelBuilder.Entity<Batiment>(entity =>
        {
            entity.HasKey(e => e.BatimentId).HasName("pk_t_e_batiment_bat");
        });

        modelBuilder.Entity<Capteur>(entity =>
        {
            entity.HasKey(e => e.CapteurId).HasName("pk_t_e_capteur_cap");

            entity.HasOne(d => d.Marque).WithMany(p => p.Capteurs)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_capt_est_fabri_t_e_marq");
        });

        modelBuilder.Entity<CapteurInstalle>(entity =>
        {
            entity.HasKey(e => e.CapteurInstalleId).HasName("pk_t_e_capteurinstalle_cin");

            entity.HasOne(d => d.Capteur).WithMany(p => p.CapteurInstalles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_capt_associati_t_e_capt");

            entity.HasOne(d => d.Surface).WithMany(p => p.CapteurInstalles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_capt_associati_t_e_surf");
        });

        modelBuilder.Entity<Equipement>(entity =>
        {
            entity.HasKey(e => e.EquipementId).HasName("pk_t_e_equipement_equ");

            entity.HasOne(d => d.TypeEquipement).WithMany(p => p.Equipements)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_equi_est_de_ty_t_e_type");
        });

        modelBuilder.Entity<EquipementInstalle>(entity =>
        {
            entity.HasKey(e => e.EquipementInstalleId).HasName("pk_t_e_equipementinstalle_ein");

            entity.HasOne(d => d.Equipement).WithMany(p => p.EquipementInstalles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_equi_associati_t_e_equi");

            entity.HasOne(d => d.Surface).WithMany(p => p.EquipementInstalles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_equi_associati_t_e_surf");
        });

        modelBuilder.Entity<MarqueCapteur>(entity =>
        {
            entity.HasKey(e => e.MarqueId).HasName("pk_t_e_marquecapteur_mar");
        });

        modelBuilder.Entity<PositionSurface>(entity =>
        {
            entity.HasKey(e => e.PositionSurfaceId).HasName("pk_t_e_positionsurface_pos");
        });

        modelBuilder.Entity<Salle>(entity =>
        {
            entity.HasKey(e => e.SalleId).HasName("pk_t_e_salle_sal");

            entity.HasOne(d => d.Batiment).WithMany(p => p.Salles)
        .OnDelete(DeleteBehavior.Cascade) 
                .HasConstraintName("fk_t_e_sall_abrite_t_e_bati");

            entity.HasOne(d => d.TypeSalle).WithMany(p => p.Salles)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_sall_est_de_ty_t_e_type");
        });

        modelBuilder.Entity<Surface>(entity =>
        {
            entity.HasKey(e => e.SurfaceId).HasName("pk_t_e_surface_sur");

            entity.HasOne(d => d.PositionSurface).WithMany(p => p.Surfaces)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_surf_position_t_e_posi");

            entity.HasOne(d => d.Salle).WithMany(p => p.Surfaces)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_t_e_surf_associati_t_e_sall");
        });

        modelBuilder.Entity<TypeEquipement>(entity =>
        {
            entity.HasKey(e => e.TypeEquipementId).HasName("pk_t_e_typeequipement_teq");
        });

        modelBuilder.Entity<TypeSalle>(entity =>
        {
            entity.HasKey(e => e.TypeSalleId).HasName("pk_t_e_typesalle_typ");
        });

        modelBuilder.Entity<UniteMesurer>(entity =>
        {
            entity.HasKey(e => e.UniteMesurerId).HasName("pk_t_e_unitemesurer_uni");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
