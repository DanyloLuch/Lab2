using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2
{
    public class ParserContext
    {
        private IParser _parser;

        public void SetParser(IParser parser)
        {
            _parser = parser;
        }

        public List<PersonViewModel> ParseXml(string xml)
        {
            return _parser?.Parse(xml);
        }
    } 

}

