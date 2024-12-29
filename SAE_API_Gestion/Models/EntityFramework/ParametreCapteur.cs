using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[PrimaryKey("UniteMesurerId", "CapteurId")]
[Table("t_a_capteur_unitemesurer_acu")]
public partial class ParametreCapteur
{
    [Key]
    [Column("uni_id")]
    public int UniteMesurerId { get; set; }

    [Key]
    [Column("cap_id")]
    public int CapteurId { get; set; }

    [Column("acu_plagemin")]
    public int? AcuPlagemin { get; set; }

    [Column("acu_plagemax")]
    public int? AcuPlagemax { get; set; }

    [Column("acu_precision")]
    public int? AcuPrecision { get; set; }

    [ForeignKey("CapteurId")]
    [InverseProperty("ParametreCapteur")]
    public virtual Capteur Capteur { get; set; } = null!;

    [ForeignKey("UniteMesurerId")]
    [InverseProperty("ParametreCapteur")]
    public virtual UniteMesurer UniteMesurer { get; set; } = null!;
}
