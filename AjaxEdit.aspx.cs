using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace Ediable_Repeater
{
    public partial class AjaxEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Data = new Data();
            if(!IsPostBack)
            {                
                LoadData();
            }
        }

        private void LoadData()
        {
            Repeater2.DataSource = Data.Contacts;
            Repeater2.DataBind();
        }

        [WebMethod]
        public static int AddContact(string firstName, string lastName)
        {
            int id = Data.NextId;
            Data.Contacts.Add(new Contact() { ID = id, FirstName = firstName, LastName = lastName });

            return id;
        }

        [WebMethod]
        public static void UpdateContact(int id, string firstName, string lastName)
        {
            Contact contact = Data.Contacts.Single(e => e.ID == id);
            contact.FirstName = firstName;
            contact.LastName = lastName;
        }

        [WebMethod]
        public static void DeleteContact(int id)
        {
            Contact contact = Data.Contacts.Single(e => e.ID == id);
            Data.Contacts.Remove(contact);
        }

        public static Data Data { get; set; }
    }
}