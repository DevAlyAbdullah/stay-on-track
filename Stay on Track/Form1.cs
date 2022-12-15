using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;

namespace Stay_on_Track
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Left += 200;
            Top += 200;
            Hide();
            notifyIcon1.Visible = true;
            CreateAndDeleteLoop();
        }

        private async Task<bool> CreateAndDeleteLoop()
        {
            while (true)
            {
                File.Create("E:\\test.sot").Dispose();
                File.Delete("E:\\test.sot");
                await Task.Delay(25000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                Hide();
                notifyIcon1.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RegistryKey reg1 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg1.DeleteValue("Stay on Track");
                MessageBox.Show("You have been successfully removed Stay on Track from Startup", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                RegistryKey reg2 = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                reg2.SetValue("Stay on Track", Application.ExecutablePath.ToString());
                MessageBox.Show("You have been successfully added Stay on Track to Startup", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}