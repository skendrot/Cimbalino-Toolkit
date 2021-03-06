﻿// ****************************************************************************
// <copyright file="DisplayPropertiesService.cs" company="Pedro Lamas">
// Copyright © Pedro Lamas 2014
// </copyright>
// ****************************************************************************
// <author>Pedro Lamas</author>
// <email>pedrolamas@gmail.com</email>
// <project>Cimbalino.Toolkit</project>
// <web>http://www.pedrolamas.com</web>
// <license>
// See license.txt in this solution or http://www.pedrolamas.com/license_MIT.txt
// </license>
// ****************************************************************************

#if WINDOWS_PHONE
using System;
using System.Windows;
using Microsoft.Phone.Info;
using Windows.Graphics.Display;
using Rect = Cimbalino.Toolkit.Foundation.Rect;
#else
using Cimbalino.Toolkit.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
#endif

namespace Cimbalino.Toolkit.Services
{
    /// <summary>
    /// Represents an implementation of the <see cref="IDisplayPropertiesService"/>.
    /// </summary>
    public class DisplayPropertiesService : IDisplayPropertiesService
    {
        /// <summary>
        /// Gets the pixels per logical inch of the current environment.
        /// </summary>
        /// <value>The pixels per logical inch of the current environment.</value>
        public float LogicalDpi
        {
            get
            {
#if WINDOWS_PHONE
                return DisplayProperties.LogicalDpi;
#else
                return DisplayInformation.GetForCurrentView().LogicalDpi;
#endif
            }
        }

        /// <summary>
        /// Gets the raw dots per inch (DPI) along the x axis of the display monitor.
        /// </summary>
        /// <value>The raw dots per inch (DPI) along the x axis of the display monitor.</value>
        public float RawDpiX
        {
            get
            {
#if WINDOWS_PHONE
                object value;

                if (DeviceExtendedProperties.TryGetValue("RawDpiX", out value))
                {
                    return (float)value;
                }

                return 1.0f;
#else
                return DisplayInformation.GetForCurrentView().RawDpiX;
#endif
            }
        }

        /// <summary>
        /// Gets the raw dots per inch (DPI) along the y axis of the display monitor.
        /// </summary>
        /// <value>The raw dots per inch (DPI) along the y axis of the display monitor.</value>
        public float RawDpiY
        {
            get
            {
#if WINDOWS_PHONE
                object value;

                if (DeviceExtendedProperties.TryGetValue("RawDpiY", out value))
                {
                    return (float)value;
                }

                return 1.0f;
#else
                return DisplayInformation.GetForCurrentView().RawDpiY;
#endif
            }
        }

        /// <summary>
        /// Gets the height and width of the application window, as a Rect value.
        /// </summary>
        /// <value>A value that reports the height and width of the application window.</value>
        public Rect Bounds
        {
            get
            {
#if WINDOWS_PHONE
                return new Rect(0, 0, Application.Current.Host.Content.ActualWidth, Application.Current.Host.Content.ActualHeight);
#else
                var bounds = Window.Current.Bounds;

                return new Rect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
#endif
            }
        }

        /// <summary>
        /// Gets the number of raw (physical) pixels for each view (layout) pixel.
        /// </summary>
        /// <value>The number of raw (physical) pixels for each view (layout) pixel.</value>
        public double RawPixelsPerViewPixel
        {
            get
            {
#if WINDOWS_PHONE
                return 1.0;
#elif WINDOWS_PHONE_APP
                return DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;
#else
                return DisplayInformation.GetForCurrentView().LogicalDpi / 96.0;
#endif
            }
        }
    }
}