using MeshMakers.GenerateRtModel.Generator.Data_Reading.Xml;

namespace MeshMakers.GenerateRtModel.Generator.Data_Storing.RtModel;

public class RtModelManager
{
    private readonly RtModel _rtModel;

    public RtModelManager(List<XmlElementData>? eqModelList)
    {
        _rtModel = new RtModel();
        CreateModel(eqModelList);
    }

    private void CreateModel(List<XmlElementData>? eqModelList)
    {
        if (eqModelList == null) return;
        foreach (var element in eqModelList)
        {
            if (element.ElementType == "Model")
                _rtModel.AddObjectToRoot(element.ElementName, element.Id);
            else
            {
                if (element.CkTypeId != "")
                {
                    _rtModel.AddObjectToRoot(element.ElementName, element.Id, element.TargetId, element.TargetCkType, element.CkTypeId);
                }
            }
        }
        
        
        _rtModel.AddCommunicationEdgeAdapter(eqModelList);
        _rtModel.AddCommunicationDataPipeLine();
    }
    
    public RtModel GetRtModel() => _rtModel;
}