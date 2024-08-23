using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TECHBILI_PH
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }


        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }
        private void AdminForm_Load(object sender, EventArgs e)
        {
            btnAdd.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;

            btnStoreAdd.Enabled = true;
            btnStoreSearch.Enabled = true;
            btnStoreUpdate.Enabled = false;
            btnStoreRemove.Enabled = false;

            btnAdminAdd.Enabled = true;
            btnAdminSearch.Enabled = true;
            btnAdminUpdate.Enabled = false;
            btnAdminRemove.Enabled = false;

            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    ViewAccounts();
                    ViewStores();
                    ViewAdminAccount();
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

        Button btnClick;
        Label info;
        Panel infoPanel;
        PictureBox userPicture;

        async void btnEvent(object sender, EventArgs ugh)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    Button btn = (Button)sender;

                    btnAdd.Enabled = false;
                    btnSearch.Enabled = false;
                    btnUpdate.Enabled = true;
                    btnRemove.Enabled = true;
                    txtUsername.ReadOnly = true;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("Accounts/" + btn.Name.ToString());

                            if (response.Body != "null")
                            {
                                CreateAccount result = response.ResultAs<CreateAccount>();

                                byte[] image = Convert.FromBase64String(result.UserPicture);
                                MemoryStream ms = new MemoryStream();
                                ms.Write(image, 0, Convert.ToInt32(image.Length));
                                Bitmap bm = new Bitmap(ms, false);
                                ms.Dispose();

                                pboxUserPicture.Image = bm;
                                txtFullName.Text = result.Fullname;
                                txtContact.Text = result.Contact_number;
                                txtAddress.Text = result.Address;
                                txtUsername.Text = result.Username;
                                txtPassword.Text = result.Password;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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
        async void ViewAccounts()
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
                            FirebaseResponse response = client.Get("Accounts/");

                            if (response.Body != "null")
                            {
                                Dictionary<string, CreateAccount> result = response.ResultAs<Dictionary<string, CreateAccount>>();
                                foreach (var info in result)
                                {
                                    string picture = info.Value.UserPicture;
                                    string fullName = info.Value.Fullname;
                                    string userName = info.Value.Username;

                                    byte[] image = Convert.FromBase64String(picture);
                                    MemoryStream ms = new MemoryStream();
                                    ms.Write(image, 0, Convert.ToInt32(image.Length));
                                    Bitmap bm = new Bitmap(ms, false);
                                    ms.Dispose();

                                    string Username = userName;

                                    Panel panel = userPanelInfo(fullName.ToString());

                                    PictureBox pic = userPic();
                                    pic.Image = bm;
                                    pic.SizeMode = PictureBoxSizeMode.StretchImage;

                                    Label info1 = userInfo(fullName);

                                    Button btn = btnC();
                                    btn.Name = Username;
                                    btn.Text = "View";

                                    panel.Controls.Add(pic);
                                    panel.Controls.Add(btn);
                                    panel.Controls.Add(info1);

                                    flpAccounts.Controls.Add(panel);

                                    btnClick.Click += new EventHandler(this.btnEvent);
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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
        PictureBox userPic()
        {
            userPicture = new PictureBox();
            userPicture.Location = new Point(3, 3);
            userPicture.Size = new Size(51, 42);

            return userPicture;
        }
        Button btnC()
        {
            btnClick = new Button();
            btnClick.Location = new Point(216, 12);
            btnClick.BackColor = Color.LimeGreen;
            btnClick.ForeColor = Color.White;
            btnClick.FlatStyle = FlatStyle.Popup;
            btnClick.AutoSize = true;

            return btnClick;
        }
        Label userInfo(string name)
        {
            String newLine = Environment.NewLine;

            info = new Label();
            info = new Label();
            info.AutoSize = false;
            info.ForeColor = Color.White;
            info.Text = "" + name;
            info.Location = new Point(61, 16);
            info.AutoSize = true;

            return info;
        }
        Panel userPanelInfo(string name)
        {
            infoPanel = new Panel();
            infoPanel.Name = name;
            infoPanel.BorderStyle = BorderStyle.FixedSingle;
            infoPanel.BackColor = Color.DarkOrchid;
            infoPanel.Size = new Size(304, 49);

            return infoPanel;
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pboxUserPicture.Load(ofd.FileName);
                }
            }
            catch (Exception r)
            {
                MessageBox.Show("Unsupported image");
            }
        }
        public static string ImageIntoBase64String(PictureBox pbox)
        {
            MemoryStream ms = new MemoryStream();
            pbox.Image.Save(ms, pbox.Image.RawFormat);
            return Convert.ToBase64String(ms.ToArray());
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {

            btnAdd.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;

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
                            if (pboxUserPicture.Image == null || txtFullName.Text == "" || txtContact.Text == "" || txtAddress.Text == "" || txtUsername.Text == "" || txtPassword.Text == "")
                            {
                                MessageBox.Show("Please fill out all the required fields");
                            }
                            else
                            {
                                // Regular expression to check for special characters
                                string pattern = "[^a-zA-Z0-9]";
                                if (Regex.IsMatch(txtUsername.Text, pattern))
                                {
                                    MessageBox.Show("Special characters are not allowed.");
                                }
                                else
                                {
                                    List<string> usernames = new List<string>();

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
                                    else
                                    {
                                        var userInfo = new CreateAccount
                                        {
                                            UserPicture = ImageIntoBase64String(pboxUserPicture),
                                            Fullname = txtFullName.Text,
                                            Contact_number = txtContact.Text,
                                            Address = txtAddress.Text,
                                            Username = txtUsername.Text,
                                            Password = txtPassword.Text
                                        };
                                        SetResponse response2 = await client.SetTaskAsync("Accounts/" + txtUsername.Text, userInfo);

                                        MessageBox.Show("Account Added successfully");

                                        flpAccounts.Controls.Clear();
                                        ViewAccounts();

                                        pboxUserPicture.Image = default;
                                        txtFullName.Text = "";
                                        txtContact.Text = "";
                                        txtAddress.Text = "";
                                        txtUsername.Text = "";
                                        txtPassword.Text = "";
                                    }

                                    if (usernames.ToArray().Contains(txtUsername.Text.ToLower()))
                                    {
                                        MessageBox.Show(txtUsername.Text + " is already exist please try again");
                                        txtUsername.Text = "";
                                    }
                                    else
                                    {
                                        var userInfo = new CreateAccount
                                        {
                                            UserPicture = ImageIntoBase64String(pboxUserPicture),
                                            Fullname = txtFullName.Text,
                                            Contact_number = txtContact.Text,
                                            Address = txtAddress.Text,
                                            Username = txtUsername.Text,
                                            Password = txtPassword.Text
                                        };
                                        SetResponse response2 = await client.SetTaskAsync("Accounts/" + txtUsername.Text, userInfo);

                                        MessageBox.Show("Account Added successfully");

                                        flpAccounts.Controls.Clear();
                                        ViewAccounts();

                                        pboxUserPicture.Image = default;
                                        txtFullName.Text = "";
                                        txtContact.Text = "";
                                        txtAddress.Text = "";
                                        txtUsername.Text = "";
                                        txtPassword.Text = "";
                                    }
                                }
                            }
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

        private async void btnSearch_Click(object sender, EventArgs e)
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
                            FirebaseResponse response = await client.GetTaskAsync("Accounts/" + txtUsername.Text);

                            if (txtUsername.Text != "")
                            {
                                if (response.Body != "null")
                                {
                                    // Regular expression to check for special characters
                                    string pattern = "[^a-zA-Z0-9]";
                                    if (Regex.IsMatch(txtUsername.Text, pattern))
                                    {
                                        MessageBox.Show(txtUsername.Text + " does not exist");

                                        pboxUserPicture.Image = default;
                                        txtFullName.Text = "";
                                        txtContact.Text = "";
                                        txtAddress.Text = "";
                                        txtUsername.Text = "";
                                        txtPassword.Text = "";

                                        btnAdd.Enabled = true;
                                        txtUsername.ReadOnly = false;
                                    }
                                    else
                                    {
                                        CreateAccount result = response.ResultAs<CreateAccount>();

                                        byte[] image = Convert.FromBase64String(result.UserPicture);
                                        MemoryStream ms = new MemoryStream();
                                        ms.Write(image, 0, Convert.ToInt32(image.Length));
                                        Bitmap bm = new Bitmap(ms, false);
                                        ms.Dispose();

                                        pboxUserPicture.Image = bm;
                                        txtFullName.Text = result.Fullname;
                                        txtContact.Text = result.Contact_number;
                                        txtAddress.Text = result.Address;
                                        txtUsername.Text = result.Username;
                                        txtPassword.Text = result.Password;

                                        btnUpdate.Enabled = true;
                                        btnRemove.Enabled = true;
                                        btnAdd.Enabled = false;
                                        txtUsername.ReadOnly = true;
                                    }                                  
                                }
                                else
                                {
                                    MessageBox.Show(txtUsername.Text + " does not exist");

                                    pboxUserPicture.Image = default;
                                    txtFullName.Text = "";
                                    txtContact.Text = "";
                                    txtAddress.Text = "";
                                    txtUsername.Text = "";
                                    txtPassword.Text = "";

                                    btnAdd.Enabled = true;
                                    txtUsername.ReadOnly = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please write the username in Username textbox");
                                pboxUserPicture.Image = default;
                                txtFullName.Text = "";
                                txtContact.Text = "";
                                txtAddress.Text = "";
                                txtUsername.Text = "";
                                txtPassword.Text = "";

                                btnAdd.Enabled = true;
                                txtUsername.ReadOnly = false;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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

        private async void btnUpdate_Click(object sender, EventArgs e)
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
                            FirebaseResponse response = await client.GetTaskAsync("Accounts/" + txtUsername.Text);

                            if (response.Body != "null")
                            {
                                CreateAccount result = response.ResultAs<CreateAccount>();

                                var userInfo1 = new CreateAccount
                                {
                                    UserPicture = result.UserPicture,
                                    Fullname = txtFullName.Text,
                                    Contact_number = txtContact.Text,
                                    Address = txtAddress.Text,
                                    Username = txtUsername.Text,
                                    Password = txtPassword.Text
                                };

                                FirebaseResponse response2 = await client.UpdateTaskAsync("Accounts/" + txtUsername.Text, userInfo1);

                                MessageBox.Show("Account updated successfully");

                                pboxUserPicture.Image = default;
                                txtFullName.Text = "";
                                txtContact.Text = "";
                                txtAddress.Text = "";
                                txtUsername.Text = "";
                                txtPassword.Text = "";

                                btnAdd.Enabled = true;
                                btnSearch.Enabled = true;
                                txtUsername.ReadOnly = false;
                                btnUpdate.Enabled = false;
                                btnRemove.Enabled = false;
                                txtUsername.ReadOnly = false;

                                flpAccounts.Controls.Clear();
                                ViewAccounts();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnAdd.Enabled = true;
                    btnSearch.Enabled = true;
                    txtUsername.ReadOnly = false;
                    btnUpdate.Enabled = false;
                    btnRemove.Enabled = false;

                    FirebaseResponse response2 = await client.DeleteTaskAsync("Accounts/" + txtUsername.Text);

                    MessageBox.Show("Account removed successfully");
                    pboxUserPicture.Image = default;
                    txtFullName.Text = "";
                    txtContact.Text = "";
                    txtAddress.Text = "";
                    txtUsername.Text = "";
                    txtPassword.Text = "";

                    flpAccounts.Controls.Clear();
                    ViewAccounts();
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            pboxUserPicture.Image = default;
            txtFullName.Text = "";
            txtContact.Text = "";
            txtAddress.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";

            btnAdd.Enabled = true;
            btnSearch.Enabled = true;
            btnUpdate.Enabled = false;
            btnRemove.Enabled = false;

            txtUsername.ReadOnly = false;
        }

        Button btnClick2;
        Label info2;
        Panel infoPanel2;
        async void btnEvent2(object sender, EventArgs ugh)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    Button btn2 = (Button)sender;

                    btnStoreAdd.Enabled = false;
                    btnStoreSearch.Enabled = false;
                    btnStoreUpdate.Enabled = true;
                    btnStoreRemove.Enabled = true;
                    txtStorename.ReadOnly = true;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response22 = await client.GetTaskAsync("Stores/" + btn2.Name.ToString());

                            if (response22.Body != "null")
                            {
                                CreateStore result22 = response22.ResultAs<CreateStore>();

                                txtSellerName.Text = result22.Sellername;
                                txtStoreContact.Text = result22.Contact_Number;
                                txtStoreAddress.Text = result22.Address;
                                txtStorename.Text = result22.Storename;
                                txtStorePassword.Text = result22.Password;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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
        async void ViewStores()
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
                            FirebaseResponse response2 = client.Get("Stores/");

                            if (response2.Body != "null")
                            {
                                Dictionary<string, CreateStore> result2 = response2.ResultAs<Dictionary<string, CreateStore>>();
                                foreach (var info2 in result2)
                                {
                                    string storeName = info2.Value.Storename;

                                    Panel panel2 = userPanelInfo2(storeName.ToString());

                                    Label info22 = userInfo2(storeName);

                                    Button btn2 = btnC2();
                                    btn2.Name = storeName;
                                    btn2.Text = "View";

                                    panel2.Controls.Add(btn2);
                                    panel2.Controls.Add(info22);

                                    flpStores.Controls.Add(panel2);

                                    btnClick2.Click += new EventHandler(this.btnEvent2);
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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
        Button btnC2()
        {
            btnClick2 = new Button();
            btnClick2.Location = new Point(210, 5);
            btnClick2.BackColor = Color.LimeGreen;
            btnClick2.ForeColor = Color.White;
            btnClick2.FlatStyle = FlatStyle.Popup;
            btnClick2.AutoSize = true;

            return btnClick2;
        }
        Label userInfo2(string name)
        {
            String newLine = Environment.NewLine;

            info2 = new Label();
            info2 = new Label();
            info2.AutoSize = false;
            info2.ForeColor = Color.White;
            info2.Text = "" + name;
            info2.Location = new Point(13, 9);
            info2.AutoSize = true;

            return info2;
        }
        Panel userPanelInfo2(string name)
        {
            infoPanel2 = new Panel();
            infoPanel2.Name = name;
            infoPanel2.BorderStyle = BorderStyle.FixedSingle;
            infoPanel2.BackColor = Color.DarkOrchid;
            infoPanel2.Size = new Size(305, 35);

            return infoPanel2;
        }

        private async void btnStoreAdd_Click(object sender, EventArgs e)
        {
            btnStoreAdd.Enabled = true;
            btnStoreSearch.Enabled = true;
            btnStoreUpdate.Enabled = false;
            btnStoreRemove.Enabled = false;

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
                            if (txtSellerName.Text == "" || txtStoreContact.Text == "" || txtStoreAddress.Text == "" || txtStorename.Text == "" || txtStorePassword.Text == "")
                            {
                                MessageBox.Show("Please fill out all the required fields");
                            }
                            else
                            {
                                // Regular expression to check for special characters
                                string pattern = "[^a-zA-Z0-9]";
                                if (Regex.IsMatch(txtStorename.Text, pattern))
                                {
                                    MessageBox.Show("Special characters are not allowed.");
                                }
                                else
                                {
                                    List<string> storeNamesList = new List<string>();

                                    FirebaseResponse response5 = client.Get("Stores/");
                                    Dictionary<string, CreateStore> value5 = response5.ResultAs<Dictionary<string, CreateStore>>();

                                    if (response5.Body != null)
                                    {
                                        foreach (var get5 in value5)
                                        {
                                            string StoreName = get5.Value.Storename;

                                            storeNamesList.Add(StoreName.ToLower());
                                        }
                                    }

                                    if (storeNamesList.ToArray().Contains(txtStorename.Text.ToLower()))
                                    {
                                        MessageBox.Show(txtStorename.Text + " is already exist please try again");
                                        txtStorename.Text = "";
                                    }
                                    else
                                    {
                                        var userInfo5 = new CreateStore
                                        {
                                            Sellername = txtSellerName.Text,
                                            Contact_Number = txtStoreContact.Text,
                                            Address = txtStoreAddress.Text,
                                            Storename = txtStorename.Text,
                                            Password = txtStorePassword.Text
                                        };
                                        SetResponse response2 = await client.SetTaskAsync("Stores/" + txtStorename.Text, userInfo5);

                                        MessageBox.Show("Store Added successfully");

                                        flpStores.Controls.Clear();
                                        ViewStores();

                                        txtSellerName.Text = "";
                                        txtStoreContact.Text = "";
                                        txtStoreAddress.Text = "";
                                        txtStorename.Text = "";
                                        txtStorePassword.Text = "";
                                    }
                                }
                            }
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

        private async void btnStoreSearch_Click(object sender, EventArgs e)
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
                            FirebaseResponse response6 = await client.GetTaskAsync("Stores/" + txtStorename.Text);

                            if (txtStorename.Text != "")
                            {
                                if (response6.Body != "null")
                                {
                                    // Regular expression to check for special characters
                                    string pattern = "[^a-zA-Z0-9]";
                                    if (Regex.IsMatch(txtStorename.Text, pattern))
                                    {
                                        MessageBox.Show(txtStorename.Text + " does not exist");

                                        txtSellerName.Text = "";
                                        txtStoreContact.Text = "";
                                        txtStoreAddress.Text = "";
                                        txtStorename.Text = "";
                                        txtStorePassword.Text = "";

                                        btnStoreAdd.Enabled = true;
                                        txtStorename.ReadOnly = false;
                                    }
                                    else
                                    {
                                        CreateStore result6 = response6.ResultAs<CreateStore>();

                                        txtSellerName.Text = result6.Sellername;
                                        txtStoreContact.Text = result6.Contact_Number;
                                        txtStoreAddress.Text = result6.Address;
                                        txtStorename.Text = result6.Storename;
                                        txtStorePassword.Text = result6.Password;

                                        btnStoreUpdate.Enabled = true;
                                        btnStoreRemove.Enabled = true;
                                        btnStoreAdd.Enabled = false;
                                        txtStorename.ReadOnly = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(txtStorename.Text + " does not exist");

                                    txtSellerName.Text = "";
                                    txtStoreContact.Text = "";
                                    txtStoreAddress.Text = "";
                                    txtStorename.Text = "";
                                    txtStorePassword.Text = "";

                                    btnStoreAdd.Enabled = true;
                                    txtStorename.ReadOnly = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please write the store name in Store name textbox");

                                txtSellerName.Text = "";
                                txtStoreContact.Text = "";
                                txtStoreAddress.Text = "";
                                txtStorename.Text = "";
                                txtStorePassword.Text = "";

                                btnStoreAdd.Enabled = true;
                                txtStorename.ReadOnly = false;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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

        private async void btnStoreUpdate_Click(object sender, EventArgs e)
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
                            FirebaseResponse response66 = await client.GetTaskAsync("Stores/" + txtStorename.Text);

                            if (response66.Body != "null")
                            {
                                CreateStore result66 = response66.ResultAs<CreateStore>();

                                var userInfo66 = new CreateStore
                                {
                                    Sellername = txtSellerName.Text,
                                    Contact_Number = txtStoreContact.Text,
                                    Address = txtStoreAddress.Text,
                                    Storename = txtStorename.Text,
                                    Password = txtStorePassword.Text
                                };

                                FirebaseResponse response67 = await client.UpdateTaskAsync("Stores/" + txtStorename.Text, userInfo66);

                                MessageBox.Show("Account updated successfully");

                                txtSellerName.Text = "";
                                txtStoreContact.Text = "";
                                txtStoreAddress.Text = "";
                                txtStorename.Text = "";
                                txtStorePassword.Text = "";

                                btnStoreAdd.Enabled = true;
                                btnStoreSearch.Enabled = true;
                                txtStorename.ReadOnly = false;
                                btnStoreUpdate.Enabled = false;
                                btnStoreRemove.Enabled = false;
                                txtStorename.ReadOnly = false;

                                flpStores.Controls.Clear();
                                ViewStores();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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

        private async void btnStoreRemove_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnStoreAdd.Enabled = true;
                    btnStoreSearch.Enabled = true;
                    txtStorename.ReadOnly = false;
                    btnStoreUpdate.Enabled = false;
                    btnStoreRemove.Enabled = false;

                    FirebaseResponse response2 = await client.DeleteTaskAsync("Stores/" + txtStorename.Text);

                    MessageBox.Show("Store removed successfully");

                    txtSellerName.Text = "";
                    txtStoreContact.Text = "";
                    txtStoreAddress.Text = "";
                    txtStorename.Text = "";
                    txtStorePassword.Text = "";

                    flpStores.Controls.Clear();
                    ViewStores();
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

        private void btnStoreClear_Click(object sender, EventArgs e)
        {
            txtSellerName.Text = "";
            txtStoreContact.Text = "";
            txtStoreAddress.Text = "";
            txtStorename.Text = "";
            txtStorePassword.Text = "";

            btnStoreAdd.Enabled = true;
            btnStoreSearch.Enabled = true;
            btnStoreUpdate.Enabled = false;
            btnStoreRemove.Enabled = false;

            txtStorename.ReadOnly = false;
        }

        Button btnClick3;
        Label info3;
        Panel infoPanel3;
        async void btnEvent333(object sender, EventArgs ugh)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    Button btn33 = (Button)sender;

                    btnAdminAdd.Enabled = false;
                    btnAdminSearch.Enabled = false;
                    btnAdminUpdate.Enabled = true;
                    btnAdminRemove.Enabled = true;
                    txtName.ReadOnly = true;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response888 = await client.GetTaskAsync("Admin Account/" + btn33.Name.ToString());

                            if (response888.Body != "null")
                            {
                                Admin result888 = response888.ResultAs<Admin>();

                                txtName.Text = result888.Name;
                                txtAdminPassword.Text = result888.Password;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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
        async void ViewAdminAccount()
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
                            FirebaseResponse response88 = client.Get("Admin Account/");

                            if (response88.Body != "null")
                            {
                                Dictionary<string, Admin> result88 = response88.ResultAs<Dictionary<string, Admin>>();
                                foreach (var info88 in result88)
                                {
                                    string adminName = info88.Value.Name;

                                    Panel panel88 = userPanelInfo3(adminName.ToString());

                                    Label info888 = userInfo3(adminName);

                                    Button btn88 = btnC3();
                                    btn88.Name = adminName;
                                    btn88.Text = "View";

                                    panel88.Controls.Add(btn88);
                                    panel88.Controls.Add(info888);

                                    flpAdmin.Controls.Add(panel88);

                                    btnClick3.Click += new EventHandler(this.btnEvent333);
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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
        Button btnC3()
        {
            btnClick3 = new Button();
            btnClick3.Location = new Point(210, 5);
            btnClick3.BackColor = Color.LimeGreen;
            btnClick3.ForeColor = Color.White;
            btnClick3.FlatStyle = FlatStyle.Popup;
            btnClick3.AutoSize = true;

            return btnClick3;
        }
        Label userInfo3(string name)
        {
            String newLine = Environment.NewLine;

            info3 = new Label();
            info3 = new Label();
            info3.AutoSize = false;
            info3.ForeColor = Color.White;
            info3.Text = "" + name;
            info3.Location = new Point(13, 9);
            info3.AutoSize = true;

            return info3;
        }
        Panel userPanelInfo3(string name)
        {
            infoPanel3 = new Panel();
            infoPanel3.Name = name;
            infoPanel3.BorderStyle = BorderStyle.FixedSingle;
            infoPanel3.BackColor = Color.DarkOrchid;
            infoPanel3.Size = new Size(305, 35);

            return infoPanel3;
        }

        private async void btnAdminAdd_Click(object sender, EventArgs e)
        {
            btnAdminAdd.Enabled = true;
            btnAdminSearch.Enabled = true;
            btnAdminUpdate.Enabled = false;
            btnAdminRemove.Enabled = false;

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
                            if (txtName.Text == "" || txtAdminPassword.Text == "")
                            {
                                MessageBox.Show("Please fill out all the required fields");
                            }
                            else
                            {
                                // Regular expression to check for special characters
                                string pattern = "[^a-zA-Z0-9]";
                                if (Regex.IsMatch(txtName.Text, pattern))
                                {
                                    MessageBox.Show("Special characters are not allowed.");
                                }
                                else
                                {
                                    List<string> adminNameList = new List<string>();

                                    FirebaseResponse response555 = client.Get("Admin Account/");
                                    Dictionary<string, Admin> value555 = response555.ResultAs<Dictionary<string, Admin>>();

                                    if (response555.Body != null)
                                    {
                                        foreach (var get555 in value555)
                                        {
                                            string adminName = get555.Value.Name;

                                            adminNameList.Add(adminName.ToLower());
                                        }
                                    }

                                    if (adminNameList.ToArray().Contains(txtName.Text.ToLower()))
                                    {
                                        MessageBox.Show(txtName.Text + " is already exist please try again");
                                        txtName.Text = "";
                                    }
                                    else
                                    {
                                        var userInfo555 = new Admin
                                        {
                                            Name = txtName.Text,
                                            Password = txtAdminPassword.Text,
                                        };
                                        SetResponse response255 = await client.SetTaskAsync("Admin Account/" + txtName.Text, userInfo555);

                                        MessageBox.Show("Admin Added successfully");

                                        flpAdmin.Controls.Clear();
                                        ViewAdminAccount();

                                        txtName.Text = "";
                                        txtAdminPassword.Text = "";
                                    }
                                }
                            }
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

        private async void btnAdminSearch_Click(object sender, EventArgs e)
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
                            FirebaseResponse response655 = await client.GetTaskAsync("Admin Account/" + txtName.Text);

                            if (txtName.Text != "")
                            {
                                if (response655.Body != "null")
                                {
                                    // Regular expression to check for special characters
                                    string pattern = "[^a-zA-Z0-9]";
                                    if (Regex.IsMatch(txtName.Text, pattern))
                                    {
                                        MessageBox.Show(txtName.Text + " does not exist");

                                        txtName.Text = "";
                                        txtAdminPassword.Text = "";

                                        btnAdminAdd.Enabled = true;
                                        txtName.ReadOnly = false;
                                    }
                                    else
                                    {
                                        Admin result655 = response655.ResultAs<Admin>();

                                        txtName.Text = result655.Name;
                                        txtAdminPassword.Text = result655.Password;

                                        btnAdminUpdate.Enabled = true;
                                        btnAdminRemove.Enabled = true;
                                        btnAdminAdd.Enabled = false;
                                        txtName.ReadOnly = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(txtName.Text + " does not exist");

                                    txtName.Text = "";
                                    txtAdminPassword.Text = "";

                                    btnAdminAdd.Enabled = true;
                                    txtName.ReadOnly = false;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please write the admin name in name textbox");

                                txtName.Text = "";
                                txtAdminPassword.Text = "";

                                btnAdminAdd.Enabled = true;
                                txtName.ReadOnly = false;
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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

        private async void btnAdminUpdate_Click(object sender, EventArgs e)
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
                            FirebaseResponse response6655 = await client.GetTaskAsync("Admin Account/" + txtName.Text);

                            if (response6655.Body != "null")
                            {
                                Admin result6655 = response6655.ResultAs<Admin>();

                                var userInfo6655 = new Admin
                                {
                                    Name = txtName.Text,
                                    Password = txtAdminPassword.Text,
                                };

                                FirebaseResponse response6755 = await client.UpdateTaskAsync("Admin Account/" + txtName.Text, userInfo6655);

                                MessageBox.Show("Admin updated successfully");

                                txtName.Text = "";
                                txtAdminPassword.Text = "";

                                btnAdminAdd.Enabled = true;
                                btnAdminSearch.Enabled = true;
                                txtName.ReadOnly = false;
                                btnAdminUpdate.Enabled = false;
                                btnAdminRemove.Enabled = false;
                                txtName.ReadOnly = false;

                                flpAdmin.Controls.Clear();
                                ViewAdminAccount();
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("" + err);
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

        private async void btnAdminRemove_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    btnAdminAdd.Enabled = true;
                    btnAdminSearch.Enabled = true;
                    txtName.ReadOnly = false;
                    btnAdminUpdate.Enabled = false;
                    btnAdminRemove.Enabled = false;

                    FirebaseResponse response244 = await client.DeleteTaskAsync("Admin Account/" + txtName.Text);

                    MessageBox.Show("Admin removed successfully");

                    txtName.Text = "";
                    txtAdminPassword.Text = "";

                    flpAdmin.Controls.Clear();
                    ViewAdminAccount();
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

        private void btnAdminClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtAdminPassword.Text = "";

            btnAdminAdd.Enabled = true;
            btnAdminSearch.Enabled = true;
            btnAdminUpdate.Enabled = false;
            btnAdminRemove.Enabled = false;

            txtName.ReadOnly = false;
        }
    }
}
