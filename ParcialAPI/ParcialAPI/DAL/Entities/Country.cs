using System.ComponentModel.DataAnnotations;

namespace ParcialAPI.DAL.Entities;

public class Country : AuditBase
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} carácteres.")]
    [Display(Name = "País")]
    public string Name { get; set; }
}
