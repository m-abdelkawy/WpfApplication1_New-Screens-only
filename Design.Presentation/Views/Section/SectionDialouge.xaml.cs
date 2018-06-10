using Design.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Design.Presentation.Views.Section
{
    /// <summary>
    /// Interaction logic for SectionDialouge.xaml
    /// </summary>
    public partial class SectionDialouge : Window
    {
       
        public SectionDialouge()
        {
            InitializeComponent();
        }

        private void Btn_New_Section_Click(object sender, RoutedEventArgs e)
        {
            SectionEditor se = new SectionEditor(new SectionEditorVM())
            {
               SectionDialouge = this
            };
            se.ShowDialog();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
