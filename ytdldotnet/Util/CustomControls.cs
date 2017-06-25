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
                Margin = new Thickness(16,0,16,0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };
        }

        public static TextBox GetPathBox()
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

        public static Grid GetConversionButtons()
        {
            List<RadioButton> buttons = new List<RadioButton>();

            foreach(ConversionFormats format in Enum.GetValues(typeof(ConversionFormats)))
            {
                RadioButton btn = new RadioButton()
                {
                    Name = format + "rdBtn",
                    Content = format,
                    GroupName = "targetFormat",
                    IsChecked = false
                };

                Grid.SetColumn(btn, Array.IndexOf(Enum.GetValues(typeof(ConversionFormats)), format) % 3);
                Grid.SetRow(btn, (int) Math.Floor((double)(Array.IndexOf(Enum.GetValues(typeof(ConversionFormats)), format)/3)));

                buttons.Add(btn);
            }

            Grid conversionButtonsGrid = new Grid();

            conversionButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            conversionButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            conversionButtonsGrid.ColumnDefinitions.Add(new ColumnDefinition());
            conversionButtonsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });
            conversionButtonsGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(24) });

            foreach (RadioButton btn in buttons)
            {
                conversionButtonsGrid.Children.Add(btn);
            }

            return conversionButtonsGrid;
        }
    }
}
