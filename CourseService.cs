using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoListProject.DTOs;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext context;

        public CourseService(ApplicationDbContext context)
        {
            this.context = context;
        }
        //Create Action
        public int Create(string name)
        {
            if (name != null)
            {
                Course course = new Course();
                course.Name = name;
                context.Add(course);
                int re = context.SaveChanges();
                return re;
            }
            return 0;
        }

        //Read One Action
        public CourseAndItsHomeWorksDTO? ReadOne(int id)
        {
            if (id != 0)
            {
                Course? course = context.Courses.Include(c => c.HomeWorks).FirstOrDefault(c => c.Id == id);
                CourseAndItsHomeWorksDTO courseAndItsHomeWorksDTO = new CourseAndItsHomeWorksDTO();
                courseAndItsHomeWorksDTO.Name = course.Name;
                foreach(var work in course.HomeWorks)
                {
                    if(work.CourseId == id)
                    {
                        courseAndItsHomeWorksDTO.WorksNames?.Add(work.Title);
                    }
                    
                }
                return courseAndItsHomeWorksDTO;
            }
            return null;
        }

        //Read All action
        public List<Course> ReadAll()
        {
            List<Course> courses = context.Courses.ToList();
            return courses;
        }

        //Update Action
        public int Update(int id, CourseDTO courseDTO)
        {
            if (id != 0)
            {
                Course? oldcourse = context.Courses.FirstOrDefault(c => c.Id == id);
                if (oldcourse != null)
                {
                    oldcourse.Name = courseDTO.Name;
                    context.Add(oldcourse);
                    int res = context.SaveChanges();
                    return res;
                }
                return 0;
            }
            return 0;
        }

        //Delete Action
        public int Delete(int id)
        {
            if (id != 0)
            {
                Course? course = context.Courses.FirstOrDefault(c => c.Id == id);
                context.Remove(course);
                int res = context.SaveChanges();
                return res;
            }
            return 0;
        }
    }
}
