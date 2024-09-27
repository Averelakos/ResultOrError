using ResultOrError.Factories;
using ResultOrError.Partials;

namespace Tests;

public static class Convert
{
    public static ResultOrError<string> ToString(int num) => num.ToString();

    public static ResultOrError<int> ToInt(string str) => int.Parse(str);

    public static Task<ResultOrError<int>> ToIntAsync(string str) => Task.FromResult(ResultOrErrorFactory.From(int.Parse(str)));

    public static Task<ResultOrError<string>> ToStringAsync(int num) => Task.FromResult(ResultOrErrorFactory.From(num.ToString()));
}
