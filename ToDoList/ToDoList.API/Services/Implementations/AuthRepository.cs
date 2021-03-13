using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.Entities;
using ToDoList.API.Helpers.Data;
using ToDoList.API.Services.Interfaces;
using ToDoList.API.Models;

namespace ToDoList.API.Services.Implementations
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailService _emailService;

        public AuthRepository(ApplicationDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        public async Task<AppUser> Register(AppUser appUser, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);
            appUser.PasswordHash = passwordHash;
            appUser.PasswordSalt = passwordSalt;
            await _context.Users.AddAsync(appUser);
            await _context.SaveChangesAsync();
            return appUser;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<AppUser> Login(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        private string randomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        public void ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // always return ok response to prevent email enumeration
            if (account == null) return;

            // create reset token that expires after 10 minutes
            account.ResetToken = randomTokenString();
            account.ResetTokenExpires = DateTime.UtcNow.AddMinutes(10);
            _context.Users.Update(account);
            _context.SaveChanges();

            // send email
            SendPasswordResetEmail(account, origin);
        }

        private void SendPasswordResetEmail(AppUser account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var resetUrl = $"{origin}/api/Auth/reset-password?token={account.ResetToken}";
                message =
                    $@"<p>Please click the below link to reset your password, the link will be valid for 10 minutes:</p>
                             <p><a href=""{resetUrl}"">{resetUrl}</a></p>";
            }
            else
            {
                message =
                    $@"<p>Please use the below token to reset your password with the <code>/api/Auth/reset-password</code> api route:</p>
                             <p><code>{account.ResetToken}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Sign-up Verification API - Reset Password",
                html: $@"<h4>Reset Password Email</h4>
                         {message}"
            );
        }


        public void ResetPassword(ResetPasswordRequest model)
        {
            var account = _context.Users.SingleOrDefault(x =>
                x.ResetToken == model.Token &&
                x.ResetTokenExpires > DateTime.UtcNow);

            if (account == null)
            {
                return;
            }

            // update password and remove reset token
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = passwordSalt;
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            _context.Users.Update(account);
            _context.SaveChanges();
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public async Task<bool> UserExists(string email)
        {
            if (await _context.Users.AnyAsync(x => x.Email == email))
            {
                return true;
            }

            return false;
        }
    }
}