using MediatR;

namespace CQRSMediaTr.Command
{
    public class DeleteEmployeeCommand : IRequest<int>
    {

        public int Id { get; set; }
    }
}