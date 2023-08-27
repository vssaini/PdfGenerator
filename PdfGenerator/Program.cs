using PdfGenerator.Services;

PdfService.SetQuestPdfLicense();

//var invoiceService = new InvoiceService();
//invoiceService.GenerateDoc();

var docService = new RoyaltyService();
docService.GenerateDoc();