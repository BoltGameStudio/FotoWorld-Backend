using FotoWorldBackend.Models;
using FotoWorldBackend.Models.OperatorModels;
using FotoWorldBackend.Services.Email;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FotoWorldBackend.Services.Operator
{
    public class OperatorService : IOperatorService
    {
        private readonly FotoWorldContext _context;
        private readonly IConfiguration _config;
        public OperatorService(FotoWorldContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public Offer CreateOffer(CreateOfferModel offer)
        {
            var newOffer = new Offer();
            return newOffer;
            
        }

        public Offer UpdateOffer(CreateOfferModel offer)
        {
            throw new NotImplementedException();
        }

        public List<int> UploadPhotos(CreateOfferModel offer)
        {
            var ret= new List<int>();
            var baseUrl = _config.GetSection("UploadsDirectory").Value;
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
                }



                var databasePhoto = new Photo();

                databasePhoto.PhotoUrl = filePath;
                
                ret.Add(databasePhoto.Id);
            }

            return ret;

        }
    }
}
