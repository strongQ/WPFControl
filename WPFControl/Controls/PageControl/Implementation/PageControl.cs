using JP.HCZZP.WPFApp.Infrastructure.Controls.PagingControl.Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls
{
    [TemplatePart(Name = "PART_FirstPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_PreviousPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_PageTextBox", Type = typeof(TextBox)),
    TemplatePart(Name = "PART_NextPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_LastPageButton", Type = typeof(Button)),
    TemplatePart(Name = "PART_PageSizesCombobox", Type = typeof(ComboBox))]
    public class PageControl : Control
    {
        #region CUSTOM CONTROL VARIABLES

        protected Button btnFirstPage, btnPreviousPage, btnNextPage, btnLastPage;
        protected TextBox txtPage;
        protected ComboBox cmbPageSizes;

        #endregion

        #region PROPERTIES

        public static readonly DependencyProperty ItemsSourceProperty;
        public static readonly DependencyProperty PageProperty;
        public static readonly DependencyProperty CurrentPageProperty;
        public static readonly DependencyProperty TotalRecordsProperty;
        public static readonly DependencyProperty TotalPagesProperty;
        public static readonly DependencyProperty PageSizesProperty;
        public static readonly DependencyProperty PageContractProperty;
        public static readonly DependencyProperty FilterProperty;

        public ObservableCollection<object> ItemsSource
        {
            get
            {
                return GetValue(ItemsSourceProperty) as ObservableCollection<object>;
            }
            protected set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public int Page
        {
            get
            {
                return (int)GetValue(PageProperty);
            }
            set
            {
                SetValue(PageProperty, value);
            }
        }

        

        public int CurrentPage
        {
            get
            {
                return (int)GetValue(CurrentPageProperty);
            }
            protected set
            {
                SetValue(TotalPagesProperty, value);
            }
        }
        public int TotalPages
        {
            get
            {
                return (int)GetValue(TotalPagesProperty);
            }
            protected set
            {
                SetValue(TotalPagesProperty, value);
            }
        }

        public int TotalRecords
        {
            get
            {
                return (int)GetValue(TotalRecordsProperty);
            }
            protected set
            {
                SetValue(TotalRecordsProperty, value);
            }
        }


        public ObservableCollection<int> PageSizes
        {
            get
            {
                return GetValue(PageSizesProperty) as ObservableCollection<int>;
            }
            set
            {
                SetValue(PageSizesProperty, value);
            }
        }

        public IPageControlContract PageContract
        {
            get
            {
                return GetValue(PageContractProperty) as IPageControlContract;
            }
            set
            {
                SetValue(PageContractProperty, value);
            }
        }

        public object Filter
        {
            get
            {
                return GetValue(FilterProperty);
            }
            set
            {
                SetValue(FilterProperty, value);
            }
        }

        #endregion

        #region EVENTS

        public delegate void PageChangedEventHandler(object sender, PageChangedEventArgs args);

        public static readonly RoutedEvent PreviewPageChangeEvent;
        public static readonly RoutedEvent PageChangedEvent;

        public event PageChangedEventHandler PreviewPageChange
        {
            add
            {
                AddHandler(PreviewPageChangeEvent, value);
            }
            remove
            {
                RemoveHandler(PreviewPageChangeEvent, value);
            }
        }

        public event PageChangedEventHandler PageChanged
        {
            add
            {
                AddHandler(PageChangedEvent, value);
            }
            remove
            {
                RemoveHandler(PageChangedEvent, value);
            }
        }

        #endregion

        #region CONTROL CONSTRUCTORS

        static PageControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PageControl), new FrameworkPropertyMetadata(typeof(PageControl)));

            ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(ObservableCollection<object>), typeof(PageControl), new PropertyMetadata(new ObservableCollection<object>()));

            CurrentPageProperty= DependencyProperty.Register("CurrentPage", typeof(int), typeof(PageControl));

            PageProperty = DependencyProperty.Register("Page", typeof(int), typeof(PageControl));

            TotalRecordsProperty = DependencyProperty.Register("TotalRecords", typeof(int), typeof(PageControl));

            TotalPagesProperty = DependencyProperty.Register("TotalPages", typeof(int), typeof(PageControl));

            PageSizesProperty = DependencyProperty.Register("PageSizes", typeof(ObservableCollection<int>), typeof(PageControl), new PropertyMetadata(new ObservableCollection<int>()));

            PageContractProperty = DependencyProperty.Register("PageContract", typeof(IPageControlContract), typeof(PageControl));

            FilterProperty = DependencyProperty.Register("Filter", typeof(object), typeof(PageControl), new FrameworkPropertyMetadata(Target));

            PreviewPageChangeEvent = EventManager.RegisterRoutedEvent("PreviewPageChange", RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(PageControl));

            PageChangedEvent = EventManager.RegisterRoutedEvent("PageChanged", RoutingStrategy.Bubble, typeof(PageChangedEventHandler), typeof(PageControl));
        }

        private static async void Target(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var target = (PageControl)dependencyObject;
           await target.Navigate(PageChanges.Current);
        }

        public PageControl()
        {
            this.Loaded += new RoutedEventHandler(PaggingControl_Loaded);
        }
        

        ~PageControl()
        {
            UnregisterEvents();
        }

        #endregion

        #region EVENTS

        void PaggingControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Template == null)
            {
                throw new Exception("Control template not assigned.");
            }

            if (PageContract == null)
            {
                throw new Exception("IPageControlContract not assigned.");
            }

            RegisterEvents();
            SetDefaultValues();
            BindProperties();
        }

        async void  btnFirstPage_Click(object sender, RoutedEventArgs e)
        {

            await Navigate(PageChanges.First);
        }

        async void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
           await Navigate(PageChanges.Previous);
        }

        async void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            await Navigate(PageChanges.Next);
        }

        async void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            btnFirstPage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            btnLastPage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009bff"));
            await Navigate(PageChanges.Last);
        }

        async void txtPage_LostFocus(object sender, RoutedEventArgs e)
        {
            await  Navigate(PageChanges.Current);
           
            
        }

        async void cmbPageSizes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           await Navigate(PageChanges.Current);
        }

        #endregion

        #region INTERNAL METHODS

        public override void OnApplyTemplate()
        {
            btnFirstPage = this.Template.FindName("PART_FirstPageButton", this) as Button;
            btnPreviousPage = this.Template.FindName("PART_PreviousPageButton", this) as Button;
            txtPage = this.Template.FindName("PART_PageTextBox", this) as TextBox;
            btnNextPage = this.Template.FindName("PART_NextPageButton", this) as Button;
            btnLastPage = this.Template.FindName("PART_LastPageButton", this) as Button;
            cmbPageSizes = this.Template.FindName("PART_PageSizesCombobox", this) as ComboBox;

            if (btnFirstPage == null ||
                btnPreviousPage == null ||
                txtPage == null ||
                btnNextPage == null ||
                btnLastPage == null ||
                cmbPageSizes == null)
            {
                throw new Exception("Invalid Control template.");
            }

            base.OnApplyTemplate();
        }

        private void RegisterEvents()
        {
            btnFirstPage.Click += new RoutedEventHandler(btnFirstPage_Click);
            btnPreviousPage.Click += new RoutedEventHandler(btnPreviousPage_Click);
            btnNextPage.Click += new RoutedEventHandler(btnNextPage_Click);
            btnLastPage.Click += new RoutedEventHandler(btnLastPage_Click);
            txtPage.TextChanged += TxtPage_TextChanged;
            txtPage.LostFocus += new RoutedEventHandler(txtPage_LostFocus);
            txtPage.KeyDown += TxtPage_KeyDown;
            cmbPageSizes.SelectionChanged += new SelectionChangedEventHandler(cmbPageSizes_SelectionChanged);
        }

        private void TxtPage_TextChanged(object sender, TextChangedEventArgs e)
        {

            btnLastPage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
            btnFirstPage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));

            if (txtPage.Text == TotalPages.ToString()) btnLastPage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009bff"));
            if (txtPage.Text == "1") btnFirstPage.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#009bff"));
        }

        private async void TxtPage_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key==System.Windows.Input.Key.Enter)  txtPage_LostFocus(null,null);
        }

        private void UnregisterEvents()
        {
            btnFirstPage.Click -= btnFirstPage_Click;
            btnPreviousPage.Click -= btnPreviousPage_Click;
            btnNextPage.Click -= btnNextPage_Click;
            btnLastPage.Click -= btnLastPage_Click;
            txtPage.TextChanged -= TxtPage_TextChanged;
            txtPage.LostFocus -= txtPage_LostFocus;
            txtPage.KeyDown -= TxtPage_KeyDown;

            
            cmbPageSizes.SelectionChanged -= cmbPageSizes_SelectionChanged;
        }

        private void SetDefaultValues()
        {
            ItemsSource = new ObservableCollection<object>();

            cmbPageSizes.IsEditable = false;
            cmbPageSizes.SelectedIndex = 0;
        }

        private void BindProperties()
        {
          
            Binding propBinding;

            propBinding = new Binding("Page");
            propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            propBinding.Mode = BindingMode.TwoWay;
            propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            txtPage.SetBinding(TextBox.TextProperty, propBinding);


            cmbPageSizes.ItemsSource = PageSizes;
            //propBinding = new Binding("PageSizes");
            //propBinding.RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent);
            //propBinding.Mode = BindingMode.TwoWay;
            //propBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            //cmbPageSizes.SetBinding(ComboBox.ItemsSourceProperty, propBinding);
        }

        private void RaisePageChanged(int OldPage, int NewPage)
        {
            PageChangedEventArgs args = new PageChangedEventArgs(PageChangedEvent, OldPage, NewPage, TotalPages);
            RaiseEvent(args);
        }

        private void RaisePreviewPageChange(int OldPage, int NewPage)
        {
            PageChangedEventArgs args = new PageChangedEventArgs(PreviewPageChangeEvent, OldPage, NewPage, TotalPages);
            RaiseEvent(args);
        }

        private async Task Navigate(PageChanges change)
        {
           
            int totalRecords;
            int newPageSize;

            if (PageContract == null)
            {
                return;
            }


            TotalRecords=totalRecords = await PageContract.GetTotalCount();
            newPageSize = (int)cmbPageSizes.SelectedItem;

            if (totalRecords == 0)
            {
                ItemsSource.Clear();
                TotalPages = 1;
                Page = 1;
            }
            else
            {
                TotalPages = (totalRecords + newPageSize - 1) / newPageSize;
               // TotalPages = (totalRecords / newPageSize) + (int)((totalRecords % newPageSize == 0) ? 0 : 1);
            }

            int newPage = 1;

            switch (change)
            {
                case PageChanges.First:
                    if (Page == 1)
                    {
                        return;
                    }
                    break;
                case PageChanges.Previous:
                    newPage = (Page - 1 > TotalPages) ? TotalPages : (Page - 1 < 1) ? 1 : Page - 1;
                    break;
                case PageChanges.Current:
                    newPage = (Page > TotalPages) ? TotalPages : (Page < 1) ? 1 : Page;
                    break;
                case PageChanges.Next:
                    newPage = (Page + 1 > TotalPages) ? TotalPages : Page + 1;
                    //(Page + 1) < 1 ? 1 :
                    break;
                case PageChanges.Last:
                    if (Page == TotalPages)
                    {
                        return;
                    }
                    newPage = TotalPages;
                    break;
                default:
                    break;
            }

            var startingIndex = (newPage - 1) * newPageSize;

            var oldPage = Page;
            RaisePreviewPageChange(Page, newPage);

            Page = newPage;

            ItemsSource.Clear();
            //ICollection<object> fetchData =await PageContract.GetRecordsBy(startingIndex, newPageSize, Filter);
            ICollection<object> fetchData = await PageContract.GetRecordsBy(Page, newPageSize, Filter);
            foreach (object row in fetchData)
            {
                ItemsSource.Add(row);
            }

            RaisePageChanged(oldPage, Page);
        }

        #endregion
    }
}
