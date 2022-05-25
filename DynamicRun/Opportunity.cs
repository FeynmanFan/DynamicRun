namespace DynamicRun
{
    public class Opportunity : ObjectFromString
    {
        public Opportunity(string rowData) : base(rowData)
        {
        }

        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Skill { get; set; }

        public override void LoadFromFields()
        {
            this.Company = this.Fields[0];
            this.City = this.Fields[1];
            this.State = this.Fields[2];
            this.Skill = this.Fields[3];
        }
    }
}
