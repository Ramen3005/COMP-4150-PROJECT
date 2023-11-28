using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBallAppGUI
{
    public class UserData
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string error;
        public int error_num = 0;
        public Boolean LoggedIn { get; set; }
        public bool Accurate { get; set; }

        public bool LogIn(string loginName, string passWord)
        {
            Accurate = true;
            var dbUser = new UserInfo();
            int letterFlag = 0, numberFlag = 0;

            byte[] ASCIIValues = Encoding.ASCII.GetBytes(passWord);

            for (int i = 0; i < ASCIIValues.Length; i++)
            {
                if ((ASCIIValues[i] >= 97 && ASCIIValues[i] <= 122) || (ASCIIValues[i] >= 65 && ASCIIValues[i] <= 90))
                {
                    letterFlag = 1;
                }
                if (ASCIIValues[i] >= 49 && ASCIIValues[i] <= 57)
                {
                    numberFlag = 1;
                }
            }

            int? userId = dbUser.LogIn(loginName, passWord);

            if (userId.HasValue && userId.Value > 0)
            {
                UserID = userId.Value;
                Username = loginName;
                Password = passWord;
                LoggedIn = true;
                return true;
            }
            else
            {
                LoggedIn = false;
                if (loginName.Equals("") || passWord.Equals(""))
                {
                    error_num = 1;
                    error = "Please fill in all slots";
                }
                else if (passWord.Length < 6 || numberFlag == 0 || letterFlag == 0)
                {
                    error_num = 1;
                    error = "A valid password must have at least 6 characters with both letters and numbers";
                }
                return false;
            }
        }

        public Boolean passReqs(string Password)
        {
            if (Password.Length > 5 && Password.Any(char.IsDigit) && Password.All(char.IsLetterOrDigit) && ((Password[0] >= 65 && Password[0] <= 90) || (Password[0] >= 97 && Password[0] <= 122)))
                return true;
            else
            {
                this.Accurate = false;
                return false;
            }
        }

        public bool IsPremium(int userId)
        {
            var dbUser = new UserInfo();
            if (dbUser.UserType(userId) == "PR")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsNotPremium(int userId)
        {
            var dbuser  = new UserInfo();
            if (dbuser.UserType(userId) == "BS")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
