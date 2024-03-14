using System.Xml.Linq;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml
{
    public class XmlManager
    {
        public XElement? GetRootElement(string programPart)
        {
            XElement? rootElement = new XmlLoader(programPart).RootElement;
            return rootElement;
        }

        public void IterateOverEqModel(XElement element, int depth, EqModelRepository eqModelRepository)
        {
            bool elementIsModelOrGroup = CheckElementForModelOrGroup(element);
            if (elementIsModelOrGroup)
            {
               var elementData = new XmlElementData(element, depth)
               {
                   TargetCkType = element.Parent?.Parent?.Name == "Model" ? "Basic/EquipmentModel" : "Basic/EquipmentGroup"
               };

               eqModelRepository.AddElementToList(elementData);
            }
            
            foreach (var childElement in element.Elements())
            {
                IterateOverEqModel(childElement, depth + 1, eqModelRepository);
            }
        }

        private bool CheckElementForModelOrGroup(XElement element)
        {
            if (element.Name == "Model" || element.Name == "Group")
            {
                return true;
            }

            return false;
        }

        public void IterateOverVariableModel(XElement element, VariableRepository variableList)
        {
            IEnumerable<XElement>? variableCollection = element.Element("Apartment")?.Elements();

            if (variableCollection != null)
            {
                foreach (XElement variable in variableCollection)
                {
                    string? variableName = variable.Element("Name")?.Value;
                    string? description = variable.Element("Description")?.Value;
                    string? modelGroup = variable.Element("SystemModelGroup")?.Value;

                    if (!string.IsNullOrEmpty(modelGroup))
                    {
                        string[] groups = modelGroup.Split(';');
                        variableList.AddVariableToList(variableName ?? string.Empty, description ?? string.Empty, groups);
                    }
                }
            }
        }

        
    }
}
