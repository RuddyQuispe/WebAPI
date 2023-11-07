using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserModule;

public partial class Role
{
    [NotMapped]
    public ICollection<Permission> Permissions { get; set; }
}
