// <copyright file="NodeOperatorFactory.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Creates operator nodes.
    /// </summary>
    public class NodeOperatorFactory
    {
        private Dictionary<char, Type> operators = new Dictionary<char, Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeOperatorFactory"/> class.
        /// </summary>
        public NodeOperatorFactory()
        {
            this.TraverseOperators((op, type) => this.operators.Add(op, type));
        }

        private delegate void OnOperator(char op, Type type);

        /// <summary>
        /// Operator function.
        /// </summary>
        /// <param name="newOperation">
        /// The operation type of the new node.
        /// </param>
        /// <returns>
        /// The new operator node.
        /// </returns>
        public NodeBinaryOperator CreateOperatorNode(string newOperation)
        {
            if (this.ValidOperator(newOperation))
            {
                object operatorNode = System.Activator.CreateInstance(this.operators[newOperation[0]]);
                if (operatorNode is NodeBinaryOperator)
                {
                    return (NodeBinaryOperator)operatorNode;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the precedence of an operator.
        /// </summary>
        /// <param name="operation">
        /// An operation.
        /// </param>
        /// <returns>
        /// The precedence.
        /// </returns>
        public int GetPrecedence(string operation)
        {
            int precedence = 0;
            if (this.ValidOperator(operation))
            {
                Type type = this.operators[operation[0]];
                PropertyInfo propertyInfo = type.GetProperty("Precedence");
                if (propertyInfo != null)
                {
                    object propertyValue = propertyInfo.GetValue(type);
                    if (propertyValue is int)
                    {
                        precedence = (int)propertyValue;
                    }
                }
            }

            return precedence;
        }

        /// <summary>
        /// Gets the associativity of an operator.
        /// </summary>
        /// <param name="operation">
        /// An operator.
        /// </param>
        /// <returns>
        /// The associativity of an operator.
        /// </returns>
        public string GetAssociativity(string operation)
        {
            string associativity = string.Empty;
            if (this.ValidOperator(operation))
            {
                Type type = this.operators[operation[0]];
                PropertyInfo propertyInfo = type.GetProperty("Associativity");
                if (propertyInfo != null)
                {
                    object propertyValue = propertyInfo.GetValue(type);
                    if (propertyValue is string)
                    {
                        associativity = (string)propertyValue;
                    }
                }
            }

            return associativity;
        }

        /// <summary>
        /// Determines if an operation is valid for this tree.
        /// </summary>
        /// <param name="operation">
        /// The operation.
        /// </param>
        /// <returns>
        /// Whether that operation is valid or not.
        /// </returns>
        public bool ValidOperator(string operation)
        {
            if (operation != string.Empty)
            {
                return this.operators.ContainsKey(operation[0]);
            }

            return false;
        }

        /// <summary>
        /// Finds all the supported operators from subclasses.
        /// </summary>
        /// <param name="onOperator">
        /// The on operator.
        /// </param>
        private void TraverseOperators(OnOperator onOperator)
        {
            Type operatorNodeType = typeof(NodeBinaryOperator);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                IEnumerable<Type> operatorTypes = assembly.GetTypes()
                    .Where(type => type.IsSubclassOf(operatorNodeType));
                foreach (var type in operatorTypes)
                {
                    PropertyInfo operatorField = type.GetProperty("Operator");
                    if (operatorField != null)
                    {
                        object value = operatorField.GetValue(type);
                        if (value is char)
                        {
                            char operatorSymbol = (char)value;
                            onOperator(operatorSymbol, type);
                        }
                    }
                }
            }
        }
    }
}
