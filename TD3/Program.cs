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
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.ForegroundColor=ConsoleColor.DarkBlue;
            Console.WriteLine("Bienvenue dans le modfificateur d'image crée par Guillaume Bourg et Erwan-Henri Burlisson :");
            Console.WriteLine("Commençons par séléctionner l'image à modifier : (ex : ./Images/lac.bmp)");
            string nom1 = Console.ReadLine();
            MyImage image = new MyImage(nom1);
            Console.WriteLine("Rentrez le nombre corespondant à l'action que vous voulez réaliser :\n1) Transformez votre image en Nuance De Gris\n2) Transformez votre image en Noir et Blanc\n3) Appliquez un effet miroir à votre image");
            int n=Convert.ToInt32(Console.ReadLine());
            bool fin =false;
            while(fin==false)
            {
                switch (n)
                {
                    case 1 :
                        Console.Clear();
                        Console.WriteLine("Nous allons modifier votre image en nuance de gris :");
                        NuanceDeGris(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Console.ReadLine());
                        break;
                    case 2 :
                        Console.Clear();
                        Console.WriteLine("Nous allons modifier votre image en Noir et Blanc :");
                        Console.WriteLine("Si vous voulez mettre une valeur seuil tapez 0 sinon tapez 1 :(valeur seuil par défault : 128");
                        bool test = Convert.ToBoolean(Console.ReadLine());
                        if (test==false)
                        {
                            Console.WriteLine("Rentrez la valeur seuil souhaitée :");
                            int valeur =Convert.ToInt32(Console.ReadLine());
                            NoirEtBlanc(image,valeur);
                        }
                        else NoirEtBlanc(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Console.ReadLine());
                        break;
                    case 3 :
                        Console.Clear();
                        Console.WriteLine("Nous allons appliquez un effet miroir à votre image :");
                        Miroir(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Console.ReadLine());
                        break;
                }
            }
            Console.WriteLine("Comment voulez-vous appelez votre nouvelle image ? (ex : ./Images/lac2.bmp)");
            string nom2 = Console.ReadLine();
            image.From_Image_To_File(nom2);
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
            int[,][] matriceRGBMiroir =matriceRGB;
            for(int i=0;i<matriceRGB.GetLength(0);i++)
            {
                for(int j=0;j<matriceRGB.GetLength(1);j++)
                {
                    matriceRGBMiroir[i,j]=matriceRGB[matriceRGB.GetLength(0)-1-i,matriceRGB.GetLength(1)-1-j];
                }
            }
            image.MatriceRGB = matriceRGBMiroir;
        }

        #endregion

        #region Filtre (TD4)

        #endregion
    }
}
