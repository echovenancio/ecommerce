using Microsoft.AspNetCore.Identity;
using RO.DevTest.Application.Contracts.Infrastructure;
using RO.DevTest.Domain.Entities;
using RO.DevTest.Domain.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RO.DevTest.Infrastructure.Abstractions;


/// <summary>
/// This is a abstraction of the Identity library, creating methods that will interact with 
/// it to create and update users
/// </summary>
public class IdentityAbstractor : IIdentityAbstractor
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _configuration;

    public IdentityAbstractor(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _configuration = configuration;
    }

    public async Task<User?> FindUserByEmailAsync(string email) => await _userManager.FindByEmailAsync(email);

    public async Task<User?> FindUserByIdAsync(string userId) => await _userManager.FindByIdAsync(userId);

    public async Task<IList<string>> GetUserRolesAsync(User user) => await _userManager.GetRolesAsync(user);

    public async Task<IdentityResult> CreateUserAsync(User partnerUser, string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException($"{nameof(password)} cannot be null or empty", nameof(password));
        }

        if (string.IsNullOrEmpty(partnerUser.Email))
        {
            throw new ArgumentException($"{nameof(User.Email)} cannot be null or empty", nameof(partnerUser));
        }

        return await _userManager.CreateAsync(partnerUser, password);
    }

    public async Task<IdentityResult> UpdateUserAsync(User user)
    {
        if (user == null)
        {
            throw new ArgumentNullException(nameof(user), "User cannot be null");
        }
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentException($"{nameof(User.Email)} cannot be null or empty", nameof(user));
        }

        return await _userManager.UpdateAsync(user);
    }

    public async Task<SignInResult> PasswordSignInAsync(User user, string password)
        => await _signInManager.PasswordSignInAsync(user, password, false, false);

    public async Task<IdentityResult> DeleteUser(User user) => await _userManager.DeleteAsync(user);

    public async Task<IdentityResult> AddToRoleAsync(User user, UserRoles role)
    {
        if (await _roleManager.RoleExistsAsync(role.ToString()) is false)
        {
            await _roleManager.CreateAsync(new IdentityRole { Name = role.ToString() });
        }

        return await _userManager.AddToRoleAsync(user, role.ToString());
    }

    public async Task<String> GenerateUserTokenAsync(User user)
    {
        var authClaims = new List<Claim> {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));
        if (authSigningKey == null)
        {
            throw new Exception("Auth signing key is null");
        }
        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
