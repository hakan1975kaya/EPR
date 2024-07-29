﻿using Microsoft.IdentityModel.JsonWebTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class ClaimExtension
    {
        public static void AddEmail(this ICollection<Claim> claims ,string email)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email)); 
        }
        public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdenfier)
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdenfier));
        }
        public static void AddRoles(this ICollection<Claim> claims, string[] roles)
        {
            roles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
        }
        public static void AddRegistrationNumber(this ICollection<Claim> claims, int registrationNumber)
        {
            claims.Add(new Claim(ClaimTypes.SerialNumber, registrationNumber.ToString()));
        }
    }
}
