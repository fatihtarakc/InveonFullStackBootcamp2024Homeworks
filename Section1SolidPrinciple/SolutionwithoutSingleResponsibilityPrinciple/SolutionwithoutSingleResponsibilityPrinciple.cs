namespace Section1SolidPrinciple.SolutionwithoutSingleResponsibilityPrinciple
{
    public class Employee
    {
        public string FullName { get; set; }
        public Company Company { get; set; }

        public void Add(Employee employee)
        {
        }

        public void ChangeCompany(Employee employee, Company newCompany)
        {
        }
    }

    public class Company
    {
        public Company()
        {
            Employees = new List<Employee>();
        }

        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }

        public void Add(Company company)
        {
        }

        public void AddEmployee(Employee employee)
        {
        }
    }
}
