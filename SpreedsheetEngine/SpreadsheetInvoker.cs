// <copyright file="SpreadsheetInvoker.cs" company="Benjamin Hoover 011622025">
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
    /// The invoker.
    /// </summary>
    public class SpreadsheetInvoker
    {
        private Stack<ICommand> undoStack;
        private Stack<ICommand> redoStack;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpreadsheetInvoker"/> class.
        /// </summary>
        public SpreadsheetInvoker()
        {
            this.undoStack = new Stack<ICommand>();
            this.redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Gets the top of the undo stack.
        /// </summary>
        /// <returns>
        /// The text of the next command undo.
        /// </returns>
        public string GetTopUndoStack()
        {
            if (this.undoStack.Count > 0)
            {
                return this.undoStack.Peek().GetCommandName();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the top of the redo stack.
        /// </summary>
        /// <returns>
        /// The text of the next command redo.
        /// </returns>
        public string GetTopRedoStack()
        {
            if (this.redoStack.Count > 0)
            {
                return this.redoStack.Peek().GetCommandName();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Resets the undo and redo stacks.
        /// </summary>
        public void ResetStacks()
        {
            this.undoStack = new Stack<ICommand>();
            this.redoStack = new Stack<ICommand>();
        }

        /// <summary>
        /// Adds and undo command.
        /// </summary>
        /// <param name="command">
        /// The command to add.
        /// </param>
        public void AddUndo(ICommand command)
        {
            command.Execute();
            this.undoStack.Push(command);
        }

        /// <summary>
        /// Pops the top command and UnExecutes it.
        /// </summary>
        public void Undo()
        {
            ICommand command = this.undoStack.Pop();
            command.UnExecute();
            this.redoStack.Push(command);
        }

        /// <summary>
        /// Pops the top command and Executes it.
        /// </summary>
        public void Redo()
        {
            ICommand command = this.redoStack.Pop();
            command.Execute();
            this.undoStack.Push(command);
        }
    }
}
