using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WPFControl.Controls
{
    public class MessageBarItem:ContentControl
    {
        static MessageBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBarItem), new FrameworkPropertyMetadata(typeof(MessageBarItem)));
        }
    }
}
