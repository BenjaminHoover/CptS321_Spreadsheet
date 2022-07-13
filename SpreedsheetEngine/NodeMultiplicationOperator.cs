namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Node of multiplication operation.
    /// </summary>
    public class NodeMultiplicationOperator : NodeBinaryOperator
    {
        /// <summary>
        /// Gets Operator.
        /// </summary>
        public static char Operator => '*';

        /// <summary>
        /// Gets precedence.
        /// </summary>
        public static int Precedence => 6;

        /// <summary>
        /// Gets associativity.
        /// </summary>
        public static string Associativity => "right";

        /// <summary>
        /// Evaluation of the operation.
        /// </summary>
        /// <returns>
        /// The result of the operation.
        /// </returns>
        public override double Evaluate()
        {
            return this.LeftNode.Evaluate() * this.RightNode.Evaluate();
        }
    }
}
