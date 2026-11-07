using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RealProject____
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {

            try
            {
                int inCase;
                if (txtCosname.Text == " " || int.TryParse(txtCosname.Text, out inCase))
                {
                    MessageBox.Show("please enter the customer`s name even numbers are not allowed!");
                    return;
                    txtCosname.Focus();

                }
                    int  QUANTITY;
                    if (!int.TryParse(txtQuantity.Text, out QUANTITY) || QUANTITY < 1)
                    {
                        MessageBox.Show("please enter a valid quantity(1 and more)");
                        return;
                    }
                    double DrinkPrice = 0;
                    String DrinkName = "";

                    if (rbTea.Checked)
                    {
                        DrinkName = "you ordered tea.";
                        DrinkPrice = 1.00;
                    }
                    else if (rbCoffee.Checked)
                    {
                        DrinkName = "you orederd coffee";
                        DrinkPrice = 2.00;

                    }
                    else if (rbJuice.Checked)
                    {
                        DrinkName = "you orederd a cold juice";
                        DrinkPrice = 3.00;
                    }
                    else
                    {
                        MessageBox.Show("please select a drink type!", "Eroor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                int WININGCOUNT = 0;


                lstOrderList.Items.Clear();



                    for (int j = 1; j <= QUANTITY; j++)
                    {
                        Random rand = new Random();

                        int luckunumber = rand.Next(51);

                        lstOrderList.Items.Add( j   +  ")  "    +     txtCosname.Text   +  " - "  +  DrinkName + " -   lucky:  " +
                             luckunumber  +  " - $" +  DrinkPrice);

                    if(luckunumber  == 7 || luckunumber == 17)
                    {

                        lblLuckyMessage.Text = "you got a free cookie👍🏻";
                    }
                    else
                    {
                        lblLuckyMessage.Text = "no prize this time😭";
                    }

                    }


                    double TOTAL = DrinkPrice * QUANTITY;

                    if (chkStudentMember.Checked)
                    {
                        TOTAL = TOTAL - (TOTAL * 0.1);
                    }
                    lblTotalPrice.Text = "Total price: $" + TOTAL.ToString("0.00");
                }

            
            catch (Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (lstOrderList.Items.Count == 0)
            {
                MessageBox.Show("No order to save.");
                return;
            }
            try
            {
                StreamWriter writer;
                writer = File.AppendText("orders.txt");

                for(int i = 0; i < lstOrderList.Items.Count; i++)
                {
                    writer.WriteLine(lstOrderList.Items[i].ToString());
                }
                writer.WriteLine(lblTotalPrice.Text);
                writer.WriteLine(lblLuckyMessage.Text);

                writer.Close();

                MessageBox.Show("order saved!👏🏻👏🏻");


            }catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (!File.Exists("orders.txt"))
            {
                MessageBox.Show("no file found😒😒");
                return;
            }
            try
            {
                lstOrderList.Items.Clear();

                StreamReader reader;
                reader = File.OpenText("orders.txt");
                while (!reader.EndOfStream)
                {
                    lstOrderList.Items.Add(reader.ReadLine());
                }
                reader.Close();

            }
            catch(Exception exe)
            {
                MessageBox.Show(exe.Message);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtCosname.Clear();

            txtQuantity.Clear();


            lstOrderList.Items.Clear();


            lblTotalPrice.Text = "TotalPrice = 0.00";

            lblLuckyMessage.Text = "Lucku Message: no prize this time";


            chkStudentMember.Checked = false;

            rbTea.Checked = false;
            rbCoffee.Checked = false;
            rbJuice.Checked = false;

            txtCosname.Focus();
        }
    }
}
