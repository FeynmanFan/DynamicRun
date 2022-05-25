namespace DynamicRun
{
    using System.Collections.Generic;

    public abstract class ObjectFromString
    {
        public string[] Fields {get;set;}

        public ObjectFromString(string rowData)
        {
            this.Fields = rowData.Split(',');

            this.LoadFromFields();
        }

        public abstract void LoadFromFields();
    }
}
