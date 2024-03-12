using Meshmakers.Octo.ConstructionKit.Contracts;

namespace MeshMakers.GenerateRtModel.Logic.Generator.Data_Storing.RtModel
{
    public class GroupIdsRepository
    {
        private readonly List<StructForGroupRepository> _listOfGroupIds;

        public GroupIdsRepository()
        {
            _listOfGroupIds = new List<StructForGroupRepository>();
        }
    
        public void AddGroupIdToList(int depth)
        {
            StructForGroupRepository newData;
            newData.Depth = depth;
            newData.GroupRtId = OctoObjectId.GenerateNewId();
    
            _listOfGroupIds.Add(newData);
        }

        public int GetDepthFromListIndex(int index)
        {
            return _listOfGroupIds[index].Depth;
        }

        public OctoObjectId GetGroupIdFromListIndex(int index)
        {
            return _listOfGroupIds[index].GroupRtId;
        }

        public int GetLengthOfList()
        {
            return _listOfGroupIds.Count;
        }
    }
}


