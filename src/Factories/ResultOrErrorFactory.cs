using ResultOrError.Partials;

namespace ResultOrError.Factories
{
    public static class ResultOrErrorFactory
    {
        public static ResultOrError<TValue> From<TValue>(TValue value)
        {
            return value;
        }
    }
}
