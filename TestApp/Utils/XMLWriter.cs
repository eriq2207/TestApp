using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TestApp.Models;

namespace TestApp.Utils
{
    class XMLWriter
    {
        public void SaveXML(List<Towar> towary, String fileName)
        {
            XmlWriter writer = XmlWriter.Create(fileName + ".xml");
            writer.WriteStartElement("Plik");
            writer.WriteStartElement("Towary");
            foreach (var towar in towary)
            {
                writer.WriteStartElement("Towar");
                writer.WriteElementString("Nazwa", towar.Nazwa);
                writer.WriteElementString("Nazwa", towar.Cena.ToString());
                writer.WriteStartElement("Opis");
                writer.WriteElementString("A", towar.Opis.A);
                writer.WriteElementString("B", towar.Opis.B);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
            writer.WriteEndElement();
            writer.Flush();
        }
    }
    

}
