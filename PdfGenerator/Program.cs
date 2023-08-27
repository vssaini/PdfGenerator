using PdfGenerator.Services;

PdfService.SetQuestPdfLicense();
PdfService.SetAppCulture();

//var invoiceService = new InvoiceService();
//invoiceService.GenerateDoc();

var docService = new RoyaltyService();
docService.GenerateDoc();