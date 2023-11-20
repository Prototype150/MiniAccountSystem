using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models.Interfaces
{
    public interface IPasswordContainer : IDisposable
    {
        public SecureString Password { get; set; }
    }
}
