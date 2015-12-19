using System;

namespace ShowCoords.Services
{
    public class NetherCoordsEvaluator : INetherCoordsEvaluator
    {
        private readonly int _multiplier;

        public NetherCoordsEvaluator(int multiplier)
        {
            _multiplier = multiplier;
        }

        public decimal EvaluateCoordsToOverworld(string coord)
        {
            return EvaluateCoordsOperand(coord, (a, b) => a * b);
        }

        public decimal EvaluateCoordsToNether(string coord)
        {
            return EvaluateCoordsOperand(coord, (a, b) => a / b);
        }

        private decimal EvaluateCoordsOperand(string coord, Func<decimal, decimal, decimal> operand)
        {
            decimal result;
            if (decimal.TryParse(coord, out result))
            {
                return Math.Floor(operand.Invoke(result, _multiplier));
            };
            return 0;
        }
    }
}
