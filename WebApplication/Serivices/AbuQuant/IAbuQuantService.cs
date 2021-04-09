using Stock.EntityFrameWork.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication.Serivices.AbuQuant
{
    public interface IAbuQuantService
    {
        public Task<IList<AbuQuantModel>> GetFinalScoreRank();
    }
}
