namespace Warehouse.Models
{
    public class User
    {
        public long Id { get; set; }

        public string UserName { get; set; }
        
        public string Password { get; set; }

        public long RoleId { get; set; }

        public virtual Role Role { get; set; }
    }
}