using System.Xml.Linq;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml
{
    public class XmlElementData
    {
        private readonly int _depth;
        private string? _elementType;
        private string? _elementName;
        private bool _modelIsTheParentElement;
        private string _description;

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
        
        public bool ModelIsTheParentElement
        {
            get => _modelIsTheParentElement;
            private set => _modelIsTheParentElement = value;
        }
        public XmlElementData(XElement element, int depth)
        {
            _depth = depth;
            ElementType = element.Name.ToString();
            ElementName = element.Element("Name")?.Value;
            CheckForModelParent(element);
        }
        
        private void CheckForModelParent(XElement element)
        {
            if (element.Parent?.Parent?.Name == "Model")
            {
                ModelIsTheParentElement = true;
            }
        }
 
        public override string ToString()
        {
           return $"Depth: {_depth}  {new string(' ', _depth * 4)} {ElementType}"; 
        }
    }    
}

