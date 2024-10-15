using DreamRides.Database.Model;

namespace DreamRides.Data.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Car> FavoriteCars { get; set; }

        public UserDTO(User user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            CreatedAt = user.CreatedAt;
            FavoriteCars = user.Favorites.Where(f=>f.UserId == this.Id).Select(t=>t.Car).ToList();
        }
    }
}
