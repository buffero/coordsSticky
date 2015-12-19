namespace ShowCoords
{
    public interface IConfigReader<T>
    {
        T CoordsConfig { get; set; }
    }
}