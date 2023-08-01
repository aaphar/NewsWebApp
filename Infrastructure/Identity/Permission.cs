namespace Infrastructure.Identity;
public class Permission
{
    public byte Id { get; set; }
    public string Name { get; set; }

    public virtual List<ApplicationRole> Roles { get; set; }
}
