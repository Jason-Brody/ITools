using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SAPScriptTemplate
{
    class SC_101
    {
        [DataSetBinding("sheet1","colName")]
        public string Order
        {
            get
            {
                return "abc";
            }
            set
            {
                this.Order = value;
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property,AllowMultiple=true)]
    public class DataSetBindingAttribute:Attribute
    {
        public string TableName { get; set; }
        public string ColName { get; set; }
        public DataSetBindingAttribute(string TableName,string ColumnName)
        {
            this.TableName = TableName;
            this.ColName = ColName;
        }

        public string SampleData { get; set; }
    }
}
