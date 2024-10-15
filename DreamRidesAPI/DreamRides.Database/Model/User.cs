namespace DreamRides.Database.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required byte[] PasswordHash { get; set; } = [];
        public required byte[] PasswordSalt { get; set; } = [];
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
