using Design.Presentation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MaterialEditor.xaml
    /// </summary>
    public partial class MaterialEditor : Window
    {
        public MaterialDialouge MaterialDialouge { get; set; }
        public MaterialEditor()
        {
          
            InitializeComponent();
            Cb_MaterialType.ItemsSource =
                Enum.GetValues(typeof(MaterialType)).Cast<MaterialType>();
            Cb_MaterialType.SelectedIndex = 0;
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            var materials = (ObservableCollection<MaterialEditorVM>)MaterialDialouge.DataContext;
           // Adding current material to the list
            materials.Add((MaterialEditorVM)DataContext);
            //close the window
            this.Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            MaterialEditorVM.Counter--;
            Close();
        }

       
    }
}
