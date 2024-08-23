using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TECHBILI_PH
{
    class InternetChecker
    {
        [DllImport("wininet.dll")]
        public extern static bool InternetGetConnectedState(out int Description, int Reservation);
    }
    class config
    {
        public static string authSecret = "3ctOwCKFWxZWj3O0ScpcHrsPZGdF2bEJ87piMAOo";
        public static string basePath = "https://e-commerce-database-32ccd-default-rtdb.asia-southeast1.firebasedatabase.app/";
    }
    class Admin
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
    class CreateAccount
    {
        public string UserPicture { get; set; }
        public string Fullname { get; set; }
        public string Contact_number { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    class CreateStore
    {
        public string Sellername { get; set; }
        public string Contact_Number { get; set; }
        public string Address { get; set; }
        public string Storename { get; set; }
        public string Password { get; set; }
    }
    class ProductsInventory
    {
        public string ItemPicture { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemQuantity { get; set; }
        public string ItemDescription { get; set; }
    }
    class AllProducts
    {
        public string StoreName { get; set; }
        public string ItemPicture { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemQuantity { get; set; }
        public string ItemDescription { get; set; }
    }
    class Cart
    {
        public string StoreName { get; set; }
        public string ItemPicture { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemQuantity { get; set; }
        public string ItemDescription { get; set; }
    }
    class Checkout
    {
        public string StoreName { get; set; }
        public string ItemPicture { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemQuantity { get; set; }
        public string ItemDescription { get; set; }
    }
    class OrderHistory
    {
        public string StoreName { get; set; }
        public string ItemPicture { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemQuantity { get; set; }
        public string ItemDescription { get; set; }
    }
}
