using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller.ViewModels
{
    public class UserViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int? Age { get; set; }
        public virtual ICollection<ProductViewModel> Products { get; set; }

        public static UserViewModel GetUserViewModel(User user, UserViewModel uvm = null)
        {
            UserViewModel userViewModel;

            if (uvm == null)
            {
                userViewModel = new UserViewModel();
            }
            else
            {
                userViewModel = uvm;
            }

            userViewModel.ID = user.ID;
            userViewModel.Name = user.Name;
            userViewModel.Age = user.Age;

            return userViewModel;
        }

        public static List<UserViewModel> GetUsersViewModel(ICollection<User> users, List<UserViewModel> uvmList = null)
        {
            List<UserViewModel> usersViewModel;

            if (uvmList == null)
            {
                usersViewModel = new List<UserViewModel>();
            }
            else
            {
                usersViewModel = uvmList;
            }

            foreach (User user in users)
            {
                UserViewModel userViewModel = GetUserViewModel(user);
                usersViewModel.Add(userViewModel);
            }

            return usersViewModel;
        }

        internal static User GetUser(UserViewModel userViewModel)
        {
            User user = new User();

            user.ID = userViewModel.ID;
            user.Name = userViewModel.Name;
            user.Age = userViewModel.Age;

            return user;
        }

        internal static List<User> GetUsers(List<UserViewModel> usersViewModel)
        {
            List<User> users = new List<User>();

            foreach (UserViewModel userViewModel in usersViewModel)
            {
                User user = GetUser(userViewModel);
                users.Add(user);
            }

            return users;
        }

    }
}
