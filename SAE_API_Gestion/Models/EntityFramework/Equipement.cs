using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_equipement_equ")]
public partial class Equipement
{
    [Key]
    [Column("equ_id")]
    public int EquipementId { get; set; }

    [Column("teq_id")]
    public int TypeEquipementId { get; set; }

    [Column("equ_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("equ_hauteur")]
    [Precision(100, 2)]
    public decimal Hauteur { get; set; }

    [Column("equ_longueur")]
    [Precision(100, 2)]
    public decimal Longueur { get; set; }

    [Column("equ_largeur")]
    [Precision(100, 2)]
    public decimal Largeur { get; set; }

    [Column("equ_imagedata")]
    public byte[]? ImageData { get; set; }

    [InverseProperty("Equipement")]
    public virtual ICollection<EquipementInstalle> EquipementInstalles { get; set; } = new List<EquipementInstalle>();

    [ForeignKey("TypeEquipementId")]
    [InverseProperty("Equipements")]
    public virtual TypeEquipement TypeEquipement { get; set; } = null!;
}
