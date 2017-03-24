using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
 * Jay Bennett, Matriculation Number: 150005948
 * Marcus Robertson-Jones, Matriculation Number: 150010807
 */
namespace ATMSimulator
{
    /*
     * This class is used as a bank account
     * This includes checking the pin, getting, setting and decreasing the balance and getting the account number 
     */
    public class Account
    {
        private int balance; //variable balance used to store the balance of the account 
        private int pin; //variable pin used to store the pin associated with the account 
        private int accountNumber; //variable accountNumber used to store the account number of the account 
        private String name; //vaiable name is used to hold the name of the account holder 
        private Boolean blocked = false;
        private int attempts = 0;

        /*
         * This is the constructor for the account class
         */
        public Account(int balance, int pin, int accountNumber, String name)
        {
            this.balance = balance; //set the balance of the account 
            this.pin = pin; //set the pin of the account 
            this.accountNumber = accountNumber; //set the account number of the account 
            this.name = name; //set the name of the account holder 
        }

        /*
         * This function is used to return the account number of the account 
         */
        public int getAccountNumber()
        {
            return accountNumber; //return the account number
        }

        /*
         * This function is used to return the pin of the account
         */
        public int getPin()
        {
            return pin; //return the pin 
        }

        /*
         * This function is used to return the name of the account holder 
         */
        public String getName()
        {
            return name; //return the name of the account holder 
        }

        /*
         * This function is used to get the balance of the account 
         */
        public int getBalance()
        {
            return balance; //return the balance
        }

        /*
         * This function is used to set the balance of the account 
         */
        public void setBalance(int enteredBalance)
        {
            this.balance = enteredBalance; //set the balace to be the balance that was entered 
        }
        /*
         * This function gets the blocked field 
         */
        public Boolean getBlocked()
        {
            return blocked;
        }
        /*
         * This function return the number of attempts
         */
        public int getAttempts()
        {
            return attempts;
        }

        /*
         * This function sets the value of blocked
         */
        public void setBlocked(Boolean block)
        {
            this.blocked = block;
        }

        /*
         * Increments the number of attempts to enter pin
         */
        public void incrementAttempts(int atmID)
        {
            attempts++;
            if(attempts >=3 && blocked == false)
            {
                blocked = true;
                Console.WriteLine("Time: " + DateTime.Now + " - " + atmID + ": Card with account number " + accountNumber + " has been blocked"); //update the bank log to say card was blocked
                Console.WriteLine("");
            }
            else if(attempts == 2)
            {
                Console.WriteLine("Time: " + DateTime.Now + " - " + atmID + ": Card with account number " + accountNumber + " has 1 attempt to enter the pin correctly left"); // update bank log with number of attempts left
                Console.WriteLine("");
            }
            else if (attempts == 1)
            {
                Console.WriteLine("Time: " + DateTime.Now + " - " + atmID + ": Card with account number " + accountNumber + " has 2 attempts to enter the pin correctly left"); // update bank log with number of attempts left
                Console.WriteLine("");
            }


        }


        /*
         * This function is used to check if the balance can be removed from an account 
         */
        public Boolean deductBalance(int value)
        {
            if(this.balance >= value) //if the balance has a value greater than or equal to the value to be removed from the account then 
            {
                return true; //return true 
            }
            else //if the value to be deducted is more than the balance of the account then
            {
                return false; //return false;
            }
        }
        
        /*
         * This function is used to check whether the pin that was entered was the correct pin
         */
        public Boolean checkInputPin(int enteredPin)
        {
            if(enteredPin == pin) //if the pin entered is the same as the pin associated with the account then 
            {
                return true; //return true 
            }
            else //if the pin entered is incorrect then 
            {
                return false; //return false 
            }
        }

        public void checkBlocked()
        {
            if (attempts >= 3)
            {
                blocked = true;
            }
        }
    }
}
