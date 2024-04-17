namespace MeshMakers.GenerateRtModel.Generator
{
    public static class Program
    {
        private static async Task Main(string[] args)
        {
            const string filepathFromZenonXmlEqModel =
                @"C:\dev\GenerateRtModel\src\Generator\DataReading\Xml\XML Files\EQModel_Basisproject.XML";

            const string filepathFromXmlZenonVariable =
                @"C:\dev\GenerateRtModel\src\Generator\DataReading\Xml\XML Files\Variables_Basisproject.XML";

            RtModel rtModel = new RtModel(filepathFromZenonXmlEqModel, filepathFromXmlZenonVariable);
            await rtModel.CreateRtModel();
        }
    }  
}

