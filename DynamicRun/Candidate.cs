namespace DynamicRun
{
    public class Candidate : ObjectFromString
    {
        public Candidate(string rowData) : base(rowData)
        {
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string CompanyName { get; private set; }
        public string Address { get; private set; }
        public string City { get; private set; }
        public string County { get; private set; }
        public string State { get; private set; }
        public string Zip { get; private set; }
        public string[] Skills { get; private set; }

        public override void LoadFromFields()
        {
            this.FirstName = this.Fields[0];
            this.LastName = this.Fields[1];
            this.CompanyName = this.Fields[2];
            this.Address = this.Fields[3];
            this.City = this.Fields[4];
            this.County = this.Fields[5];
            this.State = this.Fields[6];
            this.Zip = this.Fields[7];
            this.Skills = this.Fields[12].Split(';');
        }
    }
}
