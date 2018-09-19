using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord;
using Accord.MachineLearning;

namespace WindowsFormsApplication2
{
    internal class K_means
    {
        private List<User> us;
        public double[][] observations;

        public List<User> Us
        {
            get
            {
                return us;
            }

            set
            {
                us = value;
            }
        }

        public void Clustering(List<User> us_, int num_clust, Form1 form)
        {
            Us = us_;
            observations = new double[Us.Count][];
            for(int i=0;i<Us.Count;i++)
            {
                observations[i] = Us[i].Observation;
            }
            Accord.Math.Random.Generator.Seed = 0;
            double[][] orig = observations;

            BalancedKMeans kmeans = new BalancedKMeans(num_clust)
            {
                MaxIterations = 100
            };

            KMeansClusterCollection clusters = kmeans.Learn(observations);
            int[] labels = clusters.Decide(observations);
            for (int i = 0; i < labels.Length; i++)
            {
                Us[i].Num_clust = labels[i];
            }
            form.textBox3.Text += @"K-means";
            for (int i=0;i< num_clust; i++)
            {
                form.textBox3.Text += Environment.NewLine + Environment.NewLine+@"Кластер № " + (i+1).ToString() + Environment.NewLine;
                for(int j = 0; j < Us.Count; j++)
                {
                    if (Us[j].Num_clust == i) form.textBox3.Text += Us[j].Lastname + " ";
                }
            }
            form.textBox3.Text += Environment.NewLine + @"-------------------------------------------------------------------";
            
            //throw new NotImplementedException();
        }
    }
}
