using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Reports.EBoard;
using PdfGenerator.Models.Reports.EBoard;

namespace PdfGenerator.Data.Reports.EBoard;

public class DispatchSumDocDataSource : IDispatchSumDocDataSource
{
    private readonly IDispatchSumRepo _dsRepo;
    private readonly ILogger<DispatchSumDocDataSource> _logger;
    private usp_EBoard_DispatchSummary_Result _disSumResp;

    public DispatchSumDocDataSource(IDispatchSumRepo dsRepo, ILogger<DispatchSumDocDataSource> logger)
    {
        _dsRepo = dsRepo;
        _logger = logger;
    }

    public async Task<DispatchSumModel> GetDispatchSummaryModelAsync(DispatchSumFilter filter)
    {
        _disSumResp = await _dsRepo.GetDispatchSummaryAsync(filter);

        _logger.LogInformation("Generating dispatch summary model for date range {StartDate} - {EndDate}", filter.StartDate, filter.EndDate);

        //var header = new Header
        //{
        //    CompanyLogoPath = GetImageAbsolutePath("CompanyLogo.png"),
        //    Title = Resources.LetterHeader,
        //    LocalLogoPath = GetImageAbsolutePath("LocalLogo.png")
        //};

        //var address = new Address
        //{
        //    Name = _disSumResp.Contact,
        //    Designation = _disSumResp.ContactTitle,
        //    Employer = _disSumResp.Employer,
        //    Address1 = _disSumResp.Address1,
        //    Address2 = _disSumResp.Address2,
        //    CountryWithPinCode = _disSumResp._CSZ
        //};

        //var body = new Body
        //{
        //    FirstPara = _disSumResp.StepOne.Trim(),
        //    SecondPara = _disSumResp.Expr5.Trim(),
        //    ThirdPara = _disSumResp.Issue.Trim(),
        //    ClosingPara = _disSumResp.Remedy.Trim()
        //};

        //var signature = new Signature
        //{
        //    ComplementaryClose = Resources.ComplementaryClose,
        //    WriterName = _disSumResp.Assigned,
        //    WriterDesignation = _disSumResp.AssingedTitle
        //};

        //var cc = new CarbonCopy
        //{
        //    PersonThree = string.IsNullOrWhiteSpace(_disSumResp.CC) ? "Laura Brassington" : _disSumResp.CC
        //};

        //var footer = new Footer
        //{
        //    Telephone = Resources.Telephone,
        //    Fax = Resources.Fax,
        //    CompanyAddressFirstLine = Resources.CompanyAddressFirstLine,
        //    CompanyAddressSecondLine = Resources.CompanyAddressSecondLine,
        //    CompanyWebsite = Resources.CompanyWebsite
        //};

        return new DispatchSumModel
        {
            //Header = header,
            //Address = address,
            //Body = body,
            //Signature = signature,
            //CarbonCopy = cc,
            //Footer = footer
        };
    }
}