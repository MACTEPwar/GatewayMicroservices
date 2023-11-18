using System.Security.Claims;
using System;
using AuthMicroservice.Models.DAL;
using AuthMicroservice.Repositories;
using AuthMicroservice.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace AuthMicroservice.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ClaimsIdentity? GetIdentity(string username, string password)
        {
            User? user = _userRepository.GetList().Include(i => i.Scopes).FirstOrDefault(w => w.Login == username && w.Password == password);
            if (user != null)
            {
                //var claims = new List<Claim>
                //{
                //    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
                //};
                var claims = user.Scopes.Select(s => new Claim("scope", s.Name));
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
