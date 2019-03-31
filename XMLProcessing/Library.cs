using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XMLProcessing
{
    [XmlRoot]
    public class Library
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("address")]
        public string Address { get; set; }
        
        [XmlArray]
        //[XmlElement]
        public List<Book> Books { get; set; }
    }
}
