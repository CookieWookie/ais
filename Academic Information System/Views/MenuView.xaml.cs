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
            this.addModelContextMenu.PlacementTarget = this.addButton;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            this.addModelContextMenu.IsOpen = true;
        }
    }
}
