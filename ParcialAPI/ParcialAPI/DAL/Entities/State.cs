using System.ComponentModel.DataAnnotations;

namespace ParcialAPI.DAL.Entities;

public class State : AuditBase
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} carácteres.")]
    [Display(Name = "Estado/Departamento")]
    public string Name { get; set; }
    [Display(Name = "Id país")]
    public Guid CountryId { get; set; }
    [Display(Name = "País")]
    public Country? Country { get; set; }

}
