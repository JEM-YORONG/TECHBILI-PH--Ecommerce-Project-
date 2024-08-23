using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TECHBILI_PH
{
    public partial class CheckoutForm : Form
    {
        public CheckoutForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        Label itemI;
        Panel productP;
        PictureBox itemPicture;

        public string orderQuantity;

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    FirebaseResponse response1 = await client.DeleteTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");
                    CartForm cartForm = new CartForm();
                    cartForm.Show();
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

        private async void CheckoutForm_Load(object sender, EventArgs e)
        {
            lblMessage.Visible = false;

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

                            lblName.Text = result.Fullname;
                            lblAddress.Text = result.Address;
                            lblContact.Text = result.Contact_number;


                            //display all items in checkout
                            ViewProduct();

                            try
                            {
                                client = new FireSharp.FirebaseClient(conf);

                                if (client != null)
                                {
                                    FirebaseResponse response1 = client.Get("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");

                                    if (response1.Body != "null")
                                    {
                                        List<int> quantity = new List<int>();
                                        List<int> price = new List<int>();
                                        List<int> total = new List<int>();

                                        Dictionary<string, Checkout> result1 = response1.ResultAs<Dictionary<string, Checkout>>();

                                        foreach (var item in result1)
                                        {
                                            quantity.Add(Convert.ToInt32(item.Value.ItemQuantity));
                                            price.Add(Convert.ToInt32(item.Value.ItemPrice));
                                        }

                                        for (int i = 0; i < quantity.Count; i++)
                                        {
                                            if (i == quantity.Count)
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                total.Add(quantity[i] * price[i]);
                                            }
                                        }

                                        lblTotal.Text = total.Sum().ToString();
                                    }
                                }
                            }
                            catch (Exception err)
                            {
                                MessageBox.Show("" + err);
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

        private async void btnOrder_Click(object sender, EventArgs e)
        {
            timer1.Start();
            lblMessage.Visible = true;
            lblMessage.Text = "Please wait...";

            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    btnOrder.Enabled = false;
                    btnCancel.Enabled = false;
                    //add to order history of this user
                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");
                            Dictionary<string, Checkout> result = response.ResultAs<Dictionary<string, Checkout>>();

                            foreach (var item in result)
                            {
                                orderQuantity = item.Value.ItemQuantity;
                                DateTime now = DateTime.Now;

                                var itemInfo = new OrderHistory
                                {
                                    StoreName = item.Value.StoreName,
                                    ItemPicture = item.Value.ItemPicture,
                                    ItemName = item.Value.ItemName + " (" + now.ToString("F") + ")",
                                    ItemPrice = item.Value.ItemPrice,
                                    ItemDescription = item.Value.ItemDescription,
                                    ItemQuantity = item.Value.ItemQuantity
                                };
                                try
                                {
                                    client = new FireSharp.FirebaseClient(conf);

                                    if (client != null)
                                    {
                                        FirebaseResponse response4 = await client.GetTaskAsync("Stores/" + item.Value.StoreName + "/" + "Product Inventory/" + item.Value.ItemName);
                                        ProductsInventory result4 = response4.ResultAs<ProductsInventory>();

                                        if (Convert.ToInt32(result4.ItemQuantity) == 0 || Convert.ToInt32(result4.ItemQuantity) < 0)
                                        {
                                            MessageBox.Show("Item is out of stock");

                                            FirebaseResponse response0 = await client.DeleteTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");

                                            MainScreenForm main = new MainScreenForm();
                                            main.Show();
                                            this.Close();
                                            break;
                                        }
                                        else
                                        {
                                            //add items in Order History
                                            SetResponse response2 = await client.SetTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Order History/" + item.Value.ItemName + " " + now.ToString("F"), itemInfo);
                                            //remove items in Cart
                                            FirebaseResponse response3 = await client.DeleteTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Cart/" + item.Value.ItemName);
                                            //Remove Checkout 
                                            FirebaseResponse response1 = await client.DeleteTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");
                                            //minus quantity to seller inventory
                                            int quantity = Convert.ToInt32(result4.ItemQuantity) - Convert.ToInt32(orderQuantity);

                                            var itemInfo1 = new ProductsInventory
                                            {
                                                ItemPicture = result4.ItemPicture,
                                                ItemName = result4.ItemName,
                                                ItemPrice = result4.ItemPrice,
                                                ItemQuantity = quantity.ToString(),
                                                ItemDescription = result4.ItemDescription
                                            };

                                            var allItem = new AllProducts
                                            {
                                                StoreName = item.Value.StoreName,
                                                ItemPicture = result4.ItemPicture,
                                                ItemName = result4.ItemName,
                                                ItemPrice = result4.ItemPrice,
                                                ItemQuantity = quantity.ToString(),
                                                ItemDescription = result4.ItemDescription
                                            };
                                            FirebaseResponse response5 = await client.UpdateTaskAsync("Stores/" + item.Value.StoreName + "/" + "Product Inventory/" + item.Value.ItemName, itemInfo1);
                                            FirebaseResponse response6 = await client.UpdateTaskAsync("All Products/" + item.Value.ItemName, allItem);


                                        }
                                    }
                                }
                                catch (Exception err)
                                {
                                    timer1.Stop();
                                    MessageBox.Show("We’re unable to process your order at this time due to an issue with the Product.A", "Product unavailable");

                                    MainScreenForm main = new MainScreenForm();
                                    main.Show();
                                    this.Close();
                                }
                            }
                        }
                    }
                    catch (Exception err)
                    {
                        //MessageBox.Show("We’re unable to process your order at this time due to an issue with the Product.A", "Product unavailable");
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
                            FirebaseResponse response = client.Get("Accounts/" + LoginForm.userNameHolder + "/" + "Checkout/");

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

                                    panel.Controls.Add(itemImage);
                                    panel.Controls.Add(itemInfo);

                                    flpChechoutItem.Controls.Add(panel);
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

        public int count = 30;

        private void timer1_Tick(object sender, EventArgs e)
        {
            count--;

            lblCount.Text = count.ToString();

            run(count);
        }
        void run(int run)
        {
            if (run == 10)
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Item ordered successfully. Seller will contact you about the delivery please wait a few minutes. ";
            }
            if (run == 0)
            {
                timer1.Stop();
                MainScreenForm main = new MainScreenForm();
                main.Show();
                this.Close();
            }
        }
    }
}
