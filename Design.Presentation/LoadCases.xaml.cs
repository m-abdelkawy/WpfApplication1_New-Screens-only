﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using SAP2000v20;
using Design.Presentation.ViewModels;
using Design.Presentation.Model;
using Desing.Core.Sap;
using System.Data;
using System.Collections.ObjectModel;

namespace Design.Presentation
{
    //public class loadCaseItems
    //{
    //    private string name;

    //    public string Name
    //    {
    //        get { return name; }
    //        set { name = value; }
    //    }

    //    private List<string> loadcaseNames;

    //    public List<string> LoadCaseNames
    //    {
    //        get { return loadcaseNames; }
    //        set { loadcaseNames = value; }
    //    }


    //    public loadCaseItems(string _name, List<string> _names)
    //    {
    //        this.name = _name;
    //        this.loadcaseNames = _names;
    //    }

    //}
    /// <summary>
    /// Interaction logic for LoadCases.xaml
    /// </summary>
    public partial class LoadCases : Window
    {
        //public static ObservableCollection<string> loadPatName = new ObservableCollection<string>();
        //LoadPattern
        public static List<string> loadPatternName = new List<string>();
        public List<double> SelfWtMultiplier = new List<double>();
        public List<eLoadPatternType> patternType = new List<eLoadPatternType>();


        public List<SapLoadPattern> loadPatterns = new List<SapLoadPattern>();
        public eCNameType ec = eCNameType.LoadCase;
        /*------------------------------*/

        //Load Combinations
        List<string> Combinations = new List<string>();
        public static List<string> loadCaseName = new List<string>();
        List<double> loadFactorList = new List<double>();
        /*------------------------------*/
        public LoadCases()
        {
            DataContext = new LoadCasesVM();
            InitializeComponent();
            loadCaseGridData.CanUserAddRows = false;
            loadCaseGridData.CanUserDeleteRows = false;
            loadCaseGridData.CanUserSortColumns = false;
            loadCaseGridData.CanUserReorderColumns = false;
            loadCaseGridData.CanUserResizeColumns = false;
            loadCaseGridData.CanUserResizeRows = false;

            loadComboGridData.CanUserAddRows = false;
            loadComboGridData.CanUserDeleteRows = false;
            loadComboGridData.CanUserSortColumns = false;
            loadComboGridData.CanUserReorderColumns = false;
            loadComboGridData.CanUserResizeColumns = false;
            loadComboGridData.CanUserResizeRows = false;
            #region DataBindingTrial
            //LoadCaseGridData load1 = new LoadCaseGridData();
            //load1.LoadcaseName = "Dead";
            //load1.Id = 5;

            //loadCaseGridData.Items.Add(load1);
            #endregion
        }

        static int AddLoadCaseBtnClicked = 0;

        private void AddLoadCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            var newLoadCaseRow = new LoadCaseGridData();

<<<<<<< HEAD
            var viewModel =(LoadCasesVM) DataContext;
            var newLoadCaseRow = new LoadCaseGridData();
            viewModel.loadCaseGridData.Add(newLoadCaseRow);

=======
            LoadCasesVM.loadCaseGridData.Add(newLoadCaseRow);


            #region Amr
            //var viewModel = (LoadCasesVM)DataContext;
            //var newLoadCaseRow = new LoadCaseGridData();

            //viewModel.loadCaseGridData.Add(newLoadCaseRow);
            #endregion

            //loadPatternName.Add(loadCaseGridData[);
            //SelfWtMultiplier.Add(viewModel.SelfWtMult);
            //patternType.Add(viewModel.loadPatternType);
>>>>>>> d595c6264d5ef05c3e907d634bc4dcc57e69ec0e
            #region old
            //loadCaseGridData.SelectAllCells();
            //loadCaseGridData.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            //ApplicationCommands.Copy.Execute(null, loadCaseGridData);
            //loadCaseGridData.UnselectAllCells();
            //String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //string[] Lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            //string[] Fields;
            //Fields = Lines[0].Split(new char[] { ',' });
            //int Cols = Fields.GetLength(0);

            ////1st row must be column names; force lower case to ensure matching later on.
            //for (int i = 0; i < Cols; i++)
            //    dt.Columns.Add(Fields[i].ToUpper(), typeof(string));
            //DataRow Row;
            //for (int i = 1; i < Lines.GetLength(0) - 1; i++)
            //{
            //    Fields = Lines[i].Split(new char[] { ',' });
            //    Row = dt.NewRow();
            //    for (int f = 0; f < Cols; f++)
            //    {
            //        Row[f] = Fields[f];
            //    }
            //    dt.Rows.Add(Row);
            //    loadPatternName.Add(dt.Rows[AddLoadCaseBtnClicked].ItemArray[1].ToString());



