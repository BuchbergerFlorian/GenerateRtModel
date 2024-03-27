using MeshMakers.GenerateRtModel.Generator.DataReading.Xml;
using Meshmakers.Octo.ConstructionKit.Contracts;
using Meshmakers.Octo.Runtime.Contracts.DataTransferObjects;

namespace MeshMakers.GenerateRtModel.Generator.DataStoring.RtModel
{ 
    public class RtModel
    {
        public readonly RtModelRootDto Root;

        public RtModel()
        {
            Root = new RtModelRootDto
            {
                Dependencies = ["Basic", "System.Communication"],
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

        public void AddCommunicationEdgeAdapter(List<XmlElementData>? eqModelList)
        {
            EdgeAdapterConfig edgeAdapterConfig = new EdgeAdapterConfig(eqModelList);
            
            Root.Entities.Add( new RtEntityDto
            {
               RtId = OctoObjectId.GenerateNewId(),
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
                       Id= "System.Communication/AdapterConfiguration",
                       Value = "Variables: " + edgeAdapterConfig.SerializedVariables
                   }
               ]
            });
        }

        public void AddCommunicationDataPipeLine()
        {
            var rtId = OctoObjectId.GenerateNewId();
            Root.Entities.Add(new RtEntityDto
            { 
                RtId = rtId,
                CkTypeId = "System.Communication/DataPipeline",
                Attributes = 
                [
                    new RtAttributeDto
                    { 
                        Id = "System/Name",
                        Value = "Zenon Pipeline"
                    }
                ]
            });
            Root.Entities.Add(new RtEntityDto
            {
                RtId = OctoObjectId.GenerateNewId(),
                CkTypeId = "System.Communication/MeshPipeline",
                Attributes = 
                [
                    new RtAttributeDto
                    {
                        Id = "System.Communication/PipelineDefinition",
                        Value = "transformations:   " +
                                "- type: RetrieveFromMessage@1" +
                                "    description: Retrieve from distributed event hub message" +
                                "  - type: GetRtEntitiesById@1" +
                                "    targetPropertyName: EnergyMeterResult" +
                                "    ckTypeId: Industry.Energy-1.0.0/EnergyMeter-1.0.0" +
                                "    rtIds: " +
                                "      - 65dc6d24cc529cdc46c84fcc" +
                                "    description: Retrieve RtEntity if exists" +
                                "  - type: CreateUpdateInfo@1" +
                                "    targetPropertyName: _UpdateItems" +
                                "    appendToTargetPropertyName: true" +
                                "    rtId: 65dc6d24cc529cdc46c84fcc" +
                                "    ckTypeId: Industry.Energy-1.0.0/EnergyMeter-1.0.0" +
                                "    attributeUpdates: " +
                                "      - attributeName: Voltage" +
                                "        attributeValueType: Double" +
                                "        valuePath: $.Sinus5" +
                                "    description: update" +
                                "  - type: ApplyChanges@1" +
                                "    targetPropertyName: _UpdateItems" +
                                "  - type: EnrichWithMongoDataNode@1" +
                                "    path: $._UpdateItems" +
                                "    targetPropertyName: _UpdateItems" +
                                "    appendToTargetPropertyName: true" +
                                "    rtId: 65dc6d24cc529cdc46c84fcc" +
                                "    ckTypeId: Industry.Energy-1.0.0/EnergyMeter-1.0.0" +
                                "    attributeUpdates: " +
                                "      - attributeName: Power" +
                                "        attributeValueType: Double" +
                                "        valuePath: $.Power" +
                                "      - attributeName: Ampere" +
                                "        attributeValueType: Double" +
                                "        valuePath: $.Ampere" +
                                "    description: update" +
                                "  - type: SaveInTimeSeries@1"
                    }
                ],
                Associations = 
                [
                    new()
                    {
                        RoleId = "System/ParentChild",
                        TargetRtId = rtId,
                        TargetCkTypeId = "System.Communication/DataPipeline",
                    }
                ]
            });
            Root.Entities.Add(new RtEntityDto
            {
                RtId = OctoObjectId.GenerateNewId(),
                CkTypeId = "System.Communication/EdgePipeline",
                Attributes = 
                [
                    new RtAttributeDto
                    {
                        Id = "System.Communication/PipelineDefinition",
                        Value = "transformations: \n  - type: PublishToDistributionEventHub@1\n    description: Load to event hub"
                    }
                ],
                Associations = 
                [
                    new()
                    {
                        RoleId = "System/ParentChild",
                        TargetRtId = rtId,
                        TargetCkTypeId = "System.Communication/DataPipeline",
                    }
                ]
            });
        }
    }
}

