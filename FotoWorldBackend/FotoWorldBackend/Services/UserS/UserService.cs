using FotoWorldBackend.Models;
using FotoWorldBackend.Models.UserModels;
using FotoWorldBackend.Services.Email;

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

        public Offer GetOfferDetailed(int offerId)
        {
            throw new NotImplementedException();
        }

        public List<Offer> GetOfferList()
        {
            throw new NotImplementedException();
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
