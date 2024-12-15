namespace CollegeManagement.Contracts.Courses;

public record CreateCourseRequest(
    string Title,
    string Description,
    int Hours,
    int Price
);