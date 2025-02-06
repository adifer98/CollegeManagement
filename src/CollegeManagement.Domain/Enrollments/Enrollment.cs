namespace CollegeManagement.Domain.Enrollments;

public class Enrollment
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CourseId { get; private set; }
    public string grade { get; private set; }

    
    public Enrollment(
        Guid userId,
        Guid courseId,
        Guid? id = null
    )
    {
        Id = id ?? Guid.NewGuid();
        UserId = userId;
        CourseId = courseId;
        grade = string.Empty;
    }

    private Enrollment()
    {
    }
}