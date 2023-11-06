using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserModule;

public class Role
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    public string Description { get; set; }
    public bool isEnabled { get; set; }

    //[ForeignKey(nameof(Permission.IdRole))/*, DeleteBehavior(DeleteBehavior.NoAction)*/]
    //public ICollection<Permission> Permissions { get; set; }
}
