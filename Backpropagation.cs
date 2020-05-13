using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backpropagation
{
    public class BackpropagationClass
    {
        public enum LayerType { E, O, S }
        //E -> camada de entrada
        //O -> camada de oculta
        //S -> camada de saida

        //possui dois neuronios na entrada, e dois na oculto
        readonly static int NEURONIO_ENTRADA = 2;
        readonly static int NEURONIO_OCULTO = 2;
        readonly static int NEURONIO_SAIDA = 1;
        readonly static double TAXA_APRENDIZAGEM = 0.8;

        //cria uma matriz de neuronios
        private Neuronio[] neuronios = new Neuronio[NEURONIO_ENTRADA + NEURONIO_OCULTO + NEURONIO_SAIDA];

        //vai instaciar os neuronis de entrada, oculto e saida
        public BackpropagationClass()
        {
            for (int i = 0; i < NEURONIO_ENTRADA; i++)
            {
                neuronios[i] = new Neuronio(LayerType.E);
            }
            for (int i = NEURONIO_ENTRADA; i < NEURONIO_ENTRADA+NEURONIO_OCULTO; i++)
            {
                neuronios[i] = new Neuronio(LayerType.O);
            }
            neuronios[NEURONIO_ENTRADA + NEURONIO_OCULTO] = new Neuronio(LayerType.S);
        }

        public Neuronio[] getNeuronios()
        {
            return neuronios;
        }

        //executa a rede TODO: entender
        public BackpropagationClass busca(double []entrada)
        {
            double weightedSum = 0;

            //percorre os neuronios
            for (int i = 0; i < neuronios.Length; i++)
            {
                switch (neuronios[i].getLayerType())
                {
                    //caso o neuronio seja de entrada
                    case LayerType.E:
                        neuronios[i].setSaida(entrada[i]);
                        break;
                    //caso o neuronio seja oculto
                    case LayerType.O:
                        //calcula a soma ponderada
                        weightedSum = neuronios[i].getThreshold() +
                                neuronios[i].getPeso()[0] * neuronios[0].getSaida() +
                                neuronios[i].getPeso()[1] * neuronios[1].getSaida();

                        //calcula a saida com base no resultado da soma pondeirada
                        neuronios[i].sigmoide(weightedSum);
                        break;
                    //caso o neuronio seja de saida
                    case LayerType.S:
                        //calcula a soma pondeirada
                        weightedSum = neuronios[i].getThreshold() +
                                neuronios[i].getPeso()[0] * neuronios[2].getSaida() +
                                neuronios[i].getPeso()[1] * neuronios[3].getSaida();

                        //calcula a saida com base no resultado da soma pondeirada
                        neuronios[i].sigmoide(weightedSum);
                        break;
                }
            }
            return this;
        }

        //propaga o erro
        public BackpropagationClass recalculaErro(double resultado)
        {

            //neuronio 4 é a saida, ele possui dois pessos

            //calcula o erro do neuronio (resultado esperado - sua saida multiplicado pela derivada do neuronio)
            neuronios[4].setErro((resultado - neuronios[4].getSaida()) * neuronios[4].derivada());
            //com base no erro acha a limiar
            neuronios[4].setThreshold(neuronios[4].getThreshold() + TAXA_APRENDIZAGEM * neuronios[4].getErro());
            //com base no erro acha os dois pessos
            neuronios[4].getPeso()[0] = neuronios[4].getPeso()[0] + TAXA_APRENDIZAGEM * neuronios[4].getErro() * neuronios[2].getSaida();
            neuronios[4].getPeso()[1] = neuronios[4].getPeso()[1] + TAXA_APRENDIZAGEM * neuronios[4].getErro() * neuronios[3].getSaida();

            //neuronios da camada oculta, cada possui um limiar e dois pessos
            //o erro do neuronio 4 e propagado para os dois neuronios e depois eles tem seu erro calculado tambem
            //apos achar o erro dos neuronio este erro e usado para calcular a limiar
            //e depois os dois pessos sao encontrados

            neuronios[3].setErro((neuronios[4].getPeso()[1] * neuronios[4].getErro()) * neuronios[3].derivada());
            neuronios[3].setThreshold(neuronios[3].getThreshold() + TAXA_APRENDIZAGEM * neuronios[3].getErro());
            neuronios[3].getPeso()[0] = neuronios[3].getPeso()[0] + TAXA_APRENDIZAGEM * neuronios[3].getErro() * neuronios[0].getSaida();
            neuronios[3].getPeso()[1] = neuronios[3].getPeso()[1] + TAXA_APRENDIZAGEM * neuronios[3].getErro() * neuronios[1].getSaida();

            neuronios[2].setErro((neuronios[4].getPeso()[0] * neuronios[4].getErro()) * neuronios[2].derivada());
            neuronios[4].setThreshold(neuronios[2].getThreshold() + TAXA_APRENDIZAGEM * neuronios[2].getErro());
            neuronios[4].getPeso()[0] = neuronios[2].getPeso()[0] + TAXA_APRENDIZAGEM * neuronios[2].getErro() * neuronios[0].getSaida();
            neuronios[4].getPeso()[1] = neuronios[4].getPeso()[1] + TAXA_APRENDIZAGEM * neuronios[2].getErro() * neuronios[1].getSaida();

            return this;
        }

    }
}
