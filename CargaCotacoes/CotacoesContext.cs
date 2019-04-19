using System.Collections.Generic;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;

namespace CargaCotacoes
{
    public class CotacoesContext
    {
        private MongoClient _client;
        private IMongoDatabase _db;

        public CotacoesContext(IConfiguration config)
        {
            _client = new MongoClient(
                config.GetConnectionString("BaseCotacoes"));
            _db = _client.GetDatabase("DBCotacoes");
        }

        public void IncluirCotacao<T>(T dadosCotacao)
        {
            var cotacoes = _db.GetCollection<T>("Cotacoes");
            cotacoes.InsertOne(dadosCotacao);
        }

        public void IncluirCotacoes<T>(IEnumerable<T> dadosCotacoes)
        {
            var cotacoes = _db.GetCollection<T>("Cotacoes");
            cotacoes.InsertMany(dadosCotacoes);
        }
    }
}