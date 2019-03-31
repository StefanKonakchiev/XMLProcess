using System.Xml.Serialization;

namespace XMLProcessing
{
    public class Author
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("nationality")]
        public string Nationality { get; set; }
    }
}