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

            // Select all monto_total elements inside factura nodes where num_liq="0"
            IEnumerable<XElement> xElements = xmlDocument.Descendants("liquidacion")
                                                             .Where(l => (string)l.Attribute("num_liq") == "0")
                                                             .Elements("facturas")
                                                             .Elements("factura")
                                                             .Where(f => (string)f.Parent.Parent.Attribute("num_liq") == "0")
                                                             .Elements("conceptos")
                                                             .Elements("concepto")
                                                             .Elements("monto_total");
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