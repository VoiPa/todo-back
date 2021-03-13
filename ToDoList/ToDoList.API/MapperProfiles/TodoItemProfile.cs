using AutoMapper;
using ToDoList.API.DTO;
using ToDoList.API.Models;


namespace TodoList.API.MapperProfiles
{
    public class TodoItemProfile : Profile
    {
        public TodoItemProfile()
        {
            CreateMap<ToDoItemDto, ToDoItem>();
        }
    }
}
