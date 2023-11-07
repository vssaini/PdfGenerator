using MediatR;
using PdfGenerator.Models.Invoice;

namespace PdfGenerator.Queries;

public sealed record GetInvoiceQuery : IRequest<InvoiceModel>;