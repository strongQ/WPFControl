using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JP.HCZZP.WPFApp.Infrastructure.Controls
{
    public class MessageBarItem:ContentControl
    {
        static MessageBarItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBarItem), new FrameworkPropertyMetadata(typeof(MessageBarItem)));
        }
    }
}
