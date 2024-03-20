using MeshMakers.GenerateRtModel.Logic.Generator.Data_Reading.Xml;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel;

public class RtModelManager
{
    private readonly RtModel _rtModel; 
    
    public RtModelManager() => _rtModel = new RtModel();
    
    public void CreateModel(List<XmlElementData>? eqModelList)
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
    }
    
    public RtModel GetRtModel() => _rtModel;
}