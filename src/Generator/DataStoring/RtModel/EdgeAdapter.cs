using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel;

public class EdgeAdapter
{
    private readonly OctoObjectId _rtId;
    private RtEntityDto _edgeAdapter;
    private readonly EdgeAdapterConfig _edgeAdapterConfig;
    
    public EdgeAdapter(List<XmlElementData>? eqModelList)
    {
        _rtId = OctoObjectId.GenerateNewId();
        _edgeAdapter = new RtEntityDto();
        _edgeAdapterConfig = new EdgeAdapterConfig(eqModelList);
        SetEdgeAdapter();
    }

    public RtEntityDto GetEdgeAdapter()
    {
        return _edgeAdapter;
    }
    
    private void SetEdgeAdapter()
    {
        _edgeAdapter = new RtEntityDto
        {
            RtId = _rtId,
            CkTypeId = "System.Communication/EdgeAdapter",
            Attributes =
            [
                new RtAttributeDto
                {
                    Id = "System/Name",
                    Value = "Zenon Sample PlugIn"
                },
                new RtAttributeDto
                {
                    Id = "System.Communication/AdapterType",
                    Value = "Meshmakers.Plugs.zenon"
                },
                new RtAttributeDto
                {
                    Id = "System.Communication/AdapterConfiguration",
                    Value = $"{{\"variables\":{_edgeAdapterConfig.SerializedVariables}}}"
                }
            ]
        };
    }
}