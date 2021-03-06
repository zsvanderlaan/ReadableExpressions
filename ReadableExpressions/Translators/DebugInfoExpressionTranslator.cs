namespace AgileObjects.ReadableExpressions.Translators
{
    using System.Globalization;
    using System.Linq.Expressions;

    internal class DebugInfoExpressionTranslator : ExpressionTranslatorBase
    {
        public DebugInfoExpressionTranslator()
            : base(ExpressionType.DebugInfo)
        {
        }

        public override string Translate(Expression expression, TranslationContext context)
        {
            var debugInfo = (DebugInfoExpression)expression;

            string debugInfoText;

            if (debugInfo.IsClear)
            {
                debugInfoText = $"Clear debug info from {debugInfo.Document.FileName}";
            }
            else
            {
                debugInfoText = string.Format(
                    CultureInfo.InvariantCulture,
                    "Debug to {0}, {1}, {2} -> {3}, {4}",
                    debugInfo.Document.FileName,
                    debugInfo.StartLine,
                    debugInfo.StartColumn,
                    debugInfo.EndLine,
                    debugInfo.EndColumn);
            }

            var debugInfoComment = ReadableExpression.Comment(debugInfoText);

            return context.Translate(debugInfoComment);
        }
    }
}