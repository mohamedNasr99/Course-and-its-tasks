using ToDoListProject.DTOs;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{
    public interface ICourseService
    {
        int Create(string name);
        int Delete(int id);
        List<Course> ReadAll();
        CourseAndItsHomeWorksDTO? ReadOne(int id);
        int Update(int id, CourseDTO courseDTO);
    }
}