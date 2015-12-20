using System.Collections.Generic;
using System.Xml.Serialization;

namespace ShowCoords.Services
{
    [XmlRoot(ElementName = "CoordsList")]
    public class SerializableContainer<T>
    {
        [XmlElement(ElementName = "CoordsItems")]
        public List<T> Wrapper
        {
            get; set;
        }
    }
}
