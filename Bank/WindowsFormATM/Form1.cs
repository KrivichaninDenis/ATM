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
            textBox1.Font = new Font("Times New Roman",  Height / 16);
            richTextBox1.Font = new Font("Times New Roman",  12);
            var ci = new CultureInfo("en-US");
            GetRes(ci, _outInf);
        }


        public static readonly ILog Log = LogManager.GetLogger(typeof ( Form1)); 
        
        readonly Bank.Bank _atm = new Bank.Bank();
        readonly LoadCassete _loadCassete = new LoadCassete();
        readonly Algorithm _algToGiveMoney = new Algorithm();
        StateOpeartion _state = StateOpeartion.AllOk;
        readonly OutputInformation _outInf=new OutputInformation();

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control contr in Controls)
            {
                contr.KeyPress += Form1_KeyPress;
            }
            richTextBox1.Visible = false;
        }
        private void GetRes(CultureInfo ci, OutputInformation outInf)
        {
            Assembly assembl = Assembly.Load("WindowsFormATM");
            ResourceManager rm = new ResourceManager("WindowsFormATM.Language.Language", assembl);

            button11.Text = rm.GetString("Give", ci);
            button12.Text = rm.GetString("Clear", ci);
            button13.Text = rm.GetString("LoadCassete", ci);
            button14.Text = rm.GetString("ClearCassete", ci);
            button15.Text = rm.GetString("ClearAll", ci);

            outInf.AllOk = rm.GetString("AllOk", ci);
            outInf.PleaseLoadCassete = rm.GetString("PleaseLoadCassete", ci);
            outInf.SuccessfullyIssued = rm.GetString("SuccessfullyIssued", ci);
            outInf.MoneyValue = rm.GetString("MoneyValue", ci);
            outInf.MoneyCount = rm.GetString("MoneyCount", ci);
            outInf.CassettesSuccessfullyLoaded = rm.GetString("CassettesSuccessfullyLoaded", ci);
            outInf.CassettesSuccessfullyCleared = rm.GetString("CassettesSuccessfullyCleared", ci);
            outInf.NoMoneyToGive = rm.GetString("NoMoneyToGive", ci);
            outInf.CasseteProblem = rm.GetString("CasseteProblem", ci);
            outInf.CanNotGiveThisCombination = rm.GetString("CanNotGiveThisCombination", ci);
            outInf.WantMoreThanHave = rm.GetString("WantMoreThanHave", ci);
            outInf.IncorrectInput = rm.GetString("IncorrectInput", ci);
            outInf.StartBankProcess = rm.GetString("StartBankProcess", ci);
            outInf.EndBankProcess = rm.GetString("EndBankProcess", ci);

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var ci = new CultureInfo("ru-RU");
            GetRes(ci,_outInf);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("en-US");
            GetRes(ci, _outInf);
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
                _state=StateOpeartion.NoMoneyToGive;
                richTextBox1.Text += Environment.NewLine + _outInf.NoMoneyToGive;
            }
            else
            {
                if (textBox1.Text == string.Empty) return;
                var inputMoney = int.Parse(textBox1.Text);
                var copyIputMoney = inputMoney;


                if (_atm.AllCassete.Count == 0)
                {
                    richTextBox1.Text += Environment.NewLine +_outInf.PleaseLoadCassete;
                }
                else
                {
                    if (_atm.GiveClientMoney(_algToGiveMoney, out _state, inputMoney) == 0)
                    {
                        _state = StateOpeartion.AllOk;
                        richTextBox1.Text += Environment.NewLine + _outInf.SuccessfullyIssued  + copyIputMoney;
                        Log.Info(_outInf.SuccessfullyIssued + copyIputMoney);
                        listBox1.Items.Clear();
                        foreach (var m1 in _atm.AllCassete)
                        {
                            listBox1.Items.Add(_outInf.MoneyValue + m1.MoneyValue +"  "+ _outInf.MoneyCount + m1.MoneyCount + "\n");
                        }
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        if (_state == StateOpeartion.WantMoreThanHave)
                        {
                            richTextBox1.Text += Environment.NewLine + _outInf.WantMoreThanHave+@" ("+copyIputMoney+@")";
                            Log.Error(_outInf.WantMoreThanHave + " (" + copyIputMoney + ")");
                            textBox1.Text = string.Empty;
                        }
                        else
                        {
                            _state = StateOpeartion.CanNotGiveThisCombination;
                            richTextBox1.Text += Environment.NewLine  + _outInf.CanNotGiveThisCombination + @" (" + copyIputMoney + @")";
                            Log.Error(_outInf.CanNotGiveThisCombination + " (" + copyIputMoney + ")");
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
            _state=StateOpeartion.AllOk;
            XmlConfigurator.Configure();          
            Log.Debug(_outInf.StartBankProcess);
            textBox1.Text = string.Empty;
            _atm.AllCassete.Clear();
            listBox1.Items.Clear();

            _atm.ReadCassete(_loadCassete, _state);
            _state = _loadCassete.CheckState();
                       
            if (_state == StateOpeartion.AllOk)
            {
                foreach (var m1 in _atm.AllCassete)
                {
                    listBox1.Items.Add(_outInf.MoneyValue + m1.MoneyValue + "  " + _outInf.MoneyCount + m1.MoneyCount + "\n");
                }
                richTextBox1.Text +=Environment.NewLine + _outInf.CassettesSuccessfullyLoaded; 
                Log.Info(_outInf.CassettesSuccessfullyLoaded);
            }
            else
            {
                _state=StateOpeartion.CasseteProblem;
                richTextBox1.Text +=Environment.NewLine+ @"Exception: " + _outInf.CasseteProblem+@"\n";               
                Log.Error("Exception " + _state + "\n");
            }
            _state = StateOpeartion.AllOk;
        }

        private void button14_Click(object sender, EventArgs e)
        {           
            textBox1.Text = string.Empty;
            _atm.AllCassete.Clear();
            listBox1.Items.Clear();
            richTextBox1.Text += Environment.NewLine  + _outInf.CassettesSuccessfullyCleared;
            Log.Info(_outInf.CassettesSuccessfullyCleared);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Log.Debug(_outInf.EndBankProcess);
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var ch = e.KeyChar;
                
            if(ch=='0') { button10.PerformClick();}
            if (ch == '1') { button1.PerformClick(); }
            if (ch == '2') { button2.PerformClick(); }
            if (ch == '3') { button3.PerformClick(); }
            if (ch == '4') { button4.PerformClick(); }
            if (ch == '5') { button5.PerformClick(); }
            if (ch == '6') { button6.PerformClick(); }
            if (ch == '7') { button7.PerformClick(); }
            if (ch == '8') { button8.PerformClick(); }
            if (ch == '9') { button9.PerformClick(); }
            if (ch == (char) Keys.Back) {button12.PerformClick();}
            if(ch==(char)Keys.Delete) {button15.PerformClick();}
            if(ch==(char)Keys.Enter) {button11.PerformClick();}
            if(ch==(char)Keys.A) {button13.PerformClick();}
            if (ch == (char)Keys.C) { button14.PerformClick(); }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
        }

        
        

       

        


        

        

       
    }
}
