using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductCatalog.Data.Entities;
using ProductCatalog.DAL.Contracts;

namespace ProductCatalog.DAL.Repositories {
    public class CsvRepository : IBaseCsvRepository {
        private const int Win1252Cp = 1252;
        private char _separator = char.Parse(ConfigurationManager.AppSettings["CsvSeparator"]);
        private string _filePath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["CsvEquipmentStore"];

        public async Task<ICollection<Equipment>> AllAsync(string filePath = "") {
            if (string.IsNullOrEmpty(filePath)) filePath = _filePath;

            var result = new List<Equipment>();
            foreach (var row in await ReadAll(filePath)) {
                var columns = row.Split(_separator);
                var equipment = new Equipment {
                    Id = Guid.TryParse(columns[0], out var id) ? id : Guid.Empty,
                    Specifications = columns[1],
                    Price = decimal.TryParse(columns[2], out var price) ? price : decimal.One,
                    BrandId = Guid.TryParse(columns[3], out var brandId) ? brandId : Guid.Empty,
                    ToolTypeId = Guid.TryParse(columns[4], out var toolTypeId) ? toolTypeId : Guid.Empty,
                    CreatedAt = DateTime.TryParse(columns[5], out var created) ? created : DateTime.Now,
                    ModifiedAt = DateTime.TryParse(columns[6], out var modified) ? modified : DateTime.Now,
                    IsArchive = bool.TryParse(columns[7], out var archive) && archive
                };
                result.Add(equipment);
            }

            return result;
        }

        public async Task<Equipment> FindByIdAsync(Guid id, string filePath) {
            if (string.IsNullOrEmpty(filePath)) filePath = _filePath;
            return (await AllAsync(filePath)).FirstOrDefault(e => e.Id == id);
        }

        public async Task<bool> AnyAsync(Func<Equipment, bool> predicates, string filePath) {
            if (string.IsNullOrEmpty(filePath)) filePath = _filePath;
            return (await AllAsync(filePath)).Any(predicates);
        }

        public async Task AddAsync(Equipment equipment, string filePath) {
            if (string.IsNullOrEmpty(filePath)) filePath = _filePath;
            equipment.Id = Guid.NewGuid();
            equipment.CreatedAt = DateTime.UtcNow;
            equipment.ModifiedAt = DateTime.UtcNow;
            await AddToFile(equipment, filePath);
        }

        public async Task UpdateAsync(Equipment equipment, string filePath) {
            if (string.IsNullOrEmpty(filePath)) filePath = _filePath;
            equipment.ModifiedAt = equipment.ModifiedAt = DateTime.UtcNow;
            await UpdateToFile(equipment, filePath);
        }

        public async Task RemoveAsync(Equipment equipment, string filePath) {
            if (string.IsNullOrEmpty(filePath)) filePath = _filePath;
            var entity = await FindByIdAsync(equipment.Id, filePath);
            entity.IsArchive = true;
            await UpdateToFile(entity, filePath);
        }

        private async Task<List<string>> ReadAll(string filePath) {
            List<string> result = new List<string>();
            //Encoding.GetEncoding(Win1252Cp)
            using (var reader = new StreamReader(filePath)) {
                while (!reader.EndOfStream) {
                    var line = await reader.ReadLineAsync();
                    result.Add(line);
                }
                reader.Close();
                reader.Dispose();
            }

            return result;
        }

        private async Task AddToFile(Equipment equipment, string filePath) {
            var lines = await ReadAll(filePath);
            using (var reader = new StreamWriter(filePath)) {
                lines.Add(GetString(equipment));
                foreach (var line in lines) {
                    await reader.WriteLineAsync(line);
                }
                reader.Close();
                reader.Dispose();
            }
        }

        private async Task UpdateToFile(Equipment equipment, string filePath) {
            var lines = await ReadAll(filePath);
            foreach (var line in lines) {
                var id = line.Split(_separator).FirstOrDefault();
                if (id == equipment.Id.ToString()) {
                    lines.Remove(line);
                    break;
                }
            }

            lines.Add(GetString(equipment));
            using (var writer = new StreamWriter(filePath)) {
                foreach (var line in lines) {
                    await writer.WriteLineAsync(line);
                }

                writer.Close();
                writer.Dispose();
            }
        }

        private string GetString(Equipment equipment) =>
            $"{equipment.Id}{_separator}{equipment.Specifications}{_separator}{equipment.Price}{_separator}{equipment.BrandId}{_separator}{equipment.ToolTypeId}{_separator}{equipment.CreatedAt}{_separator}{equipment.ModifiedAt}{_separator}{equipment.IsArchive}";
    }
}
