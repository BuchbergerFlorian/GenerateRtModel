using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel
{ 
    public class RtModel
    {
        private readonly RtModelRootDto _root;

        public RtModel()
        {
            _root = new RtModelRootDto
            {
                Dependencies = ["Basic"],
                Entities = []
            };
        }
        
        public void AddModelToRoot(string? modelName, OctoObjectId modelRtId)
        {
            _root.Entities.Add(new RtEntityDto
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
    
        public void AddGroupToRoot(string? groupName, OctoObjectId targetId, OctoObjectId objectId, string targetCkType)
        {
            _root.Entities.Add(new RtEntityDto
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

        public RtModelRootDto GetModelRoot() => _root;
    }
}

