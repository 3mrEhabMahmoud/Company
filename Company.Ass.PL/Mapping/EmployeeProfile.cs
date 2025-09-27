using AutoMapper;
using Company.Ass.DAL.Models;
using Company.Ass.PL.Dtos;
using Microsoft.Build.Framework.Profiler;


namespace Company.Ass.PL.Mapping
{
    public class EmployeeProfile: Profile
    {
        public EmployeeProfile()
        {
            CreateMap<CreateEmployeeDto, Employee>();
        }
    }
}
