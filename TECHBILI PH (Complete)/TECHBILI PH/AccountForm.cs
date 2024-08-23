using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace TECHBILI_PH
{
    public partial class AccountForm : Form
    {
        public AccountForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;


        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    MainScreenForm main = new MainScreenForm();
                    main.Show();
                    this.Close();
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

        private async void AccountForm_Load(object sender, EventArgs e)
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

                            lblUsername.Text = "" + result.Fullname;

                            byte[] img = Convert.FromBase64String(result.UserPicture);
                            MemoryStream ms = new MemoryStream();
                            ms.Write(img, 0, Convert.ToInt32(img.Length));
                            Bitmap bm = new Bitmap(ms, false);
                            ms.Dispose();


                            pboxUserpicture.Image = bm;
                            ViewProduct();
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

        private void btnLogout_Click(object sender, EventArgs e)
        {
            LoginForm login = new LoginForm();
            login.Show();
            this.Close();
        }

        private void btnMyStore_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    MyStoreLoginForm myStoreLogin = new MyStoreLoginForm();
                    myStoreLogin.Show();
                    this.Close();
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

        async void ViewProduct()
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
                            FirebaseResponse response = client.Get("Accounts/" + LoginForm.userNameHolder + "/" + "Order History/");

                            if (response.Body != "null")
                            {
                                Dictionary<string, OrderHistory> result = response.ResultAs<Dictionary<string, OrderHistory>>();
                                foreach (var info in result)
                                {
                                    string picture = info.Value.ItemPicture;
                                    string name = info.Value.ItemName;
                                    string price = info.Value.ItemPrice;
                                    string Store = info.Value.StoreName;
                                    string quantity = info.Value.ItemQuantity;

                                    byte[] image = Convert.FromBase64String(picture);
                                    MemoryStream ms = new MemoryStream();
                                    ms.Write(image, 0, Convert.ToInt32(image.Length));
                                    Bitmap bm = new Bitmap(ms, false);
                                    ms.Dispose();

                                    string getProductName = name;

                                    Panel panel = productPanels(name.ToString());

                                    PictureBox itemImage = itempic();
                                    itemImage.Image = bm;
                                    itemImage.SizeMode = PictureBoxSizeMode.StretchImage;

                                    Label itemInfo = itemInf(name, price, Store, quantity);

                                    panel.Controls.Add(itemImage);
                                    panel.Controls.Add(itemInfo);

                                    flpHistory.Controls.Add(panel);
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

        PictureBox itempic()
        {
            PictureBox itemPicture = new PictureBox();
            itemPicture.Location = new Point(3, 3);
            itemPicture.Size = new Size(135, 122);

            return itemPicture;
        }
        Label itemInf(string itemName, string itemPrice, string storeName, string quantity)
        {
            String newLine = Environment.NewLine;

            Label itemI = new Label();
            itemI = new Label();
            itemI.AutoSize = false;
            itemI.ForeColor = Color.White;
            itemI.Text = " " + itemName + newLine + newLine + " PHP " +
            itemPrice + newLine + newLine + " " + storeName + newLine + newLine + " Quantity: " + quantity;
            itemI.Location = new Point(144, 12);
            itemI.AutoSize = true;

            return itemI;
        }
        Panel productPanels(string name)
        {
            Panel productP = new Panel();
            productP.Name = name;
            productP.BorderStyle = BorderStyle.FixedSingle;
            productP.BackColor = Color.DarkOrchid;
            productP.Size = new Size(756, 128);

            return productP;
        }
    }
}
