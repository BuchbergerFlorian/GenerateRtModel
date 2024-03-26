namespace MeshMakers.GenerateRtModel.Generator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            const string filepathFromZenonXmlEqModel =
                @"C:\dev\GenerateRtModel\src\Generator.TestConsole\Data_Reading\Xml\XML Files\EQModel_Basisproject.XML";

            const string filepathFromXmlZenonVariable =
                @"C:\dev\GenerateRtModel\src\Generator.TestConsole\Data_Reading\Xml\XML Files\Variables_Basisproject.XML";

            CreateRtModel rtModel = new CreateRtModel(filepathFromZenonXmlEqModel, filepathFromXmlZenonVariable);
            rtModel.Create();
        }
    }  
}

