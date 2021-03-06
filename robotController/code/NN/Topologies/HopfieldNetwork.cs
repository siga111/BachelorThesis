﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RobotSimulationController.NN
{
    class HopfieldNetwork : AbstractNN
    {
        /*
         * Simple network containing N neurons: 2 input(0,1), 2 output(N-1, N) and else in hidden layer.
         * Each neuron in hidden layer is connecteed to other neurons with reccurant link.
         * 
         * Each in input layer is connected to each in hidden layer.
         * Each in hidden layer is connected with each in output layer. 
         * 
         * 2*2*5 + 5*4 = 40  connections in total.
         */
        public const int HIDDEN_NEURON_COUNT = 5;

        public HopfieldNetwork()
        {
            NeuronCount = 2 + HIDDEN_NEURON_COUNT + 2;
            LinkCount = 2 * HIDDEN_NEURON_COUNT * 2 + HIDDEN_NEURON_COUNT * (HIDDEN_NEURON_COUNT - 1);
        }

        public override void InitNetwork()
        {
            // Creating neurons
            for (int ii = 0; ii < NeuronCount; ii++)
            {
                Neurons.Add(new Neuron(ii));
            }
            // Adding link btween neurons
            int jj = 0;
            // input to hidden layer
            for (int kk = 0; kk < HIDDEN_NEURON_COUNT; kk++)
            {
                CreateSynapse(Neurons[0], Neurons[2 + kk], jj++);
            }
            for (int kk = 0; kk < HIDDEN_NEURON_COUNT; kk++)
            {
                CreateSynapse(Neurons[1], Neurons[2 + kk], jj++);
            }
            // Reccurant links
            for (int kk = 0; kk < HIDDEN_NEURON_COUNT; kk++)
            {
                for (int nn = 0; nn < HIDDEN_NEURON_COUNT; nn++)
                {
                    if (kk != nn)
                    {
                        CreateSynapse(Neurons[2 + kk], Neurons[2 + nn], jj++);
                    }
                }
            }
            // hidden layer to input
            for (int kk = 0; kk < HIDDEN_NEURON_COUNT; kk++)
            {
                CreateSynapse(Neurons[2 + kk], Neurons[NeuronCount - 2], jj++);
            }
            for (int kk = 0; kk < HIDDEN_NEURON_COUNT; kk++)
            {
                CreateSynapse(Neurons[2 + kk], Neurons[NeuronCount - 1], jj++);
            }
        }

    }
}
