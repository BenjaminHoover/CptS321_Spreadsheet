// <copyright file="NodeBase.cs" company="Benjamin Hoover 011622025">
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
    /// The base for all node types.
    /// </summary>
    public abstract class NodeBase
    {
        /// <summary>
        /// Abstract delcaration of Evalutate, to be defined in derived classes.
        /// </summary>
        /// <returns>
        /// The evaluation.
        /// </returns>
        public abstract double Evaluate();
    }
}
