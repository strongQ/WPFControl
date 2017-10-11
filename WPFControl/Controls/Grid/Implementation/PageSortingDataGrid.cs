using JP.HCZZP.WPFApp.Infrastructure.Controls.Grid.Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls
{
    public class PageSortingDataGrid:DataGrid
    {
        public PageSortingDataGrid()
        {
            Sorting += PageSortingDataGrid_Sorting;
        }

        private void PageSortingDataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {
            e.Handled = true;
            if (SortCommand != null)
            {
                var column = e.Column;
                var sortDirection = GetNextSortDirection(column.SortDirection);
                column.SortDirection = sortDirection;
                SortCommand.Execute(new SortData(column.Header.ToString(), sortDirection));
            }
        }
        private ListSortDirection? GetNextSortDirection(ListSortDirection? sortDirection)
        {
            if (!sortDirection.HasValue)
            {
                return ListSortDirection.Ascending;
            }
            else
            {
                if (sortDirection == ListSortDirection.Ascending)
                {
                    return ListSortDirection.Descending;
                }
                else
                {
                    return null;
                }
            }
        }

        public static readonly DependencyProperty SortCommandProperty = DependencyProperty.Register(
           "SortCommand",
           typeof(ICommand),
           typeof(PageSortingDataGrid),
           new PropertyMetadata(default(ICommand)));

        public ICommand SortCommand
        {
            get { return (ICommand)GetValue(SortCommandProperty); }
            set { SetValue(SortCommandProperty, value); }
        }
    }
}
