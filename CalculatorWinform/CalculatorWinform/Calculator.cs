//using Calculator;
//using CalculatorApp;
using desktop_calculator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows.Forms;
namespace CalculatorWinform
{
    public partial class Calculator : Form
    {
        private List<CalculatorButtonData> _advanceKeys;
        private List<CalculatorButtonData> _basicKeys;
        private List<CalculatorButtonData> _allKeys;
        private ExpressionCalculator _calculator;
        private string _calculatorExpression;
        private double _memoryValue;
        private bool _exceptionAppeared;
        private Dictionary<string, CalculatorButtonData> _buttonRelation;
        private int _openBracketCount;
        private char _lastOperatorInExpression;
        private bool _decimalSequence;
        public Calculator()
        {
            _decimalSequence = false;
            _lastOperatorInExpression = '0';
            _openBracketCount = 0;
            _exceptionAppeared = false;
            _memoryValue = double.NaN;
            LoadCalculatorKeyData();
            InitializeComponent();
        }

        /// <summary>
        /// stores the textlabel value in the memory if the Text is a Numeric type.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMemoryStoreClick(object sender, EventArgs e)
        {
            if (double.TryParse(_calculatorTextBox.Text, out double result))
            {
                this._memoryRead.Enabled = true;
                this._memoryClear.Enabled = true;
                this._memoryAdd.Enabled = true;
                this._memorySubtract.Enabled = true;
                _memoryValue = double.Parse(_calculatorTextBox.Text);
                this._memoryLabel.Text = ButtonTextData.MemoryLabel + _calculatorTextBox.Text;
            }
        }

        /// <summary>
        /// Reads and display the value currently in the Memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMemoryReadClick(object sender, EventArgs e)
        {
            string memoryVal = _memoryValue.ToString();
            _calculatorTextBox.Text = memoryVal;
            _calculatorExpression = memoryVal;
            this._memoryLabel.Text = ButtonTextData.MemoryLabel + memoryVal;
        }

        /// <summary>
        /// Clears the Memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMemoryClearClick(object sender, EventArgs e)
        {
            _memoryValue = 0;
            this._memoryLabel.Text = ButtonTextData.MemoryLabel + _memoryValue.ToString();
        }

        /// <summary>
        /// Add the current text label value to the memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMemoryAddClick(object sender, EventArgs e)
        {
            _memoryValue += double.Parse(_calculatorTextBox.Text);
            this._memoryLabel.Text = ButtonTextData.MemoryLabel + _memoryValue.ToString();
        }

        /// <summary>
        /// Subtract the current value from the value stored in memory.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnMemorySubtractClick(object sender, EventArgs e)
        {
            _memoryValue -= double.Parse(_calculatorTextBox.Text);
            this._memoryLabel.Text = ButtonTextData.MemoryLabel + _memoryValue.ToString();
        }

        /// <summary>
        ///  Loads the data of button from the key List
        /// </summary>
        private void LoadCalculatorKeyData()
        {
            //string fileName = ConfigurationManager.AppSettings["ButtonData"]; ;
            string fileName = "CalculatorButtonData.json";
            string jsonString = File.ReadAllText(fileName);
            _allKeys = JsonConvert.DeserializeObject<List<CalculatorButtonData>>(jsonString);

            _basicKeys = _allKeys.Where(key => key.CalculatorType == "Basic").ToList();
            _advanceKeys = _allKeys.Where(key => key.CalculatorType == "Advance").ToList();
            _buttonRelation = _advanceKeys.ToDictionary(X => X.KeyName, x => x);
        }

        /// <summary>
        /// Handles the Copy button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCopyClick(object sender, EventArgs e)
        {
            Clipboard.SetText(this._calculatorTextBox.Text);
        }

        /// <summary>
        /// Handles the Paste button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPasteClick(object sender, EventArgs e)
        {
            PasteOperation();
        }

