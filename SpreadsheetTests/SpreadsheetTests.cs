// <copyright file="SpreadsheetTests.cs" company="Benjamin Hoover 011622025">
// Copyright (c) Benjamin Hoover 011622025
// </copyright>

namespace SpreadsheetEngineTests
{
    // NUnit 3 tests
    // See documentation : https://github.com/nunit/docs/wiki/NUnit-Documentation
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Xml;
    using NUnit.Framework;

    /// <summary>
    /// The tests for the SpreadSheet class.
    /// </summary>
    [TestFixture]
    public class SpreadsheetTests
    {
        /// <summary>
        /// Setup function as required by NUnit.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
        }

        /// <summary>
        /// Normal test of determing if an xml file was created at location.
        /// </summary>
        [Test]
        public void NormalSaveFileCreated()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            string path = Environment.CurrentDirectory + "\\testOutputs\\SpreadSheetExists.xml";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream outfile = File.Create(path);
            StreamWriter streamWriter = new StreamWriter(outfile);
            spreadsheet.SaveXMLFile(streamWriter.BaseStream);
            streamWriter.Close();

            Assert.That(File.Exists(path), Is.EqualTo(true));
        }

        /// <summary>
        /// Normal test of determing if the spreadsheet was correctly saved with random data.
        /// Normal format.
        /// </summary>
        [Test]
        public void NormalSaveFileContents()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("Hello World", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901760, 5 - 1, 6);

            string path = Environment.CurrentDirectory + "\\testOutputs\\SpreadSheetFileContents.xml";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream outfile = File.Create(path);
            StreamWriter streamWriter = new StreamWriter(outfile);
            spreadsheet.SaveXMLFile(streamWriter.BaseStream);
            streamWriter.Close();

            // Open the file up for reading, assuming it exists.
            StreamReader stream = new StreamReader(path);
            XmlReader xmlReader = XmlReader.Create(stream);
            StringBuilder testOutput = new StringBuilder();
            xmlReader.ReadToFollowing("spreadsheet");
            while (xmlReader.ReadToFollowing("cell"))
            {
                testOutput.Append(xmlReader.GetAttribute("name"));
                xmlReader.ReadToFollowing("bgcolor");
                testOutput.Append(xmlReader.ReadElementContentAsString());
                if (xmlReader.Name == "text")
                {
                    testOutput.Append(xmlReader.ReadElementContentAsString());
                }
            }

            xmlReader.Close();
            stream.Close();

            // Expected output of read content as string.
            string expectedOutput = "G54294901760Hello World";

            Assert.That(testOutput.ToString(), Is.EqualTo(expectedOutput));
        }

        /// <summary>
        /// Normal test of determing if an xml file with expected contents was correctly loaded.
        /// Single cell.
        /// </summary>
        [Test]
        public void NormalLoadFileSingleCell()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(5 - 1, 6);
            spreadsheet.ChangeCell("Hello World", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901760, 5 - 1, 6);

            string path = Environment.CurrentDirectory + "\\testOutputs\\SpreadSheetLoadSingle.xml";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream outfile = File.Create(path);
            StreamWriter streamWriter = new StreamWriter(outfile);
            spreadsheet.SaveXMLFile(streamWriter.BaseStream);
            streamWriter.Close();

            spreadsheet.UserInput();
            spreadsheet.ChangeCell("Changed", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901765, 5 - 1, 6);

            StreamReader streamReader = new StreamReader(path);
            spreadsheet.LoadXMLFile(streamReader.BaseStream);
            streamReader.Close();

            Assert.That(spreadsheet.GetCell(5 - 1, 6).Value, Is.EqualTo("Hello World"));
            Assert.That(spreadsheet.GetCell(5 - 1, 6).BGColorValue.ToString(), Is.EqualTo("4294901760"));
        }

        /// <summary>
        /// Normal test of determing if an xml file with expected contents was correctly loaded.
        /// Multiple cells.
        /// </summary>
        [Test]
        public void NormalLoadFileMultipleCells()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(5 - 1, 6);
            spreadsheet.ChangeCell("Hello World", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901760, 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(6 - 1, 6);
            spreadsheet.ChangeCell("Hello Again", 6 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901760, 6 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(7 - 1, 6);
            spreadsheet.ChangeCell("Hello Once More", 7 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901760, 7 - 1, 6);

            string path = Environment.CurrentDirectory + "\\testOutputs\\SpreadSheetLoadMultiple.xml";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream outfile = File.Create(path);
            StreamWriter streamWriter = new StreamWriter(outfile);
            spreadsheet.SaveXMLFile(streamWriter.BaseStream);
            streamWriter.Close();

            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(5 - 1, 6);
            spreadsheet.ChangeCell("Changed", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901765, 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(6 - 1, 6);
            spreadsheet.ChangeCell("Changed Again", 6 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901765, 6 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(7 - 1, 6);
            spreadsheet.ChangeCell("Changed Once More", 7 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901765, 7 - 1, 6);

            StreamReader streamReader = new StreamReader(path);
            spreadsheet.LoadXMLFile(streamReader.BaseStream);
            streamReader.Close();

            Assert.That(spreadsheet.GetCell(5 - 1, 6).Value, Is.EqualTo("Hello World"));
            Assert.That(spreadsheet.GetCell(5 - 1, 6).BGColorValue.ToString(), Is.EqualTo("4294901760"));
            Assert.That(spreadsheet.GetCell(6 - 1, 6).Value, Is.EqualTo("Hello Again"));
            Assert.That(spreadsheet.GetCell(6 - 1, 6).BGColorValue.ToString(), Is.EqualTo("4294901760"));
            Assert.That(spreadsheet.GetCell(7 - 1, 6).Value, Is.EqualTo("Hello Once More"));
            Assert.That(spreadsheet.GetCell(7 - 1, 6).BGColorValue.ToString(), Is.EqualTo("4294901760"));
        }

        /// <summary>
        /// Fringe test of determing if an xml file with unexpected contents was correctly loaded.
        /// </summary>
        [Test]
        public void FringeLoadFile()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(5 - 1, 6);
            spreadsheet.ChangeCell("Hello World", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901760, 5 - 1, 6);

            string path = Environment.CurrentDirectory + "\\testOutputs\\SpreadSheetLoadFringe.xml";
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            FileStream outfile = File.Create(path);
            StreamWriter streamWriter = new StreamWriter(outfile);
            XmlWriter xmlWriter = XmlWriter.Create(streamWriter);
            xmlWriter.WriteStartElement("spreadsheet");
            xmlWriter.WriteStartElement("cell");
            xmlWriter.WriteAttributeString("name", "G5");
            xmlWriter.WriteStartElement("NonsenseTag");
            xmlWriter.WriteString("Shouldn't read this in");
            xmlWriter.WriteEndElement(); // End NonsenseTag
            xmlWriter.WriteStartElement("Text");
            xmlWriter.WriteString("Hello World");
            xmlWriter.WriteEndElement(); // End Text
            xmlWriter.WriteStartElement("anotherjunktag");
            xmlWriter.WriteString("This isn't a proper tag");
            xmlWriter.WriteEndElement(); // End anotherjunktag
            xmlWriter.WriteStartElement("BGColor");
            xmlWriter.WriteString("4294901760");
            xmlWriter.WriteEndElement(); // End BGColor
            xmlWriter.WriteStartElement("Lastone");
            xmlWriter.WriteString("not this one either");
            xmlWriter.WriteEndElement(); // End lastone
            xmlWriter.WriteEndElement(); // End cell
            xmlWriter.WriteEndElement(); // End spreadsheet
            xmlWriter.Flush();
            xmlWriter.Close();
            streamWriter.Close();

            spreadsheet.UserInput();
            spreadsheet.ChangeCell("Changed", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeBGColor(4294901765, 5 - 1, 6);

            StreamReader streamReader = new StreamReader(path);
            spreadsheet.LoadXMLFile(streamReader.BaseStream);
            streamReader.Close();

            Assert.That(spreadsheet.GetCell(5 - 1, 6).Value, Is.EqualTo("Hello World"));
            Assert.That(spreadsheet.GetCell(5 - 1, 6).BGColorValue.ToString(), Is.EqualTo("4294901760"));
        }

        /// <summary>
        /// Test changing the text of a cell in the middle.
        /// </summary>
        [Test]
        public void ChangeCellTextNormal()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            string inputText = "TEST";
            int inputRow = 10;
            int inputColumn = 10;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(10, 10).Value, Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Test changing the text of the firts cell, a:1.
        /// </summary>
        [Test]
        public void ChangeCellTextFringeFirstCell()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            string inputText = "TEST";
            int inputRow = 0;
            int inputColumn = 0;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(0, 0).Value, Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Test changing the text of the last cell, z:50.
        /// </summary>
        [Test]
        public void ChangeCellTextFringeLastCell()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            string inputText = "TEST";
            int inputRow = 49;
            int inputColumn = 25;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(49, 25).Value, Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Test changing the text of a cell in the middle that needs to be calculated but doesn't use a expression.
        /// </summary>
        [Test]
        public void CalculateCellNormalNoExpression()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("TEST", 5 - 1, 6);

            string inputText = "=G5";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Test changing the text of a cell in the middle that needs to be calculated from an empty cell.
        /// </summary>
        [Test]
        public void CalculateCellNormalEmptyCell()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            string inputText = "=G5";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("0"));
        }

        /// <summary>
        /// Normal test of changing the value of a cell when a cell it referenced changes.
        /// </summary>
        [Test]
        public void NormalFindReferencingCellsSingleCell()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("TEST", 5 - 1, 6);

            string inputText = "=G5";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(inputRow, inputColumn);
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("TEST"));
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("CHANGED", 5 - 1, 6);
            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("CHANGED"));
        }

        /// <summary>
        /// Normal test of changing the value of a cell when two cells it references in an expression change.
        /// </summary>
        [Test]
        public void NormalFindReferencingCellsCalculated()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("7", 5 - 1, 6);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("5", 6 - 1, 6);

            string inputText = "=G5 + G6";
            int inputRow = 17;
            int inputColumn = 18;
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(inputRow, inputColumn);
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(17, 18).Value, Is.EqualTo("12"));
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("8", 5 - 1, 6);
            Assert.That(spreadsheet.GetCell(17, 18).Value, Is.EqualTo("13"));
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("6", 6 - 1, 6);
            Assert.That(spreadsheet.GetCell(17, 18).Value, Is.EqualTo("14"));
        }

        /// <summary>
        /// Test changing the text of a cell in the middle that needs to be calculated and uses a expression with one ref.
        /// </summary>
        [Test]
        public void CalculateCellNormalExpressionSingleRef()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("7", 5 - 1, 6);

            string inputText = "=G5 + 5";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("12"));
        }

        /// <summary>
        /// Test changing the text of a cell in the middle that needs to be calculated and uses a expression with two refs.
        /// </summary>
        [Test]
        public void CalculateCellNormalExpressionMultipleRef()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.CCell = spreadsheet.GetCell(5 - 1, 6);
            spreadsheet.ChangeCell("7", 5 - 1, 6);
            spreadsheet.CCell = spreadsheet.GetCell(6 - 1, 6);
            spreadsheet.ChangeCell("5", 6 - 1, 6);

            string inputText = "=G5 + G6";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.CCell = spreadsheet.GetCell(inputRow, inputColumn);
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("12"));
        }

        /// <summary>
        /// Test changing the text of a cell in the middle that needs to be calculated and uses a expression with a null ref.
        /// </summary>
        [Test]
        public void CalculateCellNormalExpressionBadRef()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.CCell = spreadsheet.GetCell(5 - 1, 6);
            spreadsheet.ChangeCell("7", 5 - 1, 6);

            string inputText = "=G5 + G6";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.CCell = spreadsheet.GetCell(inputRow, inputColumn);
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("7"));
        }

        /// <summary>
        /// Test changing the text of a cell that needs to be calculated from a cell that doesn't exist.
        /// </summary>
        [Test]
        public void CalculateCellFringeBadAddress()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("TEST", 5 - 1, 6);

            string inputText = "=66ffd";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("!(bad reference)"));
        }

        /// <summary>
        /// Test changing the text of a cell that needs to be calculated from a cell that doesn't exist, a more complicated example.
        /// </summary>
        [Test]
        public void CalculateCellFringeBadAddressComplicated()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            string inputText = "=6+Cell*27";
            int inputRow = 7;
            int inputColumn = 8;
            spreadsheet.UserInput();
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(7, 8).Value, Is.EqualTo("!(bad reference)"));
        }

        /// <summary>
        /// Test changing the text of a cell that needs to be calculated from itself.
        /// </summary>
        [Test]
        public void CalculateCellFringeSelfReference()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("TEST", 5 - 1, 6);

            string inputText = "=G5";
            int inputRow = 5 - 1;
            int inputColumn = 6;
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(inputRow, inputColumn);
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(5 - 1, 6).Value, Is.EqualTo("!(self reference)"));
        }

        /// <summary>
        /// Test changing the text of a cell that needs to be calculated from itself, more complicated example.
        /// </summary>
        [Test]
        public void CalculateCellFringeSelfReferenceComplicated()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.UserInput();
            spreadsheet.ChangeCell("TEST", 0, 0);

            string inputText = "=101+(322*B2/A1)";
            int inputRow = 0;
            int inputColumn = 0;
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(inputRow, inputColumn);
            spreadsheet.ChangeCell(inputText, inputRow, inputColumn);

            Assert.That(spreadsheet.GetCell(0, 0).Value, Is.EqualTo("!(self reference)"));
        }

        /// <summary>
        /// Test changing the text of a cell that needs to be calculated from itself indirectly.
        /// </summary>
        [Test]
        public void CalculateCellFringeSelfReferenceIndirect()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);

            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(0, 0);
            spreadsheet.ChangeCell("B1*2", 0, 0); // A1
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(1, 0);
            spreadsheet.ChangeCell("A1*5", 1, 0); // A2
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(0, 1);
            spreadsheet.ChangeCell("B2*3", 0, 1); // B1
            spreadsheet.UserInput();
            spreadsheet.CCell = spreadsheet.GetCell(1, 1);
            spreadsheet.ChangeCell("A2*4", 1, 1); // B2

            Assert.That(spreadsheet.GetCell(1, 1).Value, Is.EqualTo("!(circular reference)"));
        }

        /// <summary>
        /// Test changing the text of a cell that needs to be calculated from itself.
        /// </summary>
        [Test]
        public void CalculateCellNormalBase()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.ChangeCell("TEST", 5 - 1, 6);

            string inputText = "=G5";

            Assert.That(spreadsheet.CalculateCell(inputText.Substring(1)), Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Normal test of changing the color of a cell from default.
        /// </summary>
        [Test]
        public void NormalColorChangeFromDefault()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            spreadsheet.ChangeBGColor(0xFF000000, 5, 7);

            Assert.That(spreadsheet.GetCell(5, 7).BGColorValue, Is.EqualTo(0xFF000000));
        }

        /// <summary>
        /// Normal test of undoing a single text input.
        /// </summary>
        [Test]
        public void NormalUndoSingleText()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.TextChangeCommand commandTest = new SpreadsheetEngine.TextChangeCommand();
            commandTest.SetAttributes(ref spreadsheet, "TEST", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandTest, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChanged = new SpreadsheetEngine.TextChangeCommand();
            commandChanged.SetAttributes(ref spreadsheet, "CHANGED", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChanged, 5 - 1, 6);
            spreadsheet.Undo();

            Assert.That(spreadsheet.GetCell(5 - 1, 6).Text, Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Normal test of undoing multiple text inputs.
        /// </summary>
        [Test]
        public void NormalUndoMultipleText()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.TextChangeCommand commandTest = new SpreadsheetEngine.TextChangeCommand();
            commandTest.SetAttributes(ref spreadsheet, "TEST", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandTest, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChanged = new SpreadsheetEngine.TextChangeCommand();
            commandChanged.SetAttributes(ref spreadsheet, "CHANGED", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChanged, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChangedAgain = new SpreadsheetEngine.TextChangeCommand();
            commandChangedAgain.SetAttributes(ref spreadsheet, "CHANGED AGAIN", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChangedAgain, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChangedMore = new SpreadsheetEngine.TextChangeCommand();
            commandChangedMore.SetAttributes(ref spreadsheet, "CHANGED ONE MORE TIME", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChangedMore, 5 - 1, 6);

            spreadsheet.Undo();
            spreadsheet.Undo();
            spreadsheet.Undo();
            Assert.That(spreadsheet.GetCell(5 - 1, 6).Text, Is.EqualTo("TEST"));
        }

        /// <summary>
        /// Normal test of undoing multiple inputs that have other cells using them in calculations.
        /// </summary>
        [Test]
        public void NormalUndoMultipleCalculation()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.TextChangeCommand commandLeftOp = new SpreadsheetEngine.TextChangeCommand();
            commandLeftOp.SetAttributes(ref spreadsheet, "5", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandLeftOp, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandRightOp = new SpreadsheetEngine.TextChangeCommand();
            commandRightOp.SetAttributes(ref spreadsheet, "10", spreadsheet.GetCell(5 - 1, 7).Text, 5 - 1, 7);
            spreadsheet.AddUndo(commandRightOp, 5 - 1, 7);
            SpreadsheetEngine.TextChangeCommand commandExpression = new SpreadsheetEngine.TextChangeCommand();
            commandExpression.SetAttributes(ref spreadsheet, "=G5 + H5", spreadsheet.GetCell(5 - 1, 8).Text, 5 - 1, 8);
            spreadsheet.AddUndo(commandExpression, 5 - 1, 8);
            SpreadsheetEngine.TextChangeCommand commandRightOpNew = new SpreadsheetEngine.TextChangeCommand();
            commandRightOpNew.SetAttributes(ref spreadsheet, "20", spreadsheet.GetCell(5 - 1, 7).Text, 5 - 1, 7);
            spreadsheet.AddUndo(commandRightOpNew, 5 - 1, 7);

            spreadsheet.Undo();
            Assert.That(spreadsheet.GetCell(5 - 1, 8).Value, Is.EqualTo("15"));
        }

        /// <summary>
        /// Normal test of undoing cell background color changes.
        /// </summary>
        [Test]
        public void NormalUndoColor()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.BGColorChangeCommand command = new SpreadsheetEngine.BGColorChangeCommand();
            command.SetAttributes(ref spreadsheet, "4294901760", spreadsheet.GetCell(5, 7).BGColorValue.ToString(), 5, 7);
            spreadsheet.AddUndo(command, 5, 7);

            spreadsheet.Undo();
            Assert.That(spreadsheet.GetCell(5, 7).BGColorValue, Is.EqualTo(0xFFFFFFFF));
        }

        /// <summary>
        /// Normal test of redoing a single text input.
        /// </summary>
        [Test]
        public void NormalRedoSingleText()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.TextChangeCommand commandTest = new SpreadsheetEngine.TextChangeCommand();
            commandTest.SetAttributes(ref spreadsheet, "TEST", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandTest, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChanged = new SpreadsheetEngine.TextChangeCommand();
            commandChanged.SetAttributes(ref spreadsheet, "CHANGED", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChanged, 5 - 1, 6);
            spreadsheet.Undo();
            spreadsheet.Redo();

            Assert.That(spreadsheet.GetCell(5 - 1, 6).Text, Is.EqualTo("CHANGED"));
        }

        /// <summary>
        /// Normal test of redoing multiple text inputs.
        /// </summary>
        [Test]
        public void NormalRedoMultipleText()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.TextChangeCommand commandTest = new SpreadsheetEngine.TextChangeCommand();
            commandTest.SetAttributes(ref spreadsheet, "TEST", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandTest, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChanged = new SpreadsheetEngine.TextChangeCommand();
            commandChanged.SetAttributes(ref spreadsheet, "CHANGED", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChanged, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChangedAgain = new SpreadsheetEngine.TextChangeCommand();
            commandChangedAgain.SetAttributes(ref spreadsheet, "CHANGED AGAIN", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChangedAgain, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandChangedMore = new SpreadsheetEngine.TextChangeCommand();
            commandChangedMore.SetAttributes(ref spreadsheet, "CHANGED ONE MORE TIME", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandChangedMore, 5 - 1, 6);

            spreadsheet.Undo();
            spreadsheet.Undo();
            spreadsheet.Undo();
            spreadsheet.Redo();
            Assert.That(spreadsheet.GetCell(5 - 1, 6).Text, Is.EqualTo("CHANGED"));
        }

        /// <summary>
        /// Normal test of undoing multiple inputs that have other cells using them in calculations.
        /// </summary>
        [Test]
        public void NormalRedoMultipleCalculation()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.TextChangeCommand commandLeftOp = new SpreadsheetEngine.TextChangeCommand();
            commandLeftOp.SetAttributes(ref spreadsheet, "5", spreadsheet.GetCell(5 - 1, 6).Text, 5 - 1, 6);
            spreadsheet.AddUndo(commandLeftOp, 5 - 1, 6);
            SpreadsheetEngine.TextChangeCommand commandRightOp = new SpreadsheetEngine.TextChangeCommand();
            commandRightOp.SetAttributes(ref spreadsheet, "10", spreadsheet.GetCell(5 - 1, 7).Text, 5 - 1, 7);
            spreadsheet.AddUndo(commandRightOp, 5 - 1, 7);
            SpreadsheetEngine.TextChangeCommand commandExpression = new SpreadsheetEngine.TextChangeCommand();
            commandExpression.SetAttributes(ref spreadsheet, "=G5 + H5", spreadsheet.GetCell(5 - 1, 8).Text, 5 - 1, 8);
            spreadsheet.AddUndo(commandExpression, 5 - 1, 8);
            SpreadsheetEngine.TextChangeCommand commandRightOpNew = new SpreadsheetEngine.TextChangeCommand();
            commandRightOpNew.SetAttributes(ref spreadsheet, "20", spreadsheet.GetCell(5 - 1, 7).Text, 5 - 1, 7);
            spreadsheet.AddUndo(commandRightOpNew, 5 - 1, 7);

            spreadsheet.Undo();
            spreadsheet.Redo();
            Assert.That(spreadsheet.GetCell(5 - 1, 8).Value, Is.EqualTo("25"));
        }

        /// <summary>
        /// Normal test of undoing cell background color changes.
        /// </summary>
        [Test]
        public void NormalRedoColor()
        {
            SpreadsheetEngine.Spreadsheet spreadsheet = new SpreadsheetEngine.Spreadsheet(50, 26);
            SpreadsheetEngine.BGColorChangeCommand command = new SpreadsheetEngine.BGColorChangeCommand();
            command.SetAttributes(ref spreadsheet, "4294901760", spreadsheet.GetCell(5, 7).BGColorValue.ToString(), 5, 7);
            spreadsheet.AddUndo(command, 5, 7);

            spreadsheet.Undo();
            spreadsheet.Redo();
            Assert.That(spreadsheet.GetCell(5, 7).BGColorValue, Is.EqualTo(4294901760));
        }
    }
}
