using System.Collections.Generic;

namespace ShowCoords.Services
{
    public interface ICoordsCollectionPersistanceService<T> where T : ICanSerialize
    {
        void PersistToFile(IEnumerable<T> collectionToPersist);
        IEnumerable<T> LoadFromFile();
    }
}