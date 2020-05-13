using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpropagation
{
    class Sobel
    {
        public Image sobel(Bitmap img)
        {
            Bitmap imagem = new Bitmap(img);
            Bitmap retorno = new Bitmap(img);
            int width = imagem.Width;
            int height = imagem.Height;
            int[,] mascaraX = new int[3, 3];
            int[,] mascaraY = new int[3, 3];
            int[,] pixR = new int[width, height];
            int[,] pixG = new int[width, height];
            int[,] pixB = new int[width, height];
            int novoY_R, novoY_G, novoY_B, novoX_R, novoX_G, novoX_B;
            int lim = 128 * 128;

            mascaraX[0, 0] = 1;
            mascaraX[0, 1] = -2;
            mascaraX[0, 2] = -1;
            mascaraX[1, 0] = 0;
            mascaraX[1, 1] = 0;
            mascaraX[1, 2] = 0;
            mascaraX[2, 0] = 1;
            mascaraX[2, 1] = 2;
            mascaraX[2, 2] = 1;

            mascaraY[0, 0] = -1;
            mascaraY[0, 1] = 0;
            mascaraY[0, 2] = 1;
            mascaraY[1, 0] = -2;
            mascaraY[1, 1] = 0;
            mascaraY[1, 2] = 2;
            mascaraY[2, 0] = -1;
            mascaraY[2, 1] = 0;
            mascaraY[2, 2] = 1;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color cor = imagem.GetPixel(j, i);
                    pixR[j, i] = cor.R;
                    pixG[j, i] = cor.G;
                    pixB[j, i] = cor.B;
                }
            }

            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    novoY_R = 0;
                    novoY_G = 0;
                    novoY_B = 0;
                    novoX_R = 0;
                    novoX_G = 0;
                    novoX_B = 0;

                    for (int l = -1; l < 2; l++)
                    {
                        for (int c = -1; c < 2; c++)
                        {
                            novoY_R += mascaraY[l + 1, c + 1] * pixR[j + l, i + c];
                            novoY_G += mascaraY[l + 1, c + 1] * pixG[j + l, i + c];
                            novoY_B += mascaraY[l + 1, c + 1] * pixB[j + l, i + c];
                            novoX_R += mascaraX[l + 1, c + 1] * pixR[j + l, i + c];
                            novoX_G += mascaraX[l + 1, c + 1] * pixG[j + l, i + c];
                            novoX_B += mascaraX[l + 1, c + 1] * pixB[j + l, i + c];
                        }
                    }

                    if (novoX_R * novoX_R + novoY_R * novoY_R > lim || novoX_G * novoX_G + novoY_G * novoY_G > lim || novoX_B * novoX_B + novoY_B * novoY_B > lim)
                    {
                        retorno.SetPixel(j, i, Color.Transparent);
                    }
                    else
                    {
                        retorno.SetPixel(j, i, Color.Black);
                    }

                }
            }

            return retorno;
        }
    }
}
