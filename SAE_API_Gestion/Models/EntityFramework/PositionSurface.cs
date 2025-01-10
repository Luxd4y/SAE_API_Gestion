using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_positionsurface_pos")]
public partial class PositionSurface
{
    [Key]
    [Column("pos_id")]
    public int PositionSurfaceId { get; set; }

    [Column("pos_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [InverseProperty("PositionSurface")]
    [JsonIgnore]
    public virtual ICollection<Surface> Surfaces { get; set; } = new List<Surface>();
}
