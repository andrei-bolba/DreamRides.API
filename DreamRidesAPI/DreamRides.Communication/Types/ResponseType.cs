namespace DreamRides.Communication.Types
{
    public class ResponseType<T>
    {
        public T? Object { get; init; }
        public IEnumerable<T>? Collection { get; init; }
        public string Message { get; init; } = string.Empty;
        public bool IsSuccess { get; init; } = false;
    }
}