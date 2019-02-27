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
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        //parametar za uporedjivanje (<, >, >= ,<=)
        public string CompareOperator { get; set; } 
        public decimal? Price { get; set; }

    }
}