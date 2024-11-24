using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppAlquiler_BusinessLayer.Interfaces
{
    public interface ICurrentUserService
    {
        int? UserId { get; }
        string UserName { get; }
    }
}
