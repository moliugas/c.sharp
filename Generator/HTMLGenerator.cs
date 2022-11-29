
using RestaurantSystem.Entity;
namespace RestaurantSystem
{

    public static class HTMLGenerator
    {
        public static string GenerateReceiptHTML(Receipt receipt)
        {
            string result = $"<!DOCTYPE html>  <div id=\"invoice-POS\">        <center id=\"top\">      <div class=\"logo\"></div>      <div class=\"info\">         <h2>{receipt.RestaurantName}</h2>      </div><!--End Info-->    </center>       <div id=\"mid\">      <div class=\"info\">        <h2>Contact Info</h2>        <p>             Food tax : {receipt.FoodTax}</br>            Drinks tax: {receipt.DrinksTax}</br>        </p>      </div>    </div> <div id=\"bot\"><div id=\"table\"><table><tr class=\"tabletitle\"><td class=\"item\"><h2>Item</h2></td><td class=\"Hours\"><h2>Qty</h2></td><td class=\"Rate\"><h2>Sub Total</h2></td></tr>";

            foreach (ReceiptItem item in receipt.Items)
            {

                result += $"<tr class=\"service\"><td class=\"tableitem\"><p class=\"itemtext\">{item.Name}</p></td><td class=\"tableitem\"><p class=\"itemtext\">{item.Amount}</p></td><td class=\"tableitem\"><p class=\"itemtext\">{item.Price * item.Amount}</p></td></tr>";
            }

            return result += $"<tr class=\"tabletitle\"><td></td><td class=\"Rate\"><h2>Total sum: {receipt.TotalSum}</h2></td><td class=\"payment\"><h2>Total tax: {receipt.TotalSum * receipt.FoodTax / 100}</h2></td></tr></table></div><div id=\"legalcopy\"><p class=\"legal\"><strong>Thank you for your business!</strong></p></div></div></div>";

        }

    }
}