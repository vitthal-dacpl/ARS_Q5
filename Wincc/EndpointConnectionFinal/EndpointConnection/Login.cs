using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ArsServer1
{
    public partial class Login : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );
        public Login()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }
        private void Submit_Click(object sender, EventArgs e)
        {
            string User;
            string Password;
            User = textBox2.Text;
            Password = textBox3.Text;

            if (User == "Admin" && Password == "Admin@123")
            {

                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
                string motherbord = "";
                foreach (ManagementObject getserial in MOS.Get())
                {
                    motherbord = getserial["SerialNumber"].ToString();
                }
                WriteToFile("SerialNumber = " + motherbord);

                // string StaticNumber = "6139-1257-9052-5792-9093-4207-93";
                string StaticNumber = "L2HF81401MT";
                if (StaticNumber == motherbord)
                {
                    this.Hide();
                    Form1 form2 = new Form1();
                    form2.Show();

                }
                else
                {
                    MessageBox.Show("Unauthorized device Invalide License key", "Access Denied");
                    this.Close();
                }


            }
            else
            {
                MessageBox.Show("Please enter valid credentials");
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        public static void WriteToFile(string text)
        { // Define the file path and name
            string filePath = @"E:\Log\ARS_Error.txt";

            // Ensure the directory exists
            string directoryPath = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            string path = "E:\\Log\\ARS_Error.txt";
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine($"{text} {"Print time=>" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}");
                writer.Close();
            }
        }
    }
}
