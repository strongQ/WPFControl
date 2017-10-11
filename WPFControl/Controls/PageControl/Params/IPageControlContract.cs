using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls.PagingControl.Params
{
    public interface IPageControlContract
    {
        Task<int> GetTotalCount();
        Task<ICollection<object>> GetRecordsBy(int startingIndex, int numberOfRecords, object sortData);
    }
}
