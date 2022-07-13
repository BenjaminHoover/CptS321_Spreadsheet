// <copyright file="FormMain.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace Spreadsheet_Benjamin_Hoover
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using CptS321;

    /// <summary>
    /// Main Form object.
    /// </summary>
    public partial class FormMain : Form
    {
        private SpreadsheetEngine.Spreadsheet spreadsheet;
        private ColorDialog colorDialog;

        /// <summary>
        /// Initializes a new instance of the <see cref="FormMain"/> class.
        /// Initialize FormMain.
        /// </summary>
        public FormMain()
        {
            this.InitializeComponent();
            this.MakeGrid();
            this.spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            this.spreadsheet.PropertyChanged += this.CellPropertyChanged;
        }

        /// <summary>
        /// Initializes the rows and columns of dataGridMain.
        /// </summary>
        private void MakeGrid()
        {
            // Make the columns.
            for (char colLabel = 'A'; colLabel <= 'Z'; colLabel++)
            {
                DataGridViewColumn dataGridViewColumn = new DataGridViewTextBoxColumn();
                dataGridViewColumn.Name = colLabel.ToString();
                this.dataGridMain.Columns.Add(dataGridViewColumn);
            }

            // Make the rows.
            for (int rowLabel = 1; rowLabel <= 50; rowLabel++)
            {
                string[] row = new string[26];
                for (int rowCol = 0; rowCol < 26; rowCol++)
                {
                    row[rowCol] = string.Empty;
                }

                this.dataGridMain.Rows.Add(row);
                this.dataGridMain.Rows[rowLabel - 1].HeaderCell.Value = rowLabel.ToString();
            }
        }

        /// <summary>
        /// Called when a property changed in SpreadSheetCell.
        /// </summary>
        /// <param name="sender">
        /// The sender object.
        /// </param>
        /// <param name="e">
        /// The property that changed.
        /// </param>
        private void CellPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Cell")
            {
                Cell cell = sender as Cell;
                this.UpdateCell(cell.Value, cell.RowIndex, cell.ColumnIndex, cell.BGColorValue);
            }
        }

        /// <summary>
        /// Update the cell in the dataGridMain.
        /// </summary>
        /// <param name="newValue">
        /// The new value of the cell.
        /// </param>
        /// <param name="rowIndex">
        /// The row of the cell.
        /// </param>
        /// <param name="columnIndex">
        /// The column of the cell.
        /// </param>
        /// <param name="newBGColor">
        /// The background of the cell.
        /// </param>
        private void UpdateCell(string newValue, int rowIndex, int columnIndex, uint newBGColor)
        {
            this.dataGridMain.Rows[rowIndex].Cells[columnIndex].Value = newValue;
            DataGridViewCellStyle newStyle = new DataGridViewCellStyle();
            newStyle.BackColor = Color.FromArgb((int)newBGColor);
            this.dataGridMain.Rows[rowIndex].Cells[columnIndex].Style = newStyle;
        }

        /// <summary>
        /// Updates the undo button text.
        /// </summary>
        private void UpdateUndoText()
        {
            string undoText = this.spreadsheet.NextUndoCommand();

            if (undoText != string.Empty)
            {
                this.undoMenuItem.Text = "Undo " + undoText;
                this.undoMenuItem.Enabled = true;
            }
            else
            {
                this.undoMenuItem.Text = "Undo";
                this.undoMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// Updates the redo button text.
        /// </summary>
        private void UpdateRedoText()
        {
            string redoText = this.spreadsheet.NextRedoCommand();

            if (!string.IsNullOrEmpty(redoText))
            {
                this.redoMenuItem.Text = "Redo " + redoText;
                this.redoMenuItem.Enabled = true;
            }
            else
            {
                this.redoMenuItem.Text = "Redo";
                this.redoMenuItem.Enabled = false;
            }
        }

        /// <summary>
        /// When the user begins editing a cell.
        /// </summary>
        /// <param name="sender">
        /// The sender cell.
        /// </param>
        /// <param name="e">
        /// The event called.
        /// </param>
        private void DataGridMain_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            this.UpdateCell(this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text, e.RowIndex, e.ColumnIndex, this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).BGColorValue);
        }

        /// <summary>
        /// When the user stops editing a cell.
        /// </summary>
        /// <param name="sender">
        /// The sender cell.
        /// </param>
        /// <param name="e">
        /// The event called.
        /// </param>
        private void DataGridMain_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dataGridMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
            {
                this.dataGridMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = string.Empty;
            }

            if (this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text !=
                this.dataGridMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString())
            {
                SpreadsheetEngine.TextChangeCommand command = new SpreadsheetEngine.TextChangeCommand();
                command.SetAttributes(
                    ref this.spreadsheet,
                    this.dataGridMain.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(),
                    this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text,
                    e.RowIndex,
                    e.ColumnIndex);
                this.spreadsheet.AddUndo(command, e.RowIndex, e.ColumnIndex);
            }

            if (this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text.Length > 0 && this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Text[0] == '=')
            {
                this.UpdateCell(this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).Value, e.RowIndex, e.ColumnIndex, this.spreadsheet.GetCell(e.RowIndex, e.ColumnIndex).BGColorValue);
            }

            this.UpdateUndoText();
            this.UpdateRedoText();
        }

        /// <summary>
        /// Handels the demo button features.
        /// </summary>
        /// <param name="sender">
        /// The button object.
        /// </param>
        /// <param name="e">
        /// The event click.
        /// </param>
        private void ButtonDemo_Click(object sender, EventArgs e)
        {
            // Randomly select 50 cells and enter "Hello World!" into it. May overlap.
            Random random = new Random();
            for (int i = 0; i < 50; i++)
            {
                int randRowIndex = random.Next(0, 49);
                int randColumnIndex = random.Next(2, 25);

                this.spreadsheet.ChangeCell("Hello World!", randRowIndex, randColumnIndex);
            }

            // Set all the cells in column B to specified text.
            for (int bRowIndex = 0; bRowIndex < 50; bRowIndex++)
            {
                string text = "This is cell B" + (bRowIndex + 1);
                this.spreadsheet.ChangeCell(text, bRowIndex, 1);
            }

            // Set all the cells in column A to the value of cell B.
            for (int aRowIndex = 0; aRowIndex < 50; aRowIndex++)
            {
                string text = "=B" + (aRowIndex + 1);
                this.spreadsheet.ChangeCell(text, aRowIndex, 0);
            }
        }

        /// <summary>
        /// When a user clicks on the content of a cell.
        /// </summary>
        /// <param name="sender">
        /// The sender cell.
        /// </param>
        /// <param name="e">
        /// The event of clicking on a cell.
        /// </param>
        private void DataGridMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCellCancelEventArgs dataGridViewCellCancelEventArgs = new DataGridViewCellCancelEventArgs(e.RowIndex, e.ColumnIndex);
        }

        /// <summary>
        /// Clicking on the "Change background color..." option.
        /// </summary>
        /// <param name="sender">
        /// The menu item.
        /// </param>
        /// <param name="e">
        /// The click event.
        /// </param>
        private void ChangeColorMenuItem_Click(object sender, EventArgs e)
        {
            this.colorDialog = new ColorDialog();
            if (this.colorDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (DataGridViewCell cell in this.dataGridMain.SelectedCells)
                {
                    SpreadsheetEngine.BGColorChangeCommand command = new SpreadsheetEngine.BGColorChangeCommand();
                    command.SetAttributes(
                        ref this.spreadsheet,
                        ((uint)this.colorDialog.Color.ToArgb()).ToString(),
                        this.spreadsheet.GetCell(cell.RowIndex, cell.ColumnIndex).BGColorValue.ToString(),
                        cell.RowIndex,
                        cell.ColumnIndex);
                    this.spreadsheet.AddUndo(command, cell.RowIndex, cell.ColumnIndex);
                    this.UpdateUndoText();
                    this.UpdateRedoText();
                }
            }
        }

        /// <summary>
        /// Clicking on the "Undo" menu option under Edit.
        /// </summary>
        /// <param name="sender">
        /// The menu item.
        /// </param>
        /// <param name="e">
        /// The clicking event.
        /// </param>
        private void UndoMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Undo();
            this.UpdateUndoText();
            this.UpdateRedoText();
        }

        /// <summary>
        /// The "Redo" menu item click event under Edit.
        /// </summary>
        /// <param name="sender">
        /// The menu item.
        /// </param>
        /// <param name="e">
        /// The clicking event.
        /// </param>
        private void RedoMenuItem_Click(object sender, EventArgs e)
        {
            this.spreadsheet.Redo();
            this.UpdateUndoText();
            this.UpdateRedoText();
        }

        /// <summary>
        /// Handles the saving mechanic.
        /// </summary>
        /// <param name="sender">
        /// The menu item.
        /// </param>
        /// <param name="e">
        /// The clicking event.
        /// </param>
        private void SaveMenuItem_Click(object sender, EventArgs e)
        {
            // Use SaveFileDialog object for user input.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            Stream newStream;

            // Only text files.
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.RestoreDirectory = true;

            // Once user clicks OK.
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                newStream = saveFileDialog.OpenFile();

                // If stream is valid.
                if (newStream != null)
                {
                    this.spreadsheet.SaveXMLFile(newStream);
                    newStream.Close();
                }
            }
        }

        /// <summary>
        /// Handles the loading mechanic.
        /// </summary>
        /// <param name="sender">
        /// The menu button.
        /// </param>
        /// <param name="e">
        /// The clicking event.
        /// </param>
        private void LoadMenuItem_Click(object sender, EventArgs e)
        {
            // Use OpenFileDialog object for user input.
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Stream newStream;

            // Only text files.
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            openFileDialog.RestoreDirectory = true;

            // Once user clicks OK.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                newStream = openFileDialog.OpenFile();

                // If stream is valid.
                if (newStream != null)
                {
                    this.spreadsheet.LoadXMLFile(newStream);
                    newStream.Close();
                    this.UpdateUndoText();
                    this.UpdateRedoText();
                }
            }
        }
    }
}
