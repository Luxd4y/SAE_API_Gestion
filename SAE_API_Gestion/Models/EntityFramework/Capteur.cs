using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_capteur_cap")]
public partial class Capteur
{
    [Key]
    [Column("cap_id")]
    public int CapteurId { get; set; }

    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Column("cap_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("cap_description")]
    [StringLength(500)]
    public string? Description { get; set; }

    [Column("cap_reference")]
    [StringLength(200)]
    public string? Reference { get; set; }

    [Column("cap_hauteur")]
    [Precision(100, 2)]
    public decimal Hauteur { get; set; }

    [Column("cap_longueur")]
    [Precision(100, 2)]
    public decimal Longueur { get; set; }

    [Column("cap_largeur")]
    [Precision(100, 2)]
    public decimal Largeur { get; set; }

    [Column("cap_imagedata")]
    public byte[]? Imagedata { get; set; }

    [ForeignKey("MarqueId")]
    [InverseProperty("Capteurs")]
    public virtual MarqueCapteur Marque { get; set; } = null!;

    [InverseProperty("Capteur")]
    public virtual ICollection<ParametreCapteur> ParametreCapteur { get; set; } = new List<ParametreCapteur>();

    [InverseProperty("Capteur")]
    public virtual ICollection<CapteurInstalle> CapteurInstalles { get; set; } = new List<CapteurInstalle>();
}
