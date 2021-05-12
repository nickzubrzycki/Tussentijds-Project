using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tussentijds_Project
{
    static class ActiveUser
    {
        private static int userId;

        public static int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private static string firstName;

        public static string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private static string lastName;

        public static string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private static string role;

        public static string Role
        {
            get { return role; }
            set { role = value; }
        }

    }
}
