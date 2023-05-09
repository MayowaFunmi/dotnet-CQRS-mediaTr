using CQRSMediaTr.Command;
using CQRSMediaTr.Models;
using CQRSMediaTr.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRSMediaTr.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        private IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<Employee>> EmployeeList()
        {
            var employeeList = await _mediator.Send(new GetEmployeeListQuery());   
            return employeeList;
        }

        [HttpGet("{id}")]
        public async Task<Employee> EmployeeById(int id)
        {
            var employee = await _mediator.Send(new GetEmployeeByIdQuery() { Id=id});
            return employee;
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            var emp = await _mediator.Send(new CreateEmployeeCommand(
                employee.Name, employee.Address, employee.Email, employee.Phone
            ));
            return Ok(emp);
        }

        [HttpPut]
        public async Task<int> UpdateEmployee(Employee employee)
        {
            var emp = await _mediator.Send(new UpdateEmployeeCommand(
                employee.Id, employee.Name, employee.Address, employee.Email, employee.Phone
            ));
            return emp;
        }

        [HttpDelete("{id}")]
        public async Task<int> Delete(int id)
        {
            return await _mediator.Send(new DeleteEmployeeCommand() {Id=id});
        }
    }
}