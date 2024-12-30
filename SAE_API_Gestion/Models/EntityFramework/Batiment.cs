using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_batiment_bat")]
public partial class Batiment
{
    [Key]
    [Column("bat_id")]
    public int BatimentId { get; set; }

    [Column("bat_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("bat_imagedata")]
    [JsonIgnore]
    public byte[]? ImageData { get; set; }

    [InverseProperty("Batiment")]
    public virtual ICollection<Salle> Salles { get; set; } = new List<Salle>();
}
