namespace NetworkLogParseManager.Models
{
    public interface ILogLine
    {
        char ExpressionDelimiter { get; }
        string ExpressionSeparatorPattern { get; }
        LogFieldExpression ExpressionStructure { get; set; }
        LogField[] LogFieldArray { get; set; }
        char Separator { get; set; }
    }
}