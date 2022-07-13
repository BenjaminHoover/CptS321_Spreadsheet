// <copyright file="ExpressionTreeTests.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetTests
{
    // NUnit 3 tests
    // See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
    using System.Collections;
    using System.Collections.Generic;
    using NUnit.Framework;
    using SpreadsheetEngine;

    /// <summary>
    /// Tests for the ExpressionTree class.
    /// </summary>
    [TestFixture]
    public class ExpressionTreeTests
    {
        /// <summary>
        /// Normal test of IsExpression method that is an expression with spaces.
        /// </summary>
        [Test]
        public void NormalIsExpressionTestTrueSpaces()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);

            Assert.That(tree.IsExpression("G6 + F2"), Is.EqualTo(true));
        }

        /// <summary>
        /// Normal test of IsExpression method that is an expression without spaces.
        /// </summary>
        [Test]
        public void NormalIsExpressionTestTrueNoSpaces()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);

            Assert.That(tree.IsExpression("G6+F2"), Is.EqualTo(true));
        }

        /// <summary>
        /// Normal test of IsExpression method that is not an expression.
        /// </summary>
        [Test]
        public void NormalIsExpressionTestFalse()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);

            Assert.That(tree.IsExpression("G6"), Is.EqualTo(false));
        }

        /// <summary>
        /// Normal test of parsing an expression with a single operator.
        /// </summary>
        [Test]
        public void ParseExpressionSingleOperator()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);
            string[] expectedOutput = { "A", "+", "B" };

            Assert.That(tree.ParseExpression("A+B"), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Normal test of parsing an expression with multiple operators.
        /// </summary>
        [Test]
        public void ParseExpressionMultipleOperators()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);
            string[] expectedOutput = { "A", "+", "B", "+", "C" };

            Assert.That(tree.ParseExpression("A+B+C"), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Fringe test of parsing an expression with no operators.
        /// </summary>
        [Test]
        public void ParseExpressionNoOperator()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);
            string[] expectedOutput = { "A" };

            Assert.That(tree.ParseExpression("A"), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Normal test of converting an expression with a single operator.
        /// </summary>
        [Test]
        public void PostFixConversionSingleOperator()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);
            string[] expectedOutput = { "A", "B", "+" };

            Assert.That(tree.PostFixConversion("A+B"), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Normal test of converting an expression with multiple operators.
        /// </summary>
        [Test]
        public void PostFixConversionMultipleOperators()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);
            string[] expectedOutput = { "A", "B", "+", "C", "+" };

            Assert.That(tree.PostFixConversion("A+B+C"), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Fringe test of converting an expression with no operators.
        /// </summary>
        [Test]
        public void PostFixConversionNoOperator()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree(string.Empty);
            string[] expectedOutput = { "A" };

            Assert.That(tree.PostFixConversion("A"), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Normal test of adding two numbers together without using variables.
        /// </summary>
        [Test]
        public void ExpressionNormalAdditionNumbers()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("5+7");

            Assert.That(tree.Evaluate(), Is.EqualTo(12.0));
        }

        /// <summary>
        /// Normal test of adding two numbers together.
        /// </summary>
        [Test]
        public void ExpressionNormalAddition()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1+B1");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);

            Assert.That(tree.Evaluate(), Is.EqualTo(12.0));
        }

        /// <summary>
        /// Normal test of adding three numbers together.
        /// </summary>
        [Test]
        public void ExpressionNormalDoubleAddition()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1+B1+Ab123");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(22.0));
        }

        /// <summary>
        /// Normal test of subtracting two numbers.
        /// </summary>
        [Test]
        public void ExpressionNormalSubtraction()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1-B1");
            tree.SetVariable("A1", 7);
            tree.SetVariable("B1", 5);

            Assert.That(tree.Evaluate(), Is.EqualTo(2.0));
        }

        /// <summary>
        /// Normal test of subtracting three numbers.
        /// </summary>
        [Test]
        public void ExpressionNormalDoubleSubtraction()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1-B1-Ab123");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(-12.0));
        }

        /// <summary>
        /// Normal test of multiply two numbers.
        /// </summary>
        [Test]
        public void ExpressionNormalMultiplication()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1*B1");
            tree.SetVariable("A1", 7);
            tree.SetVariable("B1", 5);

            Assert.That(tree.Evaluate(), Is.EqualTo(35.0));
        }

        /// <summary>
        /// Normal test of multiply three numbers.
        /// </summary>
        [Test]
        public void ExpressionNormalDoubleMultiplication()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1*B1*Ab123");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(350.0));
        }

        /// <summary>
        /// Normal test of dividing two numbers.
        /// </summary>
        [Test]
        public void ExpressionNormalDivision()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1/B1");
            tree.SetVariable("A1", 7);
            tree.SetVariable("B1", 5);

            Assert.That(tree.Evaluate(), Is.EqualTo(1.4));
        }

        /// <summary>
        /// Normal test of dividing three numbers.
        /// </summary>
        [Test]
        public void ExpressionNormalDoubleDivision()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1/B1/Ab123");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(5.0 / 7.0 / 10.0));
        }

        /// <summary>
        /// Fringe test of multiple operator types.
        /// </summary>
        [Test]
        public void ExpressionFringeMultipleOperators()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1+B1-Ab123");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(2.0));
        }

        /// <summary>
        /// Fringe test of an incomplete expression.
        /// </summary>
        [Test]
        public void ExpressionFringeIncomplete()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1+");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(5.0));
        }

        /// <summary>
        /// Fringe test of singular variable expression.
        /// </summary>
        [Test]
        public void ExpressionFringeSingularVariable()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("B1");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("Ab123", 10);

            Assert.That(tree.Evaluate(), Is.EqualTo(7.0));
        }

        /// <summary>
        /// Normal test of adding two numbers together within parenthesis.
        /// </summary>
        [Test]
        public void ExpressionNormalParenthesisOuter()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("(A1+B1)");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);

            Assert.That(tree.Evaluate(), Is.EqualTo(12.0));
        }

        /// <summary>
        /// Normal test of adding two numbers together within parenthesis and another outside.
        /// </summary>
        [Test]
        public void ExpressionNormalParenthesisInner()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("(A1+B1)+C1");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("C1", 3);

            Assert.That(tree.Evaluate(), Is.EqualTo(15.0));
        }

        /// <summary>
        /// Normal test of evaluting an expression with multiple parenthesis.
        /// </summary>
        [Test]
        public void ExpressionNormalParenthesisMultiple()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("(A1+B1)+(C1-D1)");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);
            tree.SetVariable("C1", 5);
            tree.SetVariable("D1", 2);

            Assert.That(tree.Evaluate(), Is.EqualTo(15.0));
        }

        /// <summary>
        /// Normal test of evaluting an expression with an excessive amount of parenthesis.
        /// </summary>
        [Test]
        public void ExpressionFringeParenthesisExcessive()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("((((A1+B1))))");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 7);

            Assert.That(tree.Evaluate(), Is.EqualTo(12.0));
        }

        /// <summary>
        /// Normal test of evaluting an expression with precedence without parenthesis.
        /// </summary>
        [Test]
        public void ExpressionNormalPrecedence()
        {
            CptS321.ExpressionTree tree = new CptS321.ExpressionTree("A1*B1/C1+D1");
            tree.SetVariable("A1", 5);
            tree.SetVariable("B1", 8);
            tree.SetVariable("C1", 2);
            tree.SetVariable("D1", 2);

            Assert.That(tree.Evaluate(), Is.EqualTo(22.0));
        }
    }
}
