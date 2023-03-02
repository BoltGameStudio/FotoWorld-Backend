using FotoWorldBackend.Models;
using FotoWorldBackend.Models.OperatorModels;

namespace FotoWorldBackend.Services.Operator
{
    public interface IOperatorService
    {
        /// <summary>
        /// Creates offer
        /// </summary>
        /// <param name="offer">offer data</param>
        /// <param name="authorId">encoded operator id</param>
        /// <returns>new offer</returns>
        public Offer CreateOffer(CreateOfferModel offer, string authorId);

        /// <summary>
        /// Edits offer with given id
        /// </summary>
        /// <param name="newOffer">edited data</param>
        /// <param name="authorId">encoded operator id</param>
        /// <param name="oldOfferId"></param>
        /// <returns>edited offer</returns>
        public Offer UpdateOffer(CreateOfferModel newOffer, string authorId, int oldOfferId);

        /// <summary>
        /// saves photos passed in offer form
        /// </summary>
        /// <param name="offer">offer data</param>
        /// <returns>list of new photos ids</returns>
        public List<int> UploadPhotos(CreateOfferModel offer);
    }
}
