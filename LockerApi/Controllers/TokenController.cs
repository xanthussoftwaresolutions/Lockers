﻿using System;
using Microsoft.AspNetCore.Mvc;
using BusinessLibrary.Model.User;
using BusinessLibrary.Model.Token;
using BusinessLibrary.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LockerApi.Controllers
{
   
   
    [Route("api/locker/login")]
    public class TokenController : Controller
    {
        private ITokenProvider _tokenProvider;
        private IUserRepository userRepository;

        public TokenController(ITokenProvider tokenProvider, IUserRepository UserRepository) // We'll create this later, don't worry.
        {
            _tokenProvider = tokenProvider;
            userRepository = UserRepository;
        }
        [HttpGet]
        public JsonWebToken Get([FromQuery] string grant_type, [FromQuery] int userId, [FromQuery] int pin, [FromQuery] string refresh_token)
        {
            // Authenticate depending on the grant type.
            UserViewModel user = grant_type == "refresh_token" ? GetUserByToken(refresh_token) : GetUserByCredentials(userId, pin);

            if (user == null)
                throw new UnauthorizedAccessException("No!");

            int ageInMinutes = 20;  // However long you want...

            DateTime expiry = DateTime.UtcNow.AddMinutes(ageInMinutes);

            var token = new JsonWebToken
            {
                access_token = _tokenProvider.CreateToken(user, expiry),
                expires_in = ageInMinutes * 60
            };

            if (grant_type != "refresh_token")
                token.refresh_token = GenerateRefreshToken(user);

            return token;
        }

        [HttpPost]
        public JsonWebToken Get([FromBody] LoginViewModel loginViewModel)
        {
            // Authenticate depending on the grant type.
            UserViewModel user = loginViewModel.grant_type == "refresh_token" ? GetUserByToken(loginViewModel.refresh_token) : GetUserByCredentials(Convert.ToInt32(loginViewModel.username), Convert.ToInt32(loginViewModel.password));

            if (user == null)
                throw new UnauthorizedAccessException("No!");

            int ageInMinutes = 20;  // However long you want...

            DateTime expiry = DateTime.UtcNow.AddMinutes(ageInMinutes);

            var token = new JsonWebToken
            {
                access_token = _tokenProvider.CreateToken(user, expiry),
                expires_in = ageInMinutes * 60
            };

            if (loginViewModel.grant_type != "refresh_token")
                token.refresh_token = GenerateRefreshToken(user);

            return token;
        }

        private UserViewModel GetUserByToken(string refreshToken)
        {
            // TODO: Check token against your database.
            if (refreshToken == "test")
                return new UserViewModel { FirstName = "test" };

            return null;
        }

        private UserViewModel GetUserByCredentials(int userId, int pin)
        {
            // TODO: Check username/password against your database.
            UserViewModel isLoginUser = userRepository.AuthenticateUser(userId, pin);
            if (isLoginUser.UserId >0)
                return new UserViewModel { Email = isLoginUser.Email };

            return null;
        }

        private string GenerateRefreshToken(UserViewModel user)
        {
            // TODO: Create and persist a refresh token.
            return "test";
        }
    }
}
