using _2013201694_ENT;
using _2013201694_ENT.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2013201694_PER.Repositories
{
    public class TipoLugarRepository : Repository<TipoLugar>, ITipoLugarRepository
    {
        public TipoLugarRepository(DbContext context) : base(context)
        {
        }

    }
}
