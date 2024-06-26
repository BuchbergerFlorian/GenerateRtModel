using System.Linq.Expressions;
using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel
{ 
    public class RtModelTemplate
    {
        public readonly RtModelRootDto Root;

        public RtModelTemplate()
        {
            Root = new RtModelRootDto
            {
                Dependencies = [],
                Entities = [],
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
    
        public void AddObjectToRoot(string? elementName, OctoObjectId elementRtId, OctoObjectId targetId, string targetCkType, string ckTypeId, Dictionary<string, object> additionalAttributes)
        {
            var entity = new RtEntityDto
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
            };

            foreach(var kvp in additionalAttributes)
            {
                try
                {
                    entity.Attributes.Add(new RtAttributeDto
                    {
                        Id = kvp.Key,
                        Value = kvp.Value
                    });
                }catch(Exception e)
                {
                    Console.WriteLine(e);
                }
   
            }
            
            Root.Entities.Add(entity);

        }

        public void AddEdgeAdapter(List<XmlElementData>? eqModelList)
        {
            EdgeAdapter edgeAdapter = new EdgeAdapter(eqModelList);
            Root.Entities.Add(edgeAdapter.GetEdgeAdapter());
        }

        public async Task AddDataPipeLine(List<XmlElementData> eqModelList)
        {
            DataPipeline dataPipeline = new DataPipeline(eqModelList);
            await dataPipeline.CreateDataPipeline();
            foreach (var rtEntityDto in dataPipeline.GetDataPipeline())
            {
                Root.Entities.Add(rtEntityDto);
            }
        }
    }
}

