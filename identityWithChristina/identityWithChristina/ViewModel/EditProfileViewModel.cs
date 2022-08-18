namespace identityWithChristina.ViewModel
{
    public class EditProfileViewModel
    {
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string postalCode { get; set; }
        public string gender { get; set; }
        public string Lname { get; set; }
        public IFormFile profilePicture { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public int?  points { get; set; }
        public int?  ssn { get; set; }
  

    }
}
