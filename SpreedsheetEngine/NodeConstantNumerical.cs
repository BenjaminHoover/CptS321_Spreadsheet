// <copyright file="NodeConstantNumerical.cs" company="Benjamin Hoover 011622025">
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
    /// Node containing a constant number.
    /// </summary>
    public class NodeConstantNumerical : NodeBase
    {
        private double value;

        /// <summary>
        /// Initializes a new instance of the <see cref="NodeConstantNumerical"/> class.
        /// </summary>
        /// <param name="newValue">
        /// The value to set as the constant.
        /// </param>
        public NodeConstantNumerical(double newValue)
        {
            this.value = newValue;
        }

        /// <summary>
        /// Returns the value of the node.
        /// </summary>
        /// <returns>
        /// The node's value.
        /// </returns>
        public override double Evaluate()
        {
            return this.value;
        }
    }
}
