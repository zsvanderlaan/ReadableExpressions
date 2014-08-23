﻿namespace AgileObjects.ReadableExpressions.Translators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal static class TranslationHelper
    {
        internal static string GetParameters<TExpression>(
            IEnumerable<TExpression> parameters,
            IExpressionTranslatorRegistry translatorRegistry)
            where TExpression : Expression
        {
            if (!parameters.Any())
            {
                return "()";
            }

            var parametersString = string.Join(
                ", ",
                parameters.Select(translatorRegistry.Translate));

            if (parameters.Count() > 1)
            {
                parametersString = "(" + parametersString + ")";
            }

            return parametersString;
        }
    }
}
