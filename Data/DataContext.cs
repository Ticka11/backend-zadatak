using BackEnd_zadatak.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd_zadatak.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            :base(options) {}

        //definisanje tabela koje cemo imati u bazi
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DevicePropertyValue> DevicePropertyValues { get; set; }
        public DbSet<DeviceTypeProperty> DeviceTypeProperties { get; set; }


    }
}