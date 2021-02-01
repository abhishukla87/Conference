using System.Collections.Generic;

namespace ConferenceAPI.Models
{
    public class CollectionModel
    {
       public string Version { get; set; }
       public List<ItemsModel> Items { get; set; }
       public List<LinkModel> Links { get; set; }
       public List<QueryModel> Queries { get; set; }
       public List<TemplateModel> Template { get; set; }
    }
}
