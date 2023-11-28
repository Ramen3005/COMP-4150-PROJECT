using FootballApp;
using FootBallAppGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FootBallAppGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int loggedInUserID = -1;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void login_click(object sender, RoutedEventArgs e)
        {
            UserData userData = new UserData();
            var dlg = new Login();
            dlg.Owner = this;
            dlg.ShowDialog();

            if (dlg.DialogResult == true)
            {
                if (userData.LogIn(dlg.txtUsername.Text, dlg.txtPassword.Password) == true)
                {
                    MessageBox.Show("Logged in as user #" + userData.UserID);
                    loggedInUserID = userData.UserID;

                    if (userData.IsPremium(loggedInUserID) || userData.IsNotPremium(loggedInUserID))
                    {
                        // Depending on the user type, perform actions
                        if (userData.IsPremium(loggedInUserID))
                        {
                            PremiumView premiumView = new PremiumView(userData);
                            premiumView.Show();
                            this.Close();
                        }
                        else
                        {
                            // Code for non-Premium user
                            BasicView basicView = new BasicView(userData);
                            basicView.Show();
                            this.Close();
                        }
                    }
                }
                else
                {
                    if (userData.error_num == 1)
                    {
                        MessageBox.Show(userData.error);
                    }
                    else
                    {
                        MessageBox.Show("You could not be verified. Please try again.");
                    }
                    loggedInUserID = -1;
                }
            }
        }
    }
}


