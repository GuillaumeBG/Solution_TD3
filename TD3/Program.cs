using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor=ConsoleColor.DarkRed;
            Console.WriteLine("Bienvenue dans le modfificateur d'image crée par Guillaume Bourg et Erwan-Henri Burlisson :");
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("Commençons par séléctionner l'image à modifier : (ex : ./Images/lac.bmp)");
            string nom1 = Console.ReadLine();
            MyImage image = new MyImage(nom1);
            bool fin =true;
            bool afficher=false;
            while(fin==true)
            {
                Console.WriteLine("Rentrez le nombre corespondant à l'action que vous voulez réaliser :\n" +
                    "1) Transformez votre image en Nuance De Gris\n" +
                    "2) Transformez votre image en Noir et Blanc\n" +
                    "3) Appliquez un effet miroir à votre image\n" +
                    "4) Appliquer une rotation à l'image\n" +
                    "5) Detection de contour\n");
                int n=Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1 :
                        Console.Clear();
                        Console.WriteLine("Nous allons modifier votre image en nuance de gris :");
                        NuanceDeGris(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez vous l'afficher ?(1 pour oui et 0 pour non)");
                        afficher = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                        if (afficher==true)
                        {
                            image.From_Image_To_File("./temp.bmp");
                            Process.Start("./temp.bmp");
                            Console.WriteLine("Appuyer sur une touche pour avancer.");
                            Console.ReadKey();
                            File.Delete("./temp.bmp");
                            afficher=false;
                        }
                        Console.WriteLine("Souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 2 :
                        Console.Clear();
                        Console.WriteLine("Nous allons modifier votre image en Noir et Blanc :");
                        Console.WriteLine("Si vous voulez mettre une valeur seuil tapez 1 sinon tapez 0 :(valeur seuil par défault : 128)");
                        bool test = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                        if (test==true)
                        {
                            Console.WriteLine("Rentrez la valeur seuil souhaitée :");
                            int valeur =Convert.ToInt32(Console.ReadLine());
                            NoirEtBlanc(image,valeur);
                        }
                        else NoirEtBlanc(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 3 :
                        Console.Clear();
                        Console.WriteLine("Nous allons appliquez un effet miroir à votre image :");
                        Miroir(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("Nous allons faire tourner l'image selon un angle compris entre 0 et 360 :");
                        int angle = Convert.ToInt32(Console.ReadLine());
                        Rotation(image,angle);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Console.ReadLine());
                        break;
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Nous allons appliquez un filtre pour faire ressortir les countours de l'image :");
                        image = Filtre.Convolution(image,1);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                        break;
                     
                }
            }
            Console.WriteLine("Comment voulez-vous appelez votre nouvelle image ? (ex : ./Images/lac2.bmp)");
            string nom2 = Console.ReadLine();
            image.From_Image_To_File(nom2);
        }

        #region Traitemement d'image (TD3)

        public static void NuanceDeGris(MyImage image)
        {
            int[,][] matriceBGR = image.MatriceBGR;
            int moyenne =0;
            for(int i=0;i<matriceBGR.GetLength(0);i++)
            {
                for(int j=0;j<matriceBGR.GetLength(1);j++)
                {
                    moyenne = (matriceBGR[i,j][0]+matriceBGR[i,j][1]+matriceBGR[i,j][2])/3;
                    matriceBGR[i,j][0]=moyenne;
                    matriceBGR[i,j][1]=moyenne;
                    matriceBGR[i,j][2]=moyenne;
                    moyenne =0;
                }
            }
            image.MatriceBGR = matriceBGR;
        }
        public static void NoirEtBlanc(MyImage image,int valeur=128)
        {
            int[,][] matriceBGR = image.MatriceBGR;
            int moyenne = 0;
            for(int i=0;i<matriceBGR.GetLength(0);i++)
            {
                for(int j=0;j<matriceBGR.GetLength(1);j++)
                {
                    moyenne = (matriceBGR[i,j][0]+matriceBGR[i,j][1]+matriceBGR[i,j][2])/3;
                    if(moyenne<valeur) moyenne=0;
                    else moyenne=255;
                    matriceBGR[i,j][0]=moyenne;
                    matriceBGR[i,j][1]=moyenne;
                    matriceBGR[i,j][2]=moyenne;
                }
            }
            image.MatriceBGR = matriceBGR;
        }

        public static void Miroir(MyImage image)
        {
            int[,][] matriceBGR = image.MatriceBGR;
            int[,][] matriceBGRMiroir =new int[matriceBGR.GetLength(0),matriceBGR.GetLength(1)][];
            for(int i=0;i<matriceBGR.GetLength(0);i++)
            {
                for(int j=0;j<matriceBGR.GetLength(1);j++)
                {
                    matriceBGRMiroir[i,j]=matriceBGR[i,matriceBGR.GetLength(1)-1-j];
                }
            }
            image.MatriceBGR = matriceBGRMiroir;
        }

        public static void Rotation(MyImage image, int angle)
        {
            int[,][] matriceBGR = image.MatriceBGR;
            int[,][] matriceBGRMiroir =new int[matriceBGR.GetLength(0),matriceBGR.GetLength(1)][];
            for(int i=0;i<matriceBGR.GetLength(0);i++)
            {
                for(int j=0;j<matriceBGR.GetLength(1);j++)
                {
                    matriceBGRMiroir[i,j]=new int[]{0,0,0};
                }
            }
        }

        #endregion

    }
}
