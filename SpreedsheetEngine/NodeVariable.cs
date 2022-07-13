// <copyright file="NodeVariable.cs" company="Benjamin Hoover 011622025">
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
    /// Node containing a variable value.
    /// </summary>
    public class NodeVariable : NodeBase
    {
        private string name;
        private Dictionary<string, NodeConstantNumerical> value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeVariable"/> class.
        /// </summary>
        /// <param name="newName">
        /// The name of the variable.
        /// </param>
        /// <param name="newValue">
        /// The value of the variable.
        /// </param>
        public NodeVariable(string newName, ref Dictionary<string, NodeConstantNumerical> newValue)
        {
            this.name = newName;
            this.value = newValue;
        }

        /// <summary>
        /// Returns the value of the node.
        /// </summary>
        /// <returns>
        /// The value of the node.
        /// </returns>
        public override double Evaluate()
        {
            if (this.value.ContainsKey(this.name))
            {
                return this.value[this.name].Evaluate();
            }

            return 0.0;
        }
    }
}
