using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContacts
{
    class DataAccessLayer
    {
        private SqlConnection conn = new SqlConnection("");

        public void InsertContact(Contact c)
        {
            try
            {
                conn.Open();
                string query = @"
                                    insert into Contacts(firstname, lastname, phone, address)
                                    values(@FirstName,@LastName,@Phone,@Address)
                                ";
                SqlParameter firstName = new SqlParameter("@FirstName", c.firstName);
                SqlParameter lastName = new SqlParameter("@LastName", c.lastName);
                SqlParameter phone = new SqlParameter("@Phone", c.phone);
                SqlParameter address = new SqlParameter("@Address", c.address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Ocurrio un Error " + ex);
            }
            finally
            {
                conn.Close();
            }
        }
        public void UpdateContact(Contact c)
        {
            try
            {
                conn.Open();
                string query = @"
                                 UPDATE Contacts set firstName = @FirstName, lastName = @LastName, phone = @Phone, address = @Address where id = @Id;
                                ";

                SqlParameter id = new SqlParameter("@Id", c.id);
                SqlParameter firstName = new SqlParameter("@FirstName", c.firstName);
                SqlParameter lastName = new SqlParameter("@LastName", c.lastName);
                SqlParameter phone = new SqlParameter("@Phone", c.phone);
                SqlParameter address = new SqlParameter("@Address", c.address);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(id);
                command.Parameters.Add(firstName);
                command.Parameters.Add(lastName);
                command.Parameters.Add(phone);
                command.Parameters.Add(address);

                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void DeleteContact(int id)
        {
            try
            {
                conn.Open();
                string query = @"
                                    DELETE from Contacts where id = @Id
                                ";

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }
        public List<Contact> GetContacts(string searchText = null)
        {
            List<Contact> contacts = new List<Contact>();
            try
            {
                conn.Open();
                string query = @"
                                select id, firstName, lastName, phone, address from Contacts
                            ";
                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(searchText))
                {
                    query += "where id like @SearchText Or firstName like @SearchText Or lastName " +
                                "like @SearchText Or phone like @SearchText Or address like @SearchText";
                    command.Parameters.Add(new SqlParameter("@SearchText", $"%{searchText}%"));
                }

                command.CommandText = query;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();

                while(reader.Read())
                {
                    contacts.Add(new Contact
                    {
                        id = int.Parse(reader["id"].ToString()),
                        firstName = reader["firstName"].ToString(),
                        lastName = reader["lastName"].ToString(),
                        phone = reader["phone"].ToString(),
                        address = reader["address"].ToString()
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }

            return contacts;
        }
    }
}
