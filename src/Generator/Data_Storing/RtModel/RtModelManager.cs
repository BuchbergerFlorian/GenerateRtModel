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
                _rtModel.AddModelToRoot(element.ElementName, element.Id);
            else
                _rtModel.AddGroupToRoot(element.ElementName, element.TargetId, element.Id, element.TargetCkType );
        }
    }
    
    public RtModel GetRtModel() => _rtModel;
}