using PdfGenerator.Data;
using PdfGenerator.Services;
using System.Diagnostics;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;

// Ref - https://www.questpdf.com/license/configuration.html
QuestPDF.Settings.License = LicenseType.Community;

var filePath = "invoice.pdf";

var model = InvoiceDocumentDataSource.GetInvoiceDetails();
var document = new InvoiceDocument(model);

// instead of the standard way of generating a PDF file
//document.GeneratePdf(filePath);
//Process.Start("explorer.exe", filePath);

// Ref - https://www.questpdf.com/document-previewer.html
// To view in previewer
document.ShowInPreviewer();

