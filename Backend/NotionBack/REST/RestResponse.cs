namespace NotionBack.REST
{
    public class RestResponse<T>
    {
        public RestResponseStatus Status { get; set; }
        public RestMetaData Meta { get; set; }
        public T Data { get; set; }

        public RestResponse(int code_status, T data, RestMetaData meta)
        {
            this.Status = new RestResponseStatus(code_status);
            this.Data = data;
            this.Meta = meta;
        }
        public RestResponse(RestResponseStatus status, T data, RestMetaData meta)
        {
            this.Status = status;
            this.Data = data;
            this.Meta = meta;
        }
    }
}
