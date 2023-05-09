using CQRSMediaTr.Data;
using CQRSMediaTr.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSMediaTr.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext _dbContext;

        public EmployeeRepository(DataContext dbContext) => _dbContext = dbContext;
        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            var result = _dbContext.Employees.Add(employee);
            await _dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<int> DeleteEmployeeAsync(int Id)
        {
            var filteredData = _dbContext.Employees.Where(x => x.Id == Id).FirstOrDefault();
            _dbContext.Employees.Remove(filteredData);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int Id)
        {
            var filteredData = await _dbContext.Employees.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return filteredData;
        }

        public async Task<List<Employee>> GetEmployeeListAsync()
        {
            var filteredData = await _dbContext.Employees.ToListAsync();
            return filteredData;
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            _dbContext.Employees.Update(employee);
            return await _dbContext.SaveChangesAsync();
        }
    }
}