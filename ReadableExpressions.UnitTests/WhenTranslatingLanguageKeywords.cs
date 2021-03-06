﻿namespace AgileObjects.ReadableExpressions.UnitTests
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WhenTranslatingLanguageKeywords
    {
        [TestMethod]
        public void ShouldTranslateADefaultExpression()
        {
            var defaultInt = Expression.Default(typeof(uint));
            var translated = defaultInt.ToReadableString();

            Assert.AreEqual("default(uint)", translated);
        }

        [TestMethod]
        public void ShouldIgnoreADefaultVoidExpression()
        {
            var defaultVoid = Expression.Default(typeof(void));
            var translated = defaultVoid.ToReadableString();

            Assert.IsNull(translated);
        }

        [TestMethod]
        public void ShouldEscapeAKeywordVariable()
        {
            VerifyIsEscaped("int");
            VerifyIsEscaped("typeof");
            VerifyIsEscaped("default");
            VerifyIsEscaped("void");
            VerifyIsEscaped("readonly");
            VerifyIsEscaped("do");
            VerifyIsEscaped("while");
            VerifyIsEscaped("switch");
            VerifyIsEscaped("if");
            VerifyIsEscaped("else");
            VerifyIsEscaped("try");
            VerifyIsEscaped("catch");
            VerifyIsEscaped("finally");
            VerifyIsEscaped("throw");
            VerifyIsEscaped("for");
            VerifyIsEscaped("foreach");
            VerifyIsEscaped("goto");
            VerifyIsEscaped("return");
            VerifyIsEscaped("implicit");
            VerifyIsEscaped("explicit");
        }

        private static void VerifyIsEscaped(string keyword)
        {
            var variable = Expression.Variable(typeof(bool), keyword);
            var translated = variable.ToReadableString();

            Assert.AreEqual("@" + keyword, translated);
        }

        [TestMethod]
        public void ShouldTranslateADeclaredBlockVariableKeyword()
        {
            var variable = Expression.Variable(typeof(string), "string");
            Expression<Action> writeLine = () => Console.WriteLine("La la la");
            var block = Expression.Block(new[] { variable }, writeLine.Body);
            var translated = block.ToReadableString();

            const string EXPECTED = @"
string @string;
Console.WriteLine(""La la la"");";

            Assert.AreEqual(EXPECTED.TrimStart(), translated);
        }
    }
}
