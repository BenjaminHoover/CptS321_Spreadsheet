// <copyright file="Spreadsheet.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml;
    using CptS321;

    /// <summary>
    /// The spreedsheet class.
    /// </summary>
    public class Spreadsheet : INotifyPropertyChanged
    {
        private Cell[,] cellArray;
        private int rowCount;
        private int columnCount;
        private SpreadsheetInvoker invoker;

        // cCell is the current cell that has been edited.
        private Cell cCell;
        private bool userInputRecieved = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Spreadsheet"/> class.
        /// </summary>
        /// <param name="rowNum">
        /// Number of rows.
        /// </param>
        /// <param name="columnNum">
        /// Number of columns.
        /// </param>
        public Spreadsheet(int rowNum, int columnNum)
        {
            // Declare cell array and fill it with instances.
            this.InitializeCellArray(rowNum, columnNum);

            this.rowCount = rowNum;
            this.columnCount = columnNum;
            this.SubscribeToCells(rowNum, columnNum);
            this.invoker = new SpreadsheetInvoker();
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets rowCount.
        /// </summary>
        public int RowCount
        {
            get { return this.rowCount; }
        }

        /// <summary>
        /// Gets columnCount.
        /// </summary>
        public int ColumnCount
        {
            get { return this.columnCount; }
        }

        /// <summary>
        /// Sets the CCell value.
        /// </summary>
        public Cell CCell
        {
            set { this.cCell = value; }
        }

        /// <summary>
        /// Initializes the cell array.
        /// </summary>
        /// <param name="rowNum">
        /// The number of rows.
        /// </param>
        /// <param name="columnNum">
        /// The number of columns.
        /// </param>
        public void InitializeCellArray(int rowNum, int columnNum)
        {
            this.cellArray = new Cell[rowNum, columnNum];
            for (int row = 0; row < rowNum; row++)
            {
                for (int column = 0; column < columnNum; column++)
                {
                    // SpreadsheetCell only implements Cell so an instance can be made of it.
                    this.cellArray[row, column] = new CptS321.SpreadsheetCell(string.Empty, row, column);
                }
            }
        }

        /// <summary>
        /// Clears the cell array.
        /// </summary>
        public void CleanCellArray()
        {
            for (int row = 0; row < this.rowCount; row++)
            {
                for (int column = 0; column < this.columnCount; column++)
                {
                    this.ChangeCell(string.Empty, row, column);
                    this.ChangeBGColor(0xFFFFFFFF, row, column);
                }
            }
        }

        /// <summary>
        /// Called on PropertyChanged signal in a cell.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The event called.
        /// </param>
        public void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                Cell s = sender as Cell;

                // If its a calculated value, else if just set text.
                if (s.Text.Length > 0 && s.Text[0] == '=')
                {
                    // Casts the cell SpreadsheetCell to edit the Value.
                    SpreadsheetCell ssc = (SpreadsheetCell)this.cellArray[s.RowIndex, s.ColumnIndex];
                    ssc.SetValue(this.CalculateCell(s.Text.Substring(1)));
                    this.PropertyChanged(ssc, new PropertyChangedEventArgs("Cell"));
                }
                else
                {
                    this.cellArray[s.RowIndex, s.ColumnIndex].Text = s.Text;
                    this.PropertyChanged(s, new PropertyChangedEventArgs("Cell"));
                }

                this.FindReferencingCells(s);
            }
            else if (e.PropertyName == "BGColor")
            {
                Cell s = sender as Cell;

                this.cellArray[s.RowIndex, s.ColumnIndex].BGColorValue = s.BGColorValue;
                this.PropertyChanged(s, new PropertyChangedEventArgs("Cell"));
            }
        }

        /// <summary>
        /// Find and change all cells that reference the changing cell.
        /// </summary>
        /// <param name="changingCell">
        /// The changing cell.
        /// </param>
        public void FindReferencingCells(Cell changingCell)
        {
            StringBuilder cellName = new StringBuilder();
            cellName.Append((char)(changingCell.ColumnIndex + 65));
            cellName.Append(changingCell.RowIndex + 1);
            if (this.cCell != null && (this.userInputRecieved == false && this.cCell.Text.Contains(cellName.ToString())))
            {
                this.userInputRecieved = false;
                SpreadsheetCell ssc = (SpreadsheetCell)this.cCell;
                if (changingCell != this.cCell)
                {
                    ssc.SetValue("!(circular reference)");
                }
                else
                {
                    ssc.SetValue("!(self reference)");
                }

                this.PropertyChanged(ssc, new PropertyChangedEventArgs("Cell"));
            }
            else
            {
                this.userInputRecieved = false;

                // Check all cells
                foreach (Cell cell in this.cellArray)
                {
                    // If a cell references the changing cell
                    if (cell.Text.Contains(cellName.ToString()))
                    {
                        StringBuilder referencingCellName = new StringBuilder();
                        referencingCellName.Append((char)(cell.ColumnIndex + 65));
                        referencingCellName.Append(cell.RowIndex + 1);
                        SpreadsheetCell ssc = (SpreadsheetCell)cell;
                        ssc.SetValue(this.CalculateCell(ssc.Text.Substring(1)));

                        this.PropertyChanged(ssc, new PropertyChangedEventArgs("Cell"));
                        this.FindReferencingCells(cell);
                    }
                }
            }
        }

        /// <summary>
        /// Change the text of a cell.
        /// </summary>
        /// <param name="newText">
        /// The new text.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <param name="columnIndex">
        /// The column index.
        /// </param>
        public void ChangeCell(string newText, int rowIndex, int columnIndex)
        {
            this.cellArray[rowIndex, columnIndex].Text = newText;
        }

        /// <summary>
        /// Changes the background of a cell.
        /// </summary>
        /// <param name="newColor">
        /// The new color value.
        /// </param>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <param name="columnIndex">
        /// The column index.
        /// </param>
        public void ChangeBGColor(uint newColor, int rowIndex, int columnIndex)
        {
            this.cellArray[rowIndex, columnIndex].BGColorValue = newColor;
        }

        /// <summary>
        /// Fills the dictionary with the values from the cells.
        /// </summary>
        /// <param name="variableDictionary">
        /// The expression tree's variable dictionary.
        /// </param>
        /// <returns>
        /// The variable dicitonary filled out.
        /// </returns>
        public Dictionary<string, NodeConstantNumerical> FillCells(Dictionary<string, NodeConstantNumerical> variableDictionary)
        {
            Dictionary<string, NodeConstantNumerical> newVariables = new Dictionary<string, NodeConstantNumerical>();
            foreach (KeyValuePair<string, NodeConstantNumerical> pair in variableDictionary)
            {
                // If a variable name, else just a number.
                double cellValue;
                if (!double.TryParse(pair.Key, out cellValue))
                {
                    string foundValue = this.FindCellValue(pair.Key);
                    if (foundValue != null)
                    {
                        if (double.TryParse(foundValue, out cellValue))
                        {
                            newVariables.Add(pair.Key, new NodeConstantNumerical(cellValue));
                        }
                        else
                        {
                            newVariables.Add(pair.Key, new NodeConstantNumerical(0.0));
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    newVariables.Add(pair.Key, new NodeConstantNumerical(cellValue));
                }
            }

            return newVariables;
        }

        /// <summary>
        /// Calculates the value of the cell.
        /// </summary>
        /// <param name="newText">
        /// The equation to be evaluated.
        /// </param>
        /// <returns>
        /// The new text of the cell.
        /// </returns>
        public string CalculateCell(string newText)
        {
            ExpressionTree expressionTree = new ExpressionTree(string.Empty);
            if (expressionTree.IsExpression(newText) || double.TryParse(newText, out _))
            {
                expressionTree = new ExpressionTree(newText);
                Dictionary<string, NodeConstantNumerical> newDictionry = this.FillCells(expressionTree.VariableDictionary);
                if (newDictionry != null)
                {
                    expressionTree.FillVariables(newDictionry);
                    return expressionTree.Evaluate().ToString();
                }
                else
                {
                    return "!(bad reference)";
                }
            }
            else
            {
                string foundValue = this.FindCellValue(newText);
                if (foundValue != null)
                {
                    return foundValue;
                }
                else
                {
                    return "!(bad reference)";
                }
            }
        }

        /// <summary>
        /// Parses through the cell name and returns the representative index.
        /// </summary>
        /// <param name="cellName">
        /// The name of the cell.
        /// </param>
        /// <returns>
        /// A tuple of the cell's coordinates.
        /// </returns>
        public Tuple<int, int> ParseCellName(string cellName)
        {
            int rowIndex;
            int columnIndex = (int)cellName[0] - 65; // Get the letter index.
            int.TryParse(cellName.Substring(1), out rowIndex); // Gets the numbers after the letter.
            return new Tuple<int, int>(rowIndex, columnIndex);
        }

        /// <summary>
        /// Find and return the value of the specified cell.
        /// </summary>
        /// <param name="cellName">
        /// The specified cell name.
        /// </param>
        /// <returns>
        /// The value of the cell.
        /// </returns>
        public string FindCellValue(string cellName)
        {
            try
            {
                Tuple<int, int> coordinates = this.ParseCellName(cellName);
                if (coordinates.Item1 < this.rowCount && coordinates.Item2 < this.columnCount)
                {
                    Cell cell = this.GetCell(coordinates.Item1 - 1, coordinates.Item2);
                    if (!string.IsNullOrEmpty(cell.Value))
                    {
                        return cell.Value;
                    }
                    else
                    {
                        return "0";
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        /// <summary>
        /// Returns the cell at the index.
        /// </summary>
        /// <param name="rowIndex">
        /// The row index.
        /// </param>
        /// <param name="columnIndex">
        /// The column index.
        /// </param>
        /// <returns>
        /// The cell at the indexs.
        /// </returns>
        public Cell GetCell(int rowIndex, int columnIndex)
        {
            try
            {
                return this.cellArray[rowIndex, columnIndex];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Saves an XML file to the specified path.
        /// </summary>
        /// <param name="path">
        /// Where to save the file.
        /// </param>
        public void SaveXMLFile(Stream path)
        {
            XmlWriter writer = XmlWriter.Create(path);
            writer.WriteStartElement("spreadsheet");

            // Get the cells
            foreach (Cell cell in this.cellArray)
            {
                if (!string.IsNullOrEmpty(cell.Text) || cell.BGColorValue != 0xFFFFFFFF)
                {
                    writer.WriteStartElement("cell");
                    string cellName = Convert.ToChar(cell.ColumnIndex + 65).ToString() + (cell.RowIndex + 1).ToString();
                    writer.WriteAttributeString("name", cellName);
                    writer.WriteStartElement("bgcolor");
                    writer.WriteString(cell.BGColorValue.ToString());
                    writer.WriteEndElement(); // End BGColor
                    writer.WriteStartElement("text");
                    writer.WriteString(cell.Text);
                    writer.WriteEndElement(); // End text
                    writer.WriteEndElement(); // End cell
                }
            }

            writer.WriteEndElement(); // End spreadsheet
            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// Loads an XML file at the specified path.
        /// </summary>
        /// <param name="path">
        /// Where to load the file from.
        /// </param>
        public void LoadXMLFile(Stream path)
        {
            this.CleanCellArray();
            this.invoker.ResetStacks();

            try
            {
                XmlReader reader = XmlReader.Create(path);
                reader.ReadToFollowing("spreadsheet");
                reader.ReadToDescendant("cell");

                // Read all cells
                do
                {
                    // Get all attributes of the cell
                    string name = reader.GetAttribute("name");
                    Tuple<int, int> coordinates = this.ParseCellName(name);
                    string color = "0xFFFFFFFF";
                    string text = string.Empty;
                    XmlReader cellContent = reader.ReadSubtree();

                    // Read cell info
                    while (!cellContent.EOF)
                    {
                        if (cellContent.Name.ToLower() == "bgcolor")
                        {
                            color = cellContent.ReadElementContentAsString();
                        }
                        else if (cellContent.Name.ToLower() == "text")
                        {
                            text = cellContent.ReadElementContentAsString();
                        }
                        else
                        {
                            cellContent.Read();
                        }
                    }

                    // Update cells
                    this.ChangeCell(text, coordinates.Item1 - 1, coordinates.Item2);
                    uint uintColor;
                    if (uint.TryParse(color, out uintColor))
                    {
                        this.ChangeBGColor(uintColor, coordinates.Item1 - 1, coordinates.Item2);
                    }
                }
                while (reader.ReadToNextSibling("cell"));
            }
            catch
            {
            }
        }

        /// <summary>
        /// Sets userInput to true. Indicates if the user has just inputed a cell value.
        /// </summary>
        public void UserInput()
        {
            this.userInputRecieved = true;
        }

        /// <summary>
        /// Adds an command to the undo stack.
        /// </summary>
        /// <param name="command">
        /// A command to add.
        /// </param>
        /// <param name="rowIndex">
        /// The row of the changing cell.
        /// </param>
        /// <param name="columnIndex">
        /// The column of the changing cell.
        /// </param>
        public void AddUndo(ICommand command, int rowIndex, int columnIndex)
        {
            this.cCell = this.GetCell(rowIndex, columnIndex);
            this.UserInput();
            this.invoker.AddUndo(command);
        }

        /// <summary>
        /// Calls the invoker's undo.
        /// </summary>
        public void Undo()
        {
            this.UserInput();
            this.invoker.Undo();
        }

        /// <summary>
        /// Gets what the next undo command is.
        /// </summary>
        /// <returns>
        /// The lable of the next undo command.
        /// </returns>
        public string NextUndoCommand()
        {
            return this.invoker.GetTopUndoStack();
        }

        /// <summary>
        /// Calls the invoker's redo.
        /// </summary>
        public void Redo()
        {
            this.UserInput();
            this.invoker.Redo();
        }

        /// <summary>
        /// Gets what the next redo command is.
        /// </summary>
        /// <returns>
        /// The lable of the next redo command.
        /// </returns>
        public string NextRedoCommand()
        {
            return this.invoker.GetTopRedoStack();
        }

        /// <summary>
        /// Automatically subscribes to all the cell PropertyChanged signals.
        /// </summary>
        /// <param name="rowNum">
        /// Number of rows.
        /// </param>
        /// <param name="columnNum">
        /// Number of columns.
        /// </param>
        private void SubscribeToCells(int rowNum, int columnNum)
        {
            for (int r = 0; r < rowNum; r++)
            {
                for (int c = 0; c < columnNum; c++)
                {
                    this.cellArray[r, c].PropertyChanged += this.CellPropertyChanged;
                }
            }
        }
    }
}
