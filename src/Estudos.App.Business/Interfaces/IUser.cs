using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Estudos.App.Business.Interfaces
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        bool IsInRole(string[] roles);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}