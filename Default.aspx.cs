using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Ediable_Repeater
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Data = new Data();
            if(!IsPostBack)
            {                
                EditIndex = -1;
                LoadData();
            }
        }

        private void LoadData()
        {
            Repeater1.DataSource = Data.Contacts;
            Repeater1.DataBind();
        }

        protected void OnAddRecord(object sender, EventArgs e)
        {
            // Get the textboxes using the button as the starting point
            TextBox firstName = ((Control)sender).Parent.FindControl("NewFirstName") as TextBox;
            TextBox lastName = ((Control)sender).Parent.FindControl("NewLastName") as TextBox;

            //  No point in adding anything if empty
            if(!string.IsNullOrWhiteSpace(firstName.Text) || !string.IsNullOrWhiteSpace(lastName.Text))
            {
                // Add a new Contact and rebind the repeater
                Data.Contacts.Add(new Contact() { ID = Data.NextId, FirstName = firstName.Text, LastName = lastName.Text });

                Repeater1.DataSource = Data.Contacts;
                Repeater1.DataBind();
            }
        }

        protected void OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if(e.CommandName == "delete")
            {
                Data.Contacts.RemoveAt(e.Item.ItemIndex);
            }
            else if(e.CommandName == "edit")
            {
                EditIndex = e.Item.ItemIndex;
            }
            else if(e.CommandName == "save")
            {
                HtmlInputHidden t = e.Item.FindControl("firstNameHidden") as HtmlInputHidden;
                Data.Contacts[e.Item.ItemIndex].FirstName = t.Value;
                t = e.Item.FindControl("lastNameHidden") as HtmlInputHidden;
                Data.Contacts[e.Item.ItemIndex].LastName = t.Value;
                EditIndex = -1;
            }

            Repeater1.DataSource = Data.Contacts;
            Repeater1.DataBind();
        }

        protected void OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if(e.Item.ItemIndex == EditIndex)
                {
                    // Find the placeholder
                    PlaceHolder p = e.Item.FindControl("firstNameEditPlaceholder") as PlaceHolder;

                    // Create textBox and assign the current value of the data item
                    TextBox t = new TextBox();
                    t.ID = "firstNameEdit";
                    t.Text = ((Contact)e.Item.DataItem).FirstName;

                    // Add the textbox to the placeholder
                    p.Controls.Add(t);

                    // Get the existing label and hide it
                    Label l = e.Item.FindControl("firstName") as Label;
                    l.Visible = false;

                    p = e.Item.FindControl("lastNameEditPlaceholder") as PlaceHolder;
                    t = new TextBox();
                    t.ID = "lastNameEdit";
                    t.Text = ((Contact)e.Item.DataItem).LastName;
                    p.Controls.Add(t);

                    l = e.Item.FindControl("lastName") as Label;
                    l.Visible = false;

                    // Make hidden fields visible
                    HtmlInputHidden h = e.Item.FindControl("firstNameHidden") as HtmlInputHidden;
                    h.Visible = true;
                    h = e.Item.FindControl("lastNameHidden") as HtmlInputHidden;
                    h.Visible = true;

                    // Remove the edit button from display
                    ImageButton b = e.Item.FindControl("Edit") as ImageButton;
                    b.Visible = false;

                    // Resuse the delete button
                    b = e.Item.FindControl("Delete") as ImageButton;
                    b.CommandName = "save";
                    b.OnClientClick = "OnSave(this)";
                    b.ImageUrl = "~/Images/base_floppydisk_32.png";                    
                }
            }
        }

        public Data Data { get; set; }
        private int EditIndex
        {
            get { return (int)ViewState["EditIndex"]; }
            set { ViewState["EditIndex"] = value; }
        }
    }
}
