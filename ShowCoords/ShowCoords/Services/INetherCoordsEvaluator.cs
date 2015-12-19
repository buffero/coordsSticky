namespace ShowCoords.Services
{
    public interface INetherCoordsEvaluator
    {
        decimal EvaluateCoordsToOverworld(string coord);
        decimal EvaluateCoordsToNether(string coord);
    }
}