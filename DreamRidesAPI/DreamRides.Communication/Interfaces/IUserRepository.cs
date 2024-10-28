using DreamRides.Communication.Types;
using DreamRides.Database.Model;

namespace DreamRides.Communication.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        public ResponseType<User> GetUserByEmail(string email);
    }
}
