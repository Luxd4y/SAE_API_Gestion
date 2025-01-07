using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_equipementinstalle_ein")]
public partial class EquipementInstalle
{
    [Key]
    [Column("ein_id")]
    public int EquipementInstalleId { get; set; }

    [Column("equ_id")]
    public int EquipementId { get; set; }

    [Column("sur_id")]
    public int SurfaceId { get; set; }

    [Column("ein_posx")]
    [Precision(100, 2)]
    public decimal PositionX { get; set; }

    [Column("ein_posy")]
    [Precision(100, 2)]
    public decimal PositionY { get; set; }

    [ForeignKey("EquipementId")]
    [InverseProperty("EquipementInstalles")]
    public virtual Equipement? Equipement { get; set; }

    [ForeignKey("SurfaceId")]
    [InverseProperty("EquipementInstalles")]
    public virtual Surface? Surface { get; set; } 
}
