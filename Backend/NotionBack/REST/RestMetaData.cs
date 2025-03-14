namespace NotionBack.REST
{
    public class RestMetaData
    {
        public String uri {  get; set; }
        public String method { get; set; }
        public String name{  get; set; }
        public DateTime serverTime{  get; set; }
        public Dictionary<String, Object> _params{  get; set; }
        public String locale{  get; set; }
        public String[] acceptMethods{  get; set; }


    }
}
