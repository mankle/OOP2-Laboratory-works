using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOP2Lab4
{
    public class Agentas: IComparable<Agentas>, IEquatable<Agentas>
    {
        public string kodas { get; private set; }
        public string pavarde { get; private set; }
        public string vardas { get; private set; }
        public List<int> kruvis { get; private set; }

        /// <summary>
        /// konstruktorius 
        /// </summary>
        /// <param name="kodas"> agento kodas </param>
        public Agentas(string kodas, string pavarde, string vardas)
        {
            this.kodas = kodas;
            this.pavarde = pavarde;
            this.vardas = vardas;
            kruvis = new List<int>(); //new int[12];
            for (int i = 0; i < 12; i++)
            {
                kruvis.Add(0);
            }
        }

        public int CompareTo(Agentas kitas)
        {
            return string.Compare(pavarde, kitas.pavarde);
        }
        
        public bool Equals(Agentas kitas)
        {
            return kodas == kitas.kodas;
        }

        
        public void PridetiKruvi(List<bool> menesiai, int prenumeratos)
        {
            for (int i = 0; i < 12; i++)
            {
                if (menesiai[i])
                {
                    kruvis[i] += prenumeratos;
                }
            }
        }

        public override string ToString()
        {
            return kodas + " " + pavarde + " " + vardas + "\n";
        }

        public string GetString(int menesis)
        {
            return kodas + " " + pavarde + " " + vardas + " " + kruvis[menesis - 1] + "\n";
        }
        
    }
}