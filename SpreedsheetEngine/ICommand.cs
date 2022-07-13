// <copyright file="ICommand.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface for commands.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Set initial attributes of the command.
        /// </summary>
        /// <param name="spreadsheet">
        /// Reference to the spreadsheet.
        /// </param>
        /// <param name="newValue">
        /// New value for a cell.
        /// </param>
        /// <param name="prevValue">
        /// Old value of a cell.
        /// </param>
        /// <param name="rowNum">
        /// Row index.
        /// </param>
        /// <param name="columnNum">
        /// Column index.
        /// </param>
        void SetAttributes(
            ref Spreadsheet spreadsheet,
            string newValue,
            string prevValue,
            int rowNum,
            int columnNum);

        /// <summary>
        /// Gets the name of the command.
        /// </summary>
        /// <returns>
        /// The name of the command.
        /// </returns>
        string GetCommandName();

        /// <summary>
        /// Execution of the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// The reverse of the execution of a command.
        /// </summary>
        void UnExecute();
    }
}
