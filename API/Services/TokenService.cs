using DatingApp.Entities;
using DatingApp.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DatingApp.Services
{
    public class TokenService : ITokenService
    {

        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)
        {

            // change the key from string to array of bytes
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {

                        var claims = new List<Claim> {
                            new Claim(JwtRegisteredClaimNames.NameId,user.UserName) 
                        };

                        var cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
                        var tokenDescriptor = new SecurityTokenDescriptor { 
                            Subject=new ClaimsIdentity(claims),
                            Expires= DateTime.Now.AddDays(1),
                            SigningCredentials=cred

                        };

                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);

                        return tokenHandler.WriteToken(token);  
            
        }


    }




    }

    

