using System.Xml.Linq;
using MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel;
using Meshmakers.Octo.ConstructionKit.Contracts;
using static System.Console;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml
{
    public class XmlManager
    {
        private int _groupDepth;
        private int _groupIdListIndex;
        private OctoObjectId _modelRtId;
        private readonly GroupIdsRepository _groupList;
        
        public XmlManager()
        {
            _groupDepth = 0;
            _groupIdListIndex = 0;
            _groupList = new GroupIdsRepository();
        }

        public XElement? GetRootElement(string programPart)
        {
            XElement? rootElement = new XmlLoader(programPart).RootElement;
            return rootElement;
        }

        public void IterateOverEqModel(XElement element, int depth, RtModel rtModel)
        {
            var elementData = new XmlElementData(element, depth);
            WriteLine($"{elementData}");
            
            switch (elementData.ElementType)
            {
                case "Model":
                    _modelRtId = OctoObjectId.GenerateNewId();
                    string? elementName = elementData.ElementName;
                    rtModel.AddModelToRoot(elementName, _modelRtId);
                    break;
            
                case "Group":
                    ProcessGroupElement(elementData, depth, rtModel);
                    break;
            }
            
            foreach (var childElement in element.Elements())
            {
                IterateOverEqModel(childElement, depth + 1, rtModel);
            }
        }
        
        private void ProcessGroupElement(XmlElementData elementData, int elementDepth, RtModel rtModel)
        {
            string? groupName = elementData.ElementName;
            bool isModelParent = elementData.ModelIsTheParentElement;
    
            if ((_groupDepth <= elementDepth || elementDepth < _groupDepth) && isModelParent)
            { 
                _groupList.AddGroupIdToList(elementDepth);
                rtModel.AddGroupToRoot(groupName, _modelRtId,_groupList.GetGroupIdFromListIndex(_groupIdListIndex), "Basic/EquipmentModel");
                _groupIdListIndex ++;

            }
            else if (_groupDepth < elementDepth)
            {
                _groupList.AddGroupIdToList(elementDepth);
                rtModel.AddGroupToRoot(groupName, _groupList.GetGroupIdFromListIndex(_groupIdListIndex-1), _groupList.GetGroupIdFromListIndex(_groupIdListIndex), "Basic/EquipmentGroup");
                _groupIdListIndex ++;

            }
            else if (elementDepth <= _groupDepth)
            {
                _groupList.AddGroupIdToList(elementDepth);

                OctoObjectId targetId = default;
                for (int i = 0; i < _groupList.GetLengthOfList(); i++)
                {
                    if (_groupList.GetDepthFromListIndex(i) <
                        _groupList.GetDepthFromListIndex(_groupIdListIndex))
                    {
                        targetId = _groupList.GetGroupIdFromListIndex(i);
                    }
                }
                rtModel.AddGroupToRoot(groupName, targetId, _groupList.GetGroupIdFromListIndex(_groupIdListIndex), "Basic/EquipmentGroup");
                _groupIdListIndex ++;
            }
            _groupDepth = elementDepth;
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
                    string[] groups = [];

                    if (!string.IsNullOrEmpty(modelGroup))
                    {
                       groups = modelGroup.Split(';'); 
                       variableList.AddVariableToList(variableName ?? string.Empty, description ?? string.Empty, groups);
                    }
                }
            }
        }
    }
}
