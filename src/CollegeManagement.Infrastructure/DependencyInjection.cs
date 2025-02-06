
using CollegeManagement.Application.Common.Interfaces;
using CollegeManagement.Infrastructure.Users;
using CollegeManagement.Infrastructure.Common.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CollegeManagement.Infrastructure.Courses;
using CollegeManagement.Infrastructure.Enrollments;
using CollegeManagement.Infrastructure.Ratings;

namespace CollegeManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddPersistence();
    }

    private static IServiceCollection AddPersistence(this IServiceCollection services)
    {
       
        // services.AddDbContext<CollegeManagementDbContext>(option => 
        //     option.UseSqlServer("Server=PC-2656\\SQLEXPRESS;Initial Catalog=COLLEGE;User ID=sa;Password=BigStrongPassword123;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;")
        // );

        services.AddDbContext<CollegeManagementDbContext>(option => 
             option.UseSqlServer("Server=DESKTOP-HINUURN;Initial Catalog=COLLEGE;User ID=sa;Password=BigStrongPassword123;MultipleActiveResultSets=True;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;")
        );


        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<ICoursesRepository, CoursesRepository>();
        services.AddScoped<IEnrollmentsRepository, EnrollmentsRepository>();
        services.AddScoped<IRatingsRepository, RatingsRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<CollegeManagementDbContext>());

        return services;
    }
}