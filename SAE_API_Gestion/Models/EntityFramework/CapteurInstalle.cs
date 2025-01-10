using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_capteurinstalle_cin")]
public partial class CapteurInstalle
{
    [Key]
    [Column("cin_id")]
    public int CapteurInstalleId { get; set; }

    [Column("sur_id")]
    public int SurfaceId { get; set; }

    [Column("cap_id")]
    public int CapteurId { get; set; }

    [Column("cin_posx")]
    [Precision(100, 2)]
    public decimal PositionX { get; set; }

    [Column("cin_posy")]
    [Precision(100, 2)]
    public decimal PositionY { get; set; }

    [ForeignKey("CapteurId")]
    [InverseProperty("CapteurInstalles")]
    public virtual Capteur Capteur { get; set; } = null!;

    [ForeignKey("SurfaceId")]
    [InverseProperty("CapteurInstalles")]
    public virtual Surface Surface { get; set; } = null!;
}
