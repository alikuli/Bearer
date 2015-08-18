using Bearer.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Threading.Tasks;
namespace Bearer.MyPrograms.EmailStrategy
{
    public interface IEmailStrategy
    {
        Task SendAsync(IdentityMessage message, GlobalValuesVM g);
    }
}
