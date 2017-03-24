using System;
using System.Threading; 
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

/*
 * Jay Bennett, Matriculation Number: 150005948
 * Marcus Robertson-Jones, Matriculation Number: 150010807
 */
namespace ATMSimulator
{
    /*
     * This class is used as the main bank computer interface 
     */
     class BankMainComputer
    {
         public static Account[] accounts = new Account[3]; //an array to hold the accoounts 
         private static Thread atm1, atm2; //create two atm threads 
         public static Semaphore semaphore = new Semaphore(1, 1); //create a new semaphore 
         public static int counter = 0; 
         
         /*
          * The constructor for the bank computer
          * Fill in the account array with the three accounts 
          */
         public BankMainComputer()
         {
             accounts[0] = new Account(300, 1111, 111111, "Mr Gordon Earle Moore"); //account 1 
             accounts[1] = new Account(750, 2222, 222222, "Mr Bruce Wayne"); //account 2
             accounts[2] = new Account(3000, 3333, 333333, "President Donald Trump"); //account 3
             
         }
        
      
         /*
          *This function is the main function for the program 
          */
         static void Main()
         {
            Application.EnableVisualStyles(); 
            Application.SetCompatibleTextRenderingDefault(false);
            BankMainComputer bank = new BankMainComputer(); //initalise the bank 
            Application.Run(new mainMenu());
         }

         /*
          * This function is used to create two threads for the data race simulation 
          */
         public static void createThreadsDataRace()
         {
             atm1 = new Thread(new ThreadStart(dataRace)); //create a new thread for one atm 
             atm2 = new Thread(new ThreadStart(dataRace)); //create another thread for the second atm 
             //start the two atm threads 
             atm1.Start();
             atm2.Start(); 

             
         }

         /*
          * This function is used to create two threads for the data race fix simulation 
          */
         public static void createThreadsNoDataRace()
         {
             atm1 = new Thread(new ThreadStart(nonDataRace)); //create a new thread for one atm 
             atm2 = new Thread(new ThreadStart(nonDataRace)); //create another thread for the second atm 
             //start the two atm threads 
             atm1.Start();
             atm2.Start();
        
         }

         /*
          * This function is used to start the data race simulation 
          */
         public static void dataRace()
         {
             
             atmUI atm = new atmUI(); //create a new atm 
             counter++; //increment the counter
             atm.setAtmId(counter); //set the counter value
             atm.ShowDialog();  //display the atm
            
             
         }

         /*
          * This function is used to start the data race fix simulation 
          */
         public static void nonDataRace()
         {
             atmUI atm = new atmUI(); //create a new atm 
             atm.turnOnSemaphore(true); //set the semaphore boolean to true 
             counter++; //increment the counter
             atm.setAtmId(counter); //set the counter value
             atm.ShowDialog(); //display the atm
            
         }
    }
}

