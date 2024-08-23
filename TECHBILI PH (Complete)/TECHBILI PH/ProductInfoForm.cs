using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TECHBILI_PH
{
    public partial class ProductInfoForm : Form
    {
        public ProductInfoForm()
        {
            InitializeComponent();
        }

        IFirebaseConfig conf = new FirebaseConfig
        {
            AuthSecret = config.authSecret,
            BasePath = config.basePath
        };
        IFirebaseClient client;

        public int quantity;

        private void btnBAck_Click(object sender, EventArgs e)
        {
            MainScreenForm mainScreen = new MainScreenForm();
            mainScreen.Show();
            this.Close();
        }

        private async void ProductInfoForm_Load(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {
                    btnMinus.Enabled = false;
                    btnAddToCart.Enabled = false;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("All Products/" + MainScreenForm.btnName);
                            AllProducts result = response.ResultAs<AllProducts>();

                            byte[] image = Convert.FromBase64String(result.ItemPicture);
                            MemoryStream ms = new MemoryStream();
                            ms.Write(image, 0, Convert.ToInt32(image.Length));
                            Bitmap bm = new Bitmap(ms, false);
                            ms.Dispose();

                            pboxPicture.Image = bm;
                            lblName.Text = result.ItemName;
                            lblPrice.Text = result.ItemPrice;
                            lblDescription.Text = result.ItemDescription;
                            lblStorename.Text = result.StoreName;
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

        private async void btnAddToCart_Click(object sender, EventArgs e)
        {
            try
            {
                client = new FireSharp.FirebaseClient(conf);

                int d;
                if (InternetChecker.InternetGetConnectedState(out d, 0) == true)
                {

                    btnAddToCart.Enabled = false;

                    try
                    {
                        client = new FireSharp.FirebaseClient(conf);

                        if (client != null)
                        {
                            FirebaseResponse response = await client.GetTaskAsync("All Products/" + MainScreenForm.btnName);
                            AllProducts result = response.ResultAs<AllProducts>();

                            if (Convert.ToInt32(result.ItemQuantity) == 0 || Convert.ToInt32(txtQuantity.Text) > Convert.ToInt32(result.ItemQuantity))
                            {
                                MessageBox.Show(lblName.Text + " quantity left: " + result.ItemQuantity, "Quantity Error");

                                txtQuantity.Text = "";
                                quantity = 0;
                                btnMinus.Enabled = false;
                                btnAddToCart.Enabled = false;
                            }
                            else
                            {
                                var itemInfo = new Cart
                                {
                                    StoreName = lblStorename.Text,
                                    ItemPicture = result.ItemPicture,
                                    ItemName = lblName.Text,
                                    ItemPrice = lblPrice.Text,
                                    ItemDescription = lblDescription.Text,
                                    ItemQuantity = txtQuantity.Text
                                };

                                SetResponse response2 = await client.SetTaskAsync("Accounts/" + LoginForm.userNameHolder + "/" + "Cart/" + lblName.Text, itemInfo);

                                MessageBox.Show("Item added successfully");

                                txtQuantity.Text = "";
                                quantity = 0;
                                btnMinus.Enabled = false;
                                btnAddToCart.Enabled = false;

                                MainScreenForm main = new MainScreenForm();
                                main.Show();
                                this.Hide();
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

        private void btnMinus_Click(object sender, EventArgs e)
        {
            quantity--;
            txtQuantity.Text = quantity.ToString();

            switch (quantity)
            {
                case > 0:
                    btnMinus.Enabled = true;
                    btnAddToCart.Enabled = true;
                    break;
                case <= 0:
                    btnMinus.Enabled = false;
                    btnAddToCart.Enabled = false;
                    break;
            }
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            quantity++;
            txtQuantity.Text = quantity.ToString();

            switch (quantity)
            {
                case > 0:
                    btnMinus.Enabled = true;
                    btnAddToCart.Enabled = true;
                    break;
                case <= 0:
                    btnMinus.Enabled = false;
                    btnAddToCart.Enabled = false;
                    break;
            }
        }
    }
}
