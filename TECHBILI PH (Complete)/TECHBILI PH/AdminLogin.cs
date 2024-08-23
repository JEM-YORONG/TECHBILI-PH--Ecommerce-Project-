using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TECHBILI_PH
{
    public partial class AdminLogin : Form
    {
        public AdminLogin()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        private void cbxShowpassword_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShowpassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnBack.Enabled = false;

            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (txtName.Text != "" && txtPassword.Text != "")
                        {
                            FirebaseResponse response = client.Get("Admin Account/");
                            Dictionary<string, Admin> value = response.ResultAs<Dictionary<string, Admin>>();

                            foreach (var get in value)
                            {
                                string username = get.Value.Name;
                                string password = get.Value.Password;

                                if (txtName.Text == username && txtPassword.Text == password)
                                {
                                    lblLoginError.Visible = false;

                                    AdminForm adminForm = new AdminForm();
                                    adminForm.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    lblLoginError.Visible = true;
                                    btnLogin.Enabled = true;
                                    btnBack.Enabled = true;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please fill out all fields");
                            btnLogin.Enabled = true;
                            btnBack.Enabled = true;
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No internet connection please try again");

                    btnLogin.Enabled = true;

                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void AdminLogin_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;
        }
    }
}
