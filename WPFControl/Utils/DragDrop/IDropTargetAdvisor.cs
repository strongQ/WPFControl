using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFControl.Utils
{
    public interface IDropTargetAdvisor
    {
        UIElement TargetUI { get; set; }

        bool ApplyMouseOffset { get; }
        bool IsValidDataObject(IDataObject obj);
        void OnDropCompleted(IDataObject obj, Point dropPoint);
        UIElement GetVisualFeedback(IDataObject obj);
        UIElement GetTopContainer();
    }
}
