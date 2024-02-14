using ToDoListProject.DTOs;
using ToDoListProject.Models;

namespace ToDoListProject.Services
{
    public interface IHomeWorkService
    {
        int Create(HomeWorkDTO homeWorkDTO);
        int Delete(int id);
        List<HomeWork> ReadAll();
        HomeWork? ReadOne(int id);
        int Update(int id, HomeWorkDTO homeWorkDTO);
    }
}