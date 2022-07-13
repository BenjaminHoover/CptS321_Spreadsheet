// <copyright file="Program.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetConsole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Contains the main program.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The main function.
        /// </summary>
        /// <param name="args">
        /// The default main input.
        /// </param>
        public static void Main(string[] args)
        {
            Demo demo = new Demo();
            demo.Run();
        }
    }
}
