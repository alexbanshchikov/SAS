using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using Newtonsoft;
using VkNet.Model.RequestParams;

namespace WindowsFormsApplication2
{
    internal class GetFriends
    {
        private readonly Form1 form;
        private VkApi vk;
        private List<User> user;
        private double[,] graf;

        public GetFriends(Form1 form)
        {
            this.form = form;
        }

        public void Avtoriz()
        {
            vk = new VkApi();

            var login = form.textBox1.Text;
            var pass = form.textBox2.Text;
            ulong id = 6285595;
            Settings setting = Settings.All;
            vk.Authorize(new ApiAuthParams
            {
                Login = login,
                Password = pass,
                ApplicationId = id,
                Settings = setting,
            });
        }

        public void Get_Friend_Information()
        {
            user = new List<User>();
            var friend = vk.Friends.Get(75234556, ProfileFields.FirstName | ProfileFields.LastName, 20).ToArray();
            for (var i = 0; i < friend.Length - 1; i++)
            {
                if (friend[i].Deactivated == null)
                {
                    var follows = vk.Users.GetFollowers(friend[i].Id, 1000);
                    var friend_2 = vk.Friends.Get(friend[i].Id);

                    User u = new User();
                    u.Id_friend = friend[i].Id;
                    u.Num_followers = follows.Count;
                    u.Num_friend = friend_2.Count;
                    u.Lastname = friend[i].LastName;
                    u.Observation = new double[2];
                    u.Observation[0] = (double)u.Num_friend;
                    u.Observation[1] = (double)u.Num_followers;
                    u.Num_clust = new int();
                    user.Add(u);
                }
            }
            foreach (User u in user)
            {
                form.textBox3.Text += u.Lastname + " " + u.Num_friend.ToString() + " " + u.Num_followers.ToString() + " " + Environment.NewLine;
            }
        }

        public List<User> Get_user()
        {
            return user;
        }


        public double[,] Analysis_distance()
        {
            graf = new double[user.Count * user.Count, 3];
            double i1 = 0, i2 = 0;
            
            form.textBox3.Text = null;
            for (int k = 0; k < graf.GetLength(0); k++)
            {
                graf[k, 0] = Math.Floor(i1 / user.Count);
                graf[k, 1] = i1 % user.Count;
                double d1 = 0, d2 = 0;
                d1 = Math.Pow(user[Convert.ToInt32(graf[k, 0])].Num_followers - user[Convert.ToInt32(graf[k, 1])].Num_followers, 2);
                d2 = Math.Pow(user[Convert.ToInt32(graf[k, 0])].Num_friend - user[Convert.ToInt32(graf[k, 1])].Num_friend, 2);
                graf[k, 2] = Math.Sqrt(d1 + d2);
                i1++;

            }
            return graf;
        }
    }
    

    public class User
    {

        private string _lastname;
        private long _idFriend;
        private int num_followers;
        private int num_clust;
        private double[] observation;
        private int num_friend;

        public string Lastname
        {
            get
            {
                return _lastname;
            }

            set
            {
                _lastname = value;
            }
        }

        public long Id_friend
        {
            get
            {
                return _idFriend;
            }

            set
            {
                _idFriend = value;
            }
        }

        public int Num_followers
        {
            get
            {
                return num_followers;
            }

            set
            {
                num_followers = value;
            }
        }

        public int Num_clust
        {
            get
            {
                return num_clust;
            }

            set
            {
                num_clust = value;
            }
        }

        public double[] Observation
        {
            get
            {
                return observation;
            }

            set
            {
                observation = value;
            }
        }

        public int Num_friend
        {
            get
            {
                return num_friend;
            }

            set
            {
                num_friend = value;
            }
        }
    }
}


