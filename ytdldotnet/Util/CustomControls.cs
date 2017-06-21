using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ytdldotnet.Util
{
    static class CustomControls
    {
        public static ProgressRing GetProgressRing()
        {
            return new ProgressRing()
            {
                Height = 64,
                Width = 64,
                Margin = new Thickness(16, 16, 16, 16),
                IsActive = true
            };
        }

        public static Label GetLoadingLabel(string content)
        {
            return new Label()
            {
                Content = content,
                Margin = new Thickness(16, 16, 16, 16),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        public static TextBox GetUrlTextbox()
        {
            return new TextBox()
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                Height = 32,
                Width = 256,
                Margin = new Thickness(16, 16, 16, 16),
                TextWrapping = TextWrapping.NoWrap,
                VerticalAlignment = VerticalAlignment.Top,
                BorderThickness = new Thickness(1)
            };
        }
    }
}
