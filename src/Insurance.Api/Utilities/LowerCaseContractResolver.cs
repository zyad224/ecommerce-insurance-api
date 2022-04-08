using Newtonsoft.Json.Serialization;
namespace Insurance.Api.Utilities
{
    public class LowerCaseContractResolver: DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            return propertyName.ToLower();
        }
    }
}
