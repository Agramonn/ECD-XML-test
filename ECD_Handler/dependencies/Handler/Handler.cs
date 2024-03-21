using System.Xml.Linq;

namespace ECD_Handler.Handler
{
    class ECD_File
    {
        // O. class to handle xml statements
        public static IEnumerable<XElement> parseXML(string file)
        {
            // Loading the XML file using Xml.Linq
            XDocument xmlDocument = XDocument.Load(file);

            // Go into every node that cointains the value that we want and return the IEnumerable of Elements
            IEnumerable<XElement> xElements = xmlDocument.Descendants("monto_total");
            return xElements;
        } 

        public static decimal calculateBalance(IEnumerable<XElement> xElements)
        {
            decimal totalBalance = 0;

            //We interate into the XElement of the Document and add the monto_total to the totalBalance
            foreach (XElement xElement in xElements)
            {

                if (xElement.Value != null && decimal.TryParse(xElement.Value, out decimal montoTotal))
                {
                    totalBalance += montoTotal;
                }
            }

            return totalBalance;
        }
    }
}