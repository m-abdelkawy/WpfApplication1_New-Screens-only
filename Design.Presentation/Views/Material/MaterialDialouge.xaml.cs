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

namespace Design.Presentation.Views.Material
{
    /// <summary>
    /// Interaction logic for MaterialDialouge.xaml
    /// </summary>
    public partial class MaterialDialouge : Window
    {
        public MaterialDialouge()
        {
           
            InitializeComponent();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Btn_New_Material_Click(object sender, RoutedEventArgs e)
        {
            MaterialEditor me = new MaterialEditor()
            {
                MaterialDialouge = this,
                DataContext = new MaterialEditorVM()

            };
            
            me.ShowDialog();
        }
    }
}
