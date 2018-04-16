using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFControl.Controls.PagingControl.Params
{
    public interface IPageControlContract
    {
        Task<int> GetTotalCount();
        Task<ICollection<object>> GetRecordsBy(int startingIndex, int numberOfRecords, object sortData);
        Task<Tuple<ICollection<object>, int>> GetRecords(int startingIndex, int numberOfRecords, object sortData);
    }
}
