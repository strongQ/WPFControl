using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace WPFControl.AttachProperty
{
    public class EventBroker : DependencyObject
    {
        //        KeyUp(FrameworkElement)
        //KeyDown(FrameworkElement)
        //MouseEnter(FrameworkElement)
        //MouseLeave(FrameworkElement)
        //MouseLeftButtonUp(FrameworkElement)
        //MouseLeftButtonDown(FrameworkElement)
        //MouseRightButtonUp(FrameworkElement)
        //MouseRightButtonDown(FrameworkElement)
        //MouseMove(FrameworkElement)
        //MouseWheel(FrameworkElement)
        //SelectionChanged(Selector & TextBox)
        //Click(ButtonBase)
        //CheckedChanged(ToggleButton)
        //ValueChanged(RangeBase)


        public delegate void EventHandlerDelegate(object sender, EventArgs e, EventType eventType, string eventKey);
        public static event EventHandlerDelegate EventHandler;

        public enum EventType
        {
            KeyUp,
            KeyDown,
            MouseEnter,
            MouseLeave,
            MouseLeftButtonUp,
            MouseLeftButtonDown,
            MouseRightButtonUp,
            MouseRightButtonDown,
            MouseMove,
            MouseWheel,
            SelectionChanged,
            Click,
            Checked,
            ValueChanged,
        }

        private class EventContract
        {
            public EventType EventType { get; set; }
            public string EventKey { get; set; }
            public EventHandlerDelegate EventHandler { get; set; }
        }


        #region Private Properties
        private static List<EventContract> _Contracts = new List<EventContract>();
        #endregion Private Properties


        #region Private Methods
        private static void Fire_EventHandler(object sender, EventArgs e, EventType eventType, string eventKey)
        {
            // Step 1: Broadcast to all those connected to our EventHandler
            if (EventHandler != null)
            {
                EventHandler(sender, e, eventType, eventKey);
            }

            // Step 2: Send to matching registered handlers
            List<EventContract> contracts = _Contracts.Where(c => (c.EventType == eventType) &&
                                                                  (c.EventKey == eventKey)).ToList();
            foreach (EventContract contract in contracts)
            {
                if (contract.EventHandler != null)
                {
                    contract.EventHandler(sender, e, eventType, eventKey);
                }
            }
        }

        private static void ClearAttachedProperties(FrameworkElement element)
        {
            if (element != null)
            {
                // clear all the attached properties
                element.SetValue(KeyDownProperty, null);
                element.SetValue(KeyUpProperty, null);
                element.SetValue(MouseEnterProperty, null);
                element.SetValue(MouseLeaveProperty, null);
                element.SetValue(MouseLeftButtonUpProperty, null);
                element.SetValue(MouseLeftButtonDownProperty, null);
                element.SetValue(MouseRightButtonUpProperty, null);
                element.SetValue(MouseRightButtonDownProperty, null);
                element.SetValue(MouseMoveProperty, null);
                element.SetValue(SelectionChangedProperty, null);
                element.SetValue(ClickProperty, null);
                element.SetValue(CheckedProperty, null);
                element.SetValue(ValueChangedProperty, null);
            }
        }
        #endregion Private Methods


        #region Public Methods
        /// <summary>
        /// Registers the specified event type.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="eventKey">The event key.</param>
        /// <param name="eventHandler">The event handler.</param>
        public static void Register(EventType eventType, string eventKey, EventHandlerDelegate eventHandler)
        {
            EventContract contract = new EventContract()
            {
                EventType = eventType,
                EventKey = eventKey,
                EventHandler = eventHandler,
            };

            _Contracts.Add(contract);
        }

        /// <summary>
        /// Unregisters the specified event type.
        /// </summary>
        /// <param name="eventType">Type of the event.</param>
        /// <param name="eventKey">The event key.</param>
        /// <param name="eventHandler">The event handler.</param>
        public static void Unregister(EventType eventType, string eventKey, EventHandlerDelegate eventHandler)
        {
            List<EventContract> removethese = _Contracts.Where(c => (c.EventKey == eventKey) &&
                                                                    (c.EventType == eventType) &&
                                                                    (c.EventHandler == eventHandler)).ToList();
            foreach (EventContract contract in removethese)
            {
                _Contracts.Remove(contract);
            }
        }
        #endregion Public Methods


        #region UnLoaded event handler
        private static void Proxy_UnLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            ClearAttachedProperties(element);
        }
        #endregion UnLoaded event handler


        #region The Attached Events

        #region KeyUp
        public static readonly DependencyProperty KeyUpProperty =
            DependencyProperty.RegisterAttached("KeyUp",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnKeyUpChanged)
        );

        public static string GetKeyUp(DependencyObject obj)
        {
            return (string)obj.GetValue(KeyUpProperty);
        }

        public static void SetKeyUp(DependencyObject obj, string value)
        {
            obj.SetValue(KeyUpProperty, value);
        }

        private static void OnKeyUpChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.KeyUp += new KeyEventHandler(Proxy_KeyUp);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.KeyUp -= new KeyEventHandler(Proxy_KeyUp);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_KeyUp(object sender, KeyEventArgs e)
        {
            string eventKey = GetKeyUp(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.KeyUp, eventKey);
        }
        #endregion KeyUp


        #region KeyDown
        public static readonly DependencyProperty KeyDownProperty =
            DependencyProperty.RegisterAttached("KeyDown",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnKeyDownChanged)
        );

        public static string GetKeyDown(DependencyObject obj)
        {
            return (string)obj.GetValue(KeyDownProperty);
        }

        public static void SetKeyDown(DependencyObject obj, string value)
        {
            obj.SetValue(KeyDownProperty, value);
        }

        private static void OnKeyDownChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.KeyDown += new KeyEventHandler(Proxy_KeyDown);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.KeyDown -= new KeyEventHandler(Proxy_KeyDown);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_KeyDown(object sender, KeyEventArgs e)
        {
            string eventKey = GetKeyDown(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.KeyDown, eventKey);
        }
        #endregion KeyDown


        #region MouseLeftButtonUp
        public static readonly DependencyProperty MouseLeftButtonUpProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonUp",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseLeftButtonUpChanged)
        );

        public static string GetMouseLeftButtonUp(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseLeftButtonUpProperty);
        }

        public static void SetMouseLeftButtonUp(DependencyObject obj, string value)
        {
            obj.SetValue(MouseLeftButtonUpProperty, value);
        }

        private static void OnMouseLeftButtonUpChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseLeftButtonUp += new MouseButtonEventHandler(Proxy_MouseLeftButtonUp);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseLeftButtonUp -= new MouseButtonEventHandler(Proxy_MouseLeftButtonUp);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string eventKey = GetMouseLeftButtonUp(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseLeftButtonUp, eventKey);
        }
        #endregion MouseLeftButtonUp


        #region MouseLeftButtonDown
        public static readonly DependencyProperty MouseLeftButtonDownProperty =
            DependencyProperty.RegisterAttached("MouseLeftButtonDown",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseLeftButtonDownChanged)
        );

        public static string GetMouseLeftButtonDown(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseLeftButtonDownProperty);
        }

        public static void SetMouseLeftButtonDown(DependencyObject obj, string value)
        {
            obj.SetValue(MouseLeftButtonDownProperty, value);
        }

        private static void OnMouseLeftButtonDownChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseLeftButtonDown += new MouseButtonEventHandler(Proxy_MouseLeftButtonDown);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseLeftButtonDown -= new MouseButtonEventHandler(Proxy_MouseLeftButtonDown);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            string eventKey = GetMouseLeftButtonDown(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseLeftButtonDown, eventKey);
        }
        #endregion MouseLeftButtonDown


        #region MouseRightButtonUp
        public static readonly DependencyProperty MouseRightButtonUpProperty =
            DependencyProperty.RegisterAttached("MouseRightButtonUp",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseRightButtonUpChanged)
        );

        public static string GetMouseRightButtonUp(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseRightButtonUpProperty);
        }

        public static void SetMouseRightButtonUp(DependencyObject obj, string value)
        {
            obj.SetValue(MouseRightButtonUpProperty, value);
        }

        private static void OnMouseRightButtonUpChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseRightButtonUp += new MouseButtonEventHandler(Proxy_MouseRightButtonUp);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseRightButtonUp -= new MouseButtonEventHandler(Proxy_MouseRightButtonUp);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            string eventKey = GetMouseRightButtonUp(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseRightButtonUp, eventKey);
        }
        #endregion MouseRightButtonUp


        #region MouseRightButtonDown
        public static readonly DependencyProperty MouseRightButtonDownProperty =
            DependencyProperty.RegisterAttached("MouseRightButtonDown",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseRightButtonDownChanged)
        );

        public static string GetMouseRightButtonDown(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseRightButtonDownProperty);
        }

        public static void SetMouseRightButtonDown(DependencyObject obj, string value)
        {
            obj.SetValue(MouseRightButtonDownProperty, value);
        }

        private static void OnMouseRightButtonDownChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseRightButtonDown += new MouseButtonEventHandler(Proxy_MouseRightButtonDown);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseRightButtonDown -= new MouseButtonEventHandler(Proxy_MouseRightButtonDown);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            string eventKey = GetMouseRightButtonDown(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseRightButtonDown, eventKey);
        }
        #endregion MouseRightButtonDown


        #region MouseEnter
        public static readonly DependencyProperty MouseEnterProperty =
            DependencyProperty.RegisterAttached("MouseEnter",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseEnterChanged)
        );

        public static string GetMouseEnter(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseEnterProperty);
        }

        public static void SetMouseEnter(DependencyObject obj, string value)
        {
            obj.SetValue(MouseEnterProperty, value);
        }

        private static void OnMouseEnterChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseEnter += new MouseEventHandler(Proxy_MouseEnter);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseEnter -= new MouseEventHandler(Proxy_MouseEnter);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseEnter(object sender, MouseEventArgs e)
        {
            string eventKey = GetMouseEnter(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseEnter, eventKey);
        }
        #endregion MouseEnter


        #region MouseLeave
        public static readonly DependencyProperty MouseLeaveProperty =
            DependencyProperty.RegisterAttached("MouseLeave",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseLeaveChanged)
        );

        public static string GetMouseLeave(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseLeaveProperty);
        }

        public static void SetMouseLeave(DependencyObject obj, string value)
        {
            obj.SetValue(MouseLeaveProperty, value);
        }

        private static void OnMouseLeaveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseLeave += new MouseEventHandler(Proxy_MouseLeave);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseLeave -= new MouseEventHandler(Proxy_MouseLeave);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseLeave(object sender, MouseEventArgs e)
        {
            string eventKey = GetMouseLeave(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseLeave, eventKey);
        }
        #endregion MouseLeave


        #region MouseMove
        public static readonly DependencyProperty MouseMoveProperty =
            DependencyProperty.RegisterAttached("MouseMove",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseMoveChanged)
        );

        public static string GetMouseMove(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseMoveProperty);
        }

        public static void SetMouseMove(DependencyObject obj, string value)
        {
            obj.SetValue(MouseMoveProperty, value);
        }

        private static void OnMouseMoveChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseMove += new MouseEventHandler(Proxy_MouseMove);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseMove -= new MouseEventHandler(Proxy_MouseMove);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseMove(object sender, MouseEventArgs e)
        {
            string eventKey = GetMouseMove(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseMove, eventKey);
        }
        #endregion MouseMove


        #region MouseWheel
        public static readonly DependencyProperty MouseWheelProperty =
            DependencyProperty.RegisterAttached("MouseWheel",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnMouseWheelChanged)
        );

        public static string GetMouseWheel(DependencyObject obj)
        {
            return (string)obj.GetValue(MouseWheelProperty);
        }

        public static void SetMouseWheel(DependencyObject obj, string value)
        {
            obj.SetValue(MouseWheelProperty, value);
        }

        private static void OnMouseWheelChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement target = obj as FrameworkElement;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.MouseWheel += new MouseWheelEventHandler(Proxy_MouseWheel);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.MouseWheel -= new MouseWheelEventHandler(Proxy_MouseWheel);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_MouseWheel(object sender, MouseEventArgs e)
        {
            string eventKey = GetMouseWheel(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.MouseWheel, eventKey);
        }
        #endregion MouseWheel


        #region SelectionChanged
        public static readonly DependencyProperty SelectionChangedProperty =
            DependencyProperty.RegisterAttached("SelectionChanged",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnSelectionChangedChanged)
        );

        public static string GetSelectionChanged(DependencyObject obj)
        {
            return (string)obj.GetValue(SelectionChangedProperty);
        }

        public static void SetSelectionChanged(DependencyObject obj, string value)
        {
            obj.SetValue(SelectionChangedProperty, value);
        }

        private static void OnSelectionChangedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            Selector target = obj as Selector;
            if (target != null)
            {
                string newValue = e.NewValue as string;
                string oldValue = e.OldValue as string;

                if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
                {
                    target.SelectionChanged += new SelectionChangedEventHandler(Proxy_SelectionChanged);
                    target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
                }
                else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
                {
                    target.SelectionChanged -= new SelectionChangedEventHandler(Proxy_SelectionChanged);
                    target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
                }

                return;
            }

            TextBox textbox = obj as TextBox;
            if (textbox != null)
            {
                string newValue = e.NewValue as string;
                string oldValue = e.OldValue as string;

                if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
                {
                    textbox.SelectionChanged += new RoutedEventHandler(Proxy_SelectionChanged);
                    textbox.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
                }
                else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
                {
                    textbox.SelectionChanged -= new RoutedEventHandler(Proxy_SelectionChanged);
                    textbox.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
                }

                return;
            }
        }

        private static void Proxy_SelectionChanged(object sender, RoutedEventArgs e)
        {
            string eventKey = GetSelectionChanged(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.SelectionChanged, eventKey);
        }
        #endregion SelectionChanged


        #region Click
        public static readonly DependencyProperty ClickProperty =
            DependencyProperty.RegisterAttached("Click",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnClickChanged)
        );

        public static string GetClick(DependencyObject obj)
        {
            return (string)obj.GetValue(ClickProperty);
        }

        public static void SetClick(DependencyObject obj, string value)
        {
            obj.SetValue(ClickProperty, value);
        }

        private static void OnClickChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ButtonBase target = obj as ButtonBase;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.Click += new RoutedEventHandler(Proxy_Click);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.Click -= new RoutedEventHandler(Proxy_Click);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_Click(object sender, RoutedEventArgs e)
        {
            string eventKey = GetClick(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.Click, eventKey);
        }
        #endregion Click


        #region Checked
        public static readonly DependencyProperty CheckedProperty =
            DependencyProperty.RegisterAttached("Checked",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnCheckedChanged)
        );

        public static string GetChecked(DependencyObject obj)
        {
            return (string)obj.GetValue(CheckedProperty);
        }

        public static void SetChecked(DependencyObject obj, string value)
        {
            obj.SetValue(CheckedProperty, value);
        }

        private static void OnCheckedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            ToggleButton target = obj as ToggleButton;
            if (target == null) return;

            string newValue = e.NewValue as string;
            string oldValue = e.OldValue as string;

            if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
            {
                target.Checked += new RoutedEventHandler(Proxy_Checked);
                target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
            }
            else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
            {
                target.Checked -= new RoutedEventHandler(Proxy_Checked);
                target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
            }
        }

        private static void Proxy_Checked(object sender, RoutedEventArgs e)
        {
            string eventKey = GetChecked(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.Checked, eventKey);
        }
        #endregion Checked


        #region ValueChanged
        public static readonly DependencyProperty ValueChangedProperty =
            DependencyProperty.RegisterAttached("ValueChanged",
            typeof(string),
            typeof(EventBroker),
            new PropertyMetadata(String.Empty, OnValueChangedChanged)
        );

        public static string GetValueChanged(DependencyObject obj)
        {
            return (string)obj.GetValue(ValueChangedProperty);
        }

        public static void SetValueChanged(DependencyObject obj, string value)
        {
            obj.SetValue(ValueChangedProperty, value);
        }

        private static void OnValueChangedChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            RangeBase target = obj as RangeBase;
            if (target != null)
            {
                string newValue = e.NewValue as string;
                string oldValue = e.OldValue as string;

                if (!String.IsNullOrEmpty(newValue) && String.IsNullOrEmpty(oldValue))
                {
                    target.ValueChanged += new RoutedPropertyChangedEventHandler<double>(Proxy_ValueChanged);
                    target.Unloaded += new RoutedEventHandler(Proxy_UnLoaded);
                }
                else if (String.IsNullOrEmpty(newValue) && !String.IsNullOrEmpty(oldValue))
                {
                    target.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(Proxy_ValueChanged);
                    target.Unloaded -= new RoutedEventHandler(Proxy_UnLoaded);
                }

                return;
            }
        }

        private static void Proxy_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string eventKey = GetValueChanged(sender as DependencyObject);
            Fire_EventHandler(sender, e, EventType.ValueChanged, eventKey);
        }
        #endregion ValueChanged


        #endregion The Attached Events
    }
}
