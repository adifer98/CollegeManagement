using CollegeManagement.Domain.Users;

namespace CollegeManagement.TestsCommon.TestsConstants;

public class UserConstants
{
    public static readonly Guid ID = Guid.NewGuid();
    public static readonly string NAME = "Test Test";
    public static readonly string EMAIL = "test@test.com";
    public static readonly string CITY = "Test Farm";
    public static readonly UserRole ROLE = UserRole.Student;
}