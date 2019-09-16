using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.DataAccess;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreatePrizeForm : Form, IPrizeRequester
    {
        IPrizeRequester callingForm;
        public CreatePrizeForm(IPrizeRequester caller)
        {
            InitializeComponent();
            callingForm = caller;
        }

        public void PrizeComplete(PrizeModel model)
        {
            throw new NotImplementedException();
        }

        private void CreatePrizeButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            //!!!!If Validation returns true, declare a variable 'model' by an instantiation of the Class "PrizeModel"
            {
                PrizeModel model = new PrizeModel(
                    placeNameValue.Text,
                    placeNumberValue.Text,
                    prizeAmountValue.Text,
                    prizePercentageValue.Text
                );

                //!!!!Pass the object (instance) into the method 'CreatePrize' 
                //that was inherited by the 'Connection' Read-Write property from the 'IDataConnection' Interface

                GlobalConfig.Connection.CreatePrize(model);

                //Initialization of the PrizeForm after user inpunts being submitted
                callingForm.PrizeComplete(model);
                this.Close();
            }
            else
            {
                MessageBox.Show("This form has invalid information. Please, check it and try again.");
            }
        }

        //Validation Method to Validate Form Inputs
        private bool ValidateForm()
        {
            bool output = true;
            // Place Number of the Team: must be an integer type...
            
            bool placeNumberValidNumber = int.TryParse(placeNumberValue.Text, out int placeNumber);
            if (!placeNumberValidNumber)
            {
                output = false;
            }

            if (placeNumber < 1)
            {
                output = false;
            }
            // Place Name is the name of the team placement in the tournament: must be a string...
            if (placeNameValue.Text.Length == 0)
            {
                output = false;
            }

            // PrizeAmount is the money amount Team earns: must be a decimal...           
            // PrizePercentage is the percentage of the total amount: must be an integer...            

            bool prizeAmountValid = decimal.TryParse(prizeAmountValue.Text, out decimal prizeAmount);
            bool prizePercentageValid = double.TryParse(prizePercentageValue.Text, out double prizePercentage);

            if (!prizeAmountValid || !prizePercentageValid)
            {
                output = false;
            }

            if (prizeAmount <= 0 && prizePercentage <= 0)
            {
                output = false;
            }

            if (prizePercentage < 0 || prizePercentage > 100)
            {
                output = false;
            }
            return output;
        }
    }
}
