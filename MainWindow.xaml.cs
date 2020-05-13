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

namespace Backpropagation
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
{
        private Bitmap bitmapImagem;

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
            }
        }

        private void btnRodar_Click(object sender, RoutedEventArgs e) {

        }

        private void btnTreinar_Click(object sender, RoutedEventArgs e) {

        }
    }
}