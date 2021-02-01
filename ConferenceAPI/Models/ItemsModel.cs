using System.Collections.Generic;

namespace ConferenceAPI.Models
{
    public class ItemsModel
    {   
       public List<DataModel> Data { get; set; }
       public List<LinkModel> Links { get; set; }
       public string Href { get; set; }
    }
}
