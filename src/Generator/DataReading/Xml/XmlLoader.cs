using System.Xml.Linq;

namespace MeshMakers.GenerateRtModel.Generator.DataReading.Xml
{
    public class XmlLoader
    {
        private readonly XDocument? _xmlFile;
        public XElement? RootElement { get; private set; }
    
        public XmlLoader(string xmlFilePath)
        {
            try
            { 
                _xmlFile = XDocument.Load(xmlFilePath);
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

