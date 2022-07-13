// <copyright file="TextChangeCommand.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using CptS321;

    /// <summary>
    /// Undo a text change.
    /// </summary>
    public class TextChangeCommand : ICommand
    {
        private Spreadsheet spreadsheet;
        private string prevText;
        private string newText;
        private int rowNum;
        private int columnNum;

        /// <summary>
        /// Set attributes.
        /// </summary>
        /// <param name="spreadsheet">
        /// Reference to the spreadsheet.
        /// </param>
        /// <param name="newText">
        /// New text for the cell.
        /// </param>
        /// <param name="prevText">
        /// Old text of the cell.
        /// </param>
        /// <param name="rowNum">
        /// Row index.
        /// </param>
        /// <param name="columnNum">
        /// Column index.
        /// </param>
        public void SetAttributes(ref Spreadsheet spreadsheet, string newText, string prevText, int rowNum, int columnNum)
        {
            this.spreadsheet = spreadsheet;
            this.newText = newText;
            this.prevText = prevText;
            this.rowNum = rowNum;
            this.columnNum = columnNum;
        }

        /// <summary>
        /// Gets the command name.
        /// </summary>
        /// <returns>
        /// The command name.
        /// </returns>
        public string GetCommandName()
        {
            return "Cell text change";
        }

        /// <summary>
        /// Executes the change.
        /// </summary>
        public void Execute()
        {
            this.spreadsheet.ChangeCell(this.newText, this.rowNum, this.columnNum);
        }

        /// <summary>
        /// Undos the change.
        /// </summary>
        public void UnExecute()
        {
            this.spreadsheet.ChangeCell(this.prevText, this.rowNum, this.columnNum);
        }
    }
}
