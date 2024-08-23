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
    public partial class MyStoreLoginForm : Form
    {
        public MyStoreLoginForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        public static string storenameHolder;

        private void btnBack_Click(object sender, EventArgs e)
        {
            AccountForm account = new AccountForm();
            account.Show();
            this.Hide();
        }

        private void btnCreateStore_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    MyStoreCreateStoreForm createStore = new MyStoreCreateStoreForm();
                    createStore.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No internet connection please check your internet and log in again");

                    LoginForm login = new LoginForm();
                    login.Show();
                    this.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    btnLogin.Enabled = false;
                    btnCreateStore.Enabled = false;
                    btnBack.Enabled = false;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (txtStorename.Text != "" && txtPassword.Text != "")
                        {
                            FirebaseResponse response = client.Get("Stores/");
                            Dictionary<string, CreateStore> Info = response.ResultAs<Dictionary<string, CreateStore>>();

                            foreach (var get in Info)
                            {
                                string storeName = get.Value.Storename;
                                string password = get.Value.Password;

                                if (txtStorename.Text == storeName && txtPassword.Text == password)
                                {
                                    lblLoginError.Visible = false;

                                    storenameHolder = txtStorename.Text;

                                    MyStoreMainScreenForm main = new MyStoreMainScreenForm();
                                    main.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    btnLogin.Enabled = true;
                                    btnCreateStore.Enabled = true;
                                    btnBack.Enabled = true;

                                    lblLoginError.Visible = true;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please fill out all fields");
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No internet connection please check your internet and log in again");

                    LoginForm login = new LoginForm();
                    login.Show();
                    this.Close();
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

        }

        private void MyStoreLoginForm_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;
        }

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
    }
}
