using DreamRides.Communication.Interfaces;
using DreamRides.Communication.Types;
using DreamRides.Database.Context;
using DreamRides.Database.Model;

namespace DreamRides.Communication.Repository;

public class CarRepository(DealershipContext dealershipContext): ICarRepository
{
    public ResponseType<Car> GetOne(Guid id)
    {
        var getAllCars = GetAll();
        if (getAllCars is { IsSuccess: true, Collection: not null })
        {
            var car = getAllCars.Collection.FirstOrDefault(u => u.Id == id);
            if (car == null)
            {
                return new ResponseType<Car>()
                {
                    Message = "Car could not be find!",
                    IsSuccess = false
                };
            }

            return new ResponseType<Car>()
            {
                Object = car,
                Message = "Car find successfully!",
                IsSuccess = true
            };
        }

        return new ResponseType<Car>()
        {
            Message = getAllCars.Message,
            IsSuccess = false
        };
    }

    public ResponseType<Car> GetAll()
    {
        try
        {
            var cars = dealershipContext.Cars;

            return new ResponseType<Car>
            {
                Object = null,
                Collection = cars.ToList(),
                Message = "Cars was sent successfully",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Car>
            {
                Object = null,
                Collection = null,
                Message = ex.Message,
                IsSuccess = false
            };
        }
    }

    public ResponseType<Car> Add(Car entity)
    {
        try
        {
            dealershipContext.Cars.Add(entity);
            dealershipContext.SaveChanges();

            return new ResponseType<Car>
            {
                Object = entity,
                Message = "Car added successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Car>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<Car> Update(Car entity)
    {
        try
        {
            dealershipContext.Cars.Update(entity);
            dealershipContext.SaveChanges();

            return new ResponseType<Car>
            {
                Object = entity,
                Message = "Car updated successfully!",
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Car>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }

    public ResponseType<Car> Delete(Guid id)
    {
        try
        {
            var entity = GetOne(id);
            if (entity is { IsSuccess: true, Object: not null })
            {
                dealershipContext.Cars.Remove(entity.Object);
                dealershipContext.SaveChanges();
                return new ResponseType<Car>
                {
                    Message = "Car deleted successfully!",
                    IsSuccess = true
                };
            }
            return new ResponseType<Car>
            {
                Message = entity.Message,
                IsSuccess = true
            };
        }
        catch (Exception ex)
        {
            return new ResponseType<Car>
            {
                Message = ex.Message,
                IsSuccess = true
            };
        }
    }
}