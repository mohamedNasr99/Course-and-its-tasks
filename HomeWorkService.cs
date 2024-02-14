using Microsoft.AspNetCore.Mvc;
using ToDoListProject.DTOs;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{
    public class HomeWorkService : IHomeWorkService
    {
        private readonly ApplicationDbContext context;

        public HomeWorkService(ApplicationDbContext context)
        {
            this.context = context;
        }
        //Create Action
        [HttpPost("/create")]
        public int Create(HomeWorkDTO homeWorkDTO)
        {
            if (homeWorkDTO != null)
            {
                HomeWork homeWork = new HomeWork();
                homeWork.Title = homeWorkDTO.Title;
                homeWork.Description = homeWorkDTO.Description;
                homeWork.CourseId = homeWorkDTO.CourseId;
                context.Add(homeWork);
                int re = context.SaveChanges();
                return re;
            }
            return 0;
        }

        //Read One Action
        [HttpGet("/read")]
        public HomeWork? ReadOne(int id)
        {
            if (id != 0)
            {
                return context.HomeWorks?.FirstOrDefault(c => c.Id == id);
            }
            return null;
        }

        //Read All action
        [HttpGet("/ReadAll")]
        public List<HomeWork> ReadAll()
        {
            List<HomeWork> homeWorks = context.HomeWorks.ToList();
            return homeWorks;
        }

        //Update Action
        [HttpPut("/update")]
        public int Update(int id, HomeWorkDTO homeWorkDTO)
        {
            if (id != 0)
            {
                HomeWork? oldwork = context.HomeWorks.FirstOrDefault(c => c.Id == id);
                if (oldwork != null)
                {
                    oldwork.Title = homeWorkDTO.Title;
                    oldwork.Description = homeWorkDTO.Description;
                    context.Add(oldwork);
                    int res = context.SaveChanges();
                    return res;
                }
                return 0;
            }
            return 0;
        }

        //Delete Action
        [HttpDelete]
        public int Delete(int id)
        {
            if (id != 0)
            {
                HomeWork? homework = context.HomeWorks.FirstOrDefault(c => c.Id == id);
                context.Remove(homework);
                int res = context.SaveChanges();
                return res;
            }
            return 0;
        }


    }
}
