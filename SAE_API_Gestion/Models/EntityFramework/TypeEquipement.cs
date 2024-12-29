using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_typeequipement_teq")]
public partial class TypeEquipement
{
    [Key]
    [Column("teq_id")]
    public int TypeEquipementId { get; set; }

    [Column("teq_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [InverseProperty("TypeEquipement")]
    public virtual ICollection<Equipement> Equipements { get; set; } = new List<Equipement>();
}
