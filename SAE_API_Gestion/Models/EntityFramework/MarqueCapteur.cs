using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace SAE_API_Gestion.Models.EntityFramework;

[Table("t_e_marquecapteur_mar")]
public partial class MarqueCapteur
{
    [Key]
    [Column("mar_id")]
    public int MarqueId { get; set; }

    [Column("mar_nom")]
    [StringLength(100)]
    public string Nom { get; set; } = null!;

    [InverseProperty("Marque")]
    public virtual ICollection<Capteur> Capteurs { get; set; } = new List<Capteur>();
}
