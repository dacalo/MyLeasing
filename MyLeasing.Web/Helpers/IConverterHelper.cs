namespace MyLeasing.Web.Helpers
{
    using Data.Entities;
    using Models;
    using System.Threading.Tasks;

    public interface IConverterHelper
    {
        Task<Property> ToPropertyAsync(PropertyViewModel model, bool isNew);

        PropertyViewModel ToPropertyViewModel(Property property);

        Task<Contract> ToContractAsync(ContractViewModel model, bool isNew);

        ContractViewModel ToContractViewModel(Contract contract);
    }
}
