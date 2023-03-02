using FotoWorldBackend.Models;
using FotoWorldBackend.Models.OperatorModels;
using FotoWorldBackend.Services.Email;
using FotoWorldBackend.Utilities;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Security.Cryptography;

namespace FotoWorldBackend.Services.Operator
{

    /// <summary>
    /// Provides operations on database for operator controller
    /// </summary>
    public class OperatorService : IOperatorService
    {
        private readonly FotoWorldContext _context;
        private readonly IConfiguration _config;
        public OperatorService(FotoWorldContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Offer CreateOffer(CreateOfferModel offer, string authorId)
        {
            var photosID = UploadPhotos(offer); 
            if(photosID != null)
            {
                var newOffer = new Offer();

                var authorOperator = _context.Operators.FirstOrDefault(m => m.AccountId == Convert.ToInt32(SymmetricEncryption.Decrypt(_config["SECRET_KEY"], authorId)));

                newOffer.OperatorId = authorOperator.Id;
                newOffer.Title= offer.Title;
                newOffer.Description = offer.Description;

                _context.Offers.Add(newOffer);
                _context.SaveChanges();

                try
                {
                    foreach (int id in photosID)
                    {
                        var offerPhoto = new OfferPhoto();
                        offerPhoto.OfferId = newOffer.Id;
                        offerPhoto.PhotoId = id;

                        _context.OfferPhotos.Add(offerPhoto);
                        _context.SaveChanges();
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }




                return newOffer;
            }
            return null;

        }

        public Offer UpdateOffer(CreateOfferModel newOffer, string authorId, int oldOfferId)
        {
            var oldOffer = _context.Offers.FirstOrDefault(m => m.Id == oldOfferId);
            if(oldOffer == null)
            {
                return null;
            }

            var authorIdDecypher = Convert.ToInt32(SymmetricEncryption.Decrypt(_config["SECRET_KEY"], authorId));
            var authorOperator = _context.Operators.FirstOrDefault(m => m.AccountId == authorIdDecypher);

            if ( oldOffer.OperatorId != authorOperator.Id)
            {
                return null;
            }

            //if edit change photos then remove old and add newOnes
            if (newOffer.Photos != null)
            {

                //remove old
                var oldPhotos = _context.OfferPhotos.Where(m=> m.OfferId== oldOfferId).ToList();
                foreach (var photoOffer in oldPhotos)
                {
                    var photo = _context.Photos.FirstOrDefault(m => m.Id == photoOffer.Id);
                    _context.OfferPhotos.Remove(photoOffer);
                    _context.Photos.Remove(photo);
                    _context.SaveChanges() ;
                    
                }

                //upload new photos
                var photosID = UploadPhotos(newOffer);

                //connect new ones to offer
                foreach (int id in photosID)
                {
                    var offerPhoto = new OfferPhoto();
                    offerPhoto.OfferId = oldOfferId;
                    offerPhoto.PhotoId = id;

                    _context.OfferPhotos.Add(offerPhoto);
                    _context.SaveChanges();
                }

            }

            //update text
            oldOffer.Title = newOffer.Title;
            oldOffer.Description = newOffer.Description;

            _context.SaveChanges();
            return oldOffer;
        }

        public List<int> UploadPhotos(CreateOfferModel offer)
        {
            var ret= new List<int>();
            var baseUrl = _config["UploadsDirectory"];
            foreach (var photo in offer.Photos) {

                string newFileName = Convert.ToString( Guid.NewGuid()) + Path.GetExtension(photo.FileName);
                string filePath = Path.Combine(baseUrl, newFileName);

                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        photo.CopyTo(stream);
                    }
                }catch(Exception ex) { 
                    Console.WriteLine(ex.ToString());
                    return null;
                }

                var databasePhoto = new Photo();
                databasePhoto.PhotoUrl = filePath;

                _context.Photos.Add(databasePhoto);
                _context.SaveChanges();
                
                ret.Add(databasePhoto.Id);
            }

            return ret;

        }
    }
}
