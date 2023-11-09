using PdfGenerator.Contracts.Grievance;
using PdfGenerator.Models.Grievance.LetterStepOne;
using PdfGenerator.Properties;
using PdfGenerator.Queries;

namespace PdfGenerator.Data.Grievance
{
    public sealed class GrievanceDocDataSource : IGrievanceDocDataSource
    {
        private readonly IGrievanceRepo _grvRepo;
        private GrievanceLetterStepOneResponse _grvLetStepOneResp;

        public GrievanceDocDataSource(IGrievanceRepo grvRepo)
        {
            _grvRepo = grvRepo;
        }

        public async Task<GrievanceLetterStepOneModel> GetGrievanceStepOneModelAsync(GetGrievanceStepOneQuery query)
        {
            _grvLetStepOneResp = await _grvRepo.GetGrievanceLetterStepOneAsync(query);

            var heading = new Heading
            {
                CompanyLogo = Resources.CompanyLogo,
                Title = Resources.LetterHeader,
                LocalLogo = Resources.LocalLogo
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
                FirstPara = _grvLetStepOneResp.StepOne,
                SecondPara = _grvLetStepOneResp.Expr5,
                ThirdPara = _grvLetStepOneResp.Issue,
                ClosingPara = _grvLetStepOneResp.Remedy
            };

            var signature = new Signature
            {
                ComplementaryClose = Resources.ComplementaryClose,
                WriterName = _grvLetStepOneResp.Assigned,
                WriterDesignation = _grvLetStepOneResp.AssingedTitle
            };

            var cc = new CarbonCopy
            {
                PersonThree = _grvLetStepOneResp.CC
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
                Heading = heading,
                Address = address,
                Subject = _grvLetStepOneResp.Re,
                Body = body,
                Signature = signature,
                CarbonCopy = cc,
                CertifiedStatement = Resources.CertifiedStatement,
                Footer = footer
            };
        }
    }
}
