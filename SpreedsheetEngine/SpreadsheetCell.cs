// <copyright file="SpreadsheetCell.cs" company="Benjamin Hoover 011622025">
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
    /// Inherits Cell.
    /// </summary>
    public class SpreadsheetCell : Cell
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetCell"/> class.
        /// </summary>
        /// <param name="newText">
        /// The new text of the cell.
        /// </param>
        /// <param name="newRowInt">
        /// The row index.
        /// </param>
        /// <param name="newColumnInt">
        /// The column index.
        /// </param>
        public SpreadsheetCell(string newText, int newRowInt, int newColumnInt)
            : base(newRowInt, newColumnInt, newText)
        {
        }

        /// <summary>
        /// Sets the value of the cell.
        /// </summary>
        /// <param name="newValue">
        /// The new value for the cell.
        /// </param>
        public void SetValue(string newValue)
        {
            this.value = newValue;
        }
    }
}
