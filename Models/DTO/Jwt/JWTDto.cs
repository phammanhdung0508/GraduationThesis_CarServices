namespace GraduationThesis_CarServices.Models.DTO.Jwt
{
    public class JWTDto
    {
        public JWTDto(int accountId, string email, bool email_verified, string name, string picture, string jwt, string role)
        {
            this.accountId = accountId;
            this.email = email;
            this.email_verified = email_verified;
            this.name = name;
            this.picture = picture;
            this.jwt = jwt;
            this.role = role;
        }

        public int accountId { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string jwt { get; set; }
        public string role { get; set; }
    }

}