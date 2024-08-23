using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Text.RegularExpressions;

namespace TECHBILI_PH
{
    public partial class MyStoreMainScreenForm : Form
    {
        public MyStoreMainScreenForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        Button btnClick;
        Label itemI;
        Panel productP;
        PictureBox itemPicture;

        private void btnLogout_Click(object sender, EventArgs e)
        {
            MyStoreLoginForm storeLogin = new MyStoreLoginForm();
            storeLogin.Show();
            this.Close();
        }

        private async void MyStoreMainScreenForm_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    btnUpdateItem.Enabled = false;
                    btnRemove.Enabled = false;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder);
                            CreateStore result = response.ResultAs<CreateStore>();

                            lblStorename.Text = result.Storename;
                            ViewProduct();
                            ViewProductSold();
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

        private async void btnAddItem_Click(object sender, EventArgs e)
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
                            if (pboxItemPricture.Image == null || txtItemname.Text == "" || txtItemprice.Text == "" || txtItemquantity.Text == "" || txtItemdescription.Text == "")
                            {
                                MessageBox.Show("Please fill out all the required fields");
                            }
                            else
                            {
                                // Regular expression to check for special characters
                                string pattern = "[^a-zA-Z0-9]";
                                if (Regex.IsMatch(txtItemname.Text, pattern))
                                {
                                    MessageBox.Show("Special characters are not allowed.");
                                }
                                else
                                {
                                    // Regular expression to check for letters
                                    string pattern1 = "[a-zA-Z]";

                                    if (Regex.IsMatch(txtItemprice.Text, pattern1) || Regex.IsMatch(txtItemquantity.Text, pattern1))
                                    {
                                        MessageBox.Show("Invalit Item price and quantity");
                                    }
                                    else
                                    {
                                        var itemInfo = new ProductsInventory
                                        {
                                            ItemPicture = ImageIntoBase64String(pboxItemPricture),
                                            ItemName = txtItemname.Text,
                                            ItemPrice = txtItemprice.Text,
                                            ItemQuantity = txtItemquantity.Text,
                                            ItemDescription = txtItemdescription.Text
                                        };

                                        var allItem = new AllProducts
                                        {
                                            StoreName = lblStorename.Text,
                                            ItemPicture = ImageIntoBase64String(pboxItemPricture),
                                            ItemName = txtItemname.Text,
                                            ItemPrice = txtItemprice.Text,
                                            ItemQuantity = txtItemquantity.Text,
                                            ItemDescription = txtItemdescription.Text
                                        };
                                        SetResponse response1 = await client.SetTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/" + txtItemname.Text, itemInfo);
                                        SetResponse response2 = await client.SetTaskAsync("All Products/" + txtItemname.Text, allItem);

                                        var message = MessageBox.Show("Item added successfully");
                                        if (message == DialogResult.OK)
                                        {
                                            pboxItemPricture.Image = default;
                                            txtItemname.Text = "";
                                            txtItemprice.Text = "";
                                            txtItemquantity.Text = "";
                                            txtItemdescription.Text = "";

                                            MainScreenForm.flowLayout.Invalidate();

                                            flpInventory.Controls.Clear();
                                            ViewProduct();
                                        }
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

        private async void btnSearchItem_Click(object sender, EventArgs e)
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
                            FirebaseResponse response = await client.GetTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/" + txtItemname.Text);

                            if (txtItemname.Text != "")
                            {
                                if (response.Body != "null")
                                {
                                    // Regular expression to check for special characters
                                    string pattern = "[^a-zA-Z0-9]";
                                    if (Regex.IsMatch(txtItemname.Text, pattern))
                                    {
                                        MessageBox.Show(txtItemname.Text + " does not exist");

                                        pboxItemPricture.Image = default;
                                        txtItemname.Text = "";
                                        txtItemprice.Text = "";
                                        txtItemquantity.Text = "";
                                        txtItemdescription.Text = "";

                                        btnAddItem.Enabled = true;
                                        txtItemname.ReadOnly = false;
                                    }
                                    else
                                    {
                                        ProductsInventory result = response.ResultAs<ProductsInventory>();

                                        byte[] image = Convert.FromBase64String(result.ItemPicture);
                                        MemoryStream ms = new MemoryStream();
                                        ms.Write(image, 0, Convert.ToInt32(image.Length));
                                        Bitmap bm = new Bitmap(ms, false);
                                        ms.Dispose();

                                        pboxItemPricture.Image = bm;
                                        txtItemname.Text = result.ItemName;
                                        txtItemprice.Text = result.ItemPrice;
                                        txtItemquantity.Text = result.ItemQuantity;
                                        txtItemdescription.Text = result.ItemDescription;

                                        btnUpdateItem.Enabled = true;
                                        btnRemove.Enabled = true;
                                        btnAddItem.Enabled = false;
                                        txtItemname.ReadOnly = true;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show(txtItemname.Text + " does not exist");

                                    pboxItemPricture.Image = default;
                                    txtItemname.Text = "";
                                    txtItemprice.Text = "";
                                    txtItemquantity.Text = "";
                                    txtItemdescription.Text = "";

                                    btnAddItem.Enabled = true;
                                    txtItemname.ReadOnly = false;
                                }
                            }
                            else
                            {
                                var message = MessageBox.Show("Please write the name of the item in Item name textbox");
                                if (message == DialogResult.OK)
                                {
                                    pboxItemPricture.Image = default;
                                    txtItemname.Text = "";
                                    txtItemprice.Text = "";
                                    txtItemquantity.Text = "";
                                    txtItemdescription.Text = "";

                                    btnAddItem.Enabled = true;
                                    txtItemname.ReadOnly = false;
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

        private async void btnEditItem_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnAddItem.Enabled = true;
                    btnSearchItem.Enabled = true;
                    txtItemname.ReadOnly = false;
                    btnUpdateItem.Enabled = false;
                    btnRemove.Enabled = false;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/" + txtItemname.Text);

                            if (response.Body != "null")
                            {
                                ProductsInventory result = response.ResultAs<ProductsInventory>();

                                var itemInfo = new ProductsInventory
                                {
                                    ItemPicture = result.ItemPicture,
                                    ItemName = txtItemname.Text,
                                    ItemPrice = txtItemprice.Text,
                                    ItemQuantity = txtItemquantity.Text,
                                    ItemDescription = txtItemdescription.Text
                                };

                                var allItem = new AllProducts
                                {
                                    StoreName = lblStorename.Text,
                                    ItemPicture = result.ItemPicture,
                                    ItemName = txtItemname.Text,
                                    ItemPrice = txtItemprice.Text,
                                    ItemQuantity = txtItemquantity.Text,
                                    ItemDescription = txtItemdescription.Text
                                };
                                FirebaseResponse response1 = await client.UpdateTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/" + txtItemname.Text, itemInfo);
                                FirebaseResponse response2 = await client.UpdateTaskAsync("All Products/" + txtItemname.Text, allItem);

                                var message = MessageBox.Show("Item updated successfully");
                                if (message == DialogResult.OK)
                                {
                                    pboxItemPricture.Image = default;
                                    txtItemname.Text = "";
                                    txtItemprice.Text = "";
                                    txtItemquantity.Text = "";
                                    txtItemdescription.Text = "";

                                    MainScreenForm.flowLayout.Invalidate();

                                    flpInventory.Controls.Clear();
                                    ViewProduct();
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

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnAddItem.Enabled = true;
                    btnSearchItem.Enabled = true;
                    txtItemname.ReadOnly = false;
                    btnUpdateItem.Enabled = false;
                    btnRemove.Enabled = false;

                    FirebaseResponse response1 = await client.DeleteTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/" + txtItemname.Text);
                    FirebaseResponse response2 = await client.DeleteTaskAsync("All Products/" + txtItemname.Text);

                    var message = MessageBox.Show("Item removed successfully");
                    if (message == DialogResult.OK)
                    {
                        pboxItemPricture.Image = default;
                        txtItemname.Text = "";
                        txtItemprice.Text = "";
                        txtItemquantity.Text = "";
                        txtItemdescription.Text = "";

                        MainScreenForm.flowLayout.Invalidate();

                        flpInventory.Controls.Clear();
                        ViewProduct();
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

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            try
            {
                ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    pboxItemPricture.Load(ofd.FileName);
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

        async void btnEvent(object sender, EventArgs ugh)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    Button btn = (Button)sender;

                    btnAddItem.Enabled = false;
                    btnSearchItem.Enabled = false;
                    btnUpdateItem.Enabled = true;
                    btnRemove.Enabled = true;
                    txtItemname.ReadOnly = true;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/" + btn.Name.ToString());
                            ProductsInventory result = response.ResultAs<ProductsInventory>();

                            byte[] image = Convert.FromBase64String(result.ItemPicture);
                            MemoryStream ms = new MemoryStream();
                            ms.Write(image, 0, Convert.ToInt32(image.Length));
                            Bitmap bm = new Bitmap(ms, false);
                            ms.Dispose();

                            pboxItemPricture.Image = bm;
                            txtItemname.Text = result.ItemName;
                            txtItemprice.Text = result.ItemPrice;
                            txtItemquantity.Text = result.ItemQuantity;
                            txtItemdescription.Text = result.ItemDescription;
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
                            FirebaseResponse response = client.Get("Stores/" + MyStoreLoginForm.storenameHolder + "/" + "Product Inventory/");

                            if (response.Body != "null")
                            {
                                Dictionary<string, ProductsInventory> result = response.ResultAs<Dictionary<string, ProductsInventory>>();
                                foreach (var info in result)
                                {
                                    string picture = info.Value.ItemPicture;
                                    string name = info.Value.ItemName;
                                    string price = info.Value.ItemPrice;
                                    string Store = info.Value.ItemDescription;

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

                                    Label itemInfo = itemInf(name);

                                    Button btn = btnC();
                                    btn.Name = getProductName;
                                    btn.Text = "Select";

                                    panel.Controls.Add(itemImage);
                                    panel.Controls.Add(btn);
                                    panel.Controls.Add(itemInfo);

                                    flpInventory.Controls.Add(panel);

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
            itemPicture.Size = new Size(105, 92);

            return itemPicture;
        }
        Button btnC()
        {
            btnClick = new Button();
            btnClick.Location = new Point(337, 35);
            btnClick.BackColor = Color.LimeGreen;
            btnClick.ForeColor = Color.White;
            btnClick.FlatStyle = FlatStyle.Popup;

            return btnClick;
        }
        Label itemInf(string itemName)
        {
            String newLine = Environment.NewLine;

            itemI = new Label();
            itemI = new Label();
            itemI.AutoSize = false;
            itemI.ForeColor = Color.White;
            itemI.Text = "" + itemName;
            itemI.Location = new Point(114, 36);
            itemI.AutoSize = true;

            return itemI;
        }
        Panel productPanels(string name)
        {
            productP = new Panel();
            productP.Name = name;
            productP.BorderStyle = BorderStyle.FixedSingle;
            productP.BackColor = Color.DarkOrchid;
            productP.Size = new Size(429, 90);

            return productP;
        }

        async void ViewProductSold()
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
                            FirebaseResponse response1 = client.Get("Accounts/");

                            if (response1.Body != "null")
                            {
                                Dictionary<string, CreateAccount> result1 = response1.ResultAs<Dictionary<string, CreateAccount>>();

                                foreach (var names in result1)
                                {
                                    string usernames = names.Value.Username;

                                    FirebaseResponse response = client.Get("Accounts/" + usernames + "/" + "Order History/");

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

                                            if (lblStorename.Text == Store)
                                            {
                                                byte[] image = Convert.FromBase64String(picture);
                                                MemoryStream ms = new MemoryStream();
                                                ms.Write(image, 0, Convert.ToInt32(image.Length));
                                                Bitmap bm = new Bitmap(ms, false);
                                                ms.Dispose();

                                                string getProductName = name;

                                                Panel panel = productPanels1(name.ToString());

                                                PictureBox itemImage = itempic1();
                                                itemImage.Image = bm;
                                                itemImage.SizeMode = PictureBoxSizeMode.StretchImage;

                                                Label itemInfo = itemInf(name, price, Store, quantity);

                                                panel.Controls.Add(itemImage);
                                                panel.Controls.Add(itemInfo);

                                                flpProductSold.Controls.Add(panel);

                                            }
                                        }
                                    }
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
        PictureBox itempic1()
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
        Panel productPanels1(string name)
        {
            Panel productP = new Panel();
            productP.Name = name;
            productP.BorderStyle = BorderStyle.FixedSingle;
            productP.BackColor = Color.DarkOrchid;
            productP.Size = new Size(840, 128);

            return productP;
        }

        private void btnClearTxt_Click(object sender, EventArgs e)
        {
            btnAddItem.Enabled = true;
            btnSearchItem.Enabled = true;
            btnUpdateItem.Enabled = false;
            btnRemove.Enabled = false;
            txtItemname.ReadOnly = false;

            pboxItemPricture.Image = default;
            txtItemname.Text = "";
            txtItemprice.Text = "";
            txtItemquantity.Text = "";
            txtItemdescription.Text = "";
        }
    }
}
