
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ArsServer1
{
    public partial class Logging_Status : Form
    {
        public Logging_Status(string text, string text1 , string datetime)
        {
            InitializeComponent();
            textBox2.Text = text;
            if (text == "OK")
            {
                textBox3.BackColor = Color.Green;
            }
            else if (text == "NOK")
            {
                textBox3.BackColor = Color.Red;
            }
            else
            {
                textBox3.BackColor = Color.White;
            }
            textBox3.Text = text1;
            if (text1 == "OK" ) 
            {
                textBox3.BackColor = Color.Green;
            }
            else if (text1 == "NOK")
            {
                textBox3.BackColor = Color.Red;
            }
            else
            {
                textBox3.BackColor = Color.White;
            }
        }

        public string TextBoxValue { get; set; }

        private void Logging_Status_Load(object sender, EventArgs e)
        {
            
        }

          
    }
}
