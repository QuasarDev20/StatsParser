using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Stats stats {
            get
            {  
                WebClient web = new WebClient();
                Stream str = web.OpenRead(textBox1.Text);
                StreamReader sr = new StreamReader(str);
                string srr = sr.ReadLine();
                return(JsonConvert.DeserializeObject<Stats>(srr));
            }
            set { } }
        public Form1()
        {
            InitializeComponent();
            load();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int timer=0; int.TryParse(textBox1.Text, out timer);
            long cycle = 15000 +  timer * 1000;
            System.Threading.Timer tmr = new System.Threading.Timer(Parse, null , 1000 , cycle);
            print();           
        }
        public void Parse(object data)    // Парсинг данных в файл + вызов print()
        {
            try
            {
                if (checkBox2.Checked)
                {
                    FileStream kills = new FileStream(@"kills.txt", FileMode.Create);
                    StreamWriter writer = new StreamWriter(kills);
                    writer.Write(stats.regular_summary.kills);
                    writer.Close();
                    FileStream wins = new FileStream(@"wins.txt", FileMode.Create);
                    writer = new StreamWriter(wins);
                    writer.Write(stats.regular_summary.wins);
                    writer.Close();
                    FileStream global = new FileStream(@"global.txt", FileMode.Create);
                    writer = new StreamWriter(global);
                    writer.Write(stats.regular_summary.global_rank);
                    writer.Close();
                }
                if (checkBox1.Checked)
                {
                    FileStream kills = new FileStream(@"killshc.txt", FileMode.Create);
                    StreamWriter writer = new StreamWriter(kills);
                    writer.Write(stats.hardcore_summary.kills);
                    writer.Close();
                    FileStream wins = new FileStream(@"winshc.txt", FileMode.Create);
                    writer = new StreamWriter(wins);
                    writer.Write(stats.hardcore_summary.wins);
                    writer.Close();
                    FileStream global = new FileStream(@"globalhc.txt", FileMode.Create);
                    writer = new StreamWriter(global);
                    writer.Write(stats.hardcore_summary.global_rank);
                    writer.Close();
                }
                if (checkBox3.Checked)
                {
                    FileStream nemeses = new FileStream(@"nemeses.txt", FileMode.Create);
                    StreamWriter writer = new StreamWriter(nemeses);
                    writer.Write(stats.nemeses[0].times_died);
                    writer.Close();
                }
                if (checkBox4.Checked)
                {
                    save();
                }             
            }
            catch { }
        }
        private void print() // Пишет ник из статы
        {
            label2.Text =stats.name;
        }
        private void save() // Сохраняет файл настроек
        {
            File.Delete(@"settings.txt");
            FileStream settings = new FileStream(@"settings.txt", FileMode.Create);
            StreamWriter writer = new StreamWriter(settings);
            writer.WriteLine(textBox1.Text);
            writer.Close();
        }
        private string load() // Загрузка ссылки из файла настроек
        {
            if (File.Exists(@"settings.txt"))
            {
                FileStream settings = new FileStream(@"settings.txt",FileMode.Open);
                StreamReader reader = new StreamReader(settings);
                textBox1.Text = reader.ReadLine();
                label4.Text = "Settings : found !";
                print();
            }
            return (textBox1.Text);

        }
        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public class Rank
        {
            public string rank { get; set; }
            public string color { get; set; }
        }

        public class HardcoreSummary
        {
            public int kills { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public string time_played { get; set; }
            public double win_points { get; set; }
            public double kill_points { get; set; }
            public string distance_moved { get; set; }
            public int global_rank { get; set; }
            public double total_points { get; set; }
            public Rank rank { get; set; }
            public int average_placement { get; set; }
        }
        public class RegularSummary
        {
            public int kills { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public string time_played { get; set; }
            public double win_points { get; set; }
            public double kill_points { get; set; }
            public string distance_moved { get; set; }
            public int global_rank { get; set; }
            public double total_points { get; set; }
            public Rank rank { get; set; }
            public int average_placement { get; set; }
        }

        public class LongestKill
        {
            public string name { get; set; }
            public string profile_url { get; set; }
            public double distance { get; set; }
            public string weapon { get; set; }
            public string match_id { get; set; }
            public string match_url { get; set; }
        }

        public class Victim
        {
            public string id { get; set; }
            public string name { get; set; }
            public string profile_url { get; set; }
            public int times_killed { get; set; }
        }

        public class Nemes
        {
            public string id { get; set; }
            public string name { get; set; }
            public string profile_url { get; set; }
            public int times_died { get; set; }
        }

        public class TopGun
        {
            public string name { get; set; }
            public int kills { get; set; }
        }

        public class Map
        {
            public string map { get; set; }
            public int times_played { get; set; }
            public double percentage { get; set; }
        }

        public class Stats
        {
            public string name { get; set; }
            public string uid { get; set; }
            public string profile_url { get; set; }
            public HardcoreSummary hardcore_summary { get; set; }
            public RegularSummary regular_summary { get; set; }
            public List<LongestKill> longest_kills { get; set; }
            public List<Victim> victims { get; set; }
            public List<Nemes> nemeses { get; set; }
            public List<TopGun> top_guns { get; set; }
            public List<Map> maps { get; set; }
        }
    }
}

