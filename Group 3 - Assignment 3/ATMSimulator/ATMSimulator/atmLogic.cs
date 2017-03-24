using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Jay Bennett, Matriculation Number: 150005948
 * Marcus Robertson-Jones, Matriculation Number: 
 */
namespace ATMSimulator
{
    class atmLogic
    {
        //private Account[] account = new Account[3]; //create an account array to hold the accounts 
        private Account account; //create an account 
        
        /*
         * This is the constructor for the ATM
         */
        public atmLogic()
        {
            //display first screen to the player on the atm 
            
        }

        /*
         * This method is used to begin the ATM 
         */
        public void beginATM(Account inputAccount)
        {

            account = inputAccount; //set the account to be the account selected by the player 
            String pin = Convert.ToString(account.getPin()); //store the pin for that account 
            //userInterface.enterPin(pin); //call the ask for pin method 
        }


        private void displayMenu()
        {

        }

        private void withdrawOption()
        {

        }

        private void displayBalanceOption()
        {

        }


    }
}
