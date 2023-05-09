using CQRSMediaTr.Models;
using MediatR;

namespace CQRSMediaTr.Query
{
    public class GetEmployeeByIdQuery : IRequest<Employee>
    {
        public int Id { get; set; }
    }
}