using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_salle_sal")]
public partial class Salle
{
    [Key]
    [Column("sal_id")]
    public int SalleId { get; set; }

    [Column("bat_id")]
    public int BatimentId { get; set; }

    [Column("typ_id")]
    public int TypeSalleId { get; set; }

    [Column("sal_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("sal_imagedata")]
    [JsonIgnore]
    public byte[]? ImageData { get; set; }

    [Column("sal_capacite")]
    public int? Capacite { get; set; }

    [ForeignKey("BatimentId")]
    public virtual Batiment? Batiment { get; set; }

    [InverseProperty("Salle")]
    public virtual ICollection<CapteurInstalle> CapteurInstalles { get; set; } = new List<CapteurInstalle>();

    [InverseProperty("Salle")]
    public virtual ICollection<EquipementInstalle> EquipementInstalles { get; set; } = new List<EquipementInstalle>();

    [InverseProperty("Salle")]
    public virtual ICollection<Surface> Surfaces { get; set; } = new List<Surface>();

    [ForeignKey("TypeSalleId")]
    public virtual TypeSalle? TypeSalle { get; set; }
}