            //    //SelfWtMultiplier.Add(Convert.ToDouble(selfWttxtBox.Text));
            //    //patternType.Add((eLoadPatternType)loadTypeComboBox.SelectedValue);

            //    //distLoadShapes.Add(new List<Rectangle>());
            //}

            ////loadPatternName.Add(dgv.LoadcaseName);
            //AddLoadCaseBtnClicked += 1;



            ////static int loadCaseBtnClicked = 0;
            ////private void loadCaseBtn_Click(object sender, RoutedEventArgs e)
            ////{
            ////    loadPatternName.Add(loadCasetxtBox.Text);
            ////    SelfWtMultiplier.Add(Convert.ToDouble(selfWttxtBox.Text));
            ////    patternType.Add((eLoadPatternType)loadTypeComboBox.SelectedValue);

            ////    distLoadShapes.Add(new List<Rectangle>());
            ////    loadCaseBtnClicked += 1;
            ////}

            ////private void loadTypeComboBox_Loaded(object sender, RoutedEventArgs e)
            ////{
            ////    loadTypeComboBox.ItemsSource = Enum.GetValues(typeof(eLoadPatternType)).Cast<eLoadPatternType>();
            ////}

            ////private void loadCaseComboBox_Loaded(object sender, RoutedEventArgs e)
            ////{
            ////    var loadCaseComboBox = sender as ComboBox;
            ////    loadCaseComboBox.ItemsSource = loadPatternName;
            ////    loadCaseComboBox.SelectedIndex = 0;
            ////}

            ////private void loadCaseComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
            ////{
            ////    var loadCaseComboBox = sender as ComboBox;
            ////    string value = loadCaseComboBox.SelectedIndex.ToString();

            ////    var loadComboBoxSelectedItem = e.AddedItems[0] as string;

            ////    //loadCaseName = loadComboBoxSelectedItem;
            ////    //this.Title = value;
            ////}

            ////private void loadComboBtn_Click(object sender, RoutedEventArgs e)
            ////{

            ////}
            #endregion
            AddLoadCaseBtnClicked += 1;
        }

        private void DltLoadCaseBtn_Click(object sender, RoutedEventArgs e)
        {
            var index = loadCaseGridData.SelectedIndex;
            LoadCasesVM.loadCaseGridData.RemoveAt(index);

            #region Amr
            //var viewModel = (LoadCasesVM)DataContext;
            //var index = loadCaseGridData.SelectedIndex;
            //viewModel.loadCaseGridData.RemoveAt(index);
            #endregion
        }

        private void loadCasesOkBtn_Click(object sender, RoutedEventArgs e)
        {

            for (int i = 0; i < LoadCasesVM.loadCaseGridData.Count; i++)
            {
                if (LoadCasesVM.loadCaseGridData[i].LoadcaseName != null)
                {
                    loadPatternName.Add(LoadCasesVM.loadCaseGridData[i].LoadcaseName);
                    SelfWtMultiplier.Add(LoadCasesVM.loadCaseGridData[i].SelfWtMult);
                    patternType.Add(LoadCasesVM.loadCaseGridData[i].loadPatternType);

                    //loadPatName.Add(LoadCasesVM.loadCaseGridData[i].LoadcaseName);
                }
            }
        }

        private void AddLoadComboBtn_Click(object sender, RoutedEventArgs e)
        {
            //ComboBox loadcasecb = new ComboBox();

            //loadcasecb.ItemsSource = loadCaseName;

            var newLoadComboRow = new LoadComboGridData();
            LoadCasesVM.loadComboGridData.Add(newLoadComboRow);


        }

        private void ComboSubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            //var loadpat = new ObservableCollection<LoadCasesVM>();

            for (int i = 0; i < LoadCasesVM.loadComboGridData.Count; i++)
            {
                if (LoadCasesVM.loadComboGridData[i].LoadComboName != null && LoadCasesVM.loadComboGridData[i].LoadComboName != null)
                {
                    Combinations.Add(LoadCasesVM.loadComboGridData[i].LoadComboName);
                    loadCaseName.Add(LoadCasesVM.loadComboGridData[i].LoadCaseName);
                    loadFactorList.Add(LoadCasesVM.loadComboGridData[i].LoadFactor);
                }
            }

        }
    }
}
