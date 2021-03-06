﻿// Copyright (c) 2016 Twickt / Ceschia Davide
//Application idea, code and time are given by Davide Ceschia / Twickt
//You may use them according to the GNU GPL v.3 Licence
//GITHUB Project: https://github.com/killpowa/Twickt-Launcher
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Twickt_Launcher.Dialogs
{
    /// <summary>
    /// Interaction logic for HowTo.xaml
    /// </summary>
    public partial class HowTo : Page
    {
        Timer sw = new Timer();
        public HowTo()
        {
            Window1.singleton.MenuToggleButton.IsEnabled = false;
            Window1.singleton.popupbox.IsEnabled = false;
            Window1.singleton.homeButton.IsEnabled = false;
            InitializeComponent();
            var ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes", true);

            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                freeram.Content = Convert.ToInt32(ramCounter.NextValue() / 1024).ToString() + "GB";
            }));
            sw.Start();
            sw.Elapsed += new ElapsedEventHandler(sw_Tick);
            sw.Interval = 2000; // in miliseconds
        }
        private void sw_Tick(object source, ElapsedEventArgs e)
        {
            //GET AVAILABLE MEMORY
            var ramCounter = new System.Diagnostics.PerformanceCounter("Memory", "Available MBytes", true);
            try
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    freeram.Content = Convert.ToInt32(ramCounter.NextValue() / 1024).ToString() + "GB";
                    if(Convert.ToInt32(ramCounter.NextValue() / 1024) <= 2)
                    {
                        errorlowram.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        errorlowram.Visibility = Visibility.Hidden;
                    }
                }));
            }
            catch
            { }
            //GET TOTAL MEMORY
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            sw.Stop();
            Properties.Settings.Default["firstTimeHowTo"] = "false";
            Properties.Settings.Default["RAM"] = ram.Text;
            Properties.Settings.Default.Save();
            Window1.singleton.MenuToggleButton.IsEnabled = true;
            Window1.singleton.popupbox.IsEnabled = true;
            Window1.singleton.homeButton.IsEnabled = true;
            Window1.singleton.MainPage.Navigate(new Pages.Home());
            Window1.singleton.NavigationMenu.SelectedIndex = 0;

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twickt.com/our-launcher/");
        }
    }
}
