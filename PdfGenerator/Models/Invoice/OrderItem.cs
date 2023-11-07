namespace PdfGenerator.Models.Invoice;

public class OrderItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}