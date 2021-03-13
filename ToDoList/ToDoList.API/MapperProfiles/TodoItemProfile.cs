using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ToDoList.API.DTO;
using ToDoList.BL.Models;


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
