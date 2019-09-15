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
using TrackerLibrary.Models;

namespace TrackerUI
{
    
    public partial class CreateTeamForm : Form
    {
        /// <summary>
        /// Develop the two Lists of 'PersonModel' type;
        /// </summary>
        private List<PersonModel> availableTeamMembers = GlobalConfig.Connection.GetPerson_All();
        private List<PersonModel> selectedTeamMembers = new List<PersonModel>();
        private void WireUpLists()
        {
            selectTeamMemberDropDown.DataSource = null;
            selectTeamMemberDropDown.DataSource = availableTeamMembers;
            selectTeamMemberDropDown.DisplayMember = "FullName";

            teamMembersListBox.DataSource = null;
            teamMembersListBox.DataSource = selectedTeamMembers;
            teamMembersListBox.DisplayMember = "FullName";

        }

        /// <summary>
        /// CreateTeamForm Constructor
        /// </summary>
        public CreateTeamForm()
        {
            InitializeComponent();
            WireUpLists();
        }



        /// <summary>
        /// CreateMemberButton CLICK Event Handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateMemberButton_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                //Instantiate the PersonModel class model
                PersonModel p = new PersonModel();
                p.FirstName = firstNameValue.Text;
                p.LastName = lastNameValue.Text;
                p.EmailAddress = emailValue.Text;
                p.PhoneNumber = phoneNumberValue.Text;

                GlobalConfig.Connection.CreatePerson(p);
                firstNameValue.Text = "";
                lastNameValue.Text = "";
                emailValue.Text = "";
                phoneNumberValue.Text = "";
            }
            else
            {
                MessageBox.Show("All fields are REQUIRED to be filled in this form");
            }
        }
        /// <summary>
        /// This method validates CreateMember Form
        /// </summary>
        /// <returns></returns>
        private bool ValidateForm()
        {
            if (firstNameValue.Text.Length == 0 
                || lastNameValue.Text.Length == 0 
                || emailValue.Text.Length == 0 
                || phoneNumberValue.Text.Length == 0
            ) return false;
           
            return true;
        }

        private void AddMemberButton_Click(object sender, EventArgs e)
        {
            PersonModel p = (PersonModel)selectTeamMemberDropDown.SelectedItem;
            availableTeamMembers.Remove(p);
            selectedTeamMembers.Add(p);
            WireUpLists();
        }
        
    }
}
