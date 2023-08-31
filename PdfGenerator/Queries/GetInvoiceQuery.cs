using MediatR;
using PdfGenerator.Sample_Models;

namespace PdfGenerator.Queries;

public sealed record GetInvoiceQuery : IRequest<InvoiceModel>;