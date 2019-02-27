using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd_zadatak.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO DeviceTypes(Name, ParentId) VALUES('computer',null)");
            migrationBuilder.Sql("INSERT INTO DeviceTypes(Name, ParentId) VALUES('laptop', (SELECT ID FROM DeviceTypes WHERE Name='computer'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypes(Name, ParentId) VALUES('desktop', (SELECT ID FROM DeviceTypes WHERE Name='computer'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypes(Name, ParentId) VALUES('pc', (SELECT ID FROM DeviceTypes WHERE Name='computer'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypes(Name, ParentId) VALUES('mobile phone', null)");


            migrationBuilder.Sql("INSERT INTO DeviceTypeProperties(Name, DeviceTypeId) VALUES('ram', (SELECT ID FROM DeviceTypes WHERE Name='computer'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypeProperties(Name, DeviceTypeId) VALUES('os', (SELECT ID FROM DeviceTypes WHERE Name='computer'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypeProperties(Name, DeviceTypeId) VALUES('processor', (SELECT ID FROM DeviceTypes WHERE Name='computer'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypeProperties(Name, DeviceTypeId) VALUES('screen diagonal', (SELECT ID FROM DeviceTypes WHERE Name='laptop'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypeProperties(Name, DeviceTypeId) VALUES('computer case', (SELECT ID FROM DeviceTypes WHERE Name='desktop'))");
            migrationBuilder.Sql("INSERT INTO DeviceTypeProperties(Name, DeviceTypeId) VALUES('camera', (SELECT ID FROM DeviceTypes WHERE Name='mobile phone'))");



            migrationBuilder.Sql("INSERT INTO Devices(Name, DeviceTypeId, Price) VALUES('Lenovo IdeaPad', (SELECT ID FROM DeviceTypes WHERE Name='laptop'), 500)");
            migrationBuilder.Sql("INSERT INTO Devices(Name, DeviceTypeId, Price) VALUES('Asus VivoBook', (SELECT ID FROM DeviceTypes WHERE Name='laptop'),450)");
            migrationBuilder.Sql("INSERT INTO Devices(Name, DeviceTypeId, Price) VALUES('Acer Aspire 3', (SELECT ID FROM DeviceTypes WHERE Name='laptop'),670)");
            migrationBuilder.Sql("INSERT INTO Devices(Name, DeviceTypeId, Price) VALUES('Huawei P10', (SELECT ID FROM DeviceTypes WHERE Name='mobile phone'),120)");


            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('4gb', (SELECT ID FROM DeviceTypeProperties WHERE Name='ram'),(SELECT ID FROM Devices WHERE Name='Lenovo IdeaPad'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('windows 10', (SELECT ID FROM DeviceTypeProperties WHERE Name='os'),(SELECT ID FROM Devices WHERE Name='Lenovo IdeaPad'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('i3', (SELECT ID FROM DeviceTypeProperties WHERE Name='processor'),(SELECT ID FROM Devices WHERE Name='Lenovo IdeaPad'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('15.6', (SELECT ID FROM DeviceTypeProperties WHERE Name='screen diagonal'),(SELECT ID FROM Devices WHERE Name='Lenovo IdeaPad'))");

            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('6gb', (SELECT ID FROM DeviceTypeProperties WHERE Name='ram'),(SELECT ID FROM Devices WHERE Name='Asus VivoBook'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('windows 10', (SELECT ID FROM DeviceTypeProperties WHERE Name='os'),(SELECT ID FROM Devices WHERE Name='Asus VivoBook'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('i5', (SELECT ID FROM DeviceTypeProperties WHERE Name='processor'),(SELECT ID FROM Devices WHERE Name='Asus VivoBook'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('14', (SELECT ID FROM DeviceTypeProperties WHERE Name='screen diagonal'),(SELECT ID FROM Devices WHERE Name='Asus VivoBook'))");

            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('4gb', (SELECT ID FROM DeviceTypeProperties WHERE Name='ram'),(SELECT ID FROM Devices WHERE Name='Acer Aspire 3'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('windows 10', (SELECT ID FROM DeviceTypeProperties WHERE Name='os'),(SELECT ID FROM Devices WHERE Name='Acer Aspire 3'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('i5', (SELECT ID FROM DeviceTypeProperties WHERE Name='processor'),(SELECT ID FROM Devices WHERE Name='Acer Aspire 3'))");
            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('i7', (SELECT ID FROM DeviceTypeProperties WHERE Name='screen diagonal'),(SELECT ID FROM Devices WHERE Name='Acer Aspire 3'))");

            migrationBuilder.Sql("INSERT INTO DevicePropertyValues(Value, DeviceTypePropertyId, DeviceId) VALUES('12mp', (SELECT ID FROM DeviceTypeProperties WHERE Name='camera'),(SELECT ID FROM Devices WHERE Name='Huawei P10'))");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM DeviceTypes");
            migrationBuilder.Sql("DELETE FROM DeviceTypeProperties");
            migrationBuilder.Sql("DELETE FROM Devices");
            migrationBuilder.Sql("DELETE FROM DevicePropertyValues");
        }
    }
}
