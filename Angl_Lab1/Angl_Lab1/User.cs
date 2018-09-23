using System.Collections.Generic;

namespace Angl_Lab1
{
    class User
    {
        private string lastname;
        private long idFriend;
        private double[] observation;
        private int num_friend;
        public List<User> friend;
        public string Lastname
        {
            get => lastname;
            set => lastname = value;
        }

        public long Id_friend
        {
            get => idFriend;
            set => idFriend = value;
        }

        public double[] Observation
        {
            get => observation;
            set => observation = value;
        }

        public int Num_friend
        {
            get => num_friend;
            set => num_friend = value;
        }
    }
}
