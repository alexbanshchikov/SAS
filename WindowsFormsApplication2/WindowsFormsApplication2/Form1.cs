using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        Task tsk;
        Stopwatch timer = new Stopwatch();
        GetFriends getFriends;
        public Prim algPrima;

        public Form1()
        {
            InitializeComponent();

            getFriends = new GetFriends(this);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

           try{ getFriends.Avtoriz();
                textBox3.Text = "Авторизация прошла успешно"; }
            catch { textBox3.Text = "Авторизация не пройдена"; }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = null;
            getFriends.Get_Friend_Information();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer.Start();
            algPrima = new Prim();
            double[,] graf = getFriends.Analysis_distance();
            List<User> us = getFriends.Get_user();
            K_means k = new K_means();         
            Affine a = new Affine();
            /* k.Clustering(us, Convert.ToInt32(textBox4.Text), this);
             a.Affine_analize(graf, this, us);
             algPrima.AlgorithmByPrim((int)Math.Sqrt(graf.GetLength(0)), graf, this, Convert.ToInt32(textBox4.Text), us);
             */
            var task1 = Task.Factory.StartNew(() =>
              {
                          k.Clustering(us, Convert.ToInt32(textBox4.Text), this);

              }
                  );

              var task2 = Task.Factory.StartNew(() =>
                  {
                      algPrima.AlgorithmByPrim((int) Math.Sqrt(graf.GetLength(0)), graf, this, Convert.ToInt32(textBox4.Text), us);
                  }
              );

              var task3 = Task.Factory.StartNew(() =>
                  {
                      a.Affine_analize(graf, this, us);
                  }
              );

              task1.Wait();
              task2.Wait();
              task3.Wait();
            timer.Stop();
            textBox5.Text = timer.ElapsedMilliseconds.ToString();
        }

    }
}
