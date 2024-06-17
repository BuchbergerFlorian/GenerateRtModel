namespace MeshMakers.GenerateRtModel.Generator
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            string filePathFromZenonXmlEqModel =  @"..\..\..\..\Generator\DataReading\Xml\XML Files\EQModel_Basisproject.XML";
            string filePathFromXmlZenonVariable = @"..\..\..\..\Generator\DataReading\Xml\XML Files\Variables_Basisproject.XML";
            string filePathForYamlFile =          @"..\..\..\..\Generator\DataStoring\Yaml\RtModel.yaml";
            
            RtModel rtModel = new RtModel(filePathFromZenonXmlEqModel, filePathFromXmlZenonVariable, filePathForYamlFile);
            await rtModel.CreateRtModel();
        }
    }  
}

