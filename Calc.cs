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
        string firstExpressionMember = null;
        string secondExpressionMember = null;
        string opType = null;
        string argument = null;
        string argumentContainer = null;
        bool checkOpButtonHit = false;
        string decimalSeparator = System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator; //Decimal separator for current culture

        public calcForm()
        {
            InitializeComponent();
        }

        private void calcForm_Load(object sender, EventArgs e)
        {
            textBoxOutput.Text = "Wellcome to Calc";
        }

        public double SolveTask (string digit1, string digit2, string opType)
        {
            double result = 0; // inside varaible

            switch (opType)
            {
                case "+":
                    {
                        result = Double.Parse(digit1) + Double.Parse(digit2);
                        break;
                    }

                case "-":
                    {
                        result = Double.Parse(digit1) - Double.Parse(digit2);
                        break;
                    }

                case "*":
                    {
                        result = Double.Parse(digit1) * Double.Parse(digit2);
                        break;
                    }
                case "/":
                    {
                        if (digit2 == "0")
                        {
                            MessageBox.Show("Zero devisor error !");
                            result = 0;
                            break;
                        }
                        result = Double.Parse(digit1) / Double.Parse(digit2);
                        break;
                    }
            }
            return result;
        }
        
        public void ButtonInput (string argument, string digit)
        {
            if (argument == null) 
            {
                this.argument = argument + digit;
                this.secondExpressionMember = this.argument;
                textBoxOutput.Text = this.argument;
                return;
            }

            if (argument.Contains(decimalSeparator)) // this branch allows to work with arguments that starts with "0."
            {
                this.argument = argument + digit;
            }

                else if (argument.StartsWith("0")) // this branch needs to avoid several leading zeros
                {
                    this.argument = digit;
                }
                    else
                    {
                        this.argument = argument + digit;
                    }
            this.secondExpressionMember = this.argument;
            textBoxOutput.Text = this.argument;
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "1");
        } 

        private void button2_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ButtonInput(argument, "9");
        }

        private void buttonZero_Click(object sender, EventArgs e)
        {
            if (argument == "0") // this check needs to avoid leading zeros before digit
            {
                return;
            }

            argument = argument + "0";
            secondExpressionMember = argument;
            textBoxOutput.Text = argument;
        }

        private void floatingPoint_Click(object sender, EventArgs e)
        {
            if (argument == null) // checing of the argument value to avoid String method exception
            {
                argument = "0" + decimalSeparator;
                textBoxOutput.Text = argument;
            }

            if (argument.Contains(decimalSeparator)) // this check needs to avoid appearance of several decimal seporator signs in digit
            {
                return;
            }

            argument = argument + decimalSeparator;
            textBoxOutput.Text = argument;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
                if (checkOpButtonHit != false) // this branch executes if the operation type button been already clicked. It uses to repeat math operation each time when the operation type button hits
                    {
                        argument = Convert.ToString(SolveTask(firstExpressionMember, secondExpressionMember, opType)); // in this branch argument value must be taken from SolveTask method
                        firstExpressionMember = argument;
                        secondExpressionMember = "0";
                        opType = "+";
                        argument = argument + " + ";
                        textBoxOutput.Text = argument;
                        argument = null;
                        return;
                    }
            // this code executes if operation button clicked for the first time
            if (argument != null) 
            {
                firstExpressionMember = argument;
                argument = argument + " + ";
            }
            else
            {
                firstExpressionMember = argumentContainer; // if argument equal null, digit2 value gets from argumentContainer, this provides ability to continiue math operations with value that recived after the equal button click
                argument = argumentContainer + " + ";
            }
            secondExpressionMember = "0"; // this provides inputing of second expression member from digit buttons clicking after the operation type button been clicked 
            opType = "+";
            checkOpButtonHit = true;
            textBoxOutput.Text = argument;
            argumentContainer = null;
            argument = null;
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
                if (checkOpButtonHit != false)
                {
                    argument = Convert.ToString(SolveTask(firstExpressionMember, secondExpressionMember, opType));
                    firstExpressionMember = argument;
                    secondExpressionMember = "0";
                    opType = "-";
                    argument = argument + " - ";
                    textBoxOutput.Text = argument;
                    argument = null;
                    return;
                }
            if (argument != null)
            {
                firstExpressionMember = argument;
                argument = argument + " - ";
            }
            else
            {
                firstExpressionMember = argumentContainer;
                argument = argumentContainer + " - ";
            }
            secondExpressionMember = "0";
            opType = "-";
            checkOpButtonHit = true;
            textBoxOutput.Text = argument;
            argumentContainer = null;
            argument = null;
        } //same as buttonPlus_Click, but with "-" operation type sign

        private void buttonMult_Click(object sender, EventArgs e)
        {
                if (checkOpButtonHit != false)
                {
                    argument = Convert.ToString(SolveTask(firstExpressionMember, secondExpressionMember, opType));
                    firstExpressionMember = argument;
                    secondExpressionMember = "1";
                    opType = "*";
                    argument = argument + " * ";
                    textBoxOutput.Text = argument;
                    argument = null;
                    return;
                }
            if (argument != null)
            {
                firstExpressionMember = argument;
                argument = argument + " * ";
            }
            else
            {
                firstExpressionMember = argumentContainer;
                argument = argumentContainer + " * ";
            }
            secondExpressionMember = "1"; // in this operation type method digit1 gets "1" instead "0" to avoid expression value loss after zero multiplication
            opType = "*";
            checkOpButtonHit = true;
            textBoxOutput.Text = argument;
            argumentContainer = null;
            argument = null;
        } //got some distinctions from buttonPlus_Click method

        private void buttonDiv_Click(object sender, EventArgs e)
        {
                if (checkOpButtonHit != false)
                {
                    argument = Convert.ToString(SolveTask(firstExpressionMember, secondExpressionMember, opType));
                    firstExpressionMember = argument;
                    secondExpressionMember = "1";
                    opType = "/";
                    argument = argument + " / ";
                    textBoxOutput.Text = argument;
                    argument = null;
                    return;
                }

            if (argument != null)
            {
                firstExpressionMember = argument;
                argument = argument + " / ";
            }
            else
            {
                firstExpressionMember = argumentContainer;
                argument = argumentContainer + " / ";
            }
            secondExpressionMember = "1"; // in this operation type method digit1 gets "1" instead "0" to avoid zero divisor exception 
            opType = "/";
            checkOpButtonHit = true;
            textBoxOutput.Text = argument;
            argumentContainer = null;
            argument = null;
        } //got some distinctions from buttonPlus_Click method

        private void buttonEql_Click(object sender, EventArgs e)
        {
            checkOpButtonHit = false;
            argument = Convert.ToString(SolveTask(firstExpressionMember, secondExpressionMember, opType));
            firstExpressionMember = argument; // this operation writes result of SolveTask method which contains in argument variable to first expression member, this provides ability of operating with expression result value after "equal" button been clicked
            argumentContainer = argument; // this operation writes result of SolveTask method which contains in argument variable to argumentContainer, this provides math operations with value recived after the "equal" button click
            textBoxOutput.Text = argument;
            argument = null;
           
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            argument = null;
            argumentContainer = null;
            textBoxOutput.Text = "0";
            firstExpressionMember = "0";
            secondExpressionMember = "0";
            checkOpButtonHit = false;
        }

        private void buttonSign_Click(object sender, EventArgs e)
        {

            if (argument == null)
            {
                    if (argumentContainer == null)
                    {
                        argument = "-";
                        textBoxOutput.Text = argument;
                        return;
                    }

                    if (argumentContainer.Contains("-"))
                    {
                        argument = argumentContainer.Remove(0, 1); // this provides ability to change argument's value to negative and back by clicking the "-" button
                        secondExpressionMember = argument;
                        textBoxOutput.Text = argument;
                        return;
                    }

                    argument = "-" + argumentContainer;
                    textBoxOutput.Text = argument;
                    return;
            }
            // this code executes if argument isn't null
            if (argument.Contains("-"))
            {
                argument = argument.Remove(0, 1);
                secondExpressionMember = argument;
                textBoxOutput.Text = argument;
                return;
            }

            argument = "-" + argument;
            secondExpressionMember = argument;
            textBoxOutput.Text = argument;
        }

    
    }

   

}
