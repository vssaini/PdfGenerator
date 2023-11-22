using Microsoft.Extensions.Logging;
using PdfGenerator.Contracts.Reports.Grievance;
using PdfGenerator.Models.Reports.Grievance.LetterStepOne;
using PdfGenerator.Properties;
using System.Reflection;

namespace PdfGenerator.Data.Reports.Grievance
{
    public sealed class GrievanceDocDataSource : IGrievanceDocDataSource
    {
        private readonly IGrievanceRepo _grvRepo;
        private GrievanceLetterStepOneResponse _grvLetStepOneResp;
        private readonly ILogger<GrievanceDocDataSource> _logger;

        public GrievanceDocDataSource(IGrievanceRepo grvRepo, ILogger<GrievanceDocDataSource> logger)
        {
            _grvRepo = grvRepo;
            _logger = logger;
        }

        public async Task<GrievanceLetterStepOneModel> GetGrievanceStepOneModelAsync(GrievanceFilter filter)
        {
            _grvLetStepOneResp = await _grvRepo.GetGrievanceLetterStepOneAsync(filter);

            _logger.LogInformation("Generating grievance step one model for {GrievanceId}", filter.GrievanceId);

            var header = new Header
            {
                CompanyLogoPath = GetImageAbsolutePath("CompanyLogo.png"),
                Title = Resources.LetterHeader,
                LocalLogoPath = GetImageAbsolutePath("LocalLogo.png")
            };

            var address = new Address
            {
                Name = _grvLetStepOneResp.Contact,
                Designation = _grvLetStepOneResp.ContactTitle,
                Employer = _grvLetStepOneResp.Employer,
                Address1 = _grvLetStepOneResp.Address1,
                Address2 = _grvLetStepOneResp.Address2,
                CountryWithPinCode = _grvLetStepOneResp._CSZ
            };

            var body = new Body
            {
                FirstPara = _grvLetStepOneResp.StepOne.Trim(),
                SecondPara = _grvLetStepOneResp.Expr5.Trim(),
                ThirdPara = _grvLetStepOneResp.Issue.Trim(),
                ClosingPara = _grvLetStepOneResp.Remedy.Trim()
            };

            var signature = new Signature
            {
                ComplementaryClose = Resources.ComplementaryClose,
                WriterName = _grvLetStepOneResp.Assigned,
                WriterDesignation = _grvLetStepOneResp.AssingedTitle
            };

            var cc = new CarbonCopy
            {
                PersonThree = string.IsNullOrWhiteSpace(_grvLetStepOneResp.CC) ? "Laura Brassington" : _grvLetStepOneResp.CC
            };

            var footer = new Footer
            {
                Telephone = Resources.Telephone,
                Fax = Resources.Fax,
                CompanyAddressFirstLine = Resources.CompanyAddressFirstLine,
                CompanyAddressSecondLine = Resources.CompanyAddressSecondLine,
                CompanyWebsite = Resources.CompanyWebsite
            };

            return new GrievanceLetterStepOneModel
            {
                Header = header,
                Address = address,
                Subject = _grvLetStepOneResp.Re,
                Body = body,
                Signature = signature,
                CarbonCopy = cc,
                CertifiedStatement = Resources.CertifiedStatement,
                Footer = footer
            };
        }

        private static string GetImageAbsolutePath(string imgNameWithExtension)
        {
            var assemblyPath = Assembly.GetExecutingAssembly().Location;
            var dirPath = Path.GetDirectoryName(assemblyPath);

            if (string.IsNullOrEmpty(dirPath))
                throw new DirectoryNotFoundException("Unable to get directory path");

            return Path.Combine(dirPath, "Resources", imgNameWithExtension);
        }
    }
}
