using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using Bank;
using log4net;
using log4net.Config;

namespace WindowsFormATM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var log = LogManager.GetLogger(typeof(Form1));
            log.Debug("Start Bank Process");
            richTextBox1.SelectionColor = Color.Red;
            XmlConfigurator.Configure();
        }


        private readonly ILog _log = LogManager.GetLogger(typeof (Form1)); 
        
        readonly Bank.Bank _atm = new Bank.Bank();
        readonly LoadCassete _loadCassete = new LoadCassete();
        readonly Algorithm _algToGiveMoney = new Algorithm();
        StateOpeartion _state = StateOpeartion.AllOk;

        private void Form1_Load(object sender, EventArgs e)
        {
            
            
        }
        private void GetRes(CultureInfo ci)
        {
            Assembly assembl = Assembly.Load("WindowsFormATM");
            ResourceManager rm = new ResourceManager("WindowsFormATM.Language.Language", assembl);

            button11.Text = rm.GetString("Give", ci);
            button12.Text = rm.GetString("Clear", ci);
            button13.Text = rm.GetString("LoadCassete", ci);
            button14.Text = rm.GetString("ClearCassete", ci);
            button15.Text = rm.GetString("ClearAll", ci);
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("ru-RU");
            GetRes(ci);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-US");
            GetRes(ci);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"0";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"1";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"2";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"3";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"4";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"5";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"6";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"7";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"8";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            textBox1.Text += @"9";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            { 
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }                                
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (_algToGiveMoney.MaxMoney(_atm.AllCassete) == 0)
            {
                richTextBox1.Text += Environment.NewLine + @"The ATM ran out of money. Use another machine. ";
            }
            else
            {
                if (textBox1.Text == string.Empty) return;
                var inputMoney = int.Parse(textBox1.Text);
                var copyIputMoney = inputMoney;


                if (_atm.AllCassete.Count == 0)
                {
                    richTextBox1.Text += Environment.NewLine + @"Please,Load cassette!!!";
                }
                else
                {
                    if (_atm.GiveClientMoney(_algToGiveMoney, out _state, inputMoney) == 0)
                    {
                        _state = StateOpeartion.AllOk;
                        richTextBox1.Text += Environment.NewLine + _state + @" Successfully issued" + copyIputMoney;
                        _log.Info("Successfully issued: " + copyIputMoney);
                        listBox1.Items.Clear();
                        foreach (var m1 in _atm.AllCassete)
                        {
                            listBox1.Items.Add("Money Value: " + m1.MoneyValue + " Money Count: " + m1.MoneyCount + "\n");
                        }
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        if (_state == StateOpeartion.WantMoreThanHave)
                        {
                            richTextBox1.Text += Environment.NewLine + @"Error: " + _state+@" ("+copyIputMoney+@")";
                            _log.Error(" Error: " + _state + " (" + copyIputMoney + ")");
                            textBox1.Text = string.Empty;
                        }
                        else
                        {
                            _state = StateOpeartion.CanNotGiveThisCombination;
                            richTextBox1.Text += Environment.NewLine + @"Error: " + _state + @" (" + copyIputMoney + @")";
                            _log.Error(" Error: " + _state + " (" + copyIputMoney + ")");
                            textBox1.Text = string.Empty;
                        }

                    }
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            _atm.AllCassete.Clear();
            listBox1.Items.Clear();

            _atm.ReadCassete(_loadCassete, _state);
            _state = _loadCassete.CheckState();
                       
            if (_state == StateOpeartion.AllOk)
            {
                foreach (var m1 in _atm.AllCassete)
                {
                    listBox1.Items.Add("Money Value: " + m1.MoneyValue + " Money Count: " + m1.MoneyCount + "\n");
                }
                richTextBox1.Text +=Environment.NewLine+ _state + @": " + @"Cassettes successfully loaded"; 
                _log.Info("Cassettes successfully loaded");
            }
            else
            {
                _state=StateOpeartion.CasseteProblem;
                richTextBox1.Text +=Environment.NewLine+ @"Exception: " + _state+@"\n";               
                _log.Error("Exception " + _state + "\n");
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            _atm.AllCassete.Clear();
            listBox1.Items.Clear();
            richTextBox1.Text += Environment.NewLine + _state + @": " + @"Cassettes successfully cleared";
            _log.Info("Clear Cassete");
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            _log.Debug("End Bank Process");
        }
    }
}
