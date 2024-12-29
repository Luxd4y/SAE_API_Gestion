using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_unitemesurer_uni")]
public partial class UniteMesurer
{
    [Key]
    [Column("uni_id")]
    public int UniteMesurerId { get; set; }

    [Column("uni_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [Column("uni_symbole")]
    [StringLength(10)]
    public string Symbole { get; set; } = null!;

    [InverseProperty("UniteMesurer")]
    public virtual ICollection<ParametreCapteur> ParametreCapteur { get; set; } = new List<ParametreCapteur>();
}
