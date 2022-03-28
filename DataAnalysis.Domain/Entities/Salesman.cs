namespace DataAnalysis.Domain.Entities
{
    public class Salesman
    {
        public Salesman(string cpf, string name, double salary)
        {
            Cpf = cpf;
            Name = name;
            Salary = salary;
        }

        public string Cpf { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
    }
}
