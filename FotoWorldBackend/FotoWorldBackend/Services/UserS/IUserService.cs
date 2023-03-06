using FotoWorldBackend.Models;
using FotoWorldBackend.Models.UserModels;

namespace FotoWorldBackend.Services.UserS
{
    public interface IUserService
    {
        public Offer GetOfferDetailed(int offerId);

        public List<Offer> GetOfferList();

        public bool AddOfferToFavourite(int offerId, string userId);

        public bool RemoveOfferFromFavourite(int offerId, string userId);

        public bool CreateOperatorOpinion(CreateOperatorOpinionModel opinion);

        public bool RemoveOperatorOpinion(CreateOperatorOpinionModel opinion);

    }
}
