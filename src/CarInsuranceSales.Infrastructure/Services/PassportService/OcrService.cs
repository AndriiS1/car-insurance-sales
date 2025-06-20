using CarInsuranceSales.Domain.Models.Document;
using CarInsuranceSales.Infrastructure.Services.PassportService.Dtos;
using Mindee;
using Mindee.Input;
using Mindee.Product.Passport;
namespace CarInsuranceSales.Infrastructure.Services.PassportService;

public class OcrService(MindeeClient mindeeClient) : IOcrService
{
    public async Task<PassportResponse?> GetDocumentData(Document document)
    {
        var inputSource = new LocalInputSource(document.FilePath);
        
        var response = await mindeeClient
            .ParseAsync<PassportV1>(inputSource);

        if (response.Document.Inference.Prediction == null)
        {
            return null;
        }

        var passport = response.Document.Inference.Prediction;
        
        return new PassportResponse
        {
            IdValue = passport.IdNumber.Value,
            FirstName = passport.GivenNames.First().Value,
            LastName = passport.Surname.Value,
            BirthDate = passport.BirthDate.Value,
            BirthPlace = passport.BirthPlace.Value,
            IssuanceDate = passport.IssuanceDate.Value,
            ExpiryDate = passport.ExpiryDate.Value,
            Country = passport.Country.Value,
            Gender = passport.Gender.Value,
            Mrz1 = passport.Mrz1.Value,
            Mrz2 = passport.Mrz2.Value
        };
    }
}
