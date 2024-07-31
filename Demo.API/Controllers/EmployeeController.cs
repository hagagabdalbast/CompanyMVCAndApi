using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Demo.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo employee;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRepo employee, IMapper mapper)
        {
            this.employee = employee;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                var data = employee.Get();
                var model = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrieved",
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>()
                {
                    Code = "500",
                    Status = "Internal Server Error",
                    Message = "Data Not Retrieved",
                    Error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("~/Api/GetEmployeesById/{id}")]
        public IActionResult GetEmployeesById(int id)
        {
            try
            {
                var data = employee.GetById(id);
                if (data == null)
                {
                    return NotFound(new ApiResponse<string>()
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "Employee Not Found"
                    });
                }
                var model = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Retrieved",
                    Data = model
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>()
                {
                    Code = "500",
                    Status = "Internal Server Error",
                    Message = "Data Not Retrieved",
                    Error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("~/Api/PostEmployee")]
        public IActionResult PostEmployee([FromBody] EmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Invalid Data"
                });
            }

            try
            {
                var employeeEntity = mapper.Map<Employee>(model);

                // Ensure related entities are not set with explicit IDs
                employeeEntity.Department = null;
                employeeEntity.District = null;

                employee.Create(employeeEntity);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Created",
                    Data = mapper.Map<EmployeeVM>(employeeEntity)
                });
            }
            catch (DbUpdateException dbEx)
            {
                var innerException = dbEx.InnerException?.Message ?? dbEx.Message;
                return StatusCode(500, new ApiResponse<string>()
                {
                    Code = "500",
                    Status = "Internal Server Error",
                    Message = "Data Not Created",
                    Error = innerException
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>()
                {
                    Code = "500",
                    Status = "Internal Server Error",
                    Message = "Data Not Created",
                    Error = ex.Message
                });
            }
        }




        [HttpPut]
        [Route("~/Api/PutEmployee")]
        public IActionResult PutEmployee([FromBody] EmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Invalid Data"
                });
            }

            try
            {
                var data = mapper.Map<Employee>(model);
                employee.Edit(data);
                return Ok(new ApiResponse<EmployeeVM>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Updated",
                    Data = mapper.Map<EmployeeVM>(data)
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>()
                {
                    Code = "500",
                    Status = "Internal Server Error",
                    Message = "Data Not Updated",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("~/Api/DeleteEmployee")]
        public IActionResult DeleteEmployee([FromBody] EmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>()
                {
                    Code = "400",
                    Status = "Bad Request",
                    Message = "Invalid Data"
                });
            }

            try
            {
                var data = mapper.Map<Employee>(model);
                employee.Delete(data);
                return Ok(new ApiResponse<string>()
                {
                    Code = "200",
                    Status = "OK",
                    Message = "Data Deleted"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ApiResponse<string>()
                {
                    Code = "500",
                    Status = "Internal Server Error",
                    Message = "Data Not Deleted",
                    Error = ex.Message
                });
            }
        }
    }
}
