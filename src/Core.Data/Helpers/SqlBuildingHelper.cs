using System.Collections.Generic;
using System.Text;

namespace Core.Data.Helpers {
    public class SqlBuildingHelper {
        public static string SqlParameterList(int count, string outputElement, string separator) {
          
            var inputList = new List<string>();

            for (int i = 0; i < count; i++) {
                inputList.Add(outputElement);
            }

            return string.Join(separator, inputList);
        }

        public static string SqlParameterList(IList<string> elements, string separator) {
            
            return string.Join(separator, elements);
        }

        
    }
}
