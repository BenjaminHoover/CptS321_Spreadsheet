// <copyright file="Demo.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CptS321;

    /// <summary>
    /// The demo functionality.
    /// </summary>
    public class Demo
    {
        private ExpressionTree expressionTree = new ExpressionTree(string.Empty);

        /// <summary>
        /// Runs the demo.
        /// </summary>
        public void Run()
        {
            int userChoice = 0;
            do
            {
                this.PrintMenu();
                string tempChoice = Console.ReadLine();
                bool validChoice = int.TryParse(tempChoice, out userChoice);
                if (validChoice && (userChoice > 0 && userChoice <= 4))
                {
                    switch (userChoice)
                    {
                        case 1:
                            this.expressionTree = this.SetExpression();
                            break;
                        case 2:
                            string name = string.Empty;
                            double value = 0.0;
                            bool validVariable = this.SetVariable(ref name, ref value);
                            if (validVariable && this.expressionTree != null)
                            {
                                this.expressionTree.SetVariable(name, value);
                            }
                            else if (this.expressionTree != null)
                            {
                                this.expressionTree.SetVariable(name, 0.0);
                            }
                            else
                            {
                                Console.WriteLine("ERROR: No expression tree defined.");
                            }

                            break;
                        case 3:
                            Console.WriteLine(this.expressionTree.Evaluate());
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("INVALID INPUT: Only the numbers 1-4 are valid.");
                }
            }
            while (userChoice != 4);
        }

        /// <summary>
        /// Prints the menu.
        /// </summary>
        public void PrintMenu()
        {
            Console.WriteLine("Menu (current expression=\"" + this.expressionTree.Expression + "\")");
            Console.WriteLine("1 = Enter a new expression");
            Console.WriteLine("2 = Set a variable value");
            Console.WriteLine("3 = Evaluate tree");
            Console.WriteLine("4 = Quit");
            Console.Write(">>> ");
        }

        /// <summary>
        /// Sets a new expression.
        /// </summary>
        /// <returns>
        /// An ExpressionTree object.
        /// </returns>
        public ExpressionTree SetExpression()
        {
            Console.Write("Enter new expression >>> ");
            return new ExpressionTree(Console.ReadLine());
        }

        /// <summary>
        /// Sets a variable.
        /// </summary>
        /// <param name="name">
        /// Reference to name defined in run.
        /// </param>
        /// <param name="value">
        /// Reference to value defined in run.
        /// </param>
        /// <returns>
        /// If the value was successfully set.
        /// </returns>
        public bool SetVariable(ref string name, ref double value)
        {
            Console.Write("Enter variable name >>> ");
            name = Console.ReadLine();
            Console.Write("Enter variable value >>> ");
            string tempValue = Console.ReadLine();

            bool valid = double.TryParse(tempValue, out value);
            if (valid)
            {
                return true;
            }

            return false;
        }
    }
}
