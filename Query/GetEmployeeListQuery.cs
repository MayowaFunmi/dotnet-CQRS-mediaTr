using CQRSMediaTr.Models;
using MediatR;

namespace CQRSMediaTr.Query
{
    public class GetEmployeeListQuery : IRequest<List<Employee>>
    {
        
    }
}