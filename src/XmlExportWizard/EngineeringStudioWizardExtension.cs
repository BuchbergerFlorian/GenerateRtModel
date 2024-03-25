using Scada.AddIn.Contracts;
using System;

namespace XmlExportWizard
{
    /// <summary>
    /// Description of Engineering Studio Wizard Extension.
    /// </summary>
    [AddInExtension("RtModelGenerator", "This Wizard creates a RtModel for using OctoMesh", "OctoMesh")]
    public class EngineeringStudioWizardExtension : IEditorWizardExtension
    {
        #region IEditorWizardExtension implementation

        public void Run(IEditorApplication context, IBehavior behavior)
        {
            // enter your code which should be executed when starting the SCADA Engineering Studio Wizard

        }

        #endregion
    }

}