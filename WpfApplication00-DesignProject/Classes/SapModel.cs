using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v20;

namespace ConsoleApplication1
{
    class SapModel
    {
        #region Member Variables and Properties
        bool attachToInstance;
        bool specifyPath;
        string modelDirectory;
        string modelName;
        string modelPath;
        private cSapModel mySapObjectModel;
        private cOAPI mySapObjectAPI;



        public bool AttachToInstance
        {
            get { return attachToInstance; }
            set { attachToInstance = value; }
        }

        public bool SpecifyPath
        {
            get { return specifyPath; }
            set { specifyPath = value; }
        }

        public string ModelDirectory
        {
            get { return modelDirectory; }
            set { modelDirectory = value; }
        }

        public string ModelName
        {
            get { return modelName; }
            set { modelName = value; }
        }

        public string ModelPath
        {
            get { return modelPath; }
            set { modelPath = value; }
        }

        public cSapModel MySapObjectModel
        {
            get { return mySapObjectModel; }
            set { mySapObjectModel = value; }
        }

        public cOAPI MySapObjectAPI
        {
            get { return mySapObjectAPI; }
            set { mySapObjectAPI = value; }
        }
        #endregion

        #region Constructor
        public SapModel(string _modelDirectory, string _modelName = "Trial1")
        {
            this.modelDirectory = _modelDirectory;
            this.modelName = _modelName;


            //set the following flag to true to attach to an existing instance of the program
            //otherwise a new instance of the program will be started
            AttachToInstance = false;

            //set the following flag to true to manually specify the path to SAP2000.exe
            //this allows for a connection to a version of SAP2000 other than the latest installation
            //otherwise the latest installed version of SAP2000 will be launched
            SpecifyPath = false;

            //if the above flag is set to true, specify the path to SAP2000 below
            string ProgramPath;
            ProgramPath = "C:\\Program Files (x86)\\Computers and Structures\\SAP2000 20\\SAP2000.exe";

            //full path to the model
            //set it to the desired path of your model
            try
            { System.IO.Directory.CreateDirectory(ModelDirectory); } //Model Directory is input to the constructor.
            catch (Exception /*ex*/)
            { Console.WriteLine("Could not create directory: " + ModelDirectory); }

            modelPath = ModelDirectory + System.IO.Path.DirectorySeparatorChar + ModelName; //model name is input to the constructor.

            //dimension the SapObject as cOAPI type
            cOAPI mySapObjectAPI = null;

            //Use ret to check if functions return successfully (ret = 0) or fail (ret = nonzero)
            int ret = 0;

            if (AttachToInstance)
            {
                //attach to a running instance of SAP2000
                try
                {
                    //get the active SapObject
                    mySapObjectAPI = (cOAPI)System.Runtime.InteropServices.Marshal.GetActiveObject("CSI.SAP2000.API.SapObject");
                }
                catch (Exception /*ex*/)
                {
                    Console.WriteLine("No running instance of the program found or failed to attach.");
                    return;
                }
            }

            else

            {
                //create API helper object from which we create SAPAPI object from which we Create SAPModel//
                cHelper myHelper;
                try
                {
                    myHelper = new Helper();
                }
                catch (Exception /*ex*/)
                {
                    Console.WriteLine("Cannot create an instance of the Helper object");
                    return;
                }

                if (SpecifyPath)
                {
                    //'create an instance of the SapObject from the specified path
                    try
                    {
                        //create SapObject
                        mySapObjectAPI = myHelper.CreateObject(ProgramPath);
                    }
                    catch (Exception /*ex*/)
                    {
                        Console.WriteLine("Cannot start a new instance of the program from " + ProgramPath);
                        return;
                    }
                }

                else
                {
                    //'create an instance of the SapObject from the latest installed SAP2000
                    try
                    {
                        //create SapObject
                        mySapObjectAPI = myHelper.CreateObjectProgID("CSI.SAP2000.API.SapObject");
                    }
                    catch (Exception /*ex*/)
                    {
                        Console.WriteLine("Cannot start a new instance of the program.");
                        return;
                    }
                }

                //start SAP2000 application
                ret = mySapObjectAPI.ApplicationStart(eUnits.kN_m_C);
                
            }

            //create SapModel object
            mySapObjectModel = mySapObjectAPI.SapModel;
        }
        #endregion

        #region Methods
        //Initialize Model
        public int InitializeUnits(eUnits units = eUnits.N_mm_C)
        {
            int ret;
            //Initialize Model
            MySapObjectModel.InitializeNewModel((eUnits)units);
            //Create New Blank Model
            ret = MySapObjectModel.File.NewBlank();
            return ret;
        }
        public int ChangeUnits(eUnits units = eUnits.N_mm_C)
        {
            int ret = MySapObjectModel.SetPresentUnits((eUnits)units);
            return ret;
        }
        public void SaveAndRun()
        {
            MySapObjectModel.File.Save(ModelPath); //Save Model
            MySapObjectModel.Analyze.RunAnalysis(); //Create Analysis Model
        }
        public void CloseModel(bool fileSave = true)
        {
            if (fileSave)
            {
                MySapObjectModel.File.Save(ModelPath);
            }
            MySapObjectAPI.ApplicationExit(fileSave);
            MySapObjectModel = null;
            MySapObjectAPI = null;
        }
        #endregion

    }
}
