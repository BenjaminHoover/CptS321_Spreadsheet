// <copyright file="NodeBinaryOperator.cs" company="Benjamin Hoover 011622025">
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
    /// Node containing a binary operator.
    /// </summary>
    public abstract class NodeBinaryOperator : NodeBase
    {
        private NodeBase leftNode;
        private NodeBase rightNode;

        /// <summary>
        /// Gets or sets leftNode.
        /// </summary>
        public NodeBase LeftNode
        {
            get { return this.leftNode; }
            set { this.leftNode = value; }
        }

        /// <summary>
        /// Gets or sets rightNode.
        /// </summary>
        public NodeBase RightNode
        {
            get { return this.rightNode; }
            set { this.rightNode = value; }
        }
    }
}
