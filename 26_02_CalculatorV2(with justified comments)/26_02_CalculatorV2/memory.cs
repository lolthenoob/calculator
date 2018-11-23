using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace _26_02_CalculatorV2
{

    //This class was made to organise the program and improve clarity of code
    //All methods related to the memory buttons are found here
    
    class memory
    {
       
        //Declare global varaibles
        double m = 0;
        
        //Method called when memory button is clicked
        public string UseMemory(string input, string num)
        {
            //Declare "in method" varaibles
            string output = " ";
            double x;
            double n;

            //If num(inputBox.text) can be converted into double, n = inputBox.Text converted to double
            if (Double.TryParse(num.Trim(), out n))
            {
                x = n;

            }

            //Do correct operation for each input
            //I used switch vs If/Else to set the output of the buttons
            //as switch is more efficient than if/ELse and it improved clarity of code          
            switch (input)
            {
                case "M+":
                    if (m == 0)
                    {
                        m = n;
                        output = n.ToString() + "-> M";
                    }
                    else
                    {
                        output = Convert.ToString(m + n);
                    }
                    break;
                    

                case "MC":
                    m = 0;
                    
                    break;

                case "M":
                    output = Convert.ToString(m);
                    break;

                case "M-":
                    if(m == 0)
                    {

                        m = n;
                        output = n.ToString() + "-> M";

                    }
                    else
                    {
                        output = Convert.ToString(n - m);
                    }
                    
                    break;
            }

            return output;
        }

        //Method called when "DEL" button is pressed
        public string DeleteChar(string input)
        {

            //If lenght of inputBox.Text is greater than 0, remove last char at end of string
            if (input.Length > 0)
            {
                input = input.Remove(input.Length - 1);             
            }
            return input;
        }

        //Method called when "AC" button is pressed
        public void AllClear(TextBox input, TextBox display, List<Button> btnNum, List<Button> btnSigns, List<float> num )
        {
            //Clear inputBox, DisplayBox and listFloat
            input.Clear();
            display.Clear();
            num.Clear();

            //Enable all buttons
            //Buttons have to be enabled after method "lockDown" was called which disables all buttons in case of error
            foreach(Button b in btnSigns )
            {
                b.Enabled = true;
            }

            foreach(Button b in btnNum)
            {
                b.Enabled = true;
            }
        }

        //Method called when error is caught
        public void lockDown(string input, List<Button> listBtnNum, List<Button>  listBtnSigns, TextBox DisplayBox, TextBox inputBox)
        {


            //This methods disables all number and signs buttons
           //To ensure user cannot input more any button when error is caught
            foreach (Button b in listBtnNum)
            {
                b.Enabled = false;
            }

            foreach (Button b in listBtnSigns)
            {
                b.Enabled = false;
            }

            //Display correct error message
            if (input == "÷")
            {
                inputBox.Text =  "ERROR: UNDEFINED";
            }
            DisplayBox.Text = "PLEASE PRESS AC TO CONTINUE";
        }
    }
}
