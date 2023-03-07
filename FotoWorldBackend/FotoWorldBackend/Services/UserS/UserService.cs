using FotoWorldBackend.Models;
using FotoWorldBackend.Models.UserModels;
using FotoWorldBackend.Services.Email;
using Microsoft.AspNetCore.Mvc;

namespace FotoWorldBackend.Services.UserS
{
    public class UserService : IUserService
    {
        private readonly FotoWorldContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _config;

        public UserService(FotoWorldContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public bool AddOfferToFavourite(int offerId, string userId)
        {
            throw new NotImplementedException();
        }

        public bool CreateOperatorOpinion(CreateOperatorOpinionModel opinion)
        {
            throw new NotImplementedException();
        }

        public FileStream GetImageById(int id)
        {
            var photo = _context.Photos.FirstOrDefault(m=>m.Id == id);
            var image = System.IO.File.OpenRead(photo.PhotoUrl);
            return image;
        }

        public OfferWithPhotos GetOfferDetailed(int offerId)
        {
            var offer = _context.Offers.FirstOrDefault(o => o.Id == offerId);
            if (offer == null)
            {
                return null;
            }


            var oper = _context.Operators.FirstOrDefault(o => o.Id == offer.OperatorId);
            var operAcc = _context.Users.FirstOrDefault(o => o.Id == oper.AccountId);
            var cont = new List<string>
            {
                operAcc.Email,
                operAcc.PhoneNumber
            };


            var photos = _context.OfferPhotos.Where(m => m.OfferId == offerId).ToList();
            
            var attachments = new List<int>();

            foreach (var photo in photos)
            {
                attachments.Add(photo.Id);
            }

            var ret = new OfferWithPhotos {
                Description = offer.Description,
                Title = offer.Title,
                OperatorId = offer.OperatorId,
                OperatorName = operAcc.Username,
                OperatorContact = cont,
                PhotosId = attachments,
                OfferId = offerId
            
            };

            return ret;

        }

        public List<OfferListObject> GetOfferList()
        {
            var offers= _context.Offers.ToList();
            if(offers == null)
            {
                return null;
            }
            var ret = new List<OfferListObject>();
            foreach (var offer in offers)
            {
                try
                {

                    var oper = _context.Operators.FirstOrDefault(o => o.Id == offer.OperatorId);
                    var operAcc = _context.Users.FirstOrDefault(o => o.Id == oper.AccountId);
                    var x = new OfferListObject
                    {
                        OfferId = offer.Id,
                        Title = offer.Title,
                        OperatorName = operAcc.Username
                    };

                    ret.Add(x);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            return ret;
        }

        public bool RemoveOfferFromFavourite(int offerId, string userId)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOperatorOpinion(CreateOperatorOpinionModel opinion)
        {
            throw new NotImplementedException();
        }
    }
}
