using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBroker
{
    public class PublisherService : IPublisherService
    {
        private readonly HttpClient httpClient;

        public PublisherService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Task<PublisherModel> CreateAsync(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        public Task<PublisherModel> DetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<PublisherModel> EditAsync(PublisherModel publisher)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PublisherModel>> GetAsync()
        {
            List<PublisherModel> retValue = new List<PublisherModel>();
            HttpResponseMessage? response = await httpClient.GetAsync("");
            retValue = await response.ReadContentAsync<List<PublisherModel>>();

            return retValue;
        }

        public Task<PublisherModel> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
