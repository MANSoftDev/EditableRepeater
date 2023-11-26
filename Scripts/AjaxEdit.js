// Global storage for newly added row
var newRow;

$(document).ready(function ()
{
    // Use live so when adding new records the events will
    // automatically be bounde
    $("[id*='edit']").live('click',OnEdit);
    $("[id*='delete']").live('click', OnDelete);
    $("[id*='add']").click(OnAdd);
});

function OnEdit()
{
    // Get the row this button is within
    var tr = $(this).closest("tr");
    // Get the first and last name controls in this row
    var firstName = tr.find("span[id='firstName']");
    var lastName = tr.find("span[id='lastName']");

    // Insert an input element before the labels
    // and set the value to the label text
    // Then hide the label
    firstName.before("<input id='firstNameEdit' type='text' value='" + firstName.text() + "'/>").hide();
    lastName.before("<input id='lastNameEdit' type='text' value='" + lastName.text() + "'/>").hide();

    // Hide the existing buttons and add a save button in there place
    tr.find("[id*='delete']").hide();
    tr.find("[id*='edit']").before("<img id='save' src='images/base_floppydisk_32.png' />")
        .hide();

    tr.find("[id*='save']").one('click', OnSave);
}

function OnSave()
{
    // Get the row this button is within
    var tr = $(this).closest("tr");

    var firstName = tr.find("[id='firstNameEdit']");
    var lastName = tr.find("[id='lastNameEdit']");

    // Set the text of the labels from the input elements and show them
    tr.find("span[id='firstName']").text(firstName.val()).show();
    tr.find("span[id='lastName']").text(lastName.val()).show();

    // Remove the input elements
    firstName.remove();
    lastName.remove();

    // Show the buttons again and remove the save
    tr.find("[id*='delete']").show();
    tr.find("[id*='edit']").show();
    tr.find("[id*='save']").remove();

    // update the contact on the server
    UpdateContact( tr.attr("id"), firstName.val(), lastName.val())
}

function OnDelete()
{
    var tr = $(this).closest("tr");
    tr.remove();
    DeleteContact(tr.attr("id"));
}

function OnAdd()
{
    // Get the row this button is within
    var tr = $(this).closest("tr");
    // Get the first and last name controls in this row
    var firstName = tr.find("#newFirstName");
    var lastName = tr.find("#newLastName");

    // Create a new row and update the firstname and lastname elements
    // appropriately
    newRow = NewRow(tr);
    newRow.find("span[id='firstName']").text(firstName.val());
    newRow.find("span[id='lastName']").text(lastName.val());

    AddContact(firstName.val(), lastName.val())

    // Clear everything out to start again
    firstName.val("");
    lastName.val("");
}

function NewRow(tr)
{
    // If only one sibling then create a new row
    // otherwise just clone an exisitng one
    if(tr.siblings().length != 1)
    {
        var clone = tr.prev().clone();
        tr.before(clone);
    }
    else
    {
        var newRow = "<tr id=''>" +
            "<td>" +
                "<image id='edit' src='Images/EditDocument.png' class='imgButton' />" +
                "<image id='delete' src='Images/Delete_black_32x32.png' class='imgButton'/>" +
            "</td>" +
            "<td>" +
                "<span ID='firstName'></span>" +
            "</td>" +
            "<td>" +
                "<span ID='lastName'></span>" +
            "</td>" +
        "</tr>";
        tr.before(newRow);
    }

    return tr.prev();
}

function AddContact(firstName, lastName)
{
    var data = '{'
            + "\"firstName\":\"" + firstName + "\","
            + "\"lastName\":\"" + lastName + "\""
            + '}';
    $.ajax({
        type: "POST",
        url: "AjaxEdit.aspx/AddContact",
        data: data,
        contentType: "application/json",
        dataType: "json",
        error: OnAjaxError,
        success: OnAddContactSuccess
    });
}

function OnAddContactSuccess(data)
{
    var result = eval('(' + data.d + ')');
    // Assign id from newly added contact
    newRow.attr("id", result);
    newRow = null;
}

function UpdateContact(id, firstName, lastName)
{
    var data = '{'
            + "\"id\":" + id + ","
            + "\"firstName\":\"" + firstName + "\","
            + "\"lastName\":\"" + lastName + "\""
            + '}';
    $.ajax({
        type: "POST",
        url: "AjaxEdit.aspx/UpdateContact",
        data: data,
        contentType: "application/json",
        dataType: "json",
        error: OnAjaxError
    });
}

function DeleteContact(id)
{
    var data = '{'
            + "\"id\":" + id
            + '}';
    $.ajax({
        type: "POST",
        url: "AjaxEdit.aspx/DeleteContact",
        data: data,
        contentType: "application/json",
        dataType: "json",
        error: OnAjaxError
    });
}

function OnAjaxError(obj, status, error)
{
    var err = eval('(' + obj.responseText + ')');
    alert(err.Message);
}