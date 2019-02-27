using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd_zadatak.Models;

namespace BackEnd_zadatak.Data.Repositories
{
    public interface IDeviceTypeRepository
    {
        void CreateDeviceType(DeviceType device);
        void UpdateDeviceType(DeviceType device);
        Task DeleteDeviceType(int id);
        Task<DeviceType> GetDeviceType(int id);
        Task<IEnumerable<DeviceType>> GetDeviceTypes();
        Task<bool> SaveAll();

        
    }
}