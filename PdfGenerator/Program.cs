using PdfGenerator.Components.Royalty;
using PdfGenerator.Services;

PdfService.ConfigureQuestPdfLicense();

//var document = InvoiceService.GenerateInvoiceDoc();
var document = RoyaltyService.GenerateRoyaltyDoc();

var filePath = document is RoyaltyDocument ? "royalty.pdf" : "invoice.pdf";
PdfService.GeneratePdf(document, filePath);

// Ref - https://www.questpdf.com/document-previewer.html
// To view in previewer
//document.ShowInPreviewer();