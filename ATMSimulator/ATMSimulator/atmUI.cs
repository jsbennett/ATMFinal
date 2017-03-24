using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;


/*
 * Jay Bennett, Matriculation Number: 150005948
 * Marcus Robertson-Jones, Matriculation Number: 150010807
 */
namespace ATMSimulator
{
    /*
     * This class is used to control the user interface of the ATM
     */
    public partial class atmUI : Form
    {
        
        Button[,] keypad = new Button[3, 4]; //create an array of buttons to be the key pa
        bool enterPressed = false; //boolean to determin whether the enter button has been pressed 
        String input; //string used to hold the input from the key pad
        bool pinBeingEntered = false; //used to determine whether the input is required to be recorded or not 
        public Account currentAccount = new Account(000,0000,000000,""); //used to store the current account
        bool displayMainMenu = false; //boolean used to determine the functionality of buttons when the main menu is displayed 
        bool removeMoneyFromAccount = false;  //boolean used to determin the functionality of the buttons when the user is wanting to take money out of their account 
        bool displayBalance = false; //boolean used to determine the functionality of the button when the user is displaying their balance
        bool semaphoreOn = false; //boolean used to determine whether to use the semaphore fix for the data race problem
        int atmID = 0; //used to store the id of the atm
        System.Media.SoundPlayer clickedButton = new System.Media.SoundPlayer(@"buttonClick.wav"); //create a new soundplayer object for the sound effect when a user clicks on a button
        mainMenu home = new mainMenu();//create a main menu object 
        Receipt receipt = new Receipt(); //create a receipt object 

        /*
         * The constructor for the ATM User interface 
         */
        public atmUI()
        {
            InitializeComponent();
            makeKeypad(); //call the function to make the keypad of the ATM
            lstMenu.Items.Clear(); //clear the list box 
            clearButton.Enabled = true;
            clearButton.Visible = true;
            //add text to the list box 
            lstMenu.Items.Add("Please enter the account number.");
            lstMenu.Items.Add("This is done by clicking on one of the credit cards");
            lstMenu.Items.Add("shown on the right hand of the screen");
            
            //disable the option buttons 
            option1.Enabled = false;
            option2.Enabled = false;
            option3.Enabled = false; 
            option4.Enabled = false;
            option5.Enabled = false;
            option6.Enabled = false;
            option7.Enabled = false;
            option8.Enabled = false;

            checkBlocked();
        }

