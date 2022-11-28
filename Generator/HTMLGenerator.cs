
using RestaurantSystem.Entity;
namespace RestaurantSystem
{

    public static class HTMLGenerator
    {
        public static string GenerateReceiptHTML(Receipt receipt)
        {
            string result = $"<!DOCTYPE html>\r\n  <div id=\"invoice-POS\">\r\n    \r\n    <center id=\"top\">\r\n      <div class=\"logo\"></div>\r\n      <div class=\"info\"> \r\n        <h2>SBISTechs Inc</h2>\r\n      </div><!--End Info-->\r\n    </center><!--End InvoiceTop-->\r\n    \r\n    <div id=\"mid\">\r\n      <div class=\"info\">\r\n        <h2>Contact Info</h2>\r\n        <p> \r\n            Food tax : {receipt.FoodTax}</br>\r\n            Drinks tax: {receipt.DrinksTax}</br>\r\n        </p>\r\n      </div>\r\n    </div> <div id=\"bot\">\r\n\r\n\t\t\t\t\t<div id=\"table\">\r\n\t\t\t\t\t\t<table>\r\n\t\t\t\t\t\t\t<tr class=\"tabletitle\">\r\n\t\t\t\t\t\t\t\t<td class=\"item\"><h2>Item</h2></td>\r\n\t\t\t\t\t\t\t\t<td class=\"Hours\"><h2>Qty</h2></td>\r\n\t\t\t\t\t\t\t\t<td class=\"Rate\"><h2>Sub Total</h2></td>\r\n\t\t\t\t\t\t\t</tr>\r\n";

            foreach (ReceiptItem item in receipt.Items)
            {

                result += $"\t\t\t\t\t\t\t<tr class=\"service\">\r\n\t\t\t\t\t\t\t\t<td class=\"tableitem\"><p class=\"itemtext\">{item.Name}</p></td>\r\n\t\t\t\t\t\t\t\t<td class=\"tableitem\"><p class=\"itemtext\">{item.Amount}</p></td>\r\n\t\t\t\t\t\t\t\t<td class=\"tableitem\"><p class=\"itemtext\">{item.Price * item.Amount}</p></td>\r\n\t\t\t\t\t\t\t</tr>";
            }

            return result += $"\t\t\t\t\t\t\t<tr class=\"tabletitle\">\r\n\t\t\t\t\t\t\t\t<td></td>\r\n\t\t\t\t\t\t\t\t<td class=\"Rate\"><h2>{receipt.TotalSum}</h2></td>\r\n\t\t\t\t\t\t\t\t<td class=\"payment\"><h2>{receipt.TotalSum * receipt.FoodTax}</h2></td>\r\n\t\t\t\t\t\t\t</tr>\t</table>\r\n\t\t\t\t\t</div><!--End Table-->\r\n\r\n\t\t\t\t\t<div id=\"legalcopy\">\r\n\t\t\t\t\t\t<p class=\"legal\"><strong>Thank you for your business!</strong></p>\r\n\t\t\t\t\t</div>\r\n\r\n\t\t\t\t</div>\r\n</div>";

        }

    }
}