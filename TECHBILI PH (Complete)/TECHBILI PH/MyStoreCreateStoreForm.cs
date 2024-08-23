using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TECHBILI_PH
{
    public partial class MyStoreCreateStoreForm : Form
    {
        public MyStoreCreateStoreForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        private async void btnCreateStore_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnCreateStore.Enabled = false;
                    btnBack.Enabled = false;

                    List<string> storenames = new List<string>();

                    // Regular expression to check for special characters
                    string pattern = "[^a-zA-Z0-9]";
                    if (Regex.IsMatch(txtStorename.Text, pattern))
                    {
                        MessageBox.Show("Special characters are not allowed.");

                        btnCreateStore.Enabled = true;
                        btnBack.Enabled = true;
                    }
                    else
                    {
                        try
                        {
                            client = new FireSharp.FirebaseClient(conf);

                            if (client != null)
                            {
                                FirebaseResponse response1 = client.Get("Stores/");

                                if (response1.Body != "null")
                                {
                                    Dictionary<string, CreateStore> value = response1.ResultAs<Dictionary<string, CreateStore>>();
                                    foreach (var get in value)
                                    {
                                        string storename = get.Value.Storename;

                                        storenames.Add(storename.ToLower());
                                    }

                                    if (txtStorename.Text != "" && txtPassword.Text != "")
                                    {
                                        if (storenames.ToArray().Contains(txtStorename.Text.ToLower()))
                                        {
                                            MessageBox.Show(txtStorename.Text + " is already exist please try again");

                                            txtStorename.Text = "";

                                            btnCreateStore.Enabled = true;
                                            btnBack.Enabled = true;
                                        }
                                        else
                                        {
                                            var createStore = new CreateStore
                                            {
                                                Sellername = txtSellername.Text,
                                                Contact_Number = txtContactno.Text,
                                                Address = txtAddress.Text,
                                                Storename = txtStorename.Text,
                                                Password = txtPassword.Text
                                            };
                                            SetResponse response = await client.SetTaskAsync("Stores/" + txtStorename.Text, createStore);
                                            CreateStore result = response.ResultAs<CreateStore>();

                                            MessageBox.Show("Store created successfully you can log in now");
                                            txtSellername.Text = "";
                                            txtContactno.Text = "";
                                            txtAddress.Text = "";
                                            txtStorename.Text = "";
                                            txtPassword.Text = "";

                                            MyStoreLoginForm storeLogin = new MyStoreLoginForm();
                                            storeLogin.Show();
                                            this.Close();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Please fill out all the required fields");
                                    }
                                }
                                else
                                {
                                    var createStore = new CreateStore
                                    {
                                        Sellername = txtSellername.Text,
                                        Contact_Number = txtContactno.Text,
                                        Address = txtAddress.Text,
                                        Storename = txtStorename.Text,
                                        Password = txtPassword.Text
                                    };
                                    SetResponse response = await client.SetTaskAsync("Stores/" + txtStorename.Text, createStore);
                                    CreateStore result = response.ResultAs<CreateStore>();

                                    MessageBox.Show("Store created successfully you can log in now");
                                    txtSellername.Text = "";
                                    txtContactno.Text = "";
                                    txtAddress.Text = "";
                                    txtStorename.Text = "";
                                    txtPassword.Text = "";

                                    MyStoreLoginForm storeLogin = new MyStoreLoginForm();
                                    storeLogin.Show();
                                    this.Close();
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

        private async void MyStoreCreateStoreForm_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("Accounts/" + LoginForm.userNameHolder);
                            CreateAccount result = response.ResultAs<CreateAccount>();

                            txtSellername.Text = result.Fullname;
                            txtContactno.Text = result.Contact_number;
                            txtAddress.Text = result.Address;
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
            MyStoreLoginForm storeLogin = new MyStoreLoginForm();
            storeLogin.Show();
            this.Close();
        }
    }
}
