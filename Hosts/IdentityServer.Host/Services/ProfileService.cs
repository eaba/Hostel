using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Host.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<HostelUser> _userManager;

        public ProfileService(UserManager<HostelUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

                var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;

                var user = await _userManager.FindByIdAsync(subjectId);
                if (user == null)
                    throw new ArgumentException("Invalid subject identifier");

                var claims = await GetClaimsFromUser(user);
                var cs = claims.ToList();
                var userClaims = await _userManager.GetClaimsAsync(user);
                cs.AddRange(userClaims);
                context.IssuedClaims = cs.ToList();
            }
            catch (Exception ex)
            {
               // Log.LogError(ex.ToString());
            }
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            try
            {
                var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));

                var subjectId = subject.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
                var user = await _userManager.FindByIdAsync(subjectId);

                context.IsActive = false;

                if (user != null)
                {
                    if (_userManager.SupportsUserSecurityStamp)
                    {
                        var security_stamp = subject.Claims.Where(c => c.Type == "security_stamp").Select(c => c.Value).SingleOrDefault();
                        if (security_stamp != null)
                        {
                            var db_security_stamp = await _userManager.GetSecurityStampAsync(user);
                            if (db_security_stamp != security_stamp)
                                return;
                        }
                    }

                    context.IsActive = user != null && (!user.LockoutEnabled || !user.LockoutEnd.HasValue || user.LockoutEnd <= DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                //Log.LogError(ex.ToString());
            }
        }
        private async Task<IEnumerable<Claim>> GetClaimsFromUser(HostelUser user)
        {
            var claims = new List<Claim>
            {
                new Claim("Hostel", user.Hostel, ClaimValueTypes.String),
                new Claim(JwtClaimTypes.Subject, user.Id, ClaimValueTypes.String),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
            };

            if (_userManager.SupportsUserEmail)
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.Email, user.Email, ClaimValueTypes.Email),
                    new Claim(JwtClaimTypes.EmailVerified, user.EmailConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }

            if (_userManager.SupportsUserPhoneNumber && !string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                claims.AddRange(new[]
                {
                    new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber, ClaimValueTypes.String),
                    new Claim(JwtClaimTypes.PhoneNumberVerified, user.PhoneNumberConfirmed ? "true" : "false", ClaimValueTypes.Boolean)
                });
            }
            return claims;
        }
    }
}
