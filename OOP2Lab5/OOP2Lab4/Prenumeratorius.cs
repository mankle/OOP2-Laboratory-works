using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOP2Lab4
{
    public class Prenumeratorius: IComparable<Prenumeratorius>, IEquatable<Prenumeratorius>
    {
        public DateTime ivedimoData { get; private set; }
        public string pavarde { get; private set; }
        public string adresas { get; private set; }
        public int pradzia { get; private set; }
        public int laikotarpioIlgis { get; private set; }
        public int leidiniuKiekis { get; private set; }
        public string kodas { get; private set; }
        public List<bool> menesiai { get; private set; }

        /// <summary>
        /// konstruktorius
        /// </summary>
        public Prenumeratorius(DateTime ivedimoData, string pavarde, string adresas, int pradzia,
            int laikotarpioIlgis, int leidiniuKiekis, string kodas)
        {
            this.ivedimoData = ivedimoData;
            this.pavarde = pavarde;
            this.adresas = adresas;
            this.pradzia = pradzia;
            this.laikotarpioIlgis = laikotarpioIlgis;
            this.leidiniuKiekis = leidiniuKiekis;
            this.kodas = kodas;
            menesiai = new List<bool>();
            SuskaiciuotiMenesius();
        }
        
        public int CompareTo(Prenumeratorius kitas)
        {
            if (string.Compare(adresas, kitas.adresas) != 0)
                return string.Compare(adresas, kitas.adresas);
            else return string.Compare(pavarde, kitas.pavarde);
        }

        public bool Equals(Prenumeratorius kitas)
        {
            return pavarde == kitas.pavarde && adresas == kitas.adresas;
        }

        /// <summary>
        /// suskaiciuojama kuriais menesiais buvo uzsiprenumeraves
        /// </summary>
        public void SuskaiciuotiMenesius()
        {
            for (int i = 0; i < 12; i++)
            {
                menesiai.Add(false);
            }
            for (int i = pradzia - 1; i < pradzia + laikotarpioIlgis - 1 && i < 12; i++)
            {
                menesiai[i] = true;
            }           
        }

        public override string ToString()
        {
            return adresas + " " + pavarde + "\n";
        }
    }
}