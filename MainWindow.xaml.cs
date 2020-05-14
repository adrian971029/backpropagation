using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.IO;

namespace Backpropagation
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private Bitmap bitmapImagem;
        public readonly int EPOCAS_TREINAMENTO = 10000;

        internal static double[][][] DADOS = new double[][][]
        {
            new double[][]
            {
                new double[] {0, 0},
                 new double[] {0}
            },
            new double[][]
            {
               new double[] {0, 1},
                new double[] {1}
            },
            new double[][]
            {
                new double[] {1, 0},
                new double[] {1}
            },
            new double[][]
            {
                new double[] {1, 1},
                new double[] {0}
            }

        };





        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnImagem_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog abrirImagem = new OpenFileDialog();
            abrirImagem.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (abrirImagem.ShowDialog() == true) {
                imgImagem.Source = new BitmapImage(new Uri(abrirImagem.FileName));
                bitmapImagem = new Bitmap(abrirImagem.FileName);
                pegacor();
            }
        }

        private void btnRodar_Click(object sender, RoutedEventArgs e) {
            StreamWriter x;
            string CaminhoNome = "D:\\Rodrigo\\Desktop\\VERIFICAR\\execucao.txt";
            x = File.CreateText(CaminhoNome);
            BackpropagationClass backpropagation = new BackpropagationClass();
            double[] result = new double[] { 0, 0, 0, 0 };
            for (int i = 0; i < DADOS.Length; i++)
            {
                result[i] = backpropagation.busca(DADOS[i][0]).getNeuronios()[BackpropagationClass.NEURONIO_ENTRADA + BackpropagationClass.NEURONIO_OCULTO].getSaida();
                x.WriteLine(result[i]); ;
            }
            x.WriteLine("fIM");
            x.Close();
        }

        private void btnTreinar_Click(object sender, RoutedEventArgs e) {
            StreamWriter x;
            string CaminhoNome = "D:\\Rodrigo\\Desktop\\VERIFICAR\\treinamento.txt";
            x = File.CreateText(CaminhoNome);
            BackpropagationClass backpropagation = new BackpropagationClass();
            for (int j = 1; j < EPOCAS_TREINAMENTO; j++)
            {
                x.WriteLine("epoca: " + j);
                
                for (int i = 0; i < DADOS.Length; i++)
                {
                    x.WriteLine(backpropagation.busca(DADOS[i][0]).recalculaErro(DADOS[i][1][0]));
                }
            }

            
            x.WriteLine("fIM");
            x.Close();
        }

        private void pegacor()
        {
            Sobel sobel = new Sobel();
            System.Drawing.Image imgFinal = sobel.sobel(bitmapImagem);

            Bitmap imagem = new Bitmap(imgFinal);
            int width = imagem.Width;
            int height = imagem.Height;
            int[] freq = new int[256];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    System.Drawing.Color cor = imagem.GetPixel(j, i);
                    freq[cor.R]++;
                }
            }


            StreamWriter x;
            string CaminhoNome = "D:\\Rodrigo\\Desktop\\VERIFICAR\\cores.txt";
            x = File.CreateText(CaminhoNome);
            for (int i = 0; i < freq.Length; i++)
            {
                x.WriteLine(i + ": " + freq[i]); ;
            }
            x.WriteLine("fIM");
            x.Close();

        }
    }
}



//https://www.youtube.com/watch?v=qWLjKsgo3sE