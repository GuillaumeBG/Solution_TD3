using System;
using System.CodeDom;

namespace TD3
{
    class Pixel
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
                this.r = tab[2];
                this.v = tab[1];
                this.b = tab[0];
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
            get { int[] tab = new int[]{this.r,this.v,this.b}; return tab; }
            set { this.r = value[2];
                this.v = value[1];
                this.b = value[0];}
        }
        #endregion
    }
}
