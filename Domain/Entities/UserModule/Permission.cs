using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.UserModule;

public class Permission
{
    [Key]
    public short Id { get; set; }
    public short IdRole { get; set; }
    public string Description { get; set; }
    public bool isEnabled { get; set; }
}
