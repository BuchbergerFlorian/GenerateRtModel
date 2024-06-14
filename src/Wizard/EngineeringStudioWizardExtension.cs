using System;
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

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            _project = context.Workspace.ActiveProject;

            string filepathFromZenonXmlEqModel = ExportEquipmentModelToXml();
            string filepathFromZenonXmlVariables = ExportVariablesToXml();

            try
            {
                RtModel rtModel = new RtModel(filepathFromZenonXmlEqModel, filepathFromZenonXmlVariables);
                rtModel.CreateRtModel().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
            finally
            {
                DeleteFile(filepathFromZenonXmlEqModel);
                DeleteFile(filepathFromZenonXmlVariables);
            }
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
        
        private void DeleteFile(string filePath)
        {
            if (filePath != null && File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error deleting file {filePath}: {ex.Message}");
                }
            }
        }

        #endregion
    }
}