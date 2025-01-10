using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_typesalle_typ")]
public partial class TypeSalle
{
    [Key]
    [Column("typ_id")]
    public int TypeSalleId { get; set; }

    [Column("typ_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [InverseProperty("TypeSalle")]
    [JsonIgnore]
    public virtual ICollection<Salle> Salles { get; set; } = new List<Salle>();
}
