using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFControl.Controls
{
    public class UIElementEx
    {
        public static double RoundLayoutValue(double value, double dpiScale)
        {
            double num;
            if (!DoubleHelper.AreClose(dpiScale, 1.0))
            {
                num = Math.Round(value * dpiScale) / dpiScale;
                if (DoubleHelper.IsNaN(num) || double.IsInfinity(num) || DoubleHelper.AreClose(num, 1.7976931348623157E+308))
                {
                    num = value;
                }
            }
            else
            {
                num = Math.Round(value);
            }
            return num;
        }
    }
}
