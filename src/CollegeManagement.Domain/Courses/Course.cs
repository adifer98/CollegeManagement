namespace CollegeManagement.Domain.Courses;

public class Course
{
    public Guid Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public int Hours { get; private set; }
    public int Price { get; private set; }

    public Course(
        string title,
        string description,
        int hours,
        int price,
        Guid? id = null
    )
    {
        Id = id ?? Guid.NewGuid();
        Title = title;
        Description = description;
        Hours = hours;
        Price = price;
    }

    private Course()
    {
    }

    public void Update(string title, string description, int hours, int price)
    {
        Title = title;
        Description = description;
        Hours = hours;
        Price = price;
    }
}