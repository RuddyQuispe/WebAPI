using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserModule;

public partial class User
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [MinLength(8)]
    public string Nickname { get; set; }
    public string Name { get; set; }
    [EmailAddress(ErrorMessage = "Email no valido")]
    public string Email { get; set; }
    [Phone(ErrorMessage = "Nro telefono no valido")]
    public string Phone { get; set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public bool isEnabled { get; set; }
    public short IdRole { get; set; }
}
