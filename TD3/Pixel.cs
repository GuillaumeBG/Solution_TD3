using System;
using System.CodeDom;

namespace TD2
{
    public class Pixel
    {
        #region Instance de la classe Pixel
        int r;
        int v;
        int b;
        #endregion

        #region Constructeur de la classe Pixel
        public Pixel(int[] tab)
        {
            if (tab.Length==3)
            {
                this.r = tab[0];
                this.v = tab[1];
                this.b = tab[2];
            }
        }
        #endregion

        #region Methode 
        public int Pixelr
        {
            get { return this.r; }
            set { this.r = value; }
        }
        public int Pixelv
        {
            get { return this.v; }
            set { this.v = value; }
        }
        public int Pixelb
        {
            get { return this.b; }
            set { this.b = value; }
        }
        public int[] Pixelrvb
        {
            get { return this.r+this.v+this.b; }
            set { this.r = value[0];
                this.v = value[1];
                this.b = value[2];}
        }
        #endregion
    }
}
