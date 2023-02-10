namespace FotoWorldBackend.Models
{
    public class RegisterOperatorModel : RegisterUserModel
    {
        public bool IsCompany { get; set; }
        public string Availability { get; set; }
        public string LocationCity { get; set;}

        public string OperatingRadius { get; set;}

        public bool PhotoService { get; set; }
        public bool DronePhotoService { get; set; }
        public bool DroneFilmService { get; set; }
        public bool FilmingService { get; set; }
    }
}
