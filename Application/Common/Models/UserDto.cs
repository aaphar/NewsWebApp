namespace Application.Common.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public short RoleId { get; set; }
    }
}
