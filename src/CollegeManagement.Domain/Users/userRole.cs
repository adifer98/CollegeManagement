using Ardalis.SmartEnum;

namespace CollegeManagement.Domain.Users;

public class UserRole : SmartEnum<UserRole>
{
    public static readonly UserRole Student = new(nameof(Student), 0);
    public static readonly UserRole Teacher = new(nameof(Teacher), 1);
    
    public UserRole(string name, int value) : base(name, value)
    {
    }
}