using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;


namespace OOP2Lab4
{
    public partial class Form1 : System.Web.UI.Page
    {
        public const string fileA = "~/Agentai.txt";
        public const string fileP = "*P.txt";
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// Nuskaito agentu duomenu faila ir issaugo duomenis duotame sarase
        /// </summary>
        /// <param name="file"> duomenu failas </param>
        /// <param name="AgentuSarasas"> agentu sarasas i kuri saugoma informacija </param>
        public void ReadDataA(string file, ref List<Agentas> AgentuSarasas)
        {
            
            string[] lines = File.ReadAllLines(file);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                AgentuSarasas.Add(new Agentas(parts[0], parts[1], parts[2]));
            }

        }


        /// <summary>
        /// Nuskaito prenumeratoriu duomenu failus ir issaugo duomenis duotame sarase
        /// </summary>
        /// <param name="filep"> failo pavadinimo pabaiga </param>
        /// <param name="PrenumeratoriuSarasas"> prenumeratoriu sarasas i kuri saugoma informacija </param>
        public void ReadDataP(string filep, ref List<Prenumeratorius> PrenumeratoriuSarasas)
        {

            string[] files = Directory.GetFiles(Server.MapPath(""), filep);
            foreach (var file in files)
            {
                string[] lines = File.ReadAllLines(file);
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(' ');
                    PrenumeratoriuSarasas.Add(new Prenumeratorius(DateTime.Parse(lines[0]), parts[0], parts[1],
                        int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]), parts[5]));
                }
            }
            
        }

        



        /// <summary>
        /// Prideti kiekvieno agento visu menesiu kruvius
        /// </summary>
        /// <param name="listA"> agentu sarasas </param>
        /// <param name="listP"> prenumeratoriu sarasas </param>
        public void PridetiAgentuKruvi(ref List<Agentas> listA, List<Prenumeratorius> listP)
        {
            foreach (Agentas agentas in listA)
            {
                IEnumerable< Prenumeratorius > prenumeratoriai = from pre in listP
                                                                 where pre.kodas == agentas.kodas
                                                                 select pre;
                foreach (Prenumeratorius pre in prenumeratoriai)
                {
                    agentas.PridetiKruvi(pre.menesiai, pre.leidiniuKiekis);
                }
            }
        }

        /// <summary>
        /// Atspausdinti agentu informacija TextBox2 lauke nurodyto menesio metu
        /// </summary>
        /// <param name="listA"> agentu sarasas </param>
        /// <param name="m"> menesis </param>
        public void AtspausdintiAgentu(List<Agentas> listA, int m)
        {
            TextBox2.Text = "";
            foreach (Agentas agentas in listA)
            {
                TextBox2.Text += agentas.GetString(m);
            }
        }


        /// <summary>
        /// Atspausdinti nurodyto agento kruvi TextBox3 lauke nurodyto menesio metu
        /// </summary>
        /// <param name="listA"> Agentu sarasas </param>
        /// <param name="listP"> Prenumeratoriu sarasas </param>
        /// <param name="menesis"></param>
        public void AtspausdintiAgentoKruvi(List<Agentas> listA, List<Prenumeratorius> listP, int menesis)
        {
            TextBox4.Text = "";
            bool yra = false;
            string kodas = TextBox3.Text;
            Agentas a;
            foreach (Agentas agentas in listA)
            {
                if (agentas.kodas == kodas)
                {
                    a = agentas;
                    yra = true;
                    TextBox4.Text += a.ToString();
                    break;
                }
            }
            if (!yra)
            {
                throw new Exception(string.Format("Agento kurio kodas {0} nera siame sarase.", kodas));
            }
            else
            {
                TextBox4.Text += "Prenumeratoriai kuriems pristate " + menesis + " menesi. \n";
                var pre = listP.Where(p => p.kodas == kodas);
                foreach (Prenumeratorius p in pre)
                {
                    TextBox4.Text += p.ToString();
                }

                /*foreach (Prenumeratorius prenumeratorius in listP)
                {
                    if (prenumeratorius.kodas==kodas)
                    {
                        TextBox4.Text += prenumeratorius.ToString();
                    }
                }*/
            }
        }

       
        protected void Button1_Click(object sender, EventArgs e)
        {
            List<Prenumeratorius> Prenumeratoriai = new List<Prenumeratorius>();
            List<Agentas> Agentai = new List<Agentas>();
            //nuskaitymas
            ReadDataA(Server.MapPath(fileA), ref Agentai);
            ReadDataP(fileP, ref Prenumeratoriai);
            //rikiavimas
            Prenumeratoriai = Prenumeratoriai.OrderBy(x => x.adresas).ThenBy(x => x.pavarde).ToList();
            Agentai = Agentai.OrderBy(x => x.pavarde).ToList();
            /*Prenumeratoriai.Sort();
            Agentai.Sort();*/

            //pridedamas agentu kruvis
            PridetiAgentuKruvi(ref Agentai, Prenumeratoriai);
            //menesio nuskaitymas 
            int menesis;
            if (!int.TryParse(TextBox1.Text, out menesis))
            {
                throw new Exception(string.Format(" {0} nera skaicius.", TextBox1.Text));
            }
            else
            {
                if (menesis < 1 || menesis > 12)
                {
                    throw new Exception(string.Format(" {0} nera skaicius nurodantis menesi.", menesis));
                }
                else
                {
                    //jei menesio nuskaitymas pavyksta: atspausdinami visi agentai
                    AtspausdintiAgentu(Agentai, menesis);
                    Panel2.Visible = true;  

                }
            }
            //duomenysi saugomi i session A ir P
            Session["A"] = Agentai;
            Session["P"] = Prenumeratoriai;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            List<Prenumeratorius> Prenumeratoriai = Session["P"] as List<Prenumeratorius>;
            List<Agentas> Agentai = Session["A"] as List<Agentas>;
            AtspausdintiAgentoKruvi(Agentai, Prenumeratoriai, int.Parse(TextBox1.Text));
        }

        delegate void MyDel(string s);

        /// <summary>
        /// Atspausdina pradinius duomenis i teksto lauka
        /// </summary>
        protected void PrintDataToTextBox()
        {
            TextBox5.Text = "";
            MyDel print = (string s) =>
            {
                string[] files = Directory.GetFiles(Server.MapPath(""), s);
                foreach (var file in files)
                {

                    TextBox5.Text += "_________________________________________\n";
                    TextBox5.Text += file + "\n\n";
                    string[] lines = File.ReadAllLines(file);
                    foreach (string line in lines)
                    {
                        TextBox5.Text += line + "\n";
                    }
                    TextBox5.Text += "_________________________________________\n";
                }
            };
            print("Agentai.txt");
            print(fileP);
            Panel3.Visible = !Panel3.Visible;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            PrintDataToTextBox();
        }
    }
}
 