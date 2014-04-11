using System;
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
    /// Interaction logic for AddExamView.xaml
    /// </summary>
    public partial class AddExamView : UserControl
    {
        public AddExamView()
        {
            InitializeComponent();
            this.selectStudentContextMenu.PlacementTarget = this.plusButton;
            this.selectSubjectContextMenu.PlacementTarget = this.selectSubjectButton;
            this.selectTeacherContextMenu.PlacementTarget = this.selectTeacherButton;
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {
            this.selectStudentContextMenu.IsOpen = true;
        }

        private void SelectSubjectButton_Click(object sender, RoutedEventArgs e)
        {
            this.selectSubjectContextMenu.IsOpen = true;
        }

        private void SelectTeacherButton_Click(object sender, RoutedEventArgs e)
        {
            this.selectTeacherContextMenu.IsOpen = true;
        }
    }
}
