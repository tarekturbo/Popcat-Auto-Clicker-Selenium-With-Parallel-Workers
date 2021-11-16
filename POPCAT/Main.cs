using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Net;

namespace POPCAT
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {


        }
        private void Worker()
        {
            ParallelOptions Options = new ParallelOptions();
            Options.MaxDegreeOfParallelism = decimal.ToInt32(guna2NumericUpDown1.Value);
            Parallel.For(0, decimal.ToInt32(guna2NumericUpDown1.Value), Options, i =>
              {
                  Chrome();
              });
        }
        private void Chrome()
        {
            try
            {
                ChromeDriverService chromeDriverService = ChromeDriverService.CreateDefaultService();
                chromeDriverService.HideCommandPromptWindow = true;
                ChromeOptions chromeOptions = new ChromeOptions();
                chromeOptions.PageLoadStrategy = PageLoadStrategy.Default;
                chromeOptions.AddAdditionalCapability("useAutomationExtension", false);
                chromeOptions.AddArguments(new string[]
                {
                "--disable-notifications",
                "--ignore-certificate-errors",
                "--disable-popup-blocking",
                "--incognito",
                "--disable-hang-monitor",
                "--test-type",
                "--new-window",
                "--no-sandbox",
                "--lang=EN"
                });
                IWebDriver driver = new ChromeDriver(chromeDriverService, chromeOptions, TimeSpan.FromHours(1.0));
                IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
                driver.Manage().Window.Size = new Size(480, 630);
                driver.Navigate().GoToUrl("https://popcat.click/");
                javaScriptExecutor.ExecuteScript("var event = new KeyboardEvent('keydown', {key: 'g',ctrlKey: true});setInterval(function(){for (i = 0; i < 100; i++) {document.dispatchEvent(event);}}, 0);", Array.Empty<object>());
            }
            catch(Exception err)
            {
                {


                    MessageBox.Show("Please install chrome driver", "Driver error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            
        }
        private void guna2TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
    (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void start_Click(object sender, EventArgs e)
        {
            Thread start = new Thread(new ThreadStart(Worker));
            start.IsBackground = false;
            start.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://t.me/tarook");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/txrook");

        }
    }
}
