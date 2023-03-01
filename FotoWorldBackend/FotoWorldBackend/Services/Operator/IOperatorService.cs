using FotoWorldBackend.Models;
using FotoWorldBackend.Models.OperatorModels;

namespace FotoWorldBackend.Services.Operator
{
    public interface IOperatorService
    {
        public Offer CreateOffer(CreateOfferModel offer, string authorId);

        public Offer UpdateOffer(CreateOfferModel newOffer, int oldOfferId);

        public List<int> UploadPhotos(CreateOfferModel offer);
    }
}
