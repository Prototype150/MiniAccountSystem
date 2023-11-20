using Game.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
    public class PasswordContainer : IPasswordContainer
    {
        public SecureString Password { get; set; }

        public void Dispose()
        {
            Password?.Dispose();
        }
    }
}
