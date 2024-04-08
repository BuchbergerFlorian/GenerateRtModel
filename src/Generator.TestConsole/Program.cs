namespace MeshMakers.GenerateRtModel.Generator
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            const string filepathFromZenonXmlEqModel =
                @"C:\dev\GenerateRtModel\src\Generator\DataReading\Xml\XML Files\EQModel_Basisproject.XML";

            const string filepathFromXmlZenonVariable =
                @"C:\dev\GenerateRtModel\src\Generator\DataReading\Xml\XML Files\Variables_Basisproject.XML";

            RtModel rtModel = new RtModel(filepathFromZenonXmlEqModel, filepathFromXmlZenonVariable);
            rtModel.CreateRtModel();
        }
    }  
}

