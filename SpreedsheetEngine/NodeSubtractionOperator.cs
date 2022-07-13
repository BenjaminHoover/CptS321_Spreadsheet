namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Node of Subtraction operation.
    /// </summary>
    public class NodeSubtractionOperator : NodeBinaryOperator
    {
        /// <summary>
        /// Gets Operator.
        /// </summary>
        public static char Operator => '-';

        /// <summary>
        /// Gets precedence.
        /// </summary>
        public static int Precedence => 7;

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
            return this.LeftNode.Evaluate() - this.RightNode.Evaluate();
        }
    }
}
