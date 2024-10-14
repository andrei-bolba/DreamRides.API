using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Database.Context;
using DreamRides.Database.Model;

namespace DreamRides.Communication.Repository;

public class UserRepository(DealershipContext dealershipContext) : IUserRepository
{
    public ResponseType<User> GetOne(Guid id)
    {
        var getAllUsers = GetAll();
        if (getAllUsers is { IsSuccess: true, Collection: not null })
        {
            var user = getAllUsers.Collection.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return new ResponseType<User>()
                {
                    Message = "User could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<User>()
            {
                Object = user,
                Message = "User find successfully!",
                IsSuccess = true
            };
        }

        return new ResponseType<User>()
        {
            Message = getAllUsers.Message,
            IsSuccess = false
        };
    }

    public ResponseType<User> GetAll()
    {
        try
        {
            var users = dealershipContext.Users;

            return new ResponseType<User>
            {
                Object = null,
                Collection = users,
                Message = "Users was sent successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<User> Add(User entity)
    {
        try
        {
            dealershipContext.Users.Add(entity);
            dealershipContext.SaveChanges();

            return new ResponseType<User>
            {
                Object = entity,
                Message = "User added successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<User> Update(User entity)
    {
        try
        {
            dealershipContext.Users.Update(entity);
            dealershipContext.SaveChanges();

            return new ResponseType<User>
            {
                Object = entity,
                Message = "User updated successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<User> Delete(Guid id)
    {
        try
        {
            var entity = GetOne(id);
            if (entity is { IsSuccess: true, Object: not null })
            {
                dealershipContext.Users.Remove(entity.Object);
                dealershipContext.SaveChanges();
                return new ResponseType<User>
                {
                    Message = "User deleted successfully!",
                    IsSuccess = true
                };
            }
            return new ResponseType<User>
            {
                Message = entity.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<User>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }
}