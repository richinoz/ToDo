using System.Collections.Generic;

namespace Core.Data.SqlStoredProcedures {
    public class SqlStoredProcedureDetails {
        public string Name { get; set; }
        public IList<object> Parameters { get; set; } 
    }
}
