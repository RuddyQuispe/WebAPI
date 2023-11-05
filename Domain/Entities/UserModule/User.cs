using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Domain.Entities.UserModule;

public class User
{
    [Key]
    public int Id { get; set; }
    public string Nickname { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool isEnabled { get; set; }

    //[ForeignKey(nameof(Role.Id))/*, DeleteBehavior(DeleteBehavior.NoAction)*/]
    //public ICollection<Role> Roles { get; set; }
}
