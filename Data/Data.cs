using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ediable_Repeater
{
    public class Data
    {
        public Data()
        {
            if(Contacts.Count == 0)
            {
                // Uncomment to add initial records
                //InitContacts();
            }
        }

        private void InitContacts()
        {
            Contacts.Add(new Contact() { ID = 1, FirstName = "Elmer", LastName = "Fudd" });
            Contacts.Add(new Contact() { ID = 2, FirstName = "Bugs", LastName = "Bunny" });
            Contacts.Add(new Contact() { ID = 3, FirstName = "Daffy", LastName = "Duck" });
        }

        public int NextId
        {
            get
            {
                int id = 0;
                if(Contacts.Count != 0)
                {
                    id = Contacts.Max(c => c.ID);
                }
                return ++id;
            }
        }

        public List<Contact> Contacts
        {
            get
            {
                if(HttpContext.Current.Session["contacts"] == null)
                {
                    HttpContext.Current.Session["contacts"] = new List<Contact>();
                }
                return HttpContext.Current.Session["contacts"] as List<Contact>;
            }
        }
    }
}