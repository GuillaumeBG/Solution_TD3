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
        bool noir;
        #endregion

        #region Constructeur de la classe Pixel
        public Pixel(int[] tab,bool noir=false)
        {
            if (tab.Length==3)
            {
                this.r = tab[2];
                this.v = tab[1];
                this.b = tab[0];
                this.noir = noir;
            }
        }
        public Pixel(int r,int v,int b, bool noir = false)
        {
            this.r = r;
            this.v = v;
            this.b = b;
            this.noir = noir;
        }
        #endregion

        #region Methode 
        public int R
        {
            get { return this.r; }
            set { this.r = value; }
        }
        public int V
        {
            get { return this.v; }
            set { this.v = value; }
        }
        public int B
        {
            get { return this.b; }
            set { this.b = value; }
        }
        public int[] RVB
        {
            get { int[] tab = new int[]{this.r,this.v,this.b}; return tab; }
            set { this.r = value[2];
                this.v = value[1];
                this.b = value[0];}
        }
        public bool PixelNoir
        {
            get { return this.noir; }
            set { this.noir = value; }
        }
        #endregion
    }
}
