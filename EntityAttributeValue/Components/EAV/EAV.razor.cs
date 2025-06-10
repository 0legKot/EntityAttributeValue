using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EntityAttributeValue.Components.EAV {
    public partial class EAV {

        List<AttributeDefinitionString> AllAttributesString = [];
        string[] SelectedAttributeStringKeys = [];
        List<AttributeDefinitionInt> AllAttributesInt = [];
        string[] SelectedAttributeIntKeys = [];
        Dictionary<int, EntityView> Entities = new();

        protected override async Task OnInitializedAsync() {
            AllAttributesString = await Db.AttributesString.ToListAsync();
            AllAttributesInt = await Db.AttributesInt.ToListAsync();
        }

        async Task OnGetResults() {
            Entities = await Db.Entities.Select(x => new EntityView(x.Id, x.Name)).ToDictionaryAsync(x => x.Id);

            HashSet<int> attrStringKeys = SelectedAttributeStringKeys.Select(x => int.Parse(x)).ToHashSet();
            Dictionary<int, EntityAttributeValueString[]> entityIdToStringVal = await Db.EntityAttributeValuesString
                .Where(x => attrStringKeys.Contains(x.AttributeId))
                .GroupBy(x => x.EntityId)
                .ToDictionaryAsync(x => x.Key, y => y.ToArray());

            HashSet<int> attrIntKeys = SelectedAttributeIntKeys.Select(x => int.Parse(x)).ToHashSet();
            Dictionary<int, EntityAttributeValueInt[]> entityIdToIntVal = await Db.EntityAttributeValuesInt
                .Where(x => attrIntKeys.Contains(x.AttributeId))
                .GroupBy(x => x.EntityId)
                .ToDictionaryAsync(x => x.Key, y => y.ToArray());

            foreach (int entityId in Entities.Keys) {
                entityIdToStringVal.TryGetValue(entityId, out EntityAttributeValueString[]? stringAttrs);
                foreach (EntityAttributeValueString attr in stringAttrs ?? []) {
                    Entities[entityId].AttributeIdToText[attr.AttributeId.ToString()] = attr.Value;
                }
                entityIdToIntVal.TryGetValue(entityId, out EntityAttributeValueInt[]? intAttrs);
                foreach (EntityAttributeValueInt attr in intAttrs ?? []) {
                    Entities[entityId].AttributeIdToInt[attr.AttributeId.ToString()] = attr.Value;
                }
            }
        }
    }

    public class EntityView {
        public EntityView(int id, string name) {
            Id = id;
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Dictionary<string, string?> AttributeIdToText { get; set; } = [];
        public Dictionary<string, int?> AttributeIdToInt { get; set; } = [];
    }
}