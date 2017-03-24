using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * Jay Bennett, Matriculation Number: 150005948
 * Marcus Robertson-Jones, Matriculation Number: 150010807
 */
namespace ATMSimulator
{
    /*
     * This class is used for the main menu
     * This is the inital screen displayed to the user 
     */
    public partial class mainMenu : Form
    {
        public mainMenu()
        {
            InitializeComponent();
        }

        /* 
         * This function is used when the exit button is clicked 
         */
        private void exitButton_Click(object sender, EventArgs e)
        {
            exit(); //call the exit function
        }


        /*
         * This function is used when the exit button in the menu strip is clicked 
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exit(); //call the exit function
        }

        /*
        * This function is used to exit the ATM Simulator 
        */
        public void exit()
        {
            DialogResult leave = MessageBox.Show("Do you wish to Exit the ATM Simulator?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning); //ask the user if they want to leave the ATM simulator and store the result in variable called leave
            if (leave == DialogResult.Yes) //if the user want to leave the ATM Simulator then
            {
                Application.Exit(); //close the ATM Simulator
            }
        }

        /*
         * This function is used when the about button is pressed 
         */
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String about = "ATM Simulator is a simulator showcasing two scenarios of ATM use. It was developed using C# through Visual Studio 2013. \nOne scenario of ATM use is when a data race occurs. This is when the data is not updated fast enough for changes to be made to the data, causing the final product of the data to be incorrect.\nAnother scenario is when the data race does not occur from the use of semaphores, allowing changes to be applied accordingly, causing the final product of the data to be correct. \n\nButton click sound: \nComputer Error Alert Sound(https://soundbible.com/1540-Computer-Error-Alert.html)\nLicensed under Attribution 3.0 of the Creative Commons(https://creativecommons.org/licenses/by/3.0/)  \n\nImages Used: \nMan with raining money(http://www.clipartkid.com/images/413/three-ways-to-profit-using-call-options-pM88qq-clipart.jpg)\nKeypad(https://i.stack.imgur.com/tlGsL.jpg)\nAccount 1 credit card(http://combiboilersleeds.com/image.php?pic=/images/credit-card/credit-card-9.jpg)\nAccount 2 credit card(https://www.google.co.uk/search?q=credit+card&source=lnms&tbm=isch&sa=X&ved=0ahUKEwjGm7S9mu_SAhUlC8AKHXbnCPkQ_AUIBigB&biw=1335&bih=888#imgrc=tF1_JB4da1C_NM:&spf=264)\nAccount 3 credit card(http://www.evonice.com/wp-content/uploads/2017/01/blank-credit-card-template-blank-credit-card-template-blank-credit-card-authorization-template-blank-credit-card-receipt-template-blank-credit-card-statement.jpg)"; //the about information
            MessageBox.Show(about, "About", MessageBoxButtons.OK, MessageBoxIcon.Information); //display message box
        }

        /*
         * This function is used when the data race button is pressed 
         */
        private void dataRaceButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Time: " + DateTime.Now + " ------- Data Race Simulation Initiated -------"); //update the bank log
            BankMainComputer.createThreadsDataRace(); //call the createThreadDataRace function to start the data race simulation 
        }

        /*
         * This function is used when the data race fix button is pressed 
         */
        private void nonDataRaceButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Time: " + DateTime.Now + " ------- Data Race Fix Simulation Initiated -------"); //update the bank log
            BankMainComputer.createThreadsNoDataRace(); //call the createThreadDataRace function to start the data race fix simulation
        }

        /*
         * This function is used to display the instructions to the user 
         */
        private void button2_Click(object sender, EventArgs e)
        {
            displayInstructions(); 
        }

        /*
         * This function is used to display a message box with instructions 
         */
        public void displayInstructions()
        {
            String instructions = "Welcome to ATM Simulator!\nTo begin the Data Race Simulator, click on the Data Race button on the main menu.\nTo begin the Data Race Fix Simulator, click on the Data Race Fix button on the main menu.\nTwo ATM screens will appear and the console\nThe console is used to display the bank log. As you perform actions on the ATM,\nthe log will be updated, showing the action performed and the time it was carried out at.\nOn the screen of the ATM, it instructs you to insert your card. In order to do this,\nclick on one of the credit cards at the side of the ATM.\nYou will then be prompted to enter the pin for the account you clicked on.\nYou do this by using the keypad on the screen.\nIf you want to clear the pin you have entered, click on the clear button.\nOnce you have entered four numbers, the pin is checked.\nIf it is incorrect you will be taken back to the start and will be asked to select the account.\nIf the password is correct, you will be taken to the main menu of the ATM.\n\nOn the main menu of the ATM, there are three options.\nOption 1 is removing money from the account. Click on the corresponding button on the left-hand\nside of the screen and you will be shown another screen with different values of money to deduct\nfrom your account. Click on the corresponding button.\nIf the value of money to be deducted is smaller than or equal to the value stored in the account then\nthe money will be taken from the account. The new balance of the account will be then shown on the screen.\nIf not, then money will not be taken from the account and you will be taken back to the\nmain menu of the ATM to select an option.  Click enter to be taken back to the main menu of the ATM.\nYou will also be shown a receipt of the transaction.\nOption 2 is used to show you the balance of the account. Click on the corresponding button on the\nleft-hand side of the screen and you will be shown another screen which displays\nthe balance of the account. Click enter to be taken back to the main menu of the ATM.\nOption 3 is used to return the card. Click on the corresponding button on the left-hand side of the\nscreen and you will be taken back to the first screen and asked to click\non a credit card to select an account.\nIf you wish to leave the simulation, click on the return to main menu button at the top of the ATM.\nIf during any of the option stages you wish to return to the first screen,\nclick the cancel button to do so.\n\nWhen the Data Race option has been selected, proceed through the menus and select the withdraw\noption on both ATMs. Select the same amount on both of the ATMs.\nThere is a five second delay once the button of the amount to withdraw has been clicked.\nThis is to allow you to click the button on the other ATM so the processes occur correctly.\nThe balance shown after the money has been removed, should be the same on both of the ATMS.\nFor example, if the account had £300 and the £10 option was selected on the ATMs, the balance in both ATMs should be £290\n\nWhen the Data Race Fix option has been selected, proceed through the menus and select the\nwithdraw option on both ATMs. Select the same amount on both of the ATMs\nThere is a five second delay once the button of the amount to withdraw has been clicked.\nThis is to allow you to click the button on the other ATM so the processes occur correctly.\nWith the fix in place, one of the ATMs will take longer to complete due to the semaphore.\nThe balance shown after the money has been removed, should be different on both of the ATMs.\nFor example, if the account had £300 and the £10 option was selected on the ATMs\nthe balance that is shown first in an ATM should be £290 and\nthen in the other ATM, the balance that is shown should be £280.\n\nIf the exit button is selected, this will display a box asking you to confirm whether you wish to exit.\nIf you click yes, the simulation will close.\nIf you select no you will stay on the main menu.  "; //used to hold the instructions
            MessageBox.Show(instructions, "Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information); //the message box to be displayed
        }

        /*
         * This function is used to diplay the instructions if the user clicks on the button in the menu bar 
         */
        private void instructionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            displayInstructions(); 
        }

        private void mainMenu_Load(object sender, EventArgs e)
        {

        }

        private void titleLabel_Click(object sender, EventArgs e)
        {

        }

        

   
    }
}