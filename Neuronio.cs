using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpropagation
{
    public class Neuronio
    {
       //representara um neuronio do Backpropagation

        //tipo de camada a qual o neuronio pertence
        private Backpropagation.LayerType layerType;
        //limiar definida aleatoriamente TODO: entender o que e
        private double threshold = 0.5 - 1;//TODO: substituir 1 por valor aleatorio
        //pesos definidos aleatoriamente
        private double[] peso = { 0.5 - 1, 0.5 - 1 };//TODO: substituir 1 por valor aleatorio
        //saida
        private double saida = 0;
        //erro
        private double erro = 0;

        public Neuronio(Backpropagation.LayerType layerType)
        {
            this.layerType = layerType;
        }

        //no final o resultado passa por uma funcao de ativacao, sendo ela sigmoide ou limiar(rele)
        public void sigmoide(double weightedSum)
        {
            saida = 1.0 / (1 + Math.Exp(-1.0 * weightedSum));
        }

        //calcula a derivada
        public double derivada()
        {
            return saida * (1.0 * saida);
        }

        public Backpropagation.LayerType getLayerType()
        {
            return layerType;
        }

        public void setLayerType(Backpropagation.LayerType layerType)
        {
            this.layerType = layerType;
        }

        public double getThreshold()
        {
            return threshold;
        }

        public void setThreshold(double threshold)
        {
            this.threshold = threshold;
        }

        public double[] getPeso()
        {
            return peso;
        }

        public void setPeso(double[] peso)
        {
            this.peso = peso;
        }

        public double getSaida()
        {
            return saida;
        }

        public void setSaida(double saida)
        {
            this.saida = saida;
        }

        public double getErro()
        {
            return erro;
        }

        public void setErro(double erro)
        {
            this.erro = erro;
        }


    }

  
    
}
