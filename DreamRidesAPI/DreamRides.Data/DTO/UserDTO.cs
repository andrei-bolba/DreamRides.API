using DreamRides.Database.Model;

namespace DreamRides.Data.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Car> FavoriteCars { get; set; }

        public UserDTO(User user)
        {
            this.Id = user.Id;
            this.FirstName = user.FirstName;
            this.LastName = user.LastName;
            this.Email = user.Email;
            this.PasswordHash = user.PasswordHash;
            this.CreatedAt = user.CreatedAt;
            this.FavoriteCars = user.Favorites.Where(f=>f.UserId == this.Id).Select(t=>t.Car).ToList();
        }
    }
}
