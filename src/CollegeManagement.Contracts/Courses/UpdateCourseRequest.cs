namespace CollegeManagement.Contracts.Courses;

public record UpdateCourseRequest(string title, string description, int hours, int price);