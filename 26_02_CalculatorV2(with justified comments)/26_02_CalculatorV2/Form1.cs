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

    //Each function of the program(clear textboxes, create buttons, check error) was done in different methods to improve clarity of code for easier debugging
    public partial class Form1 : Form
    {
        //Declare textBox
        TextBox DisplayBox = new TextBox();
        TextBox inputBox = new TextBox();


        //Declare list variables
        List<Button> listBtnNum = new List<Button>();       //This list was declared to store all number buttons
        List<Button> listBtnMemory = new List<Button>();    //This list was declared to store all memory buttons
        List<Button> listBtnSigns = new List<Button>();      //This list was declared to store all operator buttons
        List<Button> listBtnClear = new List<Button>();     //This list was declared to store all clear buttons
        List<float> listFloat = new List<float>();          

        //Declare class varaibles
        
        calculation operation = new calculation();
        memory storage = new memory();
        string sign = " ";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load and create memory, number, operator and clear buttons
            createListBtnNum();
            createListBtnMemory();
            createlistBtnSigns();
            createListBtnClear();

            //Load and create textboxes
            createTextbox();
            
        }



        //Operation that occurs if operator buttons are clicked
        private void operatorButton_Click(object sender, EventArgs e)
        {
            //If "-" button is pressed and there are no text in inputBox, add - sign to inputBox
            if(inputBox.Text.Length == 0 && (sender as Button).Text == "-")
            {
                inputBox.Text += (sender as Button).Text;
            }
            //Check if inputText is valid
            if (inputBox.Text.Length > 0 && inputBox.Text.Trim() != "." && inputBox.Text != "-")
            {
                //If it is valid, add it to listFloat and clear the inputBox
                listFloat.Add(float.Parse(inputBox.Text));
                inputBox.Clear();
            }

            
            //If there is one value in listFloat
                if (listFloat.Count() == 1)
                {
                //Store the text of sign button
                    sign = (sender as Button).Text;


                //If "=" button is pressed, display correct response, and clear listFloat
                //Else, display correct operation
                if (sign == "=")
                    {
                        DisplayBox.Text = listFloat[0].ToString();
                        listFloat.Clear();
                    }
                    else
                    {
                        DisplayBox.Text = listFloat[0] + " " + sign + " ";
                    }
                }

                //If there are 2 values in listFloat
                if (listFloat.Count() == 2 && operation.checkDivision(listFloat, sign) == true)
                {
                    float total = operation.equalCalculate(listFloat, sign);                            //Call calculate me6thoid
                    DisplayBox.Text = listFloat[0] + " " + sign + " " + listFloat[1] + " = " + total;   //Display correct operation in displayBox
                    inputBox.Text = total.ToString();                                                   //Display the result in inputBox
                    listFloat.Clear();                                                                  //Clear listFloat


                }
                

                // if the operation is x / 0, display correct error
                if (listFloat.Count() == 2 && operation.checkDivision(listFloat, sign) == false)
                {
                    storage.lockDown("÷", listBtnNum, listBtnSigns, DisplayBox, inputBox);
                }
            
        }


        //When Numbetr button is clicked, pass inputText.Text thru checkDecimal(), and display output
        private void numButton_Click(object sender, EventArgs e)
        {

            inputBox.Text += operation.checkDecimal((sender as Button).Text, inputBox.Text);
       }

        //When memory button is clicked, pass inputBox.Text thru UseMemory(), and return output
        private void memoryButton_Click(object sender, EventArgs e)
        {
            inputBox.Text = storage.UseMemory((sender as Button).Text, inputBox.Text);

            if (operation.checkChar(inputBox.Text) == true)                                             //If there are letters in inputBox,
            {                                                                                           // Display correct error
                storage.lockDown(" ", listBtnNum, listBtnSigns, DisplayBox, inputBox);
            }
        }

        
        private void BtnClear_Click(object sender, EventArgs e)
        {
            //When number button is pressed
            switch ((sender as Button).Text)
            {
                case "DEL":
                    inputBox.Text = storage.DeleteChar(inputBox.Text);                              // If "DEL" is pressed, pass inputBox.Text to DeleteChar() AND RETURN OUTPUT
                    break;

                case "AC":
                    storage.AllClear(inputBox, DisplayBox, listBtnNum, listBtnSigns, listFloat);   // If "AC" is pressed, pass inputBox.Text to AllClear() AND RETURN OUTPUT
                    break;
            }
        }

        //Method for creation of number buttons
        private void createlistBtnSigns()
        {
            Button operatorButton;
            int i = 0;

            //This for loop is here to create 5 new buttons which text is the respective operator sign(+,-,x,÷)
            for (int j = 0; j < 5; j++)
            {
                operatorButton = new Button();                                          //Create new operator buttons

                operatorButton.Size = new Size(75, 31);                                 //Set button size
                operatorButton.Name = j.ToString();                                     //Set button name to variable j
                operatorButton.Location = new Point(301, i * 37 + 182);                 //Set Buttons location
                operatorButton.Click += new EventHandler(operatorButton_Click);         //Add new EventHandler for operation button
                operatorButton.Font = new Font(operatorButton.Font.FontFamily, 15);     // Set font size to 20 as the original font size was too small
                listBtnSigns.Add(operatorButton);                                       //Add operator button to listBtnSigns


                //Display correct text for each button  respectively 
                //I used switch vs If/Else to set text of the buttons here and in following methods for creating buttons
                //as switch is more efficient than if/ELse and it improved clarity of code                
                switch (j)
                {
                    case 0:
                        operatorButton.Text = "+";

                        break;

                    case 1:
                        operatorButton.Text = "-";

                        break;

                    case 2:
                        operatorButton.Text = "x";
                        break;

                    case 3:
                        operatorButton.Text = "÷";
                        break;

                    case 4:
                        operatorButton.Location = new Point(301, 330);
                        operatorButton.Text = "=";
                        break;



                }

                i++;
            }

            //Add each button as controls to Form1
            foreach (Button b in listBtnSigns)                              
            {
                this.Controls.AddRange(new Button[] { b });


            }

        }     

        //Method for the creation of number Buttons
        private void createListBtnNum()
        {
            Button numButton;
            int gap = 3;
            int i = 0;
            int x = 22;
            int y = 182;

            //This for loop is create initialise 10 number buttons which text is 0 to 9
            //It also creates the"." button
            for (int j = 1; j < 12; j++)
            {
                switch (j)
                {
                    case 4:
                        y = 219;
                        x = 22;
                        i = 0;
                        break;

                    case 7:
                        y = 256;
                        x = 22;
                        i = 0;
                        break;

                    case 10:
                        y = 293;
                        x = 115;
                        i = 0;
                        break;
                }

                numButton = new Button();                               //Initialise new button for numButton
                numButton.Location = new Point(x + i * 31, y);          //Set location of each numButton respectively
                numButton.Size = new Size(75, 31);                      //Set size of numButton
                numButton.Name = j.ToString();                          //Set name and text of button
                numButton.Text = j.ToString();
                numButton.Font = new Font(numButton.Font.FontFamily, 15); // Set font size to 15
                numButton.Click += new EventHandler(numButton_Click);   //Add new EventHandler for number button


                //For "0" and "." buttons, set their text and name respectively
                switch (j)
                {
                    case 10:
                        numButton.Text = "0";
                        numButton.Name = "0";
                        break;

                    case 11:
                        numButton.Text = ".";
                        numButton.Name = ".";
                        break;
                }
                listBtnNum.Add(numButton);
                i = i + gap;
            }

            //Add each number button as controls to Form1
            foreach (Button b in listBtnNum)
            {
                this.Controls.AddRange(new Button[] { b });
            }



        }

        //Method for creation of memory buttons
        private void createListBtnMemory()
        {
            Button memoryButton;
            int gap = 3;
            int i = 0;
            int x = 22;
            int y = 145;

            
            //This for loop is create initialise 3 memory buttons which text is M,M+,M-,MC
            for (int j = 0; j < 4; j++)
            {


                //Set variables of memory buttons
                memoryButton = new Button();
                memoryButton.Location = new Point(x + i * 31, y);
                memoryButton.Size = new Size(75, 31);
                memoryButton.Font = new Font(memoryButton.Font.FontFamily, 15);     // Set font size to 20
                memoryButton.Click += new EventHandler(memoryButton_Click);

                //Set text and name of each memory button respectively
                switch (j)
                {
                    case 0:
                        memoryButton.Text = "M+";
                        memoryButton.Name = "memoryAdd";
                        break;

                    case 1:
                        memoryButton.Text = "M-";
                        memoryButton.Name = "memorySubtract";
                        break;

                    case 2:
                        memoryButton.Text = "M";
                        memoryButton.Name = "memoryDisplay";
                        break;

                    case 3:
                        memoryButton.Text = "MC";
                        memoryButton.Name = "memoryClear";
                        break;



                }

                listBtnMemory.Add(memoryButton);
                i = i + gap;
            }

            //Add each memory button to From1 As controls
            foreach (Button b in listBtnMemory)
            {
                this.Controls.AddRange(new Button[] { b });
            }

        }



        //Method for creating textboxes
        private void createTextbox()
        {
            //Set variables of inputBoxe
            inputBox.Location = new Point(22, 81);
            inputBox.Size = new Size(354, 21);
            inputBox.ReadOnly = true;
            inputBox.Font = new Font(inputBox.Font.FontFamily, 25);

            //Set varaibles for displayBox
            DisplayBox.Location = new Point(22, 46);
            DisplayBox.Size = new Size(354, 34);
            DisplayBox.ReadOnly = true;
            DisplayBox.Font = new Font(DisplayBox.Font.FontFamily, 20);
            DisplayBox.TextAlign = HorizontalAlignment.Right;

            //Add both textboxes to form1
            this.Controls.Add(inputBox);
            this.Controls.Add(DisplayBox);
        }
        
        //Method for creating clear buttons
        public void createListBtnClear()
        {
            Button BtnClear;
            

            for (int j = 0; j < 2; j++)
            {
                //Set variables of each clear button
                BtnClear = new Button();
                BtnClear.Size = new Size(75, 31);
                BtnClear.Name = j.ToString();
                BtnClear.Font = new Font(BtnClear.Font.FontFamily, 15);     // Set font size to 20
                BtnClear.Click += new EventHandler(BtnClear_Click);


                //Set location and text of each clear button respectively
                switch(j)
                {
                    case 0:
                        BtnClear.Location = new Point(115, 330);
                        BtnClear.Text = "DEL";
                        break;

                    case 1: 
                        BtnClear.Location = new Point(208, 330);
                        BtnClear.Text = "AC";
                        break;

                }
                listBtnClear.Add(BtnClear);
            }

            //Add each clear button to Form1 as controls
            foreach (Button b in listBtnClear)
            {
                this.Controls.AddRange(new Button[] { b });
            }




        }
    }
}



