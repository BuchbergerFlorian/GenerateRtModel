using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;

namespace MeshMakers.GenerateRtModel.Generator;

public static class ConsoleOutput
{
    public static void WriteToConsole(List<XmlElementData>? eqModelList)
    {
        if (eqModelList != null)
            foreach (var item in eqModelList)
            {
                Console.WriteLine(item);
                // var variables = item.Variables;
                // foreach (var variable in variables)
                // {
                //     Console.WriteLine(
                //         $"Variable Name: {variable.Name}, Variable Description: {variable.Description}");
                // }
            }
    }
}