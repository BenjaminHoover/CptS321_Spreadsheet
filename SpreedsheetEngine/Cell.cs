// <copyright file="Cell.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace CptS321
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class of a singular, spreadsheet cell.
    /// </summary>
    public abstract class Cell : INotifyPropertyChanged
    {
        /// <summary>
        /// The text of the cell.
        /// </summary>
        protected string text;

        /// <summary>
        /// The calculated value of the cell.
        /// </summary>
        protected string value;

        /// <summary>
        /// The background color value of the cell.
        /// </summary>
        protected uint BGColor;

        private int rowIndex;
        private int columnIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class.
        /// </summary>
        /// <param name="newRowIndex">
        /// The new row index.
        /// </param>
        /// <param name="newColumnIndex">
        /// The new column index.
        /// </param>
        /// <param name="newText">
        /// The new text.
        /// </param>
        public Cell(int newRowIndex, int newColumnIndex, string newText)
        {
            this.rowIndex = newRowIndex;
            this.columnIndex = newColumnIndex;
            this.text = newText;
            this.value = newText;
            this.BGColor = 0xFFFFFFFF;
        }

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        /// <summary>
        /// Gets the row index.
        /// </summary>
        public int RowIndex
        {
            get { return this.rowIndex; }
        }

        /// <summary>
        /// Gets the column index.
        /// </summary>
        public int ColumnIndex
        {
            get { return this.columnIndex; }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text
        {
            get
            {
                return this.text;
            }

            set
            {
                // only call a signal if property changes.
                if (string.Compare(this.text, value) != 0)
                {
                    this.text = value;
                    this.value = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Text"));
                }
            }
        }

        /// <summary>
        /// Gets the value of the cell.
        /// </summary>
        public string Value
        {
            get { return this.value; }
        }

        /// <summary>
        /// Gets or sets the BGColor value.
        /// </summary>
        public uint BGColorValue
        {
            get
            {
                return this.BGColor;
            }

            set
            {
                // only call a signal if property changes.
                if (this.BGColor != value)
                {
                    this.BGColor = value;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("BGColor"));
                }
            }
        }
    }
}
