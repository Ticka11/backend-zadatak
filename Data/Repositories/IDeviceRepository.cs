using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd_zadatak.Helpers;
using BackEnd_zadatak.Models;

namespace BackEnd_zadatak.Data.Repositories
{
    public interface IDeviceRepository
    {
        void CreateDevice(Device device);
        void UpdateDevice(Device device);
        Task DeleteDevice(int id);
        Task<Device> GetDevice(int id);
        Task<IEnumerable<Device>> GetDevicesByCriteria(DeviceParams deviceParams);
        Task<bool> SaveAll(); 
    }
}