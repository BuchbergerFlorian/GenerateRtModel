using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel
{ 
    public class RtModel
    {
        public readonly RtModelRootDto Root;

        public RtModel()
        {
            Root = new RtModelRootDto
            {
                Dependencies = ["Basic"],
                Entities = []
            };
        }
        
        public void AddObjectToRoot(string? elementName, OctoObjectId elementRtId)
        {
            Root.Entities.Add(new RtEntityDto
            {
                RtId = elementRtId,
                CkTypeId = "Basic/EquipmentModel",
                Attributes =
                [
                    new()
                    {
                        Id = "System/Name",
                        Value = elementName
                    }
                ]
            });
        }
    
        public void AddObjectToRoot(string? elementName, OctoObjectId elementRtId, OctoObjectId targetId, string targetCkType, string ckTypeId)
        {
            Root.Entities.Add(new RtEntityDto
            {
                RtId = elementRtId,
                CkTypeId = ckTypeId,
                Attributes =
                [
                    new()
                    {
                        Id = "System/Name",
                        Value = elementName
                    }
                ],
                Associations =
                [
                    new()
                    {
                        RoleId = "System/ParentChild",
                        TargetCkTypeId = targetCkType,
                        TargetRtId = targetId
                    }
                ]
            });
        }
    }
}

