using MediatR;
using PdfGenerator.Models.Royalty;

namespace PdfGenerator.Queries;

public sealed record GetRoyaltyQuery(RoyaltyFilter Filter) : IRequest<RoyaltyModel>;