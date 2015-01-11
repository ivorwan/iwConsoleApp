using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace iwConsoleApp
{
    public class LINQExamples
    {
        public LINQExamples()
        {
            ReadXml();
        }

        public void CountCharsInString()
        {
            string test = "iwssdfi";

            var result1 = (from g in test.GroupBy(e => e)
                           select new { Key = g.Key, Count = g.Count() }).ToList();

            var result = test.GroupBy(e => e).Select(g => new { Key = g.Key, Count = g.Count() }).ToList();
            Console.WriteLine(result);
        }

        public void ReadXml()
        {
            string dir = Directory.GetCurrentDirectory();
            string xmlFile = "\\\\psf\\home\\documents\\visual studio 2013\\Projects\\iwConsoleApp\\iwConsoleApp\\Data\\XmlFile.xml";
            
            XDocument xdoc = XDocument.Load(xmlFile);

            // -----------
            // 2 steps
            var domain = (from o in xdoc.Element("Root").Elements("Domain")
                         where o.Attribute("id").Value.Equals("domain1")
                         select o);

            var offers = (from o in domain.Elements("Offer")
                     select o).ToList();
            // -----------



            // -----------
            // 1 step
            var offers1 = (xdoc.Element("Root").Elements("Domain").Where(d => d.Attribute("id").Value.Equals("domain2")))
                .Elements("Offer").ToList();

            var offers2 = from d in xdoc.Element("Root").Elements("Domain")
                           where d.Attribute("id").Value.Equals("domain1")
                           select d;
            // -----------



        }
    }
}
