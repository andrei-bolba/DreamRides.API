using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Database.Context;
using DreamRides.Database.Model;

namespace DreamRides.Communication.Repository;

public class FavoriteRepository(DealershipContext dealershipContext):IFavoriteRepository
{
    public ResponseType<Favorite> GetOne(Guid id)
    {
        var getAllFavorites = GetAll();
        if (getAllFavorites is { IsSuccess: true, Collection: not null })
        {
            var favorite = getAllFavorites.Collection.FirstOrDefault(u => u.FavoriteId == id);
            if (favorite == null)
            {
                return new ResponseType<Favorite>()
                {
                    Message = "Favorite could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<Favorite>()
            {
                Object = favorite,
                Message = "Favorite find successfully!",
                IsSuccess = true
            };
        }

        return new ResponseType<Favorite>()
        {
            Message = getAllFavorites.Message,
            IsSuccess = false
        };
    }

    public ResponseType<Favorite> GetAll()
    {
        try
        {
            var favorites = dealershipContext.Favorites;

            return new ResponseType<Favorite>
            {
                Object = null,
                Collection = favorites.ToList(),
                Message = "Favorites was sent successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Favorite>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Favorite> Add(Favorite entity)
    {
        try
        {
            dealershipContext.Favorites.Add(entity);
            dealershipContext.SaveChanges();

            return new ResponseType<Favorite>
            {
                Object = entity,
                Message = "Favorite added successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Favorite>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<Favorite> Update(Favorite entity)
    {
        try
        {
            dealershipContext.Favorites.Update(entity);
            dealershipContext.SaveChanges();

            return new ResponseType<Favorite>
            {
                Object = entity,
                Message = "Favorite updated successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Favorite>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<Favorite> Delete(Guid id)
    {
        try
        {
            var entity = GetOne(id);
            if (entity is { IsSuccess: true, Object: not null })
            {
                dealershipContext.Favorites.Remove(entity.Object);
                dealershipContext.SaveChanges();
                return new ResponseType<Favorite>
                {
                    Message = "Favorite deleted successfully!",
                    IsSuccess = true
                };
            }
            return new ResponseType<Favorite>
            {
                Message = entity.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Favorite>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<Car> GetAllFavoriteCarsByUserId(Guid userId)
    {
        var getAllFavorites = GetAll();
        if (getAllFavorites is not { IsSuccess: true, Collection: not null })
            return new ResponseType<Car>()
            {
                Message = getAllFavorites.Message,
                IsSuccess = false
            };
        var favoriteCars = getAllFavorites.Collection.Where(u => u.UserId == userId).Select(f=>f.Car);
        return new ResponseType<Car>()
        {
            Collection = favoriteCars,
            Message = "Favorite cars find successfully!",
            IsSuccess = true
        };
    }

    public ResponseType<User> GetAllUsersFavoritesByCarId(Guid carId)
    {
        var getAllFavorites = GetAll();
        if (getAllFavorites is not { IsSuccess: true, Collection: not null })
            return new ResponseType<User>()
            {
                Message = getAllFavorites.Message,
                IsSuccess = false
            };
        var favoriteCars = getAllFavorites.Collection.Where(u => u.CarId == carId).Select(f=>f.User);
        return new ResponseType<User>()
        {
            Collection = favoriteCars,
            Message = "Favorite user for car find successfully!",
            IsSuccess = true
        };
    }
}