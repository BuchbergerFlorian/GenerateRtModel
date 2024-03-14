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
        
        public void AddModelToRoot(string? modelName, OctoObjectId modelRtId)
        {
            Root.Entities.Add(new RtEntityDto
            {
                RtId = modelRtId,
                CkTypeId = "Basic/EquipmentModel",
                Attributes =
                [
                    new()
                    {
                        Id = "System/Name",
                        Value = modelName
                    }
                ]
            });
        }
    
        public void AddGroupToRoot(string? groupName, OctoObjectId targetId, OctoObjectId objectId, string? targetCkType)
        {
            Root.Entities.Add(new RtEntityDto
            {
                RtId = objectId,
                CkTypeId = "Basic/EquipmentGroup",
                Attributes =
                [
                    new()
                    {
                        Id = "System/Name",
                        Value = groupName
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

