using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.ComponentModel;

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
            bool menu_valide = true;

            while(fin==true)
            {
                menu_valide = true;
                Console.WriteLine("Rentrez le nombre corespondant à l'action que vous voulez réaliser :\n" +
                    "1) Transformez votre image en Nuance De Gris\n" +
                    "2) Transformez votre image en Noir et Blanc\n" +
                    "3) Appliquez un effet miroir à votre image\n" +
                    "4) Appliquer une rotation à l'image\n" +
                    "5) Filtre\n");
                int n=Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1 :
                        Console.Clear();
                        Console.WriteLine("Nous allons modifier votre image en nuance de gris :");
                        NuanceDeGris(image);
                        Console.Clear();
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
                        break;
                    case 3 :
                        Console.Clear();
                        Console.WriteLine("Nous allons appliquez un effet miroir à votre image :");
                        Miroir(image);
                        Console.Clear();
                        break;
                    case 4:
                        Console.Clear();
                        Console.WriteLine("********\nCette fonctionnalité n'est pas encore fonctionelle\n********");
                        menu_valide = false;
                        Console.ReadLine();
                        //int angle = Convert.ToInt32(Console.ReadLine());
                        //Rotation(image,angle);
                        Console.Clear();
                        break;
                    case 5:
                        Console.WriteLine("Rentrez le nombre corespondant au filtre que vous voulez appliquer :\n" +
                            "1) Detection de contour\n" +
                            "2) Renforcement des bords\n" +
                            "3) Flou\n" +
                            "4) Repoussage\n");

                        int a = Convert.ToInt32(Console.ReadLine());
                        switch (a)
                        {
                            case 1:
                                Console.WriteLine("Nous allons appliquez un filtre pour faire ressortir les countours de l'image :");
                                image = Filtre.Convolution(image, 1);
                                Console.Clear();
                                break;
                            case 2:
                                Console.WriteLine("Nous allons appliquez un filtre pour renforcer les bords de l'image :");
                                image = Filtre.Convolution(image, 2);
                                Console.Clear();
                                break;
                            case 3:
                                Console.WriteLine("Nous allons appliquez un flou à l'image:");
                                image = Filtre.Convolution(image, 3);
                                Console.Clear();
                                break;
                            case 4:
                                Console.WriteLine("Nous allons appliquez repoussage à l'image (pour faire apparaitre un relief):");
                                image = Filtre.Convolution(image, 4);
                                Console.Clear();
                                break;
                            default:
                                menu_valide = false;
                                Console.WriteLine("Le chiffre choisi ne fait pas partie du menu");
                                break;
                        }
                        break;
                    default:
                        menu_valide = false;
                        Console.WriteLine("Le chiffre choisi ne fait pas partie du menu");
                        break;
                }

                if (menu_valide)
                {
                    Console.WriteLine("Votre image a été modifiée, souhaitez vous l'afficher ?(1 pour oui et 0 pour non)");
                    afficher = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
                    if (afficher == true)
                    {
                        image.From_Image_To_File("./temp.bmp");
                        Process.Start("temp.bmp");
                        Console.WriteLine("Appuyer sur une touche pour avancer.");
                        Console.ReadKey();
                        File.Delete("./temp.bmp");
                        afficher = false;
                    }
                }
                Console.WriteLine("Souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                fin = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
            }
            Console.WriteLine("Comment voulez-vous appelez votre nouvelle image ? (ex : ./Images/lac2.bmp)");
            string nom2 = Console.ReadLine();
            image.From_Image_To_File(nom2);
        }

        #region Traitemement d'image (TD3)

        public static void NuanceDeGris(MyImage image)
        {
            Pixel[,] matriceBGR = image.MatriceBGR;
            int moyenne =0;
            for(int i=0;i<matriceBGR.GetLength(0);i++)
            {
                for(int j=0;j<matriceBGR.GetLength(1);j++)
                {
                    moyenne = (matriceBGR[i,j].R+matriceBGR[i,j].V+matriceBGR[i,j].B)/3;
                    matriceBGR[i,j].R=moyenne;
                    matriceBGR[i,j].V=moyenne;
                    matriceBGR[i,j].B=moyenne;
                    moyenne = 0;
                }
            }
            image.MatriceBGR = matriceBGR;
        }
        public static void NoirEtBlanc(MyImage image,int valeur=128)
        {
            Pixel[,] matriceBGR = image.MatriceBGR;
            int moyenne = 0;
            for(int i=0;i<matriceBGR.GetLength(0);i++)
            {
                for(int j=0;j<matriceBGR.GetLength(1);j++)
                {
                    moyenne = (matriceBGR[i,j].R+matriceBGR[i,j].V+matriceBGR[i,j].B)/3;
                    if(moyenne<valeur) moyenne=0;
                    else moyenne=255;
                    matriceBGR[i,j].R=moyenne;
                    matriceBGR[i,j].V=moyenne;
                    matriceBGR[i,j].B=moyenne;
                }
            }
            image.MatriceBGR = matriceBGR;
        }

        public static void Miroir(MyImage image)
        {
            Pixel[,] matriceBGR = image.MatriceBGR;
            Pixel[,] matriceBGRMiroir = new Pixel[matriceBGR.GetLength(0),matriceBGR.GetLength(1)];
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
            Pixel[,] matriceBGR = image.MatriceBGR;
            double longueur = Math.Ceiling(Math.Sin(angle) * matriceBGR.GetLength(0) + Math.Cos(angle) * matriceBGR.GetLength(1));
            double largeur = Math.Ceiling(Math.Cos(angle) * matriceBGR.GetLength(0) + Math.Sin(angle) * matriceBGR.GetLength(1));
            Pixel[,] matriceBGRRotation = new Pixel[Convert.ToInt32(Math.Abs(longueur)), Convert.ToInt32(Math.Abs(largeur))];
            for (int i = 0; i < matriceBGRRotation.GetLength(0); i++)
            {
                for (int j = 0; j < matriceBGRRotation.GetLength(1); j++)
                {
                    matriceBGRRotation[i, j] = new Pixel(0, 0, 0, true);
                }
            }
            for (int i = 0; i < matriceBGR.GetLength(0); i++)
            {
                for (int j = 0; j < matriceBGR.GetLength(1); j++)
                {
                    if (matriceBGR[i, j].PixelNoir != true)
                    {
                        matriceBGRRotation[Convert.ToInt32(Math.Abs(Math.Ceiling(Math.Sin(angle) * i + Math.Cos(angle) * j))), Convert.ToInt32(Math.Abs(Math.Ceiling(Math.Cos(angle) * i + Math.Sin(angle) * j)))] = matriceBGR[i, j];
                    }
                }
            }
            image.MatriceBGR = matriceBGRRotation;
        }

        #endregion

    }
}
