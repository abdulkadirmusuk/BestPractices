using Refit;

namespace RefitRestfulAPITutorial.Model.Concrete
{
    public class TodoQueryParam
    {
        [AliasAs("order")]
        public string SortOrder { get; set; }
        public int Limit { get; set; }
    }
}
