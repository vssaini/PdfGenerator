using PdfGenerator.Services;

PdfService.SetQuestPdfLicense();

//var invoiceService = new InvoiceService();
//var document = invoiceService.GenerateDoc(false,8);

var docService = new RoyaltyService();
docService.GenerateDoc(false, 8);