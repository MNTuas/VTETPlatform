using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VTET.Data.Base;
using VTET.Data.Models;

namespace VTET.Data.Repository
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>
    {
        public OrderDetailRepository()
        {

        }
        public OrderDetailRepository(Net1704_221_8_VTETPlatformContext context) => _context = context;
    }
}
