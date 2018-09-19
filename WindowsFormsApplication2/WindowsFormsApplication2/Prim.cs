using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2
{
    public class Prim
    {
        private List<Edge> MST;
        private List<List<int>> clusters;
        private double[,] matr;
        List<int> notUsed;

        public void AlgorithmByPrim(int numberV, double[,] E, Form1 form, int numberClust, List<User> us)
        {

            MST = new List<Edge>();
            clusters = new List<List<int>>();

            List<Edge> notUsedE = new List<Edge>();
            List<double> usedV = new List<double>();
            List<double> notUsedV = new List<double>();

            for (int i = 0; i < E.GetLength(0); i++)
            {
                Edge e = new Edge(E[i, 0], E[i, 1], E[i, 2]);
                notUsedE.Add(e);

            }

            for (int i = 0; i < numberV; i++)
                notUsedV.Add(i);

            Random rand = new Random();
            usedV.Add(rand.Next(0, numberV));
            notUsedV.RemoveAt((int)usedV[0]);
            while (notUsedV.Count > 0)
            {
                int minE = -1; //номер наименьшего ребра
                               //поиск наименьшего ребра
                for (int i = 0; i < notUsedE.Count; i++)
                {
                    if ((usedV.IndexOf(notUsedE[i].v1) != -1) && (notUsedV.IndexOf(notUsedE[i].v2) != -1) ||
                        (usedV.IndexOf(notUsedE[i].v2) != -1) && (notUsedV.IndexOf(notUsedE[i].v1) != -1))
                    {
                        if (minE != -1)
                        {
                            if (notUsedE[i].weight < notUsedE[minE].weight)
                                minE = i;
                        }
                        else
                            minE = i;
                    }
                }
                //заносим новую вершину в список использованных и удаляем ее из списка неиспользованных
                if (usedV.IndexOf(notUsedE[minE].v1) != -1)
                {
                    usedV.Add(notUsedE[minE].v2);
                    notUsedV.Remove(notUsedE[minE].v2);
                }
                else
                {
                    usedV.Add(notUsedE[minE].v1);
                    notUsedV.Remove(notUsedE[minE].v1);
                }
                //заносим новое ребро в дерево и удаляем его из списка неиспользованных
                MST.Add(notUsedE[minE]);
                notUsedE.RemoveAt(minE);
            }
            Make_clusters(form,numberV,numberClust,us);
        }

        public void Make_clusters(Form1 form, int numberV, int numberClust, List<User> us)
        {
            matr = new double[numberV, numberV];
            notUsed = new List<int>();
            var spanningTree = (from t in MST
                                 orderby t.weight descending   // упорядочиваем по возрастанию
                                 select t).ToList();

            for (int i = 0; i < numberV; i++)
            {
                for (int j = 0; j < numberV; j++)
                {
                    matr[i, j] = 0;
                }
            }
            for (int i = numberClust - 1; i < spanningTree.Count; i++)
            {
                matr[(int)spanningTree[i].v1, (int)spanningTree[i].v2] = 1;
                matr[(int)spanningTree[i].v2, (int)spanningTree[i].v1] = 1;
            }

            for (int i = 0; i < matr.GetLength(0); i++)
            {
                notUsed.Add(i);
            }
            Sch_Clusters();

            form.textBox3.Text += Environment.NewLine + Environment.NewLine + "Метод Прима " + Environment.NewLine;
            form.textBox3.Text += Environment.NewLine;
            int numb = 1;
            foreach (List<int> item in clusters)
            {
                form.textBox3.Text += "Кластер №" + numb + Environment.NewLine;
                foreach (int g1 in item)
                {
                    form.textBox3.Text += us[g1].Lastname + " ";
                }
                numb++;
                form.textBox3.Text += Environment.NewLine + Environment.NewLine;
            }
        }

        public void Sch_Clusters()
        {
            while (notUsed.Count != 0)
            {
                List<int> used = new List<int>();
                Dpt(notUsed.First(), used);
                clusters.Add(used);          
            }
        }

        public void Dpt(int v, List<int> used)
        {
            used.Add(v);
            notUsed.Remove(v);
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                if ((matr[v, i] == 1) && (notUsed.Contains(i))){
                    Dpt(i,used);
                }
            }
        }
    }




        public class Edge
        {
            public double v1, v2;

            public double weight;

            public Edge(double v1, double v2, double weight)
            {
                this.v1 = v1;
                this.v2 = v2;
                this.weight = weight;
            }
        }
    }
    

