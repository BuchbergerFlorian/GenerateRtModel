using System.Text.Json;
using System.Text.Json.Serialization;
using MeshMakers.GenerateRtModel.Generator.Data_Reading.Xml;

namespace MeshMakers.GenerateRtModel.Generator.Data_Storing.RtModel;

public class EdgeAdapterConfig
{
    private readonly List<string> _variables = new();
    public string SerializedVariables { get; set; }
    
    public EdgeAdapterConfig(List<XmlElementData>? eqModelList)
    {
        ExtractVariablesFromEqModel(eqModelList);
        SerializedVariables = SerializeVariables();
    }

    private string SerializeVariables()
    {
        string serializeVariables = JsonSerializer.Serialize(_variables, new JsonSerializerOptions
        { 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        return serializeVariables;
    }

    private void ExtractVariablesFromEqModel(List<XmlElementData>? eqModelList)
    {
        if (eqModelList != null)
        {
            foreach (var element in eqModelList)
            { 
                foreach (var variable in element.Variables)
                {
                    _variables.Add(variable.Name);
                }
            } 
        }
    }
}