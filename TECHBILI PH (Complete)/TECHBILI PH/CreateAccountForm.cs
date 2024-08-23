using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Net;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TECHBILI_PH
{
    public partial class CreateAccountForm : Form
    {
        public CreateAccountForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void btnUploadPicture_Click(object sender, EventArgs e)
        {

            try
            {
                oFd.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

                if (oFd.ShowDialog() == DialogResult.OK)
                {
                    pboxUserPicture.Load(oFd.FileName);
                }
            }
            catch (Exception r)
            {
                MessageBox.Show("Unsupported image");
            }
        }

        //use to upload image to firebase
        public static string ImageIntoBase64String(PictureBox pbox)
        {
            MemoryStream ms = new MemoryStream();
            pbox.Image.Save(ms, pbox.Image.RawFormat);
            return Convert.ToBase64String(ms.ToArray());
        }

        private async void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnCreate.Enabled = false;
                    btnCancel.Enabled = false;

                    List<string> usernames = new List<string>();

                    // Regular expression to check for special characters
                    string pattern = "[^a-zA-Z0-9]";
                    if (Regex.IsMatch(txtUsername.Text, pattern))
                    {
                        MessageBox.Show("Special characters are not allowed.");

                        btnCreate.Enabled = true;
                        btnCancel.Enabled = true;
                    }
                    else
                    {
                        try
                        {
                            client = new FireSharp.FirebaseClient(conf);

                            if (client != null)
                            {
                                if (pboxUserPicture.Image != null && txtFirstname.Text != "" && txtLastname.Text != "" && txtContact.Text != "" && txtAddress.Text != "" && txtUsername.Text != "" && txtPassword.Text != "")
                                {
                                    FirebaseResponse response = client.Get("Accounts/");

                                    if (response.Body != "null")
                                    {
                                        Dictionary<string, CreateAccount> value = response.ResultAs<Dictionary<string, CreateAccount>>();

                                        foreach (var get in value)
                                        {
                                            string username = get.Value.Username;

                                            usernames.Add(username.ToLower());
                                        }
                                    }

                                    if (usernames.ToArray().Contains(txtUsername.Text.ToLower()))
                                    {
                                        MessageBox.Show(txtUsername.Text + " is already exist please try again");

                                        txtUsername.Text = "";
                                        btnCreate.Enabled = true;
                                        btnCancel.Enabled = true;
                                    }
                                    else
                                    {
                                        var userInfo = new CreateAccount
                                        {
                                            UserPicture = ImageIntoBase64String(pboxUserPicture),
                                            Fullname = txtFirstname.Text + " " + txtLastname.Text,
                                            Contact_number = txtContact.Text,
                                            Address = txtAddress.Text,
                                            Username = txtUsername.Text,
                                            Password = txtPassword.Text
                                        };
                                        SetResponse response2 = await client.SetTaskAsync("Accounts/" + txtUsername.Text, userInfo);

                                        MessageBox.Show("Account created successfully you can log in now");

                                        pboxUserPicture.Image = default;
                                        txtFirstname.Text = "";
                                        txtLastname.Text = "";
                                        txtContact.Text = "";
                                        txtAddress.Text = "";
                                        txtUsername.Text = "";
                                        txtPassword.Text = "";

                                        LoginForm login = new LoginForm();
                                        login.Show();
                                        this.Close();
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Please fill out all the fields");
                                    btnCreate.Enabled = true;
                                    btnCancel.Enabled = true;
                                }
                            }
                        }
                        catch (Exception err)
                        {
                            MessageBox.Show(err.Message);
                        }
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

        private void CreateAccountForm_Load(object sender, EventArgs e)
        {

        }
    }
}