        private void PasteOperation()
        {
            string copiedText = Clipboard.GetText();
            string textLabel = copiedText;
            string textExpression = copiedText;

            for (int i = 0; i < _advanceKeys.Count; i++)
            {
                string buttonName = _advanceKeys[i].KeyName;

                textLabel = textLabel.Replace(_buttonRelation[buttonName].KeyValue, _buttonRelation[buttonName].KeyDisplaySymbol);
                textExpression = textExpression.Replace(_buttonRelation[buttonName].KeyDisplaySymbol, _buttonRelation[buttonName].KeyValue);
            }
            _calculatorTextBox.Text = textLabel;
            _calculatorExpression = textExpression;
        }

        private void OnHelpClick(object sender, EventArgs e)
        {
            MessageBox.Show(ButtonTextData.HelpInfo, ButtonTextData.Help, MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
        }

        private void OnExitClick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            string currentTextLabel = _calculatorTextBox.Text;
            int currentTextLength = _calculatorTextBox.Text.Length;
            char lastCharVal = currentTextLabel[currentTextLength - 1];
            string openBracketSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonOpenBracket].KeyDisplaySymbol;

            if (e.KeyCode == Keys.Back)
            {
                OnBackspaceClick(currentTextLabel, lastCharVal, openBracketSymbolDisplay);
            }
            else if (e.KeyCode == Keys.D9 && e.Shift)
            {
                string clickedButtonText = _buttonRelation[ConstantButtonName.ButtonOpenBracket].KeyDisplaySymbol;
                string clickedButtonTag = _buttonRelation[ConstantButtonName.ButtonOpenBracket].KeyValue;
                if (_exceptionAppeared == false)
                {
                    _openBracketCount++;
                    PerformFillingAction(currentTextLabel, clickedButtonText, clickedButtonTag);
                }
            }
            else if (e.KeyCode == Keys.D0 && e.Shift)
            {
                string clickedButtonText = _buttonRelation[ConstantButtonName.ButtonCloseBracket].KeyDisplaySymbol;
                string clickedButtonTag = _buttonRelation[ConstantButtonName.ButtonCloseBracket].KeyValue;
                if (_openBracketCount > 0 && _exceptionAppeared == false)
                {
                    PerformFillingActionOnClosingBracket(currentTextLabel, clickedButtonText, clickedButtonTag);
                    _openBracketCount--;
                }
            }
            else if (e.KeyCode >= Keys.D0 && e.KeyCode <= Keys.D9 || e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {
                string clickedButton = e.KeyData.ToString();
                int valLen = clickedButton.Length;
                string numValue = clickedButton.Substring(valLen - 1);
                if (_exceptionAppeared)
                {
                    FillTextAndExpressionIfEmptyOrNull(numValue, numValue);
                    _exceptionAppeared = false;
                }
                else
                {

                    PerformFillingAction(currentTextLabel, numValue, numValue);
                }
            }
            else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Subtract || e.KeyCode == Keys.Multiply || e.KeyCode == Keys.Divide)
            {

                string currButton = "Button" + e.KeyData.ToString();
                string displaySymbol = _buttonRelation[currButton].KeyDisplaySymbol;
                string keyValue = _buttonRelation[currButton].KeyValue;
                if (_exceptionAppeared == false)
                {
                    _lastOperatorInExpression = displaySymbol[0];
                    _decimalSequence = false;

                    try
                    {
                        PerformFillingActionOnOperator(currentTextLabel, displaySymbol, keyValue);
                    }
                    catch
                    {
                        _calculatorTextBox.Text = ButtonTextData.Error;
                        _exceptionAppeared = true;
                    }
                }
            }
            else if (e.KeyCode == Keys.Decimal)
            {
                string displaySymbol = _buttonRelation[ConstantButtonName.ButtonPeriod].KeyDisplaySymbol;
                string keyValue = _buttonRelation[ConstantButtonName.ButtonPeriod].KeyValue;
                if (_exceptionAppeared == false && currentTextLabel.Contains(displaySymbol) == false)
                {

                    try
                    {
                        if (string.IsNullOrEmpty(currentTextLabel) || currentTextLabel == Constants.IntChar0.ToString())
                        {
                            FillTextAndExpressionIfEmptyOrNullOperator(displaySymbol, keyValue);
                        }
                        else if (lastCharVal.ToString() != displaySymbol.Substring(0))
                        {
                            FillTextAndExpressionValues(currentTextLabel, displaySymbol, keyValue);
                        };
                    }
                    catch
                    {
                        _calculatorTextBox.Text = ButtonTextData.Error;
                        _exceptionAppeared = true;
                    }
                }
            }
            else if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                PasteOperation();
            }
            else if (e.KeyCode == Keys.C && e.Modifiers == Keys.Control)
            {
                Clipboard.SetText(this._calculatorTextBox.Text);
            }
        }

        private void PerformFillingActionOnOperator(string currentTextLabel, string displaySymbol, string keyValue)
        {
            char lastCharVal = currentTextLabel[currentTextLabel.Length - 1];
            if (string.IsNullOrEmpty(currentTextLabel) || currentTextLabel == Constants.IntChar0.ToString())
            {
                FillTextAndExpressionIfEmptyOrNullOperator(displaySymbol, keyValue);
            }
            else if (lastCharVal.ToString() != displaySymbol.Substring(0) && ConstantButtonName.operators.Contains(lastCharVal) == true)
            {
                currentTextLabel = currentTextLabel.Remove(currentTextLabel.Length - 1);
                _calculatorExpression = _calculatorExpression.Remove(_calculatorExpression.Length - 1);
                FillTextAndExpressionValues(currentTextLabel, displaySymbol, keyValue);
            }
            else if (lastCharVal.ToString() != displaySymbol.Substring(0))
            {
                FillTextAndExpressionValues(currentTextLabel, displaySymbol, keyValue);
            }
        }

        private void PerformFillingAction(string currentTextLabel, string clickedButtonText, string clickedButtonTag)
        {
            string multiplicationSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonMultiply].KeyDisplaySymbol;
            string multiplicationKeyValue = _buttonRelation[ConstantButtonName.ButtonMultiply].KeyValue;
            char lastCharVal = currentTextLabel[currentTextLabel.Length - 1];
            string openBracketDisplaySymbol = _buttonRelation[ConstantButtonName.ButtonOpenBracket].KeyDisplaySymbol;

            if (string.IsNullOrEmpty(currentTextLabel) || currentTextLabel == Constants.IntChar0.ToString())
            {
                FillTextAndExpressionIfEmptyOrNull(clickedButtonText, clickedButtonTag);
            }
            else if (lastCharVal.ToString() == _buttonRelation[ConstantButtonName.ButtonCloseBracket].KeyDisplaySymbol.Substring(0) && (clickedButtonText[0] >= Constants.IntChar0 && clickedButtonText[0] <= Constants.IntChar9 || clickedButtonText.Substring(0) == openBracketDisplaySymbol.Substring(0)))
            {
                _lastOperatorInExpression = multiplicationSymbolDisplay[0];
                FillTextAndExpressionValues(currentTextLabel, multiplicationSymbolDisplay + clickedButtonText, multiplicationKeyValue + clickedButtonTag);
            }
            else if (lastCharVal >= Constants.IntChar0 && lastCharVal <= Constants.IntChar9 && clickedButtonText.Equals(openBracketDisplaySymbol.Substring(0)))
            {
                _lastOperatorInExpression = openBracketDisplaySymbol[0];
                FillTextAndExpressionValues(currentTextLabel, multiplicationSymbolDisplay + clickedButtonText, multiplicationKeyValue + clickedButtonTag);
            }
            else
            {
                FillTextAndExpressionValues(currentTextLabel, clickedButtonText, clickedButtonTag);
            };
        }

        private void OnButtonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            string buttonName = clickedButton.Name;
            string currentTextLabel = _calculatorTextBox.Text;
            int currentTextLength = _calculatorTextBox.Text.Length;
            char lastCharVal = currentTextLabel[currentTextLength - 1];
            string multiplicationSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonMultiply].KeyDisplaySymbol;
            string multiplicationKeyValue = _buttonRelation[ConstantButtonName.ButtonMultiply].KeyValue;
            string openBracketSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonOpenBracket].KeyDisplaySymbol;

            switch (buttonName)
            {
                case ConstantButtonName.ButtonPercentage:
                case ConstantButtonName.ButtonDivide:
                case ConstantButtonName.ButtonMultiply:
                case ConstantButtonName.ButtonAdd:
                case ConstantButtonName.ButtonSubtract:
                case ConstantButtonName.Exponent:
                    if (_exceptionAppeared == false)
                    {
                        _lastOperatorInExpression = clickedButton.Text[0];
                        _decimalSequence = false;
                        try
                        {
                            PerformFillingActionOnOperator(currentTextLabel, clickedButton.Text, clickedButton.Tag.ToString());
                        }
                        catch
                        {
                            _calculatorTextBox.Text = ButtonTextData.Error;
                            _exceptionAppeared = true;
                        }
                    }
                    break;
                case ConstantButtonName.ButtonPeriod:
                    if (_exceptionAppeared == false && _decimalSequence == false)
                    {
                        _decimalSequence = true;
                        try
                        {
                            PerformFillingActionOnOperator(currentTextLabel, clickedButton.Text, clickedButton.Tag.ToString());
                        }
                        catch
                        {
                            _calculatorTextBox.Text = ButtonTextData.Error;
                            _exceptionAppeared = true;
                        }
                    }
                    break;
                case ConstantButtonName.ClearEntry:
                case ConstantButtonName.ClearGlobal:

                    if (_exceptionAppeared)
                    {
                        _exceptionAppeared = false;
                    }
                    SetTextZero();
                    break;
                case ConstantButtonName.Unary:

                    OnUnaryClick(currentTextLabel, _calculatorExpression);
                    break;
                case ConstantButtonName.Backspace:
                    OnBackspaceClick(currentTextLabel, lastCharVal, openBracketSymbolDisplay);
                    break;
                case ConstantButtonName.Button7:
                case ConstantButtonName.Button8:
                case ConstantButtonName.Button9:
                case ConstantButtonName.Button4:
                case ConstantButtonName.Button5:
                case ConstantButtonName.Button6:
                case ConstantButtonName.Button1:
                case ConstantButtonName.Button2:
                case ConstantButtonName.Button3:
                case ConstantButtonName.Button0:
                    if (_exceptionAppeared)
                    {
                        FillTextAndExpressionIfEmptyOrNull(clickedButton.Text, clickedButton.Tag.ToString());
                        _exceptionAppeared = false;
                    }
                    else
                    {
                        PerformFillingAction(currentTextLabel, clickedButton.Text, clickedButton.Tag.ToString());
                    }
                    break;
                case ConstantButtonName.ButtonEqual:
                    OnEqualButtonClick();
                    break;
                case ConstantButtonName.Sin:
                case ConstantButtonName.Cos:
                case ConstantButtonName.SquareRoot:
                case ConstantButtonName.Log:
                case ConstantButtonName.Inverse:
                case ConstantButtonName.Tan:
                    _lastOperatorInExpression = openBracketSymbolDisplay[0];
                    if (_exceptionAppeared == false)
                    {
                        _decimalSequence = false;
                        _openBracketCount++;
                        if (string.IsNullOrEmpty(currentTextLabel) || currentTextLabel == Constants.IntChar0.ToString())
                        {
                            _calculatorTextBox.Text = clickedButton.Text + openBracketSymbolDisplay; // Tan button is presses it should be Tan(
                            _calculatorExpression = clickedButton.Tag.ToString() + openBracketSymbolDisplay;
                        }
                        else if (lastCharVal >= Constants.IntChar0 &&
                            lastCharVal <= Constants.IntChar9 ||
                            lastCharVal.ToString() == _buttonRelation[ConstantButtonName.ButtonCloseBracket].KeyDisplaySymbol.Substring(0))
                        {
                            _calculatorTextBox.Text = currentTextLabel + multiplicationSymbolDisplay + clickedButton.Tag.ToString() + openBracketSymbolDisplay; // Tan button is pressed after a number such as 6 it should appear 6*tan(
                            _calculatorExpression = _calculatorExpression + multiplicationKeyValue + clickedButton.Tag.ToString() + openBracketSymbolDisplay;
                        }
                        else
                        {
                            _calculatorTextBox.Text = currentTextLabel + clickedButton.Text + openBracketSymbolDisplay; //else otherwise aster any operator it should be Tan(
                            _calculatorExpression = _calculatorExpression + clickedButton.Tag.ToString() + openBracketSymbolDisplay;
                        }
                    }
                    break;
                case ConstantButtonName.ButtonOpenBracket:
                    _lastOperatorInExpression = clickedButton.Text[0];
                    if (_exceptionAppeared == false)
                    {
                        _openBracketCount++;
                        PerformFillingAction(currentTextLabel, clickedButton.Text, clickedButton.Tag.ToString());
                    }
                    break;
                case ConstantButtonName.ButtonCloseBracket:
                    if (_openBracketCount > 0 && _exceptionAppeared == false)
                    {
                        PerformFillingActionOnClosingBracket(currentTextLabel, clickedButton.Text, clickedButton.Tag.ToString());
                        _openBracketCount--;
                    }
                    break;
            }
        }
        private void OnUnaryClick(string calculatorText, string calculatorExpression)
        {

            string multiplicationSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonMultiply].KeyDisplaySymbol;
            string multiplicationKeyValue = _buttonRelation[ConstantButtonName.ButtonMultiply].KeyValue;
            string openBracketSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonOpenBracket].KeyDisplaySymbol;
            string closeBracketSymbolDisplay = _buttonRelation[ConstantButtonName.ButtonCloseBracket].KeyDisplaySymbol;
            string SubtractValue = _buttonRelation[ConstantButtonName.ButtonSubtract].KeyValue.Substring(0);

            int idxOfLastOperator = calculatorText.LastIndexOf(_lastOperatorInExpression);
            string lastNum = calculatorText.Substring(idxOfLastOperator + 1);
            if (idxOfLastOperator < calculatorText.Length && lastNum.Length > 0)
            {

                string splitText = calculatorText.Remove(idxOfLastOperator + 1);
                string splitExpression = calculatorExpression.Remove(idxOfLastOperator + 1);
                if (_lastOperatorInExpression == '0')
                {
                    _calculatorExpression = SubtractValue + 1 + openBracketSymbolDisplay + _calculatorExpression + closeBracketSymbolDisplay;
                    OnEqualButtonClick();
                }
                else if (_lastOperatorInExpression == _buttonRelation[ConstantButtonName.ButtonSubtract].KeyDisplaySymbol[0])
                {
                    _calculatorTextBox.Text = splitText + 1 + multiplicationSymbolDisplay + openBracketSymbolDisplay + SubtractValue + lastNum + closeBracketSymbolDisplay;
                    _calculatorExpression = splitExpression + 1 + multiplicationKeyValue + openBracketSymbolDisplay + SubtractValue + lastNum + closeBracketSymbolDisplay;
                }
                else
                {
                    _calculatorTextBox.Text = splitText + openBracketSymbolDisplay + SubtractValue + lastNum + closeBracketSymbolDisplay;
                    _calculatorExpression = splitExpression + openBracketSymbolDisplay + SubtractValue + lastNum + closeBracketSymbolDisplay;
                }
            }
            _lastOperatorInExpression = '0';
        }

        private void OnBackspaceClick(string currentTextLabel, char lastCharVal, string openBracketSymbolDisplay)
        {
            if (_exceptionAppeared == true || string.IsNullOrEmpty(currentTextLabel) || currentTextLabel == Constants.IntChar0.ToString() || currentTextLabel.Length == 1)
            {
                SetTextZero();
                _exceptionAppeared = false;
            }
            else
            {
                if (lastCharVal.ToString() == openBracketSymbolDisplay)
                {
                    _openBracketCount--;
                }
                _calculatorTextBox.Text = currentTextLabel.Remove(currentTextLabel.Length - 1);
                _calculatorExpression = _calculatorExpression.Remove(_calculatorExpression.Length - 1);
            }
        }

        private void PerformFillingActionOnClosingBracket(string currentTextLabel, string clickedButtonText, string clickedButtonTag)
        {
            if (string.IsNullOrEmpty(currentTextLabel) || currentTextLabel == Constants.IntChar0.ToString())
            {
                SetTextZero();
            }
            else
            {
                FillTextAndExpressionValues(currentTextLabel, clickedButtonText, clickedButtonTag);
            };
        }

        private void OnTabSwitch(object sender, EventArgs e)
        {
            switch (_tabControl.SelectedIndex)
            {
                case 0:
                    SetTextZero();
                    break;
                case 1:
                    SetTextZero();
                    break;
            }
        }

        private Button CreateButton(CalculatorButtonData key)
        {
            Button button = new Button();
            //adding button values
            button.Name = key.KeyName;
            button.Text = key.KeyDisplaySymbol;
            button.Tag = key.KeyValue;
            button.Dock = DockStyle.Fill;
            button.ForeColor = key.ForeColor;
            button.Font = key.Font;
            button.BackColor = key.BackColor;
            button.FlatStyle = key.FlatStyle;
            button.FlatAppearance.BorderSize = key.BorderSize;
            button.Click += OnButtonClick;

            return button;
        }

        private void SetTextZero()
        {
            string zeroChar = Constants.IntChar0.ToString();
            _lastOperatorInExpression = '0';
            _decimalSequence = false;
            _calculatorTextBox.Text = zeroChar;
            _calculatorExpression = zeroChar;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                OnEqualButtonClick();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OnEqualButtonClick()
        {
            _calculator = new ExpressionCalculator();
            try
            {
                _calculatorTextBox.Text = _calculator.Evaluate(_calculatorExpression.Trim()).ToString();
                _calculatorExpression = _calculatorTextBox.Text;
            }
            catch (DivideByZeroException exc)
            {
                _calculatorTextBox.Text = exc.Message;
                _exceptionAppeared = true;
            }
            catch
            {
                _exceptionAppeared = true;
                _calculatorTextBox.Text = ButtonTextData.Error;
            }
        }

        private void FillTextAndExpressionValues(string currentTextLabel, string displaySymbol, string keyValue)
        {
            _calculatorTextBox.Text = currentTextLabel + displaySymbol;
            _calculatorExpression += keyValue;
        }

        private void FillTextAndExpressionIfEmptyOrNull(string clickedButtonText, string clickedButtonTag)
        {
            _calculatorTextBox.Text = clickedButtonText;
            _calculatorExpression = clickedButtonTag;
        }

        private void FillTextAndExpressionIfEmptyOrNullOperator(string clickedButtonText, string clickedButtonTag)
        {
            string zeroChar = Constants.IntChar0.ToString();
            _calculatorTextBox.Text = zeroChar + clickedButtonText;
            _calculatorExpression = _calculatorExpression + zeroChar + clickedButtonTag;
        }
    }

}