        /*
         * This function is used to create the keypad of the ATM  
         */
        private void makeKeypad()
        {
            int counter = 0; //create a counter variable used to number the buttons accordingly 
            for (int i = 0; i < keypad.GetLength(0); i++) //for the columns 
            {
                for (int j = 0; j < keypad.GetLength(1); j++) //for the rows 
                {
                    
                    keypad[i, j] = new Button(); //create the new button
                    keypad[i, j].SetBounds(250 + 70 * i,440 + 70 * j, 70, 70); //set the sizes of the buttons
                    keypad[i,j].Name = Convert.ToString((i) + "," + (j)); //set the name of the button 
 
                    if(j < 3) //if the value in row is less than 3 then 
                    {
                        keypad[i, j].MouseUp += new MouseEventHandler(this.keypadNumberEvent_Click); //set the event handler for the button to be the one for numbers 
                    }
                    else //if the value of j is 3 then 
                    {
                        if(i == 0) //if its the first column then its the cancel button 
                        {
                            keypad[i, j].MouseUp += new MouseEventHandler(this.keypadCancelEvent_Click); //set the event handler for the button to be the one for cancel button 
                        }
                        else if(i == 1) //if its the second column then its the number 0 
                        {
                            keypad[i, j].MouseUp += new MouseEventHandler(this.keypadNumberEvent_Click); //set the event handler to be a number one 
                        }
                        else //if its the last column then its the enter button 
                        {
                            keypad[i, j].MouseUp += new MouseEventHandler(this.keypadEnterEvent_Click); //set the event handler to be the the one for the enter button
                            
                        }
                    }
                    Controls.Add(keypad[i, j]); //add the buttons to the GUI
                }

            }
            
            for (int i = 0; i < keypad.GetLength(1); i++) //for the columns 
            {
                for (int j = 0; j < keypad.GetLength(0); j++) //for the rows 
                {
                    counter++; //increment the counter 
                    keypad[j, i].Text = "" + counter; //set the text to be the value of the counter 
                    keypad[j, i].Tag = "" + counter; //set the counter to be the tag of the button 
                    }
               }

           
        keypad[0,3].Text = "Cancel"; //set the text of the cancel button to say cancel 
        keypad[0, 3].Tag = "Cancel"; //set the tag of the button to say cancel 
        keypad[1,3].Text = "0"; //set the text of the 0 button to be 0 
        keypad[1, 3].Tag = 0;  //set the tag of the buttonn to 0 
        keypad[2,3].Text = "Enter"; //set the text of the enter button to say enter 
        keypad[2, 3].Tag = "Enter"; //set the tag of the enter button to say enter 
        
        //Adding images to buttons
        keypad[0, 0].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton1;
        keypad[0, 0].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[1, 0].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton2;
        keypad[1, 0].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[2, 0].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton3;
        keypad[2, 0].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[0, 1].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton4;
        keypad[0, 1].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[1, 1].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton5;
        keypad[1, 1].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[2, 1].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton6;
        keypad[2, 1].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[0, 2].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton7;
        keypad[0, 2].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[1, 2].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton8;
        keypad[1, 2].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[2, 2].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton9;
        keypad[2, 2].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[0, 3].BackgroundImage = ATMSimulator.Properties.Resources.AtmButtonCancel;
        keypad[0, 3].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[2, 3].BackgroundImage = ATMSimulator.Properties.Resources.AtmButtonEnter;
        keypad[2, 3].BackgroundImageLayout = ImageLayout.Stretch;
        keypad[1, 3].BackgroundImage = ATMSimulator.Properties.Resources.AtmButton0;
        keypad[1, 3].BackgroundImageLayout = ImageLayout.Stretch;
        }

