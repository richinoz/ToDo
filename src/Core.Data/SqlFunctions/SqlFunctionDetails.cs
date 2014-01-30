using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Data.SqlFunctions {
    public class SqlFunctionDetails {
        public string Name { get; set; }
        public IList<string> ColumnNames { get; set; }
        public IList<object> Parameters { get; set; }
    }
}
