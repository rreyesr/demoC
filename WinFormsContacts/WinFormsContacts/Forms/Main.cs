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
    public partial class Main : Form
    {
        private BusinessLogicLayer _businessLogicLayer;
        public Main()
        {
            InitializeComponent();
            _businessLogicLayer = new BusinessLogicLayer();
        }

        #region events
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenContactDetailsDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadContacts(textSearch.Text);
            textSearch.Text = string.Empty;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadContacts();
        }

        private void gridContacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = (DataGridViewCell)gridContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (cell.Value.ToString() == "Edit")
            {
                ContactDetails c = new ContactDetails();
                c.LoadContact(new Contact
                {
                    id = int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()),
                    firstName = gridContacts.Rows[e.RowIndex].Cells[1].Value.ToString(),
                    lastName = gridContacts.Rows[e.RowIndex].Cells[2].Value.ToString(),
                    phone = gridContacts.Rows[e.RowIndex].Cells[3].Value.ToString(),
                    address = gridContacts.Rows[e.RowIndex].Cells[4].Value.ToString()
                }
                );
                c.ShowDialog(this);
            }
            else if (cell.Value.ToString() == "Delete")
            {
                DeleteContact(int.Parse(gridContacts.Rows[e.RowIndex].Cells[0].Value.ToString()));
            }
            LoadContacts();
        }

        #endregion

        #region private methods
        private void OpenContactDetailsDialog()
        {
            ContactDetails c = new ContactDetails();
            c.ShowDialog();
            LoadContacts();
        }

        private void LoadContacts(string searchText = null)
        {
            List<Contact> contacts = _businessLogicLayer.GetContacts(searchText);
            gridContacts.DataSource = contacts;
        }

        private void DeleteContact(int id)
        {
            _businessLogicLayer.DeleteContact(id);
        }

        #endregion


    }
}
