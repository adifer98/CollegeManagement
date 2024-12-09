namespace CollegeManagement.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task CommitChangesAsync();
}