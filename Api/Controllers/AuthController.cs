using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Api.Entites;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers {
    [Route ("api/[controller]")]
    public class AuthController : Controller {
        public DataDbContext context;
        public AuthController (DataDbContext context) => this.context = context;
        /*
          This method for sign in user
          after sign in user get token; 
        */
        [HttpPost ("signin")]
        public IActionResult signin ([FromBody] VmLogin model) {
            if (!ModelState.IsValid) return BadRequest (ModelState.Select (f => f.Value.Errors.ToList ()));
            var findUser = context.User.FirstOrDefault (f => f.Email == model.UserName && f.Password == model.Password);
            if (findUser == null) return NotFound ("Please Check UserName And Password");

            var secretKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes ("superSecretKey@345"));
            var signinCredentials = new SigningCredentials (secretKey, SecurityAlgorithms.HmacSha256);

            var tokeOptions = new JwtSecurityToken (
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims : new List<Claim> (),
                expires : DateTime.Now.AddMinutes (5),
                signingCredentials : signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler ().WriteToken (tokeOptions);
            return Ok (new { id = findUser.Id, fullName = findUser.FullName, token = tokenString });
        }

        /*
          This method for sign uo new user
          after sign in user get token; 
        */
        [HttpPost ("signup")]
        public IActionResult signup ([FromBody] VmRegister model) {
            if (!ModelState.IsValid) return BadRequest (ModelState.Select (f => f.Value.Errors.ToList ()));
            var findUser = context.User.Any (f => f.Email == model.Email);
            if (findUser) return NotFound ("Email Already Exist");
            var newUser = new User {
                FullName = model.FullName,
                BirthDate = model.BirthDate,
                Email = model.Email,
                Password = model.Password
            };
            context.User.Add (newUser);
            context.SaveChanges ();

            return Ok ();
        }
        /*
         not use yet
        */
        public async Task<string> UploadProfilePicture ([FromForm (Name = "uploadedFile")] IFormFile file, long userId) {
            if (file == null || file.Length == 0)
                return ("Please select profile picture");

            var folderName = Path.Combine ("Resources", "ProfilePics");
            var filePath = Path.Combine (Directory.GetCurrentDirectory (), folderName);

            if (!Directory.Exists (filePath)) {
                Directory.CreateDirectory (filePath);
            }

            var uniqueFileName = $"{userId}_profilepic.png";
            var dbPath = Path.Combine (folderName, uniqueFileName);

            using (var fileStream = new FileStream (Path.Combine (filePath, uniqueFileName), FileMode.Create)) {
                await file.CopyToAsync (fileStream);
            }

            return dbPath;
        }
    }

}