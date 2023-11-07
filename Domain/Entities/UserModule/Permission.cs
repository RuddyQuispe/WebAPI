using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserModule;

public class Permission
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    public short IdRole { get; set; }
    public string Description { get; set; }
}
