using System.Reflection;

namespace DreamRides.Service.Utils;

public static class ValidationHelperUtils
{
    // TODO: Verify for int
    public static KeyValuePair<bool, string> ValidateRequiredFields<T>(T request) where T : class
    {
        var missingFields = new List<string>();
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var hasErrors = false;
        foreach (var property in properties)
        {
            var value = property.GetValue(request);

            if (!IsNullOrDefault(value)) continue;
            hasErrors = true;
            missingFields.Add(property.Name);
        }
        
        var fieldsMessage = string.Join(", ", missingFields);

        return new KeyValuePair<bool, string>(hasErrors,$"The following fields are empty: {fieldsMessage}");
    }
    
    public static KeyValuePair<bool, string> ValidateOneRequiredFields<T>(T request) where T : class
    {
        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var hasOneFilled = false;
        var message = "You need at least one filled!";
        foreach (var property in properties)
        {
            var value = property.GetValue(request);

            if (!IsNullOrDefault(value))
            {
                hasOneFilled = true;
                message = "All good";
            }
        }

        return new KeyValuePair<bool, string>(hasOneFilled,message);
    }

    private static bool IsNullOrDefault(object? value)
    {
        if (value == null) return true;
        
        var type = value.GetType();
        
        if (type.IsValueType)
        {
            return value.Equals(Activator.CreateInstance(type));
        } 
        
        return type == typeof(string) && string.IsNullOrWhiteSpace(value as string);
    }
}