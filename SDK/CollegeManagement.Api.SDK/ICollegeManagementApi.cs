using CollegeManagement.Contracts.Courses;
using CollegeManagement.Contracts.Enrollments;
using CollegeManagement.Contracts.Ratings;
using CollegeManagement.Contracts.Users;
using Refit;

namespace CollegeManagement.Api.SDK;

[Headers("Authorization: Bearer")]
public interface ICollegeManagementApi
{
    //Users
    [Post("/api/Users")]
    Task<UserResponse> CreateUserAsync(CreateUserRequest user);
    
    [Get("/api/Users/{userIdOrSlug}")]
    Task<UserResponse> GetUserAsync(string userIdOrSlug);
    
    [Get("/api/Users")]
    Task<UsersResponse> GetAllUsersAsync();
    
    [Delete("/api/Users/{userIdOrSlug}")]
    Task DeleteUserAsync(string userIdOrSlug);
    
    [Put("/api/Users/{userIdOrSlug}")]
    Task<UserResponse> UpdateUserAsync(string userIdOrSlug, UpdateUserRequest user);
    
    //Courses
    [Post("/api/Courses")]
    Task<CourseResponse> CreateCourseAsync(CreateCourseRequest course);
    
    [Get("/api/Courses")]
    Task<CoursesResponse> GetAllCoursesAsync();
    
    [Get("/api/Courses/{courseId}")]
    Task<CourseResponse> GetCourseAsync(Guid courseId);
    
    [Delete("/api/Courses/{courseId}")]
    Task DeleteCourseAsync(Guid courseId);
    
    [Put("/api/Courses/{courseId}")]
    Task<CourseResponse> UpdateCourseAsync(Guid courseId, UpdateCourseRequest request);
    
    [Get("/api/Courses/rating/{courseId}")]
    Task<RatingResponse> GetCourseRatingAsync(Guid courseId);
    
    //Ratings
    [Post("/api/Ratings")]
    Task<RatingResponse> CreateRatingAsync(CreateRatingRequest rating);
    
    //Enrollments
    [Post("/api/Enrollments")]
    Task<EnrollmentResponse> CreateEnrollmentAsync(CreateEnrollmentRequest enrollment);
    
    [Get("/api/Enrollments")]
    Task<EnrollmentsResponse> GetAllEnrollmentsAsync();
    
    [Get("/api/Enrollments/{studentId}")]
    Task<EnrollmentsResponse> GetEnrollmentsByStudentAsync(Guid studentId);
    
    [Delete("/api/Enrollments/{enrollmentId}")]
    Task DeleteEnrollmentAsync(Guid enrollmentId);
}