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
    //All methods related to the calculation and error checking are in this class
    class calculation
    {

       
        //Method called when numButton is pressed
        public string checkDecimal(string prompt, string inputbox)
        {
            //Check if there is any "." is in inputBox.Text
            //If there is, any subsequent input of "." is deleted
          Boolean haveDecimal = inputbox.Contains(".");

          if (haveDecimal == true && prompt == "." || inputbox.Length > 16)
          {
              prompt = prompt.Remove(prompt.Length - 1);
          }
            return prompt;

            
        }

        //Method called when memory button is pressed
        public bool checkChar(string prompt)
        {
            //Check if there is any letter is in inputBox.Text
            //If there is, lockDown method is called
            bool letter = false;
            foreach(char c in prompt)
            {
                letter = letter || Char.IsLetter(c);

            }

            if(letter == true)
            {
                letter = true;
            }
            return letter;
        }

        //Called during calculation proccess
        public bool checkDivision(List<float> x, string input)
        {
            // If operation is x/0
            // Output is false
            bool output = true;

            if(x[1] == 0 && input == "÷")
            {
                output = false;
            }

            return output;
        }

        //Method for calculating results
        public float equalCalculate(List<float> num, string input)
        {
            //Calculate correct result and return result as output
            float output = 0f;

            //This chunk of code was done using switch instead of if/else to be more efficient and improve clarity of code
            switch (input)
            {
                case "+":
                    output = num[0] + num[1];
                    break;

                case "-":
                    output = num[0] - num[1];
                    break;

                case "x":
                    output = num[0] * num[1];
                    break;

                case "÷":
                    output = num[0] / num[1];
                    break;
            }
            return output;

        }

       
    }
}
