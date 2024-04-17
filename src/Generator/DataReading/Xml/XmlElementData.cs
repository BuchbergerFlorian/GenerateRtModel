using System.Xml.Linq;
using Meshmakers.Octo.ConstructionKit.Contracts;

namespace MeshMakers.GenerateRtModel.Generator.DataReading.Xml
{
    public class XmlElementData
    {
        private int _depth;
        private string? _elementType;
        private string? _elementName;
        private string? _elementDescription;

        private List<StructForVariable> _variables = new();

        public XmlElementData(XElement element, int depth)
        {
            Depth = depth;
            ElementType = element.Name.ToString();
            ElementName = element.Element("Name")?.Value;
            ElementDescription = element.Element("Description")?.Value;
            CkTypeId = ExtractStringFromDescription(ElementDescription);
            Id = OctoObjectId.GenerateNewId();
            TargetId = OctoObjectId.Empty;
            TargetCkType = string.Empty;
            EqGroupName = string.Empty;
        }

        public int Depth
        {
            get => _depth;
            private set
            {
                _depth = value;
            }
        }

        public string? ElementType
        {
            get => _elementType;
            private set => _elementType = value;
        }
        public string? ElementName
        {
            get => _elementName;
            private set
            {
                if (value != null)
                { 
                    _elementName = value;
                }
            }
        }
        public string? ElementDescription
        {
            get => _elementDescription;
            private set
            {
                if (value != null)
                {
                    _elementDescription = value;
                }
            }
        }
        
        public string CkTypeId { get; private set; }

        private string ExtractStringFromDescription(string? description)
        {
            int startIndex = description!.IndexOf('"') + 1;
            int endIndex = description.LastIndexOf('"');
            if (startIndex >= 0 && endIndex >= 0 && endIndex > startIndex)
            {
                return description.Substring(startIndex, endIndex - startIndex);
            }
            else
            {
                return "";
            }
        }

        public OctoObjectId Id { get; private set; }

        public OctoObjectId TargetId { get; set; }

        public string TargetCkType { get; set; }

        public string? EqGroupName { get; set; }

        public List<StructForVariable> Variables
        {
            get => _variables;
            set => _variables = value;
        }
        public void AddVariable(string name, string description)
        {
            StructForVariable newData = new StructForVariable();
            newData.Name = name;
            newData.Description = description;
            Variables.Add(newData);
        }

        public struct StructForVariable
        {
            public string Name;
            public string Description;

            public StructForVariable()
            {
                Name = "";
                Description = "";
            }
        }
 
        public override string ToString()
        {
           return $"Depth: {_depth}  {new string(' ', _depth * 2)} Type: {ElementType}: Name: {ElementName}, {Id}, {TargetId}, CkTypeId: {CkTypeId}"; 
        }
    }    
}

