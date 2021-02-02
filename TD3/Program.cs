using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Program
    {
        static void Main(string[] args)
        {
            MyImage image = new MyImage("./Images/Test001.bmp");
            image.From_Image_To_File("./Images/File.bmp");
            Console.ReadKey();
        }

        #region Traitemement d'image (TD3)

        public void NuanceDeGris(MyImage image)
        {
            int[,][] matriceRGB = image.MatriceRGB;
            int moyenne =0;
            for(int i=0;i<matriceRGB.GetLength(0);i++)
            {
                for(int j=0;j<matriceRGB.GetLength(1);j++)
                {
                    moyenne = (matriceRGB[i,j][0]+matriceRGB[i,j][1]+matriceRGB[i,j][2])/3;
                    matriceRGB[i,j][0]=moyenne;
                    matriceRGB[i,j][1]=moyenne;
                    matriceRGB[i,j][2]=moyenne;
                }
            }
            image.MatriceRGB = matriceRGB;
        }
        public void NoirEtBlanc(MyImage image,int valeur=128)
        {
            int[,][] matriceRGB = image.MatriceRGB;
            for(int i=0;i<matriceRGB.GetLength(0);i++)
            {
                for(int j=0;j<matriceRGB.GetLength(1);j++)
                {
                    moyenne = (matriceRGB[i,j][0]+matriceRGB[i,j][1]+matriceRGB[i,j][2])/3;
                    if(moyenne<valeur) moyenne=0;
                    else moyenne=255;
                    matriceRGB[i,j][0]=moyenne;
                    matriceRGB[i,j][1]=moyenne;
                    matriceRGB[i,j][2]=moyenne;
                }
            }
            image.MatriceRGB = matriceRGB;
        }

        public void Miroir(MyImage image)
        {
            int[,][] matriceRGB = image.MatriceRGB;
        }

        #endregion

        #region Filtre (TD4)

        #endregion
    }
}
