using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MongoDB.Driver;

namespace webApp.Models
{
    public class MongoDependencyResolver : IDependencyResolver
    {
        private readonly IMongoDatabase _database;

        public MongoDependencyResolver(IMongoDatabase database)
        {
            _database = database;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IMongoDatabase))
                return _database;

            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Enumerable.Empty<object>();
        }
    }
}