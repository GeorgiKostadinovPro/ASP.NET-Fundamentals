namespace Homies.Services.Contracts
{
    public interface ITypeService
    {
        Task<IEnumerable<Data.Models.Type>> GetAllTypesAsync();
    }
}
