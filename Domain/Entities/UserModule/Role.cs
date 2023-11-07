using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserModule;

public partial class Role
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }
    public string Description { get; set; }
    public bool isEnabled { get; set; }
}
