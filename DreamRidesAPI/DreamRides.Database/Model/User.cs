namespace DreamRides.Database.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; } = string.Empty;
        public required string LastName { get; set; } = string.Empty;
        public required string Email { get; set; } = string.Empty;
        public required string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.MinValue;
        public ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
    }
}
