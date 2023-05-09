using CQRSMediaTr.Command;
using CQRSMediaTr.Services;
using MediatR;

namespace CQRSMediaTr.Handlers
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<int> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.Id);
            if (employee == null)
                return default;
            employee.Name = request.Name;
            employee.Email = request.Email;
            employee.Address = request.Address;
            employee.Phone = request.Phone;
            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }
    }
}