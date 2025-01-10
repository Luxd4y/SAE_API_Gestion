using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_surface_sur")]
public partial class Surface
{
    [Key]
    [Column("sur_id")]
    public int SurfaceId { get; set; }

    [Column("sal_id")]
    public int SalleId { get; set; }

    [Column("pos_id")]
    public int PositionSurfaceId { get; set; }

    [Column("sur_longueur")]
    [Precision(100, 2)]
    public decimal Longueur { get; set; } 

    [Column("sur_hauteur")]
    [Precision(100, 2)]
    public decimal Hauteur { get; set; }

    [ForeignKey("PositionSurfaceId")]
    public virtual PositionSurface? PositionSurface { get; set; } 

    [ForeignKey("SalleId")]
    [InverseProperty("Surfaces")]
    public virtual Salle? Salle { get; set; }

    public virtual ICollection<CapteurInstalle> CapteurInstalles { get; set; } = new List<CapteurInstalle>();

    [InverseProperty("Surface")]
    public virtual ICollection<EquipementInstalle> EquipementInstalles { get; set; } = new List<EquipementInstalle>();
}
