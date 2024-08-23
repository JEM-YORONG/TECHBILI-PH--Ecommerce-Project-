using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace TECHBILI_PH
{
    public partial class CartForm : Form
    {
        public CartForm()
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
        Button btnRemoveItem;
        Label itemI;
        Panel productP;
        PictureBox itemPicture;
        public static string btnName;
        public static string btnRemove;
        public string checkText = "✓";
        public int btnclickCount;

        private void btnBack_Click(object sender, EventArgs e)
        {

            MainScreenForm main = new MainScreenForm();
            main.Show();
            this.Hide();
        }

        private void CartForm_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    flowLayout = flpCart;
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

                    btnclickCount++;

                    switch (btnclickCount)
                    {
                        case 1:
                            btn.Text = checkText;
                            //add to checkout node
                            try
                            {
                                client = new FireSharp.FirebaseClient(conf);

                                if (client != null)
                                {
                                    FirebaseResponse response = await client.GetTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Cart/" + btnName);
                                    Cart result = response.ResultAs<Cart>();

                                    var itemInfo = new Cart
                                    {
                                        StoreName = result.StoreName,
                                        ItemPicture = result.ItemPicture,
                                        ItemName = result.ItemName,
                                        ItemPrice = result.ItemPrice,
                                        ItemDescription = result.ItemDescription,
                                        ItemQuantity = result.ItemQuantity
                                    };

                                    SetResponse response2 = await client.SetTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/" + btnName, itemInfo);
                                }
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show("" + err);
                            }
                            break;

                        case 2:
                            btn.Text = "";
                            //remove to checkout node
                            FirebaseResponse response1 = await client.DeleteTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/" + btnName);
                            btnclickCount = 0;
                            break;
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
        async void btnRemoveToCart(object sender, EventArgs ugh)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    Button btn1 = (Button)sender;
                    btnRemove = btn1.Name.ToString();

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response1 = await client.DeleteTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Cart/" + btnRemove);

                            flpCart.Controls.Clear();
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
                            FirebaseResponse response = client.Get("Accounts/" + LoginForm.userNameHolder + "/" + "Cart/");

                            if (response.Body != "null")
                            {
                                Dictionary<string, Cart> result = response.ResultAs<Dictionary<string, Cart>>();
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

                                    Button btn = btnC();
                                    btn.Name = getProductName;
                                    btn.Text = "";

                                    Button btn2 = btnR();
                                    btn2.Name = getProductName;
                                    btn2.Text = "Remove";

                                    panel.Controls.Add(itemImage);
                                    panel.Controls.Add(btn2);
                                    panel.Controls.Add(btn);
                                    panel.Controls.Add(itemInfo);

                                    flowLayout.Controls.Add(panel);

                                    btnClick.Click += new EventHandler(this.btnEvent);
                                    btnRemoveItem.Click += new EventHandler(this.btnRemoveToCart);
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
            itemPicture.Size = new Size(135, 122);

            return itemPicture;
        }
        Button btnC()
        {
            btnClick = new Button();
            btnClick.Size = new Size(43, 42);
            btnClick.Location = new Point(682, 30);
            btnClick.BackColor = Color.LimeGreen;
            btnClick.ForeColor = Color.White;
            btnClick.FlatStyle = FlatStyle.Popup;

            return btnClick;
        }
        Button btnR()
        {
            btnRemoveItem = new Button();
            btnRemoveItem.AutoSize = true;
            btnRemoveItem.Location = new Point(670, 80);
            btnRemoveItem.BackColor = Color.LimeGreen;
            btnRemoveItem.ForeColor = Color.White;
            btnRemoveItem.FlatStyle = FlatStyle.Popup;

            return btnRemoveItem;
        }
        Label itemInf(string itemName, string itemPrice, string storeName, string quantity)
        {
            String newLine = Environment.NewLine;

            itemI = new Label();
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
            productP = new Panel();
            productP.Name = name;
            productP.BorderStyle = BorderStyle.FixedSingle;
            productP.BackColor = Color.DarkOrchid;
            productP.Size = new Size(756, 128);

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

        private void btnCheckout_Click(object sender, EventArgs e)
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
                            FirebaseResponse response1 = client.Get("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");

                            if (response1.Body != "null")
                            {
                                CheckoutForm checkoutForm = new CheckoutForm();
                                checkoutForm.Show();
                                this.Hide();
                            }
                            if (response1.Body == "null")
                            {
                                MessageBox.Show("Please select a product");
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
