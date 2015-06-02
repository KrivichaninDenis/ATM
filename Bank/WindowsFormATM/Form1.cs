using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows.Forms;
using System.Xml.Serialization;
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
            textBox2.Font = new Font("Times New Roman", Height / 16);            
            richTextBox1.Font = new Font("Times New Roman",  12);
            richTextBox2.Font = new Font("Times New Roman", 12);
            var ci = new CultureInfo("en-US");
            GetRes(ci, _outInf);           
        }


        public static readonly ILog Log = LogManager.GetLogger(typeof ( Form1)); 
        
        readonly Bank.Bank _atm = new Bank.Bank();
        readonly LoadCassete _loadCassete = new LoadCassete();
        readonly Algorithm _algToGiveMoney = new Algorithm();
        StateOpeartion _state = StateOpeartion.AllOk;
        readonly OutputInformation _outInf=new OutputInformation();  
         public static  Statistics Stats=new Statistics();
        readonly ConvertMoney _converter=new ConvertMoney();

        public delegate Statistics MyDelegate(Algorithm algToGiveMoney, int money, Statistics stats);
        public MyDelegate AddStats = Stats.AddStats;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control contr in Controls)
            {
                contr.KeyPress += Form1_KeyPress;
            }
            richTextBox1.Visible = false;

            try
            {
                using (
                    var reader = new StreamReader("C:/Users/Кривичанин/Documents/Visual Studio 2012/Projects/Bank/WindowsFormATM/bin/Debug/XmlSave.xml"))                                                
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof (Statistics), new XmlRootAttribute("xml"));
                    Stats = ((Statistics) deserializer.Deserialize(reader));

                    // MessageBox.Show("Successfully read", "Read");
                }
               // richTextBox1.Text += _stats.ToString();
                using (
                    var reader = new StreamReader("C:/Users/Кривичанин/Documents/Visual Studio 2012/Projects/Bank/WindowsFormATM/bin/Debug/Cassette.xml"))                                                
                {
                    XmlSerializer deserializer = new XmlSerializer(typeof (List<Money>), new XmlRootAttribute("xml"));
                    _atm.AllCassete = ((List<Money>)deserializer.Deserialize(reader));
                    textBox2.Text = _algToGiveMoney.MaxMoney(_atm.AllCassete).ToString();                       
                    // MessageBox.Show("Successfully read", "Read");
                }
                foreach (var m1 in _atm.AllCassete)
                {
                    listBox1.Items.Add(_outInf.MoneyValue + m1.MoneyValue + "  " + _outInf.MoneyCount + m1.MoneyCount + "\n");
                }
            }
            catch
            {
                // ignored
            }
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
            button17.Text = rm.GetString("INFO", ci);
            button16.Text = rm.GetString("Show", ci);
            button18.Text = rm.GetString("Hide", ci);
            button19.Text = rm.GetString("Statistics", ci);
            label1.Text = rm.GetString("MaxValue", ci);

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
            listBox1.Items.Clear();
            var ci = new CultureInfo("ru-RU");
            GetRes(ci,_outInf);
            foreach (var m1 in _atm.AllCassete)
            {
                listBox1.Items.Add(_outInf.MoneyValue + m1.MoneyValue + "  " + _outInf.MoneyCount + m1.MoneyCount + "\n");
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            CultureInfo ci = new CultureInfo("en-US");
            GetRes(ci, _outInf);
            foreach (var m1 in _atm.AllCassete)
            {
                listBox1.Items.Add(_outInf.MoneyValue + m1.MoneyValue + "  " + _outInf.MoneyCount + m1.MoneyCount + "\n");
            }
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
                    if (_algToGiveMoney.MaxMoney(_atm.GiveClientMoney(_algToGiveMoney, out _state, inputMoney, _outInf)) == copyIputMoney)
                    {
                       
                       
                        //делегата
                        Stats = AddStats(_algToGiveMoney,copyIputMoney,Stats);

                        textBox2.Text = _algToGiveMoney.MaxMoney(_atm.AllCassete).ToString();                       
                        _state = StateOpeartion.AllOk;
                        richTextBox1.Text += Environment.NewLine + _outInf.SuccessfullyIssued  + copyIputMoney;
                        richTextBox1.Text += _converter.ConvertToString(_algToGiveMoney.AllgiveMoney);
                        Log.Info(_outInf.SuccessfullyIssued + copyIputMoney);
                        listBox1.Items.Clear();
                        _algToGiveMoney.AllgiveMoney.Clear();
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
                            _state=StateOpeartion.AllOk;
                            _algToGiveMoney.AllgiveMoney.Clear();
                        }
                        else
                        {
                            _state = StateOpeartion.CanNotGiveThisCombination;
                            richTextBox1.Text += Environment.NewLine  + _outInf.CanNotGiveThisCombination + @" (" + copyIputMoney + @")";
                            Log.Error(_outInf.CanNotGiveThisCombination + " (" + copyIputMoney + ")");
                            textBox1.Text = string.Empty;
                            _state = StateOpeartion.AllOk;
                            _algToGiveMoney.AllgiveMoney.Clear();
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
            Stats=new Statistics();
            _atm.AllCassete=new List<Money>();
            
            richTextBox1.Clear();
            richTextBox2.Clear();
            _state=StateOpeartion.AllOk;
            Stats.WhenLoad = DateTime.UtcNow;
           
            //_algToGiveMoney.AllgiveMoney.Clear();
            XmlConfigurator.Configure();   
       
            Log.Debug(_outInf.StartBankProcess);

            textBox1.Text = string.Empty;

            _atm.AllCassete.Clear();
            listBox1.Items.Clear();

            while (_loadCassete.CheckState() != StateOpeartion.AllOk)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                }
                _atm.ReadCassete(_loadCassete, _state, openFileDialog1.FileName);              
            }
            panel1.Visible = true;
            textBox2.Text = _algToGiveMoney.MaxMoney(_atm.AllCassete).ToString();
            Stats.MaxValue=_algToGiveMoney.MaxMoney(_atm.AllCassete);
            _state = _loadCassete.CheckState();
           _loadCassete.State=StateOpeartion.CasseteProblem;            
                       
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
            richTextBox1.Clear();
            textBox1.Text = string.Empty;
            _atm.AllCassete.Clear();
            listBox1.Items.Clear();
            richTextBox1.Text += Environment.NewLine + _outInf.CassettesSuccessfullyCleared;
            Log.Info(_outInf.CassettesSuccessfullyCleared);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {            

            Log.Debug(_outInf.EndBankProcess);
            if (Stats.MaxValue != 0)
            {
                using (var writer = new FileStream("XmlSave.xml", FileMode.Create))
                {

                    XmlSerializer ser = new XmlSerializer(typeof (Statistics), new XmlRootAttribute("xml"));
                    ser.Serialize(writer, Stats);
                }
                using (var writer = new FileStream("Cassette.xml", FileMode.Create))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(List<Money>), new XmlRootAttribute("xml"));
                    ser.Serialize(writer, _atm.AllCassete);
                    // MessageBox.Show("Saved successfully", "Save");
                }    
            }
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
            if (ch == (char)Keys.I) { button17.PerformClick(); }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            richTextBox1.Visible = false;
        }       

        private void button17_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"A- "+button13.Text+"\n"+
                @"C- "+button14.Text+"\n"+
                @"Backspace- " + button12.Text+"\n"+
                @"Delete- "+button15.Text+"\n"+
               @"Enter- "+button11.Text+"\n" );
        }
        
        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            panel1.Visible = false;
        }        

        private void button19_Click_1(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            panel2.Visible = true;
            richTextBox2.Text += Stats.ToString(); 
        }

        private void button20_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        
               

        

                                                    
    }
}
