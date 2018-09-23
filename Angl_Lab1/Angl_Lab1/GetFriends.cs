using System;
using System.Collections.Generic;
using System.Linq;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;

namespace Angl_Lab1
{
    class GetFriends
    {
        private readonly Form1 form;
        private VkApi vk;
        private List<User> user;

        public GetFriends(Form1 form)
        {
            this.form = form;
        }

        public void Authorization()
        {
            vk = new VkApi();

            var login = form.textBoxLogin.Text;
            var pass = form.textBoxPassword.Text;
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

        public void GetFriendInformation()
        {
            user = new List<User>();
            ProfileFields pf = ProfileFields.FirstName | ProfileFields.LastName | ProfileFields.FriendLists;
            var friend = vk.Friends.Get(new FriendsGetParams { UserId = vk.UserId, Fields = pf, Count = 5 }).ToArray();


            for (var i = 0; i < friend.Length - 1; i++)
            {
                if (friend[i].Deactivated != null)
                {
                    var friend_2 = vk.Friends.Get(new FriendsGetParams { UserId = friend[i].Id, Fields = pf });

                    User u = new User
                    {
                        Id_friend = friend[i].Id,
                        Num_friend = friend_2.Count(),
                        Lastname = friend[i].LastName,
                        Observation = new double[2]
                    };
                    u.Observation[0] = u.Num_friend;

                    u.friend = new List<User>();
                    for (var j = 0; j < friend_2.Count() - 1; j++)
                    {
                        User u_ = new User
                        {
                            Id_friend = friend_2[j].Id,
                            Lastname = friend_2[j].LastName,
                        };
                        u.friend.Add(u_);
                    }
                    user.Add(u);
                }
            }

            foreach (User u in user)
            {
                form.textBoxMain.Text += u.Lastname + " " + u.Num_friend.ToString() + " " + Environment.NewLine + " " + Environment.NewLine;
                foreach (User u_ in u.friend)
                {
                    form.textBoxMain.Text += u_.Lastname + " " + Environment.NewLine;
                }
                form.textBoxMain.Text += "---------------------------------------------------" + Environment.NewLine;
            }
        }

        public List<User> GetUser()
        {
            return user;
        }
    }
}
