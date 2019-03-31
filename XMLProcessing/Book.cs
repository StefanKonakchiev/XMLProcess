using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XMLProcessing
{
    public class Book
    {
        [XmlAttribute("language")]
        public string Language { get; set; }

        [XmlElement]
        public string Title { get; set; }

        [XmlElement]
        public Author Author { get; set; }

        [XmlIgnore]
        public decimal Price { get; set; }
    }
}
