using Microsoft.EntityFrameworkCore;

namespace Domain.Entities.UserModule;

[PrimaryKey(nameof(IdRole), nameof(IdPermission))]
public class RolePermission
{
    public short IdRole { get; set; }
    public short IdPermission { get; set; }
}
