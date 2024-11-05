using DreamRides.Communication.Types;
using DreamRides.Database.Model;

namespace DreamRides.Communication.Interfaces
{
    public interface IFavoriteRepository:IRepository<Favorite>
    {
        public ResponseType<Car> GetAllFavoriteCarsByUserId(Guid userId);
        public ResponseType<User> GetAllUsersFavoritesByCarId(Guid carId);
    }
}
