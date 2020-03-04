using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Conversions
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            for(var i=0; i<conversionTable.GetLength(0); i++)
            {
                cboConversions.Items.Add(conversionTable[i, 0]);
            }

            cboConversions.SelectedIndex = 0;
            conversionRate = conversionTable[0, 3];
        }

        string[,] conversionTable = {
			{"Miles to kilometers", "Miles", "Kilometers", "1.6093"},
			{"Kilometers to miles", "Kilometers", "Miles", "0.6214"},
			{"Feet to meters", "Feet", "Meters", "0.3048"},
			{"Meters to feet", "Meters", "Feet", "3.2808"},
			{"Inches to centimeters", "Inches", "Centimeters", "2.54"},
			{"Centimeters to inches", "Centimeters", "Inches", "0.3937"}
		};

        string conversionRate;

        public bool IsValidData()
        {
            return

                // Validation text boxes

                IsPresent(txtLength, "Length") &&
                IsDecimal(txtLength, "Length");
                
        }


        public bool IsPresent(TextBox textBox, string name)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(name + " is a required field.", "Entry Error");
                textBox.Focus();
                return false;
            }
            return true;
        }

        public bool IsDecimal(TextBox textBox, string name)
        {
            try
            {
                Convert.ToDecimal(textBox.Text);
                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show(name + " must be a decimal number.", "Entry Error");
                textBox.Focus();
                return false;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboConversions_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selected = cboConversions.SelectedItem as string;
            
            for(var i=0; i<conversionTable.GetLength(0); i++)
            {
                if(conversionTable[i,0]==selected)
                {
                    lblFromLength.Text = conversionTable[i, 1];
                    lblToLength.Text = conversionTable[i, 2];
                    conversionRate = conversionTable[i, 3];
                    lblCalculatedLength.Text = "";
                    txtLength.Focus();
                }
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            if(IsValidData())
            {
                var rate = Convert.ToDouble(conversionRate);
                var length = Convert.ToDouble(txtLength.Text);
                lblCalculatedLength.Text = (rate * length).ToString();
            }
        }
    }
}