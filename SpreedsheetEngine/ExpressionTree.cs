// <copyright file="ExpressionTree.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the functionality of expression trees.
    /// </summary>
    public class ExpressionTree
    {
        private NodeBase root;
        private NodeOperatorFactory nodeOperatorFactory = new NodeOperatorFactory();
        private Dictionary<string, NodeConstantNumerical> variableDictionary;
        private Stack<NodeBase> postFixExpression;
        private string expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpressionTree"/> class.
        /// </summary>
        /// <param name="expression">
        /// The expression to be evaluated.
        /// </param>
        public ExpressionTree(string expression)
        {
            this.variableDictionary = new Dictionary<string, NodeConstantNumerical>();
            this.postFixExpression = new Stack<NodeBase>();
            this.expression = expression;

            // BuildTree takes the expression split by spaces.
            this.BuildTree(this.PostFixConversion(expression));
        }

        /// <summary>
        /// Gets the expression of the tree.
        /// </summary>
        public string Expression
        {
            get { return this.expression; }
        }

        /// <summary>
        /// Gets the variable dictionarty and returns it.
        /// </summary>
        public Dictionary<string, NodeConstantNumerical> VariableDictionary
        {
            get { return this.variableDictionary; }
        }

        /// <summary>
        /// Determines if a cell's value is an expresion.
        /// </summary>
        /// <param name="cellValue">
        /// The cell's value.
        /// </param>
        /// <returns>
        /// If the value is an expression or not.
        /// </returns>
        public bool IsExpression(string cellValue)
        {
            foreach (char character in cellValue)
            {
                if (this.nodeOperatorFactory.ValidOperator(character.ToString()))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Parses the expression into an array of strings.
        /// </summary>
        /// <param name="expression">
        /// The passed in expression.
        /// </param>
        /// <returns>
        /// An array of strings.
        /// </returns>
        public string[] ParseExpression(string expression)
        {
            StringBuilder tempExpression = new StringBuilder(expression);

            for (int index = 0; index < tempExpression.Length; index++)
            {
                if (this.nodeOperatorFactory.ValidOperator(tempExpression[index].ToString()) || tempExpression[index] == '(' || tempExpression[index] == ')')
                {
                    tempExpression = tempExpression.Insert(index, ' ');
                    tempExpression = tempExpression.Insert(index + 2, ' ');
                    index += 2;
                }
            }

            // With parenthesis there is extra white space that this removes.
            string[] tempExpressionTrimmed = tempExpression.ToString().Trim().Split(' ');
            int deletedWhiteSpaces = 0;
            if (tempExpressionTrimmed.Length > 1)
            {
                for (int i = 0; i < tempExpressionTrimmed.Length; i++)
                {
                    if (tempExpressionTrimmed[i] == string.Empty)
                    {
                        // Increment amount of the array size reduction and move all items left by one.
                        deletedWhiteSpaces++;
                        for (int j = i; j < tempExpressionTrimmed.Length - 1; j++)
                        {
                            tempExpressionTrimmed[j] = tempExpressionTrimmed[j + 1];
                        }
                    }
                }
            }

            // Resize array to remove extra length.
            Array.Resize(ref tempExpressionTrimmed, tempExpressionTrimmed.Length - deletedWhiteSpaces);
            return tempExpressionTrimmed;
        }

        /// <summary>
        /// Counts the number of parenthesis in an expression to later be subtracted from the array length.
        /// </summary>
        /// <param name="expression">
        /// The expression entered from the user.
        /// </param>
        /// <returns>
        /// The number of parenthesis in teh expression.
        /// </returns>
        public int CountParenthesis(string expression)
        {
            int parenthesisNum = 0;
            foreach (char character in expression)
            {
                if (character == '(' || character == ')')
                {
                    parenthesisNum++;
                }
            }

            return parenthesisNum;
        }

        /// <summary>
        /// Converts an expression to postfix notation.
        /// </summary>
        /// <param name="expression">
        /// The input expression.
        /// </param>
        /// <returns>
        /// The array of the expression in postfix notation.
        /// </returns>
        public string[] PostFixConversion(string expression)
        {
            // Parse the expression into an array to convert.
            string[] stringArray = this.ParseExpression(expression);
            string[] postFixExpression = new string[stringArray.Length];
            Stack<string> operatorStack = new Stack<string>();
            int postFixIndex = 0;
            int parenthesisNum = this.CountParenthesis(expression);
            for (int i = 0; i < stringArray.Length; i++)
            {
                // Check of item is an operator, else its a variable.
                if (this.nodeOperatorFactory.ValidOperator(stringArray[i]))
                {
                    // item is an operator, depending on precedence it will either replace top of the stack or just be pushed on.
                    if (operatorStack.Count > 0 && this.nodeOperatorFactory.GetPrecedence(operatorStack.Peek()) < this.nodeOperatorFactory.GetPrecedence(stringArray[i]))
                    {
                        postFixExpression[postFixIndex] = operatorStack.Pop();
                        operatorStack.Push(stringArray[i]);
                        postFixIndex++;
                    }
                    else if (operatorStack.Count > 0 && this.nodeOperatorFactory.GetPrecedence(operatorStack.Peek()) == this.nodeOperatorFactory.GetPrecedence(stringArray[i])
                        && this.nodeOperatorFactory.GetAssociativity(stringArray[i]) == "right")
                    {
                        postFixExpression[postFixIndex] = operatorStack.Pop();
                        operatorStack.Push(stringArray[i]);
                        postFixIndex++;
                    }
                    else
                    {
                        operatorStack.Push(stringArray[i]);
                    }
                }
                else if (stringArray[i] == "(")
                {
                    // Using recursion to handle the parenthesis
                    // Create a sub expression.
                    StringBuilder tempExp = new StringBuilder();
                    int innerParenthesis = 1;
                    i++;
                    while (stringArray.Length > i)
                    {
                        // If there are inner parenthesis, keep them for their own sub expression
                        if (stringArray[i] == ")")
                        {
                            innerParenthesis--;
                            if (innerParenthesis <= 0)
                            {
                                break;
                            }
                        }
                        else if (stringArray[i] == "(")
                        {
                            innerParenthesis++;
                        }

                        tempExp.Append(stringArray[i]);
                        i++;
                    }

                    // Recursive step
                    string[] tempPost = this.PostFixConversion(tempExp.ToString());

                    // Copy the result of the recursion into the main expression.
                    foreach (string item in tempPost)
                    {
                        postFixExpression[postFixIndex] = item;
                        postFixIndex++;
                    }
                }
                else
                {
                    // Normal variable
                    postFixExpression[postFixIndex] = stringArray[i];
                    postFixIndex++;
                }
            }

            // Pop the remaining operators onto the expression.
            while (operatorStack.Count > 0)
            {
                postFixExpression[postFixIndex] = operatorStack.Pop();
                postFixIndex++;
            }

            Array.Resize(ref postFixExpression, postFixExpression.Length - parenthesisNum);
            return postFixExpression;
        }

        /// <summary>
        /// Builds the expression tree.
        /// </summary>
        /// <param name="expressionArray">
        /// The array of the expression.
        /// </param>
        public void BuildTree(string[] expressionArray)
        {
            // The below node declarations are to store the variables until a binary operator declared.
            Stack<NodeBase> nodeStorage = new Stack<NodeBase>();
            foreach (string item in expressionArray)
            {
                // If an operator, else a variable.
                if (this.nodeOperatorFactory.ValidOperator(item))
                {
                    if (nodeStorage.Count >= 2)
                    {
                        NodeBinaryOperator operation = this.nodeOperatorFactory.CreateOperatorNode(item);
                        operation.RightNode = nodeStorage.Pop();
                        operation.LeftNode = nodeStorage.Pop();
                        nodeStorage.Push(operation);
                    }
                    else
                    {
                        nodeStorage.Push(this.nodeOperatorFactory.CreateOperatorNode(item));
                    }
                }
                else
                {
                    // If item is a variable or just a number.
                    double newDouble; // can no longer have a default value
                    double.TryParse(item, out newDouble);
                    this.SetVariable(item, newDouble);

                    nodeStorage.Push(new NodeVariable(item, ref this.variableDictionary));
                }
            }

            // For handling cases where the expression is incomplete.
            if (nodeStorage.Count > 1)
            {
                nodeStorage.Pop();
            }

            this.root = nodeStorage.Pop();
        }

        /// <summary>
        /// Set varaibles.
        /// </summary>
        /// <param name="variableName">
        /// The name of the variable.
        /// </param>
        /// <param name="variableValue">
        /// The value of the variable.
        /// </param>
        public void SetVariable(string variableName, double variableValue)
        {
            this.variableDictionary[variableName] = new NodeConstantNumerical(variableValue);
        }

        /// <summary>
        /// Sets all the variables with another dictionaries values.
        /// </summary>
        /// <param name="newVariables">
        /// The dictionary containing the new variable's values.
        /// </param>
        public void FillVariables(Dictionary<string, NodeConstantNumerical> newVariables)
        {
            foreach (KeyValuePair<string, NodeConstantNumerical> pair in newVariables)
            {
                this.variableDictionary[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Evaluate the expression.
        /// </summary>
        /// <returns>
        /// The result of the expression.
        /// </returns>
        public double Evaluate()
        {
            try
            {
                return this.root.Evaluate();
            }
            catch
            {
                return 0.0;
            }
        }
    }
}
