using PdfGenerator.Services;

PdfService.SetQuestPdfLicense();

//var invoiceService = new InvoiceService();
//var document = invoiceService.GenerateDoc();

var docService = new RoyaltyService();
docService.GenerateDoc();