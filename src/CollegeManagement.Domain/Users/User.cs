

namespace CollegeManagement.Domain.Users;

public class User
{
    //private readonly List<Enrollment> _enrollments = [];
    public Guid Id { get; init; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string City { get; private set; }
    public UserRole Role { get; private set; } = null!;
    public string Slug { get; private set; }

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
        Slug = GenerateSlug();
    }

    private User()
    {
    }

    public void Update(string name, string email, string city, UserRole role)
    {
        Name = name;
        Email = email;
        City = city;
        Role = role;
        Slug = GenerateSlug();
    }

    private string GenerateSlug()
    {
        var sluggedName = Name.ToLower().Replace(" ", "-");
        var sluggedCity = City.ToLower().Replace(" ", "-");
        return $"{sluggedName}-{sluggedCity}";
    }
}