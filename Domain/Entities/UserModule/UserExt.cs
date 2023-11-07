using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserModule;

public partial class User
{
    [NotMapped]
    [MinLength(8, ErrorMessage = "Contraseña no puede tener menos de 8 catacteres")]
    public string password { get; set; }
    [NotMapped]
    public Role Role { get; set; }
}
