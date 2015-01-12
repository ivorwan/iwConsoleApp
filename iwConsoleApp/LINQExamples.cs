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
            //List<dynamic> result = CountCharsInString("iwwsdif");
            //ReadXml();
        }

        /// <summary>
        /// Given a string as parameter, returns a list if each key and its count in the string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<Result> CountCharsInString(string str)
        {
            //string test = "iwssdfi";

            var result1 = (from g in str.GroupBy(e => e)
                           select new { Key = g.Key, Count = g.Count() }).ToList();

            var result = str.GroupBy(e => e).Select(g => new Result() { Key = g.Key.ToString(), Count = g.Count() }).ToList<Result>();
            //Console.WriteLine(result);
            return result;
        }
        /// <summary>
        /// Dynamic types are internal. To expose them, add the next line to assemblyinfo, specifying friendly assemblies
        /// [assembly: InternalsVisibleTo("UnitTests")]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<dynamic> CountCharsInStringDynamic(string str)
        {
            //string test = "iwssdfi";

            var result1 = (from g in str.GroupBy(e => e)
                           select new { Key = g.Key, Count = g.Count() }).ToList();

            var result = str.GroupBy(e => e).Select(g => new { Key = g.Key.ToString(), Count = g.Count() }).ToList<dynamic>();
            //Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// Parses the xml string and returns a list of Offers
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="domainId"></param>
        /// <returns></returns>
        public List<Offer> ReadXml(Stream stream, string domainId)
        {
            //string dir = Directory.GetCurrentDirectory();
            //string xmlFile = "\\\\psf\\home\\documents\\visual studio 2013\\Projects\\iwConsoleApp\\iwConsoleApp\\Data\\XmlFile.xml";
            
            XDocument xdoc = XDocument.Load(stream);

            // -----------
            // 2 steps
            var domain = (from o in xdoc.Element("Root").Elements("Domain")
                          where o.Attribute("id").Value.Equals(domainId)
                         select o);

            var offers = (from o in domain.Elements("Offer")
                     select o).ToList();
            // -----------



            // -----------
            // 1 step
            var offers1 = (xdoc.Element("Root").Elements("Domain").Where(d => d.Attribute("id").Value.Equals("domain2")))
                .Elements("Offer").Select(o => new Offer { OfferId = Convert.ToInt32(o.Attribute("id").Value), Url = o.Element("Url").Value}).ToList<Offer>();

            // 1 step alternative
            var offers2 = from d in xdoc.Element("Root").Elements("Domain")
                          where d.Attribute("id").Value.Equals(domainId)
                          select new Offer
                          {
                              OfferId = Convert.ToInt32(d.Attribute("id").Value),
                              Url = d.Element("Url").Value
                          };
            // -----------

            return offers1;


        }
    }

    public class Result
    {
        public string Key { get; set; }
        public int Count { get; set; }
    }
    public class Offer
    {
        public int OfferId { get; set; }
        public string Url { get; set; }
    }
}
