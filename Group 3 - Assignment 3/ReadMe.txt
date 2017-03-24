========================================================================
                    Instructions for ATM Simulator
========================================================================

More detailed instructions for the ATM Simulator:



To begin the Data Race Simulator, click on the Data Race button on the main menu. To begin the Data Race Fix Simulator, click on the Data Race Fix button on the main menu. 

Two ATM screens will appear and the console. The console is used to display the bank log. 
As you perform actions on the ATM, the log will be updated, showing the action performed and the time it was carried out at. 
On the screen of the ATM, it instructs you to insert your card. In order to do this, click on one of the credit cards at the side of the ATM. 
You will then be prompted to enter the pin for the account you clicked on. You do this by using the keypad on the screen. If you want to clear the pin you have entered, click on the clear button. 
Once you have entered four numbers, the pin is checked. If it is incorrect you will be taken back to the start and will be asked to select the account. 
If the password is correct, you will be taken to the main menu of the ATM. 

On the main menu of the ATM, there are three options. 
Option 1 is removing money from the account. Click on the corresponding button on the left-hand side of the screen and you will be shown another screen with different values of money to deduct from your account. 
Click on the corresponding button. If the value of money to be deducted is smaller than or equal to the value stored in the account then the money will be taken from the account. 
The new balance of the account will be then shown on the screen. If not, then money will not be taken from the account and you will be taken back to the main menu of the ATM to select an option. 
You will also be shown a receipt of the transaction. Click on the close button on the receipt to close it. Click enter to be taken back to the main menu of the ATM. 

Option 2 is used to show you the balance of the account. Click on the corresponding button on the left-hand side of the screen and you will be shown another screen which displays the balance of the account. 
Click enter to be taken back to the main menu of the ATM.
Option 3 is used to return the card. Click on the corresponding button on the left-hand side of the screen and you will be taken back to the first screen and asked to click on a credit card to select an account. 

If you wish to leave the simulation, click on the return to main menu button at the top of the ATM. If during any of the option stages you wish to return to the first screen, click the cancel button to do so. 

When the Data Race option has been selected, proceed through the menus and select the withdraw option on both ATMs. Select the same amount on both of the ATMs. 
There is a five second delay once the button of the amount to withdraw has been clicked. This is to allow you to click the button on the other ATM so the processes occur correctly. 
The balance shown after the money has been removed, should be the same on both of the ATMS. For example, if the account had £300 and the £10 option was selected on the ATMs, the balance in both ATMs should be £290. 

When the Data Race Fix option has been selected, proceed through the menus and select the withdraw option on both ATMs. Select the same amount on both of the ATMs.
There is a five second delay once the button of the amount to withdraw has been clicked. This is to allow you to click the button on the other ATM so the processes occur correctly. 
With the fix in place, one of the ATMs will take longer to complete due to the semaphore. The balance shown after the money has been removed, should be different on both of the ATMs. 
For example, if the account had £300 and the £10 option was selected on the ATMs, the balance that is shown first in an ATM should be £290 and then in the other ATM, the balance that is shown should be £280. 

If the exit button is selected, this will display a box asking you to confirm whether you wish to exit. If you click yes, the simulation will close. If you select no you will stay on the main menu. 