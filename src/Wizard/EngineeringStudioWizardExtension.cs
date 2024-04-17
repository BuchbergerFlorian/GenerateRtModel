using System.IO;
using MeshMakers.GenerateRtModel.Generator;
using MeshMakers.GenerateRtModel.Wizard.Includes;
using Scada.AddIn.Contracts;

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

        public async void Run(IEditorApplication context, IBehavior behavior)
        {
            _project = context.Workspace.ActiveProject;

            string filepathFromZenonXmlEqModel = ExportEquipmentModelToXml();
            string filepathFromXmlZenonVariable = ExportVariablesToXml();
            
            RtModel rtModel = new RtModel(filepathFromZenonXmlEqModel, filepathFromXmlZenonVariable);
            await rtModel.CreateRtModel();

        }

        private string ExportEquipmentModelToXml()
        {
            var fileName = TempFileName;

            return _project.EquipmentModeling.ExportToXml(fileName) ? fileName : null;
        }
        
        private string ExportVariablesToXml()
        {
            var fileName = TempFileName;
            return _project.VariableCollection.ExportToXml(fileName) ? fileName : null;
        }
        
        private string TempFileName => Path.ChangeExtension(Path.GetTempFileName(), ".xml");

        #endregion
    }
}