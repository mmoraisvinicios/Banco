using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBancos
    {
        Task<List<Bancos>> ListarBancos();
    }
}
