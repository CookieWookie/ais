﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AiS.Views
{
    /// <summary>
    /// Interaction logic for MenuView.xaml
    /// </summary>
    public partial class MenuView : UserControl
    {
        public MenuView()
        {
            InitializeComponent();
            this.openWindowsContextMenu.PlacementTarget = this.openWindowsButton;
        }

        private void OpenWindowsButton_Click(object sender, RoutedEventArgs e)
        {
            this.openWindowsContextMenu.IsOpen = true;
            e.Handled = true;
        }
    }
}
