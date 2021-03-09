using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TD3
{
    class MyImage
    {
        #region Instance de la classe MyImage
        string type;
        int taille;
        int offset;
        int largeur;
        int longueur;
        int nombreBitsParPixel;
        Pixel[,] matriceBGR;
        byte[] header;

        public int Largeur
        {
            get { return this.largeur; }
        }
        public int Longueur
        {
            get { return this.longueur; }
        }
        public byte[] Header
        {
            get { return this.header; }
        }
        #endregion

        #region Constructeur de la classe MyImage
        /// <summary>
        /// Constructeur de la classe MyImage qui lit un fichier ( .bmp).
        /// </summary>
        /// <param name="image">Nom de l'image</param>
        public MyImage(string image)
        {
            byte[] myfile = File.ReadAllBytes(image);

            if (myfile[0] == 66 && myfile[1] == 77)
            {
                this.type = "BM";

                this.taille = Convertir_Endian_To_Int(myfile, 2, 4);

                this.offset = Convertir_Endian_To_Int(myfile, 10, 4);

                this.largeur = Convertir_Endian_To_Int(myfile, 18, 4);

                this.longueur = Convertir_Endian_To_Int(myfile, 22, 4);

                this.nombreBitsParPixel = Convertir_Endian_To_Int(myfile, 28, 2);

                this.matriceBGR = new Pixel[this.longueur, this.largeur];

                this.header = new byte[this.offset];

                for (int i = 0; i < this.offset; i++)
                {
                    this.header[i] = myfile[i];
                }

                int cpt = this.offset;
                int[] rvb = new int[3];

                for (int i = 0; i < this.longueur; i++)
                {
                    for (int j = 0; j < this.largeur; j++)
                    {
                        for (int index = 0; index < 3; index++)
                        {
                            rvb[index] = myfile[cpt];
                            cpt++;
                        }
                        Pixel temp = new Pixel(rvb);
                        this.matriceBGR[i, j] = temp;
                    }
                    if ((this.longueur*3) % 4 != 0)
                    {
                        cpt += (this.longueur*3) % 4;
                    }
                }
            }
        }

        /// <summary>
        /// Constructeur de la classe Image à partir d'un header seulement
        /// </summary>
        /// <param name="header"></param>
        public MyImage(byte[] header)
        {
            this.header = header;
            if (header[0] == 66 && header[1] == 77)
            {
                this.type = "BM";

                this.taille = Convertir_Endian_To_Int(header, 2, 4);

                this.offset = Convertir_Endian_To_Int(header, 10, 4);

                this.largeur = Convertir_Endian_To_Int(header, 18, 4);

                this.longueur = Convertir_Endian_To_Int(header, 22, 4);

                if (this.largeur % 4 == 0 && this.longueur % 4 == 0)
                {
                    this.nombreBitsParPixel = Convertir_Endian_To_Int(header, 28, 2);
                }
            }
            this.matriceBGR = new Pixel[this.largeur, this.longueur];
        }

        /// <summary>
        /// Constructeur de la classe Image à partir d'un header et d'une matrice de pixels RGB
        /// </summary>
        /// <param name="header"></param>
        /// <param name="matriceBGR"></param>
        public MyImage(byte[] header, Pixel[,] matriceBGR)
        {
            this.header = header;
            if (header[0] == 66 && header[1] == 77)
            {
                this.type = "BM";

                this.taille = Convertir_Endian_To_Int(header, 2, 4);

                this.offset = Convertir_Endian_To_Int(header, 10, 4);

                this.largeur = Convertir_Endian_To_Int(header, 18, 4);

                this.longueur = Convertir_Endian_To_Int(header, 22, 4);

                if (this.largeur % 4 == 0 && this.longueur % 4 == 0)
                {
                    this.nombreBitsParPixel = Convertir_Endian_To_Int(header, 28, 2);
                }
            }
            this.matriceBGR = matriceBGR;
        }

        #endregion

        #region Méthode de la classe MyImage
        /// <summary>
        /// prend une instance de MyImage et la transforme en fichier binaire respectant la structure du fichier.bmp
        /// </summary>
        /// <param name="file">Emplacement du fichier en sortie</param>
        public void From_Image_To_File(string file)
        {
            byte[] bytes = new byte[this.taille];
            int index = 0;
            int bourrage = (this.matriceBGR.GetLength(1)*3) % 4;
            for (int i = 0; i < this.offset; i++)
            {
                bytes[index] = this.header[i];
                index++;
            }
            for (int i = 0; i < this.matriceBGR.GetLength(0); i++)
            {
                for (int j = 0; j < this.matriceBGR.GetLength(1); j++)
                {
                    bytes[index] = Convert.ToByte(this.matriceBGR[i, j].B);
                    index++;
                    bytes[index] = Convert.ToByte(this.matriceBGR[i, j].V);
                    index++;
                    bytes[index] = Convert.ToByte(this.matriceBGR[i, j].R);
                    index++;
                }
                if (bourrage != 0)
                {
                    for (int a = 0; a < bourrage; a++)
                    {
                        bytes[index] = 0x00;
                        index++;
                    }
                }
            }
            File.WriteAllBytes(file, bytes); //./Images/Sortie.bmp
        }

        /// <summary>
        /// Convertit une séquence d’octets au format little endian en entier.
        /// </summary>
        /// <param name="tab">tableau à convertir</param>
        /// <param name="debut">place dans le tableau,de 1 à..., du début de la sequence à decoder </param>
        /// <param name="nombre">nombre d'octect à décoder</param>
        /// <returns></returns>
        public int Convertir_Endian_To_Int(byte[] tab, int debut, int nombre)
        {
            double somme = 0;
            int cpt = 0;
            for (int i = debut; i < debut + nombre; i++)
            {
                somme += tab[i] * Math.Pow(256, cpt);
                cpt++;
            }
            return Convert.ToInt32(somme);
        }

        /// <summary>
        /// convertit un entier en séquence d’octets au format little endian.
        /// </summary>
        /// <param name="val">valeur à convertir</param>
        /// <param name="taille">nombre d'octect sur lequel transcrire la valeur</param>
        /// <returns></returns>
        public byte[] Convertir_Int_To_Endian(int val, int taille)
        {
            int test = 0;
            for (int i = 1; i <= taille; i++)
            {
                test += Convert.ToInt32(Math.Pow(256, i));
            }
            if (test < val) return null;
            else
            {
                byte[] tab = new byte[taille];
                while (val / 256 != 0)
                {
                    taille--;
                    tab[taille] = Convert.ToByte(val / Convert.ToInt32(Math.Pow(256, taille)));
                    val -= val % Convert.ToInt32(Math.Pow(256, taille));
                }
                return tab;
            }
        }

        public Pixel[,] MatriceBGR
        {
            get { return this.matriceBGR; }
            set { this.matriceBGR=value; }
        }

        #endregion
    }
}
