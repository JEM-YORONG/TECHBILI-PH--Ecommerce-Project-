using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace TECHBILI_PH
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        public static string userNameHolder;

        private void LoginForm_Load(object sender, EventArgs e)
        {
            lblLoginError.Visible = false;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            btnLogin.Enabled = false;
            btnCreateAccount.Enabled = false; 

            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (txtUsername.Text != "" && txtPassword.Text != "")
                        {
                            FirebaseResponse response = client.Get("Accounts/");

                            if (response.Body != "null")
                            {
                                Dictionary<string, CreateAccount> value = response.ResultAs<Dictionary<string, CreateAccount>>();

                                foreach (var get in value)
                                {
                                    string username = get.Value.Username;
                                    string password = get.Value.Password;

                                    if (txtUsername.Text == username && txtPassword.Text == password)
                                    {
                                        lblLoginError.Visible = false;

                                        userNameHolder = txtUsername.Text;

                                        MainScreenForm main = new MainScreenForm();
                                        main.Show();
                                        this.Hide();
                                    }
                                    else
                                    {
                                        lblLoginError.Visible = true;
                                        btnLogin.Enabled = true;
                                        btnCreateAccount.Enabled = true;
                                    }
                                }                             
                            }
                            else
                            {
                                lblLoginError.Visible = true;
                                btnLogin.Enabled = true;
                                btnCreateAccount.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Please fill out all fields");
                            btnLogin.Enabled = true;
                            btnCreateAccount.Enabled = true;
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
                    btnCreateAccount.Enabled = true;
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }

        private void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    CreateAccountForm createAcc = new CreateAccountForm();
                    createAcc.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No internet connection please try again");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linklblAdminLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    AdminLogin adminLogin = new AdminLogin();
                    adminLogin.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("No internet connection please try again");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }
        }
    }
}