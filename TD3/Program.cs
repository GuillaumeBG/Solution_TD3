﻿using System;
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

        #region Traitemement d'image 

        public MyImage NuanceDeGris(MyImage image)
        {
                int[,][] matriceRGB = image.MatriceRGB;
        }

        #endregion

        #region Filtre 

        #endregion
    }
}
