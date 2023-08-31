using MediatR;
using PdfGenerator.Models;

namespace PdfGenerator.Queries;

public sealed record GetRoyaltyQuery(DocFilter Filter) : IRequest<RoyaltyModel>;