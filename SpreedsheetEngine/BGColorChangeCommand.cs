namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Implementation for BGColor changing command.
    /// </summary>
    public class BGColorChangeCommand : ICommand
    {
        private Spreadsheet spreadsheet;
        private string prevColor;
        private string newColor;
        private int rowNum;
        private int columnNum;

        /// <summary>
        /// Set attributes.
        /// </summary>
        /// <param name="spreadsheet">
        /// Reference to the spreadsheet.
        /// </param>
        /// <param name="newColor">
        /// New text for the cell.
        /// </param>
        /// <param name="prevColor">
        /// Old text of the cell.
        /// </param>
        /// <param name="rowNum">
        /// Row index.
        /// </param>
        /// <param name="columnNum">
        /// Column index.
        /// </param>
        public void SetAttributes(ref Spreadsheet spreadsheet, string newColor, string prevColor, int rowNum, int columnNum)
        {
            this.spreadsheet = spreadsheet;
            this.newColor = newColor;
            this.prevColor = prevColor;
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
            return "Changing cell background color";
        }

        /// <summary>
        /// Executes the change.
        /// </summary>
        public void Execute()
        {
            this.spreadsheet.ChangeBGColor(uint.Parse(this.newColor), this.rowNum, this.columnNum);
        }

        /// <summary>
        /// Undos the change.
        /// </summary>
        public void UnExecute()
        {
            this.spreadsheet.ChangeBGColor(uint.Parse(this.prevColor), this.rowNum, this.columnNum);
        }
    }
}
