using System.ComponentModel.DataAnnotations;

namespace BackEnd_zadatak.Helpers
{
    public class DeviceParams
    {
        //paremetri za pretrazivanje uredjaja
        public string Name { get; set; }
        public string Type { get; set; }
        public string PropertyValue { get; set; }

        //parametri za paging
        //postavljanje maksimalnog broja uredjaja po strani na 50
        //postavljanje default-nih vrijednosti za velicinu, i broj strane, ukoliko ne zadamo vrijednosti 
        // const int maxPageSize = 50;
        public int PageNumber { get; set; }

        public int PageSize {get; set;}
       
        //parametar za uporedjivanje (<, >, >= ,<=)
        public string CompareOperator { get; set; } 
        public decimal? Price { get; set; }

    }
}