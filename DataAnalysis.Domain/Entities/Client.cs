namespace DataAnalysis.Domain.Entities
{
    public class Client
    {
        public Client(string cnpj, string name, string businessArea)
        {
            Cnpj = cnpj;
            Name = name;
            BusinessArea = businessArea;
        }

        public string Cnpj { get; set; }
        public string Name { get; set; }
        public string BusinessArea { get; set; }
    }
}
