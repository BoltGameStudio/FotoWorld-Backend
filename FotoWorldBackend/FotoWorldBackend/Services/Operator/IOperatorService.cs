using FotoWorldBackend.Models;
using FotoWorldBackend.Models.OperatorModels;

namespace FotoWorldBackend.Services.Operator
{
    public interface IOperatorService
    {
        public Offer CreateOffer(CreateOfferModel offer);

        public Offer UpdateOffer(CreateOfferModel offer);

        public List<int> UploadPhotos(CreateOfferModel offer);
    }
}
