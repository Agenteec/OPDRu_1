using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPDRu.data
{
    public class TestUnit
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Level { get; set; }
        public int TestID { get; set; }
        public RuTest Test { get; set; }
        public IEnumerable <Answer> Answers { get; set; } = Enumerable.Empty<Answer>();
    }
}
