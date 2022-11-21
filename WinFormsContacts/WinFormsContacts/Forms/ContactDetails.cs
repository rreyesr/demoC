using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContacts
{
    public partial class ContactDetails : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region event
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            saveContact();
            this.Close();
        }

        #endregion


        #region metodos

        private void saveContact()
        {
            Contact c = new Contact();
            c.firstName = txtName.Text;
            c.lastName = txtLastName.Text;
            c.phone = txtPhone.Text;
            c.address = txtAddress.Text;
            c.id = _contact != null ? _contact.id : 0;

            _businessLogicLayer.SaveContact(c);
            ClearWindw();
        }

        public void LoadContact(Contact c)
        {
            _contact = c;
            if (c != null)
            {
                txtName.Text = c.firstName;
                txtLastName.Text = c.lastName;
                txtPhone.Text = c.phone;
                txtAddress.Text = c.address;
            }
        }

        private void ClearWindw()
        {
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        #endregion

    }
}
