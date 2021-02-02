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
            Console.WriteLine("Bienvenue dans le modfificateur d'image crée par Guillaume Bourg et Erwan-Henri Burlisson :");
            Console.ForegroundColor=ConsoleColor.Blue;
            Console.WriteLine("Commençons par séléctionner l'image à modifier : (ex : ./Images/lac.bmp)");
            string nom1 = Console.ReadLine();
            MyImage image = new MyImage(nom1);
            bool fin =true;
            while(fin==true)
            {
                Console.WriteLine("Rentrez le nombre corespondant à l'action que vous voulez réaliser :\n" +
                    "1) Transformez votre image en Nuance De Gris\n" +
                    "2) Transformez votre image en Noir et Blanc\n" +
                    "3) Appliquez un effet miroir à votre image\n" +
                    "4) A Faire\n" +
                    "5) Detection de contour\n");
                int n=Convert.ToInt32(Console.ReadLine());
                switch (n)
                {
                    case 1 :
                        Console.Clear();
                        Console.WriteLine("Nous allons modifier votre image en nuance de gris :");
                        NuanceDeGris(image);
                        Console.Clear();
                        Console.WriteLine("Votre image a été modifiée, souhaitez-vous continuer à modifier votre image ? (1 pour oui et 0 pour non)");
                        fin = Convert.ToBoolean(Convert.ToInt32(Console.ReadLine()));
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
                    case 5:
                        Console.Clear();
                        Console.WriteLine("Nous allons appliquez un filtre pour faire ressortir les countours de l'image :");
                        Filtre(image);
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

        public static void NuanceDeGris(MyImage image)
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
        public static void NoirEtBlanc(MyImage image,int valeur=128)
        {
            int[,][] matriceRGB = image.MatriceRGB;
            int moyenne = 0;
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

        public static void Miroir(MyImage image)
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
        public static MyImage Filtre(MyImage image)
        {
            int[,][] matrice = image.MatriceRGB;
            int[,] matriceConvultion = new int[,] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } };
            int[,][] nouvelleMatrice = new int[matrice.GetLongLength(0), matrice.GetLongLength(1)][];
            int ligne2 = -1;
            int colonne2 = -1;

            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    for (int ligneConvulsion = 0; ligneConvulsion < 3; ligneConvulsion++)
                    {
                        for (int colonneConvulsion = 0; colonneConvulsion < 3; colonneConvulsion++)
                        {
                            int ligneMatriceInitial = i + ligne2;
                            int colonneMatriceInitial = j + colonne2;

                            if (ligneMatriceInitial < 0)
                            {
                                ligneMatriceInitial += matrice.GetLength(0);
                            }
                            else if (ligneMatriceInitial >= matrice.GetLength(0))
                            {
                                ligneMatriceInitial -= matrice.GetLength(0);
                            }
                            if (colonneMatriceInitial < 0)
                            {
                                colonneMatriceInitial += matrice.GetLength(1);
                            }
                            else if (colonneMatriceInitial >= matrice.GetLength(1))
                            {
                                colonneMatriceInitial -= matrice.GetLength(1);
                            }

                            nouvelleMatrice[i, j] = new int[3];
                            nouvelleMatrice[i, j][0] += matrice[ligneMatriceInitial, colonneMatriceInitial][0] * matriceConvultion[ligneConvulsion, colonneConvulsion];
                            nouvelleMatrice[i, j][1] += matrice[ligneMatriceInitial, colonneMatriceInitial][1] * matriceConvultion[ligneConvulsion, colonneConvulsion];
                            nouvelleMatrice[i, j][2] += matrice[ligneMatriceInitial, colonneMatriceInitial][2] * matriceConvultion[ligneConvulsion, colonneConvulsion];
                            colonne2++;
                        }
                        colonne2 = -1;
                        ligne2++;
                    }
                    nouvelleMatrice[i, j][0] /= 9;
                    nouvelleMatrice[i, j][1] /= 9;
                    nouvelleMatrice[i, j][2] /= 9;
                    ligne2 = -1;
                    colonne2 = -1;
                }
            }
            image = new MyImage(image.Header, nouvelleMatrice);
            return image;
        }
        #endregion
    }
}
