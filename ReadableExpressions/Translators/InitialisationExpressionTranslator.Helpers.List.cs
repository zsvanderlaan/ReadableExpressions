﻿namespace AgileObjects.ReadableExpressions.Translators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal partial class InitialisationExpressionTranslator
    {
        private class ListInitExpressionHelper : InitExpressionHelperBase<ListInitExpression, NewExpression>
        {
            private readonly MethodCallExpressionTranslator _methodCallTranslator;

            public ListInitExpressionHelper(
                MethodCallExpressionTranslator methodCallTranslator,
                Func<Expression, TranslationContext, string> globalTranslator)
                : base(globalTranslator, exp => exp.NewExpression)
            {
                _methodCallTranslator = methodCallTranslator;
            }

            protected override bool ConstructorIsParameterless(NewExpression newExpression)
            {
                return !newExpression.Arguments.Any();
            }

            protected override IEnumerable<string> GetMemberInitialisations(
                ListInitExpression listInitialisation,
                TranslationContext context)
            {
                return listInitialisation.Initializers
                    .Select(initialisation =>
                    {
                        if (initialisation.Arguments.Count == 1)
                        {
                            return GlobalTranslator.Invoke(initialisation.Arguments.First(), context);
                        }

                        var listAddCall = _methodCallTranslator
                            .GetMethodCall(initialisation.AddMethod, initialisation.Arguments, context);

                        return listAddCall;
                    });
            }
        }
    }
}