        /*
         * This function is the event handler if the button is a number 
         */
        private void keypadNumberEvent_Click(object sender, MouseEventArgs e)
        {
            string temp;
            clickedButton.Play(); //play sound
            int numberPressed = Convert.ToInt32(((Button)sender).Tag); //store the number in the tag of that button
            
            if (pinBeingEntered == true) //if the player is required to enter the pin then 
            {
                pinLabel.Visible = true; 
                input = input + ((Button)sender).Tag; //add the value entered to the value of the iput 
                pinLabel.Text = pinLabel.Text + "*";

                temp = pinLabel.Text;
                if (temp.Length == 4) //when the length of the input variable is four then 
                {

                    pinLabel.Text = "****";
                        
                        
                        Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": pin was entered correctly."); //update the bank log
                        Console.WriteLine("");
                        this.Refresh(); 
                        Thread.Sleep(500);  
                        pinLabel.Visible = false;
                        pinLabel.Text = "";
                        for (int i = 0; i < keypad.GetLength(1); i++) //for the columns 
                        {
                            for (int j = 0; j < keypad.GetLength(0); j++) //for the rows 
                            {
                                keypad[j, i].Enabled = false; //disable the keypad 
                            }
                        }
                        pinBeingEntered = false; //set the pin being entered to false so it ignores any input from the user 
                        keypad[0, 3].Enabled = true; //allow cancel to be displayed 
                        Boolean pinCorrect = checkPin(input); //check the pin - true if the pin is correct and false if the pin is incorrect 

                        if (pinCorrect == true) //if the pin is correct then 
                        {
                            clearButton.Enabled = false; 
                            displayMenu(); //displays the main menu for the account 
                        
                        }
                        else //if the pin is not correct then 
                        {
                             //increments number of attemtps made to enter pin
                            currentAccount.incrementAttempts(atmID);
                            
                            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": pin was entered incorrectly"); //update the bank log
                            Console.WriteLine("");
                            lstMenu.Items.Clear();
                            lstMenu.Items.Add("Pin incorrect. You have " + (3 -currentAccount.getAttempts()) +" attempt(s) remaining."); // tells user how many attmepts they have left
                            if (currentAccount.getBlocked() == true)
                            {
                                lstMenu.Items.Add("Account " + currentAccount.getAccountNumber() + " has been blocked."); // tells user account has been blocked if attempt >= 3
                            }
                            lstMenu.Items.Add("Please enter the account number.");
                            lstMenu.Items.Add("This is done by clicking on one of the credit cards");
                            lstMenu.Items.Add("shown on the right hand of the screen");
                          
                            input = "";
                            for (int i = 0; i < keypad.GetLength(1); i++) //for the columns 
                            {
                                for (int j = 0; j < keypad.GetLength(0); j++) //for the rows 
                                {
                                    keypad[j, i].Enabled = true; //display the keypad 
                                }
                            }
                            currentAccount = null; //remove the current account 
                            
                            account1Button.Enabled = true; //display account 1 button
                            account2Button.Enabled = true; //display account 2 button
                            account3Button.Enabled = true; //display account 3 button
                            pinBeingEntered = false; //set the variable to false so it ignores the input from the user if they press the keypad 
                            checkBlocked();
                        }
                    }
            }
            //return numberPressed; //return the value of the button pressed 
        }

        /*
         * This function is used to set the atm id for the atm in use 
         */
        public void setAtmId(int counter)
        {
            atmID = counter; //set the atmID to the value stored in counter 
        }
        /*
         * This method is used to check the pin that the user has entered with the pin that is associated with the current account
         */
        private Boolean checkPin(String input)
        {
            if(input.Equals(Convert.ToString(currentAccount.getPin()))) //if the users input is the same as the pin for the current account then 
            {
                return true; //return true 
            }
            else //if the pin entered is incorrect then 
            {
                return false; //return false
            }
        }

        /*
         * This function is used to display the main menu to the user 
         */
        private void displayMenu()
        {
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": displayed Main Menu"); //update the bank log
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": current balance = £" + currentAccount.getBalance()); //update the bank log
            Console.WriteLine("");
            lstMenu.Items.Clear(); //clear the list box
            keypad[2, 3].Enabled = false; //disable the enter button 

            //display the menu 
            lstMenu.Items.Add("Main Menu ");
            lstMenu.Items.Add("Welcome " + currentAccount.getName());
            lstMenu.Items.Add("Please choose an option below by clicking on the button next to the option.");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("1) Take money from account");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("2) Check account balance");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("3)return card");            
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");

            //enable the option buttons 
            option1.Enabled = true;
            option2.Enabled = true;
            option3.Enabled = true; 
            
            //disable the option buttons
            option4.Enabled = false;
            option5.Enabled = false;
            option6.Enabled = false;
            option7.Enabled = false;
            option8.Enabled = false; 
            
            displayMainMenu = true; //set the display menu boolean to true as the menu is being displayed 
   }

        /*
         * This function is used to display the money options to remove money from the account 
         */
        private void displayRemoveMoneyMenu()
        {
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": displayed cash widthdraw menu"); //update the bank log
            lstMenu.Items.Clear(); //clear the list box 
            //display the withdraw menu to the user 
            lstMenu.Items.Add("Please select an amount to withdraw");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("     5                                                                                                                                              10");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("     20                                                                                                                                             40");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("     50                                                                                                                                           100");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("     250                                                                                                                                         500");
            lstMenu.Items.Add("");
            lstMenu.Items.Add("-----------------------------------------------------------------------------------------------------------------------");
            
            //enable the option buttons 
            option1.Enabled = true;
            option2.Enabled = true;
            option3.Enabled = true; 
            option4.Enabled = true;
            option5.Enabled = true;
            option6.Enabled = true;
            option7.Enabled = true;
            option8.Enabled = true; 
        }

        /*
         * This function is used to display the balance of the account 
         */
        private void displayAccountBalance()
        {
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": displayed account balance"); //update the bank log
            Console.WriteLine("Time: " + DateTime.Now +  " - ATM " + atmID + ": current balance = " + currentAccount.getBalance()); 
            Console.WriteLine("");
            lstMenu.Items.Clear(); //clear the list box 
            displayBalance = true; //set the display balance boolean to true 
            //display the balance in the list box 
            lstMenu.Items.Add("Account balance = £" + currentAccount.getBalance());
            lstMenu.Items.Add("To return to the main menu press the enter button.");
            
            keypad[2, 3].Enabled = true; //enable the enter button 
            
            //disable the option buttons 
            option1.Enabled = false;
            option2.Enabled = false;
            option3.Enabled = false;
            option4.Enabled = false;
            option5.Enabled = false;
            option6.Enabled = false;
            option7.Enabled = false;
            option8.Enabled = false;
        }
        /*
         * This fucniton is used to return the card 
         */
        private void returnCard()
        {
            lstMenu.Items.Clear(); //clear the list box 
            Console.WriteLine("Time: " + DateTime.Now +  " - ATM " + atmID + ": returned card for account number " + currentAccount.getAccountNumber()); //update the bank log
            Console.WriteLine("");
            //display the first menu on the list box 
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": returned to the start screen"); //update the bank log
            lstMenu.Items.Add("Please enter the account number.");
            lstMenu.Items.Add("This is done by clicking on one of the credit cards");
            lstMenu.Items.Add("shown on the right hand of the screen"); input = ""; //clear the input 
            clearButton.Enabled = true; 

            for (int i = 0; i < keypad.GetLength(1); i++) //for the columns 
            {
                for (int j = 0; j < keypad.GetLength(0); j++) //for the rows 
                {
                    keypad[j, i].Enabled = true; //display the keypad 
                }
            }
            currentAccount = null; //set the current account to nothing 
            account1Button.Enabled = true; //display account 1 
            account2Button.Enabled = true; //display account 2
            account3Button.Enabled = true; //display account 3
            pinBeingEntered = false; //set the pin bveing entered to false so it doesnt record input 

            //disable the option buttons 
            option1.Enabled = false;
            option2.Enabled = false;
            option3.Enabled = false;
            option4.Enabled = false;
            option5.Enabled = false;
            option6.Enabled = false;
            option7.Enabled = false;
            option8.Enabled = false; 
           
        }

        /*
        * This function is the event handler if the button is the cancel button 
        */
       private void keypadCancelEvent_Click(object sender, MouseEventArgs e)
        {
            clickedButton.Play(); //play sound
           Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": clicked cancel button"); //update the bank log
            Console.WriteLine("");
           returnCard(); //call the return card method 
        }

        /*
         * 
         */
        public void turnOnSemaphore(Boolean value)
       {
           semaphoreOn = value; //change the value of the semaphoreOn boolean
       }

       /*
       * This function is the event handler if the button is the enter button 
       */
        private void keypadEnterEvent_Click(object sender, MouseEventArgs e)
        {
           
            //submit the input 
            if(displayBalance == true || enterPressed == true) //if the balance or the enter boolean is true then 
            {
                clickedButton.Play(); //play sound
                Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": clicked enter button"); //update the bank log
                Console.WriteLine("");
                enterPressed = false; //set the enter boolean to false 
                displayBalance = false; //set the balance boolean to false 
                displayMenu(); //display the main menu 
            }
        }

        /*
         * This function is used to take money from the account 
         */
        public Boolean removeBalance(int money)
        {

            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": wants to remove " + money + " from account " + currentAccount.getAccountNumber()); //update the bank log
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": current balance = " + currentAccount.getBalance()); 
            Console.WriteLine("");
            if (currentAccount.deductBalance(money) == true) //check if the money can be taken out of the account and if so then 
            {
                if(semaphoreOn == true) //if a semaphore is being used to fix the data race problem then 
                {

                    BankMainComputer.semaphore.WaitOne();
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": passed semaphore wait"); //update the bank log
                    Console.WriteLine("");
                    int balance = currentAccount.getBalance(); //store the balance of the account
                    balance = balance - money; //deduct this amount of money from the balance 
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": thread asleep"); //update the bank log
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": current balance = " + currentAccount.getBalance()); 
                    Console.WriteLine("");
                    Thread.Sleep(5000); //sleep the current thread for 5 seconds 
                    currentAccount.setBalance(balance); //set the balance to be the new balance 
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": removed " + money + " from account " + currentAccount.getAccountNumber()); //update the bank log
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": current balance = " + currentAccount.getBalance()); 
                    Console.WriteLine("");
                    BankMainComputer.semaphore.Release();
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": exit semaphore"); //update the bank log
                    Console.WriteLine("");
                    receipt.displayReceipt(currentAccount, money); //get the receipt
                    return true; //return true
                }
                else //if the semaphote is not being used and is simulating the data race then 
                {
                    int balance = currentAccount.getBalance(); //store the balance of the account
                    balance = balance - money; //deduct this amount of money from the balance 
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": thread asleep"); //update the bank log
                    Console.WriteLine("");
                    Thread.Sleep(5000); //sleep the current thread for 5 seconds 
                    currentAccount.setBalance(balance); //set the balance to be the new balance 
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": removed " + money + " from account " + currentAccount.getAccountNumber()); //update the bank log
                    Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": current balance = " + currentAccount.getBalance()); 
                    Console.WriteLine("");
                    receipt.displayReceipt(currentAccount, money); //get the receipt
                    return true; //return true
                }
               
            }
            else //if you cant take the money out as there is not enough money then 
            {
                return false; //return false 
            }
        }

        /*
         * This function is to load the account number for account 1 
         */
        private void account1_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": selected account 1"); //update the bank log
            Console.WriteLine("");
            currentAccount = BankMainComputer.accounts[0]; //set the current account to be account 1 
            ((Button)sender).Enabled = false; //unenable the button so the user cant click on it 
            account2Button.Enabled = false; //unenable the button so the user cant click on it
            account3Button.Enabled = false;  //unenable the button so the user cant click on it
            lstMenu.Items.Clear(); //clear the list box 
            lstMenu.Items.Add("Please enter your pin"); //ask the user to enter their pin 
            lstMenu.Items.Add("This is done by using the keypad below.");
            pinBeingEntered = true; //set pin being entered to true so it records the users pin 
          
        }

        /*
        * This function is to load the account number for account 2 
        */
        private void button10_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": selected account 2"); //update the bank log
            Console.WriteLine("");
            currentAccount = BankMainComputer.accounts[1]; //set the current account to be account 1 
            ((Button)sender).Enabled = false; //unenable the button so the user cant click on it
            account1Button.Enabled = false; //unenable the button so the user cant click on it
            account3Button.Enabled = false; //unenable the button so the user cant click on it
            lstMenu.Items.Clear(); //clear the list box 
            lstMenu.Items.Add("Please enter your pin"); //ask the user to enter their pin 
            lstMenu.Items.Add("This is done by using the keypad below.");
            pinBeingEntered = true; //set pin being entered to true so it records the users pin 
             
        }

        /*
        * This function is to load the account number for account 3
        */
        private void account3_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": selected account 3"); //update the bank log
            Console.WriteLine("");
            currentAccount = BankMainComputer.accounts[2]; //set the current account to be account 1 
            ((Button)sender).Enabled = false; //unenable the button so the user cant click on it
            account1Button.Enabled = false; //unenable the button so the user cant click on it
            account2Button.Enabled = false; //unenable the button so the user cant click on it
            lstMenu.Items.Clear(); //clear the list box 
            lstMenu.Items.Add("Please enter your pin"); //ask the user to enter their pin 
            lstMenu.Items.Add("This is done by using the keypad below.");
            pinBeingEntered = true; //set pin being entered to true so it records the users pin 
        }

        /*
         * This function is used to control the action of the option 1 button depending on the menu that is shown to the user 
         */
        private void option1_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if(displayMainMenu == true) //if the main menu is being displayed then 
            {
                displayMainMenu = false; //set the boolean value to false 
                displayRemoveMoneyMenu(); //call the method used to display the money menu 
                removeMoneyFromAccount = true;
                
            }
            else if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(5) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £5");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                }
            }
        }

        /*
         * This function is used to control the action of the option 2 button depending on the menu that is shown to the user 
         */
        private void option2_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (displayMainMenu == true) //if the main menu is being displayed then 
            {
                displayMainMenu = false; //set the boolean value to false 
                displayAccountBalance(); //call the display account balance method  
            }
            else if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                if (removeBalance(20) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £20");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                }
            }
        }

        /*
         * This function is used to control the action of the option 3 button depending on the menu that is shown to the user 
         */
        private void option3_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (displayMainMenu == true) //if the main menu is being displayed then 
            {
                displayMainMenu = false; //set the boolean value to false 
                returnCard(); //call the return card method  
            }
            else if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(50) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £50");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                }
            }
        }

        /*
         * This function is used to control the action of the option 4 button depending on the menu that is shown to the user 
         */
        private void option4_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(250) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £250");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                }
            }
        }

        /*
         * This function is used to control the action of the option 5 button depending on the menu that is shown to the user 
         */
        private void option5_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(10) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £10");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                }
            }
        }

        /*
         * This function is used to control the action of the option 6 button depending on the menu that is shown to the user 
         */
        private void option6_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(40) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £40");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                }
            }
        }

        /*
         * This function is used to control the action of the option 7 button depending on the menu that is shown to the user 
         */
        private void option7_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(100) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = 100");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                    
                }
            }
        }

        /*
         * This function is used to control the action of the option 8 button depending on the menu that is shown to the user 
         */
        private void option8_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            if (removeMoneyFromAccount == true) //if the screen asking the user to remove money from their account is displayed then 
            {
                removeMoneyFromAccount = false; //set the boolean value to false 
                if (removeBalance(500) == true)
                {
                    lstMenu.Items.Clear();
                    lstMenu.Items.Add("Amount removed = £500");
                    lstMenu.Items.Add("Account Balance = £" + currentAccount.getBalance());
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    enterPressed = true;
                    keypad[2, 3].Enabled = true;
                }
                else
                {
                    lstMenu.Items.Clear();
                    enterPressed = true;
                    lstMenu.Items.Add("You do not have enough funds to remove the amount selected");
                    lstMenu.Items.Add("To return to the main menu press the enter button.");
                    keypad[2, 3].Enabled = true;
                    
                }
            }
        }

        /*
         * This function is for the clear button when the pin is being entered 
         */
        private void clearButton_Click(object sender, EventArgs e)
        {
            if(pinBeingEntered == true) //if the pin is being entered then 
            {
                input = ""; //clear the input
                pinLabel.Text = " "; //clear the pin label
            }
        }

        /*
         * This function is used to return to the main menu 
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clickedButton.Play(); //play sound
            this.Visible = false; //hide the current form 
            home.Visible = true; //show the main menu
            if(semaphoreOn == true)
            {
                Console.WriteLine("Time: " + DateTime.Now + " - " + atmID + ": ------- Data Race Fix Simulation Finished -------"); //update the bank log
                Console.WriteLine("");
            }
            else
            {
                Console.WriteLine("Time: " + DateTime.Now + " - ATM " + atmID + ": ------- Data Race Simulation Finished -------"); //update the bank log
                Console.WriteLine("");
            }
            turnOnSemaphore(false); //set the semaphore boolean to false 
            BankMainComputer.counter = 0; //set the counter back to 0 
        }

        /*
         *  This function checks to see if any of the cards are blocked and disables the buttons for each card if they are.
         */
        public void checkBlocked()
        {
            if (BankMainComputer.accounts[0].getBlocked() == true)
            {
                account1Button.Enabled = false;
                account1Button.BackgroundImage = ATMSimulator.Properties.Resources.CreditCard1Blocked;
              
            }
            if (BankMainComputer.accounts[1].getBlocked() == true)
            {
                account2Button.Enabled = false;
                account2Button.BackgroundImage = ATMSimulator.Properties.Resources.CreditCard2Blocked;
            }
            if (BankMainComputer.accounts[2].getBlocked() == true)
            {
                account3Button.Enabled = false;
                account3Button.BackgroundImage = ATMSimulator.Properties.Resources.CreditCard3Blocked;
            }

        }

        /*
         * This function is used to display the instructions to the user when they select the option from the menu bar
         */
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            home.displayInstructions(); //call the function to display the instructions 
        }

        private void atmScreen_TextChanged(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void atmUI_Load(object sender, EventArgs e)
        {

        }

        private void pinLabel_Click(object sender, EventArgs e)
        {

        }

        private void lstMenu_SelectedIndexChanged(object sender, EventArgs e)
        {

        }



      


    }
}
