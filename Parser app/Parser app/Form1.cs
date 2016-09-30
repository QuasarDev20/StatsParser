using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int cycle = int.Parse(textBox2.Text) * 1000;
            System.Threading.Timer tmr = new System.Threading.Timer(Parse, null , 1000 , cycle);
            

        }
        public void Parse(object data)    // Парсинг данных в файл + вызов print()
        {
            Action act = print;
            WebClient web = new WebClient();
            Stream str = web.OpenRead(textBox1.Text);
            StreamReader sr = new StreamReader(str);
            string srr = sr.ReadLine();
            Regex hctest = new Regex(@"(?<=""hardcore_summary"")+(:\w*)");
            Regex regK = new Regex(@"(?<=""kills"")+(:\w*)");
            Regex regW = new Regex(@"(?<=""wins"")+(:\w*)");
            Regex regG = new Regex(@"(?<=""global_rank"")+(:\w*)");
            Match resK, resW, resG;
            Match hctestm = hctest.Match(srr);          
            int reg = srr.IndexOf("regular_summary", 0);
            int rege = srr.IndexOf("}", reg);
            string sreg = srr.Substring(reg, rege - reg);
            regK = new Regex(@"(?<=""kills"")+(:\w*)");
            resK = regK.Match(sreg);
            regW = new Regex(@"(?<=""wins"")+(:\w*)");
            resW = regW.Match(sreg);
            regG = new Regex(@"(?<=""global_rank"")+(:\w*)");
            resG = regG.Match(sreg);
            FileStream kills = new FileStream(@"kills.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(kills);
            writer.Write(resK.ToString());
            writer.Close();
            FileStream wins = new FileStream(@"wins.txt", FileMode.Create);
            writer = new StreamWriter(wins);
            writer.Write(resW.ToString());
            writer.Close();
            FileStream global = new FileStream(@"global.txt", FileMode.Create);
            writer = new StreamWriter(global);
            writer.Write(resG.ToString());
            writer.Close();
            if (hctestm.ToString() != ":null")
            {
                int hc = srr.IndexOf("hardcore_summary", 0);
                int hce = srr.IndexOf("}", hc);
                string shc = srr.Substring(hc, hce - hc);
                regK = new Regex(@"(?<=""kills"")+(:\w*)");
                resK = regK.Match(shc);
                regW = new Regex(@"(?<=""wins"")+(:\w*)");
                resW = regW.Match(shc);
                regG = new Regex(@"(?<=""global_rank"")+(:\w*)");
                resG = regG.Match(shc);
                kills = new FileStream(@"killshc.txt", FileMode.Create);
                writer = new StreamWriter(kills);
                writer.Write(resK.ToString());
                writer.Close();
                wins = new FileStream(@"winshc.txt", FileMode.Create);
                writer = new StreamWriter(wins);
                writer.Write(resW.ToString());
                writer.Close();
                global = new FileStream(@"globalhc.txt", FileMode.Create);
                writer = new StreamWriter(global);
                writer.Write(resG.ToString());
                writer.Close();
            }
            Invoke(act);
        }
        private void print() // Пишет слеши в лэйбл с каждым обновлением статы
        {
            label2.Text += " | ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
