﻿using BusinessLibrary.Model.User;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;

namespace BusinessLibrary.Repository
{
    public class RsaJwtTokenProvider : ITokenProvider
    {
        private RsaSecurityKey _key;
        private string _algorithm;
        private string _issuer;
        private string _audience;

        public RsaJwtTokenProvider(string issuer, string audience, string keyName)
        {
            var parameters = new CspParameters { KeyContainerName = keyName };
            var provider = new RSACryptoServiceProvider(2048, parameters);

            _key = new RsaSecurityKey(provider);

            _algorithm = SecurityAlgorithms.RsaSha256Signature;
            _issuer = issuer;
            _audience = audience;
        }

        public string CreateToken(UserViewModel user, DateTime expiry)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            ClaimsIdentity identity = new ClaimsIdentity(new GenericIdentity(user.Email, "jwt"));

            // TODO: Add whatever claims the user may have...

            SecurityToken token = tokenHandler.CreateJwtSecurityToken(new SecurityTokenDescriptor
            {
                Audience = _audience,
                Issuer = _issuer,
                SigningCredentials = new SigningCredentials(_key, _algorithm),
                Expires = expiry.ToUniversalTime(),
                Subject = identity
            });

            return tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters
            {
                IssuerSigningKey = _key,
                ValidAudience = _audience,
                ValidIssuer = _issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0) // Identity and resource servers are the same.
            };
        }
    }
}
