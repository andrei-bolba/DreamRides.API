using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace DreamRides.Service.Utils;

public static class Utils
{
    public static KeyValuePair<byte[],byte[]> HashedPassword(string password)
    {
        using var hmac = new HMACSHA512();
        var passwordHash=hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        var passwordSalt=hmac.Key;

        return new KeyValuePair<byte[], byte[]>(passwordHash, passwordSalt);
    }
    
    public static KeyValuePair<bool, string> EmailValid(string email)
    {
        var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        
        if (!Regex.IsMatch(email, emailRegex))
        {
            return new KeyValuePair<bool, string>(false, "Invalid email format.");
        }
        
        return new KeyValuePair<bool, string>(true, string.Empty);
    }
    
    public static KeyValuePair<bool, string> PasswordValidation(string password)
    {
        var errors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(password))
        {
            errors.Add("Password is required.");
        }
        else
        {
            if (password.Length < 8)
            {
                errors.Add("Password must be at least 8 characters long.");
            }
            
            if (!password.Any(char.IsUpper))
            {
                errors.Add("Password must contain at least one uppercase letter.");
            }
            
            if (!password.Any(char.IsLower))
            {
                errors.Add("Password must contain at least one lowercase letter.");
            }
            
            if (!password.Any(char.IsDigit))
            {
                errors.Add("Password must contain at least one number.");
            }
            
            if (password.All(char.IsLetterOrDigit))
            {
                errors.Add("Password must contain at least one special character.");
            }
        }
        
        if (errors.Any())
        {
            return new KeyValuePair<bool, string>(true, string.Join(Environment.NewLine, errors));
        }
        
        return new KeyValuePair<bool, string>(false, string.Empty);
    }
}