using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Diagnostics;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(Object sender, EventArgs e)
    {
    }

    /*CONVERT CURRENCY*/
    public void ConvertButton_Click(Object sender, EventArgs e)
    {
        // Perform the conversion and display the results
        try
        {
            decimal dollars = Convert.ToDecimal(USD.Text);
            if (dollars < 0)
            {
                throw new FormatException();
            }
            decimal rate = Convert.ToDecimal(CurrencyBox.SelectedValue);
            decimal amount = dollars * rate;
            Output.Text = amount.ToString("f2") + " " + CurrencyBox.SelectedItem.Text;
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");

        }
        catch (FormatException)
        {
            Output.Text = "Error: Invalid input!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
        }
        catch (OverflowException)
        {
            Output.Text = "Error: Input is too large!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
        }

        //clear field
        USD.Text = "";
    }

    /* DELETE A CURRENCY*/
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        //delete the selected item from the database
        if (Sqldatasource1.Delete() <= 0)
        {
            Output.Text = "Error: Currency haven't been deleted!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
        }
        else
        {
            Output.Text = "Currency successfully deleted.";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
        }

    }

    /* ADD A NEW CURRENCY */
    protected void AddButton_Click(object sender, EventArgs e)
    {
        //see if new currency field is empty
        if (NewCurrency.Text == "")
        {
            Output.Text = "Error: Currency cannot be empty!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            return;
        }

        //try to convert new rate, and catch an exception
        try
        {
            if (Decimal.Parse(NewRate.Text) <= 0)
            {
                throw new FormatException();
            }
        }
        catch (FormatException)
        {
            Output.Text = "Error: Invalid rate!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            //clear the field
            NewRate.Text = "";
            return;
        }

        //if the currency already exists, do not add it
        if (CurrencyBox.Items.FindByText(NewCurrency.Text) != null)
        {
            Output.Text = "Error: Currency already exists!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            //clear the fields
            NewCurrency.Text = "";
            NewRate.Text = "";
            return;
        }

        //finally good to insert the new currency to the database
        Sqldatasource1.Insert();

        Output.Text = "Currency successfully added.";
        Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");

        //clear the fields
        NewCurrency.Text = "";
        NewRate.Text = "";
    }

    /* MODIFY THE CURRENT RATE */
    protected void ModifyButton_Click(object sender, EventArgs e)
    {
        //try to convert new rate, and catch an exception
        try
        {
            if (Decimal.Parse(Rate.Text) <= 0)
            {
                throw new FormatException();
            }
        }
        catch (FormatException)
        {
            Output.Text = "Error: Invalid rate!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
            //clear the field
            Rate.Text = "";
            return;
        }

        //update the rate in database
        if (Sqldatasource1.Update() > 0)
        {
            CurrentRate.Text = Decimal.Parse(Rate.Text).ToString("f4"); //show new rate
            Output.Text = "Rate successfully updated.";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#008000");
        }
        else
        {
            Output.Text = "Error: Currency haven't been updated!";
            Output.ForeColor = System.Drawing.ColorTranslator.FromHtml("#FF0000");
        }
        //clear the field
        Rate.Text = "";
    }

    protected void CurrencyBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        //show rate on postback
        CurrentRate.Text = CurrencyBox.SelectedValue;
    }
}