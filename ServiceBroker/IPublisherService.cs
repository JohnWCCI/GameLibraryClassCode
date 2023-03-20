using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBroker
{
    public interface IPublisherService
    {
        Task<PublisherModel> CreateAsync(PublisherModel publisher);
        Task DeleteAsync(PublisherModel publisher);
        Task<PublisherModel> DetailAsync(int id);
        Task<PublisherModel> EditAsync(PublisherModel publisher);
        Task<IEnumerable<PublisherModel>> GetAsync();
        Task<PublisherModel> GetByIdAsync(int id);
    }
}
