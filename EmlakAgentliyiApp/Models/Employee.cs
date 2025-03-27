using System;

namespace DasinmazEmlakAgentligi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public decimal BaseSalary { get; set; }
        public decimal CommissionRate { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }

        public Employee()
        {
            HireDate = DateTime.Now;
            IsActive = true;
            CommissionRate = 3; // Default 3%
        }

        public override string ToString()
        {
            return $"{FullName} ({Position})";
        }
    }
}
