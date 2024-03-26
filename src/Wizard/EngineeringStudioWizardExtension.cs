using System.IO;
using MeshMakers.GenerateRtModel.Wizard.Includes;
using Scada.AddIn.Contracts;
using MeshMakers.GenerateRtModel.Generator;

namespace MeshMakers.GenerateRtModel.Wizard
{
    /// <summary>
    /// Description of Engineering Studio Wizard Extension.
    /// </summary>
    [AddInExtension(Constants.Product, "This wizard creates a RtModel for OctoMesh", Constants.Product)]
    public class EngineeringStudioWizardExtension : IEditorWizardExtension
    {
        private IProject _project;
        
        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            _project = context.Workspace.ActiveProject;

            string filepathFromZenonXmlEqModel = ExportEquipmentModelToXml();
            string filepathFromXmlZenonVariable = ExportVariablesToXml();
            
            CreateRtModel rtModel = new CreateRtModel(filepathFromZenonXmlEqModel, filepathFromXmlZenonVariable);
            rtModel.Create();

        }

        private string ExportEquipmentModelToXml()
        {
            var fileName = TempFileName;

            if (_project.EquipmentModeling.ExportToXml(fileName))
            {
                return fileName;
            }
            
            return null;
        }
        
        private string ExportVariablesToXml()
        {
            var fileName = TempFileName;
            if (_project.VariableCollection.ExportToXml(fileName))
            {
                return fileName;
            }

            return null;
        }
        
        private string TempFileName
        {
            get
            {
                return Path.ChangeExtension(Path.GetTempFileName(), ".xml");
            }
        }

        #endregion
    }
}