using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Text.RegularExpressions;

namespace TECHBILI_PH
{
    public partial class MainScreenForm : Form
    {
        public MainScreenForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        public static FlowLayoutPanel flowLayout;
        Button btnClick;
        Label itemI;
        Panel productP;
        PictureBox itemPicture;
        public static string btnName;

        private async void MainScreen_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    AutoComplete(txtSearch);
                    flowLayout = flpViewProducts;
                    ViewProduct();
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

        private void btnAccount_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    AccountForm account = new AccountForm();
                    account.Show();
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

        private void btnCart_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    CartForm cart = new CartForm();
                    cart.Show();
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
        async void btnEvent(object sender, EventArgs ugh)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    Button btn = (Button)sender;
                    btnName = btn.Name.ToString();

                    ProductInfoForm productInfoForm = new ProductInfoForm();
                    productInfoForm.Show();
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
                            FirebaseResponse response = client.Get("All Products/");

                            if (response.Body != "null")
                            {
                                Dictionary<string, AllProducts> result = response.ResultAs<Dictionary<string, AllProducts>>();
                                foreach (var info in result)
                                {
                                    string picture = info.Value.ItemPicture;
                                    string name = info.Value.ItemName;
                                    string price = info.Value.ItemPrice;
                                    string store = info.Value.StoreName;

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

                                    Label itemInfo = itemInf(name, price, store);

                                    Button btn = btnC();
                                    btn.Name = getProductName;
                                    btn.Text = "View more";

                                    panel.Controls.Add(itemImage);
                                    panel.Controls.Add(btn);
                                    panel.Controls.Add(itemInfo);

                                    flowLayout.Controls.Add(panel);

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

        PictureBox itempic()
        {
            itemPicture = new PictureBox();
            itemPicture.Location = new Point(3, 3);
            itemPicture.Size = new Size(105, 94);

            return itemPicture;
        }
        Button btnC()
        {
            btnClick = new Button();
            btnClick.Location = new Point(347, 7);
            btnClick.BackColor = Color.LimeGreen;
            btnClick.ForeColor = Color.White;
            btnClick.FlatStyle = FlatStyle.Popup;

            return btnClick;
        }
        Label itemInf(string itemName, string itemPrice, string storeName)
        {
            String newLine = Environment.NewLine;
            itemI = new Label();
            itemI.ForeColor = Color.White;
            itemI.Text = " " + itemName + newLine + newLine + " PHP " +
            itemPrice + newLine + newLine + " " + storeName;
            itemI.Location = new Point(114, 7);
            //itemI.AutoSize = true;

            return itemI;
        }
        Panel productPanels(string name)
        {
            productP = new Panel();
            productP.Name = name;
            productP.BorderStyle = BorderStyle.FixedSingle;
            productP.BackColor = Color.DarkOrchid;
            productP.Size = new Size(429, 100);

            return productP;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    flowLayout.Controls.Clear();
                    ViewProduct();
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
            // Regular expression to check for special characters
            string pattern = "[^a-zA-Z0-9]";
            if (Regex.IsMatch(txtSearch.Text, pattern))
            {
                MessageBox.Show(txtSearch.Text + " does not exist");

                txtSearch.Text = "";
            }
            else
            {
                if (txtSearch.Text == "")
                {

                }
                else
                {
                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        int d;
                        if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                        {
                            flpViewProducts.Controls.Clear();
                            ViewSearchProduct();
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
            }
        }
        public async void ViewSearchProduct() 
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    try
                    {
                        FirebaseResponse response = await client.GetTaskAsync("All Products/" + txtSearch.Text);

                        if (response.Body != "null")
                        {
                            AllProducts result = response.ResultAs<AllProducts>();

                            if (txtSearch.Text == "")
                            {

                            }
                            else
                            {
                                if (txtSearch.Text.Contains(result.ItemName))
                                {
                                    string picture = result.ItemPicture;
                                    string name = result.ItemName;
                                    string price = result.ItemPrice;
                                    string store = result.StoreName;

                                    string getProductName = name;

                                    byte[] image = Convert.FromBase64String(picture);
                                    MemoryStream ms = new MemoryStream();
                                    ms.Write(image, 0, Convert.ToInt32(image.Length));
                                    Bitmap bm = new Bitmap(ms, false);
                                    ms.Dispose();

                                    Panel panel = productPanels(name.ToString());

                                    PictureBox itemImage = itempic();
                                    itemImage.Image = bm;
                                    itemImage.SizeMode = PictureBoxSizeMode.StretchImage;

                                    Label itemInfo = itemInf(name, price, store);

                                    Button btn = btnC();
                                    btn.Name = getProductName;
                                    btn.Text = "View more";

                                    panel.Controls.Add(itemImage);
                                    panel.Controls.Add(btn);
                                    panel.Controls.Add(itemInfo);

                                    flowLayout.Controls.Add(panel);

                                    txtSearch.Text = "";

                                    btnClick.Click += new EventHandler(this.btnEvent);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(txtSearch.Text + " does not exist");

                            txtSearch.Text = "";

                            flowLayout.Controls.Clear();
                            ViewProduct();
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

        List<string> itemNamesList;
        public void AutoComplete(TextBox txt)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    txt.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    txt.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = client.Get("All Products/");

                            if (response.Body != "null")
                            {
                                itemNamesList = new List<string>();

                                Dictionary<string, AllProducts> result = response.ResultAs<Dictionary<string, AllProducts>>();

                                foreach (var item in result)
                                {
                                    itemNamesList.Add(item.Value.ItemName);

                                    AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                                    source.AddRange(itemNamesList.ToArray());
                                    txt.AutoCompleteCustomSource = source;
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
    }
}
