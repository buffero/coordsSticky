using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace ShowCoords.Services
{
    public class CoordsPersistanceService<T> : ICoordsCollectionPersistanceService<T> where T : ICanSerialize
    {
        private const string XmlFileName = "coordsConfig.xml";
        private readonly string _xmlFilePath;

        public CoordsPersistanceService()
        {
            _xmlFilePath = Path.Combine(Environment.CurrentDirectory, XmlFileName);
        }

        public void PersistToFile(IEnumerable<T> collectionToPersist)
        {            
            var hasItems = collectionToPersist.Any() && collectionToPersist.Any(p => p.CanSerialize());
            if (hasItems)
            {
                var itemsForSerialization = collectionToPersist.Where(p => p.CanSerialize());

                SerializableContainer<T> container = new SerializableContainer<T>();
                container.Wrapper = itemsForSerialization.ToList();

                var serializer = new XmlSerializer(typeof(SerializableContainer<T>));
                using (TextWriter writer = new StreamWriter(_xmlFilePath))
                {
                    serializer.Serialize(writer, container);
                }
            }           
        }

        public IEnumerable<T> LoadFromFile()
        {
            try {
                if (File.Exists(_xmlFilePath))
                {
                    var serializer = new XmlSerializer(typeof(SerializableContainer<T>));
                    using (TextReader reader = new StreamReader(_xmlFilePath))
                    {
                        var deserializedResult = serializer.Deserialize(reader);
                        var container = deserializedResult as SerializableContainer<T>;
                        return container.Wrapper;
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                // Eat this exception, om nom nom, very bad
            }            
            return Enumerable.Empty<T>();
        }        
    }
}
