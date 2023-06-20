using System;
using System.Reflection;
using Raven.Client;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.Client.ServerWide.Operations;
using Raven.Client.ServerWide;

namespace Raven_Project.Helpers
{
    public static class DocumentStoreHolder
    {
        private static readonly Lazy<IDocumentStore> LazyStore =
               new Lazy<IDocumentStore>(() =>
               {
                   var store = new DocumentStore
                   {
                       Urls = new[] { "http://localhost:8080" },
                       Database = "Project"
                   };
                   return store.Initialize();
               });

        public static IDocumentStore Store =>
            LazyStore.Value;
    }
}