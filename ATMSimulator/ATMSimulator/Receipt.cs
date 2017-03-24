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
     * This class is used to simulate a receipt from the atm
     */
    public partial class Receipt : Form
    {
        Account tempAccount; //a variable to hold the temp acount 
        public Receipt()
        {
            InitializeComponent();
        }

        public void displayReceipt(Account currentAccount, int money)
        {
            tempAccount = currentAccount;//set temp account to be the current account 
            //display the receipt 
            receiptDisplay.Items.Clear();
            receiptDisplay.Items.Add("YOU HAVE WITHDRAWN          £" + money);
            receiptDisplay.Items.Add("");
            receiptDisplay.Items.Add("AT ATM SIMULATOR            AS1881");
            receiptDisplay.Items.Add("");
            receiptDisplay.Items.Add("TRANSACTION REFERENCE:      001967");
            receiptDisplay.Items.Add("");
            receiptDisplay.Items.Add("");
            receiptDisplay.Items.Add("ACCOUNT       BALANCE       YOU CAN WITHDRAW");
            receiptDisplay.Items.Add("");
            int avaliableWithdrwal = avaliableWithdraw(); 
            receiptDisplay.Items.Add("" + currentAccount.getAccountNumber() + "         £" + currentAccount.getBalance() + "            " + "£" + avaliableWithdrwal);
            receiptDisplay.Items.Add("");
            receiptDisplay.Items.Add(DateTime.Now);
            receiptDisplay.Items.Add("");
            receiptDisplay.Items.Add("A0000010120");
            this.Visible = true;
        }

        /*
         * This method is used to calculate the avaliable withdraw value from the account since you cannot widthdraw values that are not whole
         */
        private int avaliableWithdraw()
        {
            int balance = tempAccount.getBalance(); //get the balance from the account 
           
            if (balance != 0 && balance % 5 != 0) //if the balance is not 0 andthe balance is not divisable by 5 then 
            {
                    balance = balance / 10; //divide balance by 10 
                    balance = balance * 10; //multiply balance by 10 
           
            }
            return balance; //return balance 
        }

        /*
         * This function is used to close the receipt when the user has finished with it 
         */
        private void okButton_Click(object sender, EventArgs e)
        {
            this.Visible = false; //hide the receipt 
        }

        private void receiptDisplay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        
        private void Receipt_Load(object sender, EventArgs e)
        {

        }
    }
}