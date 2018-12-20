using System.Threading.Tasks;

namespace ProductCatalog.BLL.Contracts.Contracts {
    public interface IImportEquipment {
        Task ImportFromFile(string filePath);
    }
}