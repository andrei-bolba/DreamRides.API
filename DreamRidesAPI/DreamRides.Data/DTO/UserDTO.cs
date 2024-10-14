using DreamRides.Database.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamRides.Data.DTO
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
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
