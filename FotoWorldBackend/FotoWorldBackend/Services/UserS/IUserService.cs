using FotoWorldBackend.Models;
using FotoWorldBackend.Models.UserModels;
using Microsoft.AspNetCore.Mvc;

namespace FotoWorldBackend.Services.UserS
{
    public interface IUserService
    {
        public OfferWithPhotos GetOfferDetailed(int offerId);

        public List<OfferListObject> GetOfferList();

        public bool AddOfferToFavourite(int offerId, string userId);

        public bool RemoveOfferFromFavourite(int offerId, string userId);

        public bool CreateOperatorOpinion(CreateOperatorOpinionModel opinion);

        public bool RemoveOperatorOpinion(CreateOperatorOpinionModel opinion);

        FileStream GetImageById(int id);
    }
}
