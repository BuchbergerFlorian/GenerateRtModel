using System.Xml.Linq;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml
{
    public class XmlLoader
    {
        private readonly XDocument? _xmlFile;
        public XElement? RootElement { get; private set; }
    
        public XmlLoader(string programPart)
        {
            try
            {
                switch (programPart)
                {
                    case "eqModel":
                        _xmlFile = XDocument.Load(@"C:\dev\GenerateRtModel\Generator\Data_Reading\Xml\XML Files\EQModel.XML");
                        break;
                    case "variableModel":
                        _xmlFile = XDocument.Load(@"C:\dev\GenerateRtModel\Generator\Data_Reading\Xml\XML Files\Variables.XML");
                        break;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("XML File not found!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
    
            if (_xmlFile?.Root != null)
            {
                RootElement = _xmlFile.Root; 
            }
        }
    }
}

