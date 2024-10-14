namespace DreamRides.Database.Model
{
    public class Favorite
    {
        public required Guid FavoriteId { get; set; }
        public required Guid UserId { get; set; }
        public required User User { get; set; }
        public required Guid CarId { get; set; }
        public required Car Car { get; set; }
        public DateTime FavoritedAt { get; set; }
    }

}
