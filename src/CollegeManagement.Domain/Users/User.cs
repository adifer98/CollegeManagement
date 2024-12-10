namespace CollegeManagement.Domain.Users;

public class User
{
    //private readonly List<Enrollment> _enrollments = [];
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string City { get; private set; }
    public UserRole Role { get; private set; } = null!;

    public User(
        string name,
        string email,
        string city,
        UserRole role,
        Guid? id = null
    )
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Email = email;
        City = city;
        Role = role;
    }

    private User()
    {
    }
}