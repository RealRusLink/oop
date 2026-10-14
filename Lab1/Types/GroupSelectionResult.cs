namespace Lab1.Types;


public abstract record GroupSelectionResult
{
    public sealed record Ok(string Selection) : GroupSelectionResult;

    public sealed record Cancel : GroupSelectionResult
    {
        public static Cancel Instance => new ();
    };
};