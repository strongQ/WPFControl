
using System.ComponentModel;


namespace JP.HCZZP.WPFApp.Infrastructure.Controls.Grid.Params
{
    public class SortData
    {
        public SortData(string columnName, ListSortDirection? listSortDirection)
        {
            ColumnName = columnName;
            ListSortDirection = listSortDirection;
        }

        public string ColumnName { get; set; }
        public ListSortDirection? ListSortDirection { get; set; }
    }
}
