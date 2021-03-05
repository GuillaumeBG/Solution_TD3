using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TD3
{
    class Filtre
    {
        #region Filtre (TD4)
        public static MyImage Convolution(MyImage image,int effet)
        {
            int[,] matriceConvultion = new int[3,3];
            switch (effet)
            {
                case 1: //detecction des contours
                    matriceConvultion = new int[,] { { 0, 1, 0 }, { 1, -4, 1 }, { 0, 1, 0 } };
                    break;
                case 2: //renforcement des bords
                    matriceConvultion = new int[,] { { 0, 0, 0 }, { -1, 1, 0 }, { 0, 0, 0 } };
                    break;
                case 3: //flou
                    matriceConvultion = new int[,] { { 0, 0, 0, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 1, 1, 0, 0 }, { 0, 1, 1, 1, 0 }, { 0, 1, 1, 1, 0 } };
                    break;
                case 4: //repoussage
                    matriceConvultion = new int[,] { { -2, -1, 0 }, { -1, 1, 1 }, { 0, 0, 0 } };
                    break;
            }
            //NuanceDeGris(image);
            int[,][] matrice = image.MatriceBGR;
            
            int[,][] nouvelleMatrice = new int[matrice.GetLongLength(0), matrice.GetLongLength(1)][];
            int ligne2 = -matriceConvultion.GetLength(0) / 2;
            int colonne2 = -matriceConvultion.GetLength(1) / 2;

            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    nouvelleMatrice[i, j] = new int[3];
                    for (int colonneConvulsion = 0; colonneConvulsion < matriceConvultion.GetLength(0); colonneConvulsion++)
                    {
                        for (int ligneConvulsion = 0; ligneConvulsion < matriceConvultion.GetLength(1); ligneConvulsion++)
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

                            nouvelleMatrice[i, j][0] += matrice[ligneMatriceInitial, colonneMatriceInitial][0] * matriceConvultion[ligneConvulsion, colonneConvulsion];
                            nouvelleMatrice[i, j][1] += matrice[ligneMatriceInitial, colonneMatriceInitial][1] * matriceConvultion[ligneConvulsion, colonneConvulsion];
                            nouvelleMatrice[i, j][2] += matrice[ligneMatriceInitial, colonneMatriceInitial][2] * matriceConvultion[ligneConvulsion, colonneConvulsion];
                            colonne2++;
                        }
                        colonne2 = -matriceConvultion.GetLength(1) / 2;
                        ligne2++;
                    }

                    if (nouvelleMatrice[i, j][0] < 0)
                    {
                        nouvelleMatrice[i, j][0] = 0;
                    }
                    else if (nouvelleMatrice[i, j][0] > 255)
                    {
                        nouvelleMatrice[i, j][0] = 255;
                    }

                    if (nouvelleMatrice[i, j][1] < 0)
                    {
                        nouvelleMatrice[i, j][1] = 0;
                    }
                    else if (nouvelleMatrice[i, j][1] > 255)
                    {
                        nouvelleMatrice[i, j][1] = 255;
                    }

                    if (nouvelleMatrice[i, j][2] < 0)
                    {
                        nouvelleMatrice[i, j][2] = 0;
                    }
                    else if (nouvelleMatrice[i, j][2] > 255)
                    {
                        nouvelleMatrice[i, j][2] = 255;
                    }

                    ligne2 = -matriceConvultion.GetLength(0) / 2;
                    colonne2 = -matriceConvultion.GetLength(1) / 2;
                }
            }
            MyImage newImage = new MyImage(image.Header, nouvelleMatrice);
            return newImage;
        }
        #endregion
    }
}
