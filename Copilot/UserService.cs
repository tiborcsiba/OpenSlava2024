using System;
using System.Collections.Generic;
using System.Linq;

public class UserService
{
    private readonly List<User> _users = new List<User>();

    public bool CreateUser(string username, string email, DateTime dateOfBirth)
    {
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email))
        {
            return false; // Invalid input
        }

        if (!IsValidEmail(email))
        {
            return false; // Invalid email
        }

        if (dateOfBirth > DateTime.Now)
        {
            throw new ArgumentException("Date of birth cannot be in the future.");
        }

        if (_users.Any(u => u.Email == email))
        {
            return false; // Email already exists
        }

        _users.Add(new User { Username = username, Email = email, DateOfBirth = dateOfBirth });
        return true;
    }

    public User? GetUserByEmail(string email)
    {
        return _users.FirstOrDefault(u => u.Email == email);
    }

    public bool UpdateUsername(string email, string newUsername)
    {
        var user = GetUserByEmail(email);
        if (user == null || string.IsNullOrWhiteSpace(newUsername))
        {
            return false; // Invalid user or new username
        }

        user.Username = newUsername;
        return true;
    }

    public int GetUsersOlderThan(DateTime date)
    {
        return _users.Count(u => u.DateOfBirth < date);
    }

    private bool IsValidEmail(string email)
    {
        // Simple email validation logic
        return email.Contains("@");
    }
}

public class User
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
}
