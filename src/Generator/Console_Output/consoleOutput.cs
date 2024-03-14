using MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Console_Output;

public class ConsoleOutput
{
    public void WriteToConsole(List<XmlElementData>? eqModelList)
    {
        if (eqModelList != null)
            foreach (var item in eqModelList)
            {
                Console.WriteLine(item);
                var variables = item.Variables;
                foreach (var variable in variables)
                {
                    Console.WriteLine(
                        $"Variable Name: {variable.Name}, Variable Description: {variable.Description}");
                }
            }
    }
}