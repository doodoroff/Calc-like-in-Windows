using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calc
{

    public partial class calcForm : Form
    {
        string firstMemberOfExpression = null;
        string secondMemberOfExpression = null;
        string operationTypeSign = null;
        string number = null;
        bool operationTypeButtonIsBeanPushedBefore = false;
        string decimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator;

        public calcForm()
        {
            InitializeComponent();
        }

        private void calcForm_Load(object sender, EventArgs e)
        {
            ShowOnDisplay("Welcome to Calc App");
        }

        void ShowOnDisplay(string infoToShow)
        {
            textBoxOutput.Text = infoToShow;
        }

        double SolveTask (string firstMemberOfExpression, string secondMemberOfExpression)
        {
            double result = 0;

            switch (operationTypeSign)
            {
                case "+":
                    {
                        result = Double.Parse(firstMemberOfExpression) + Double.Parse(secondMemberOfExpression);
                        break;
                    }

                case "-":
                    {
                        result = Double.Parse(firstMemberOfExpression) - Double.Parse(secondMemberOfExpression);
                        break;
                    }

                case "*":
                    {
                        result = Double.Parse(firstMemberOfExpression) * Double.Parse(secondMemberOfExpression);
                        break;
                    }
                case "/":
                    {
                        result = Double.Parse(firstMemberOfExpression) / Double.Parse(secondMemberOfExpression);
                        break;
                    }
            }
            return result;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("1");
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("9");
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            HandleDigitButtonInput("0");
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {

            if (number == null)
            {
                number = "-";
                ShowOnDisplay(number);
                return;
            }

            if (number.Contains("-"))
            {
                number = number.Remove(0, 1);
                InitializeAndShowExpressionMember();
                return;
            }

            number = "-" + number;
            InitializeAndShowExpressionMember();
        }

        void HandleDigitButtonInput(string digitOnButton)
        {
            if (number == null) // checing of the typed number value to avoid String method exception
            {
                number += digitOnButton;
                InitializeAndShowExpressionMember();
                return;
            }

            if (number.Contains(decimalSeparator)) // this branch is for working with typed number that begins with "0."
            {
                number += digitOnButton;
            }
            else if (number.StartsWith("0")) // this branch needs to avoid several leading zeros
            {
                number = digitOnButton;
            }
            else if (number.StartsWith("-0")) // this branch needs to avoid several leading zeroes after negative sign
            {
                number = "-" + digitOnButton;
            }
                 else
                 {
                     number += digitOnButton;
                 }
            InitializeAndShowExpressionMember();
        }

        void InitializeAndShowExpressionMember()
        {
            secondMemberOfExpression = number;
            ShowOnDisplay(secondMemberOfExpression);
        }

        private void floatingPoint_Click(object sender, EventArgs e)
        {
            if (number == null) // checing of the typed number value to avoid String method exception
            {
                number = "0" + decimalSeparator;
                ShowOnDisplay(number);
            }

            if (number.Contains(decimalSeparator)) // this check needs to avoid appearance of several decimal seporators in digit
            {
                return;
            }

            number += decimalSeparator;
            ShowOnDisplay(number);
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            HandleOperationButtonClick("+");
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            HandleOperationButtonClick("-");
        }

        private void buttonMult_Click(object sender, EventArgs e)
        {
            HandleOperationButtonClick("*");
        }

        private void buttonDiv_Click(object sender, EventArgs e)
        {
            HandleOperationButtonClick("/");
        }

        void HandleOperationButtonClick(string operationTypeSign)
        {
            if (operationTypeButtonIsBeanPushedBefore) // this branch is for doing math operation each time when the operation type button hits
            {
                number = HandleResult(SolveTask(firstMemberOfExpression, secondMemberOfExpression));
                buildAndShowPartOfExpression(operationTypeSign);
                number = null;
                return;
            }
            
            buildAndShowPartOfExpression(operationTypeSign);
            number = null;
            operationTypeButtonIsBeanPushedBefore = true;
        }

        string HandleResult(double calculationResult)
        {
            if (double.IsInfinity(calculationResult) || double.IsNaN(calculationResult))
            {
                ImplementMeasuresToWrongCalculationResult();
                return "imposible to divide by zero";
            }
            return Convert.ToString(calculationResult);
        }

        void ImplementMeasuresToWrongCalculationResult()
        {
            groupBox.Enabled = false;
        }

        void buildAndShowPartOfExpression(string operationTypeSign)
        {
            PerformFirstMemberOfExpression();
            PerformDefaultSecondMemberOfExpression(operationTypeSign);  // this method needs to allow the select of math operation type that will be executed on calculation result
            this.operationTypeSign = operationTypeSign;
            ShowOnDisplay(firstMemberOfExpression + " " + operationTypeSign);
        }
        
        void PerformFirstMemberOfExpression()
        {
            if (number == null || number == "-")
            {
                firstMemberOfExpression = "0";
            }
            else
            {
               firstMemberOfExpression = number;
            }
        }

        void PerformDefaultSecondMemberOfExpression(string operationTypeSign)
        {
            if (operationTypeSign == "+" || operationTypeSign == "-")
            {
                secondMemberOfExpression = "0";
            }
            else
            {
                secondMemberOfExpression = "1";
            }
        }

        private void buttonEql_Click(object sender, EventArgs e)
        {
            number = HandleResult(SolveTask(firstMemberOfExpression, secondMemberOfExpression));
            ShowOnDisplay(number);
            firstMemberOfExpression = number; // this needs to continiue operations with calculated value
            operationTypeButtonIsBeanPushedBefore = false;
           
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            number = null;
            ShowOnDisplay("0");
            firstMemberOfExpression = "0";
            secondMemberOfExpression = "0";
            operationTypeButtonIsBeanPushedBefore = false;
            groupBox.Enabled = true;
        }
  
    }

}
