using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApplication2
{
    internal class Affine
    {
        public double[,] A,S,R;
        public double[] C;
        public void Affine_analize(double[,] graf, Form1 form, List<User> us)
        {

            S = new double[us.Count, us.Count];
            A = new double[us.Count, us.Count];
            R = new double[us.Count, us.Count];
            C = new double[us.Count];

            for (int i = 0; i < us.Count; i++)
            {
                for (int j = 0; j < us.Count; j++)
                {
                    A[i, j] = 0;
                    R[i, j] = 0;
                }

            }
            for (int i = 0; i < graf.GetLength(0); i++)
            {
                if (graf[i, 0] != graf[i, 2])
                    S[Convert.ToInt32(graf[i, 0]), Convert.ToInt32(graf[i, 1])] = -graf[i, 2];
                else
                {
                    S[Convert.ToInt32(graf[i, 0]), Convert.ToInt32(graf[i, 1])] = Double.MinValue;
                }
            }
            for (int m = 0; m < 50000; m++)
            {
                for (int i = 0; i < us.Count; i++)
                { // 1 строка
                    for (int k = 0; k < us.Count; k++)
                    {
                        double max = double.MinValue;
                        for (int j = 0; j < us.Count; j++)
                        {
                            if ((j != k) && (S[i, j] + A[i, j] > max)) max = S[i, j] + A[i, j];
                        }

                        R[i, k] = S[i, k] - max;
                    }
                }

                for (int i = 0; i < us.Count; i++)
                {//2 строка
                    for (int k = 0; k < us.Count; k++)
                    {
                        if (i != k)
                        {
                            double sum = 0;
                            for (int j = 0; j < us.Count; j++)
                            {
                                if (j != i && j != k && R[j, k] > 0)
                                    sum += R[j, k];
                            }

                            if (R[k, k] + sum < 0)
                                A[i, k] = R[k, k] + sum;
                            else
                                A[i, k] = 0;
                        }
                    }
                }
                //3 строка
                for (int k = 0; k < us.Count; k++)
                {
                    double sum = 0;
                    for (int j = 0; j < us.Count; j++)
                    {
                        if (j != k && R[j, k] > 0)
                            sum += R[j, k];
                    }

                    A[k, k] = sum;
                }

            }
            for (int i = 0; i < us.Count; i++)
            {
                double max = A[i, 0] + R[i, 0];
                for (int k = 1; k < us.Count; k++)
                {
                    if (A[i, k] + R[i, k] > max)
                    {
                        max = A[i, k] + R[i, k];
                        C[i] = k;
                    }
                }
            }
            
        }
    }
}
