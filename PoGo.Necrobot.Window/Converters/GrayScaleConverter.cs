﻿using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PoGo.Necrobot.Window.Converters
{
    public class GrayScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is BitmapSource)
            {
                BitmapSource orgBmp = (BitmapSource)value;
                if (orgBmp.Format == PixelFormats.Bgra32)
                {
                    byte[] orgPixels = new byte[orgBmp.PixelHeight *
                        orgBmp.PixelWidth * 4];
                    byte[] newPixels = new byte[orgPixels.Length];
                    orgBmp.CopyPixels(orgPixels, orgBmp.PixelWidth * 4, 0);
                    for (int i = 3; i < orgPixels.Length; i += 4)
                    {
                        int grayVal = ((int)orgPixels[i - 3] +
                        (int)orgPixels[i - 2] + (int)orgPixels[i - 1]);

                        if (grayVal != 0)
                            grayVal = grayVal / 3;
                        newPixels[i] = orgPixels[i]; //Set AlphaChannel
                        newPixels[i - 3] = (byte)grayVal;
                        newPixels[i - 2] = (byte)grayVal;
                        newPixels[i - 1] = (byte)grayVal;
                    }
                    return BitmapSource.Create(orgBmp.PixelWidth, orgBmp.PixelHeight,
                        96, 96, PixelFormats.Bgra32, null, newPixels,
                        orgBmp.PixelWidth * 4);
                }
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
