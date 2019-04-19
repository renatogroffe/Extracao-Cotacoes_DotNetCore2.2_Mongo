using System;
using MongoDB.Bson;

namespace CargaCotacoes
{
    public class CotacaoBitcoin
    {
        public ObjectId _id { get; set; }
        public string NomeMoeda { get; set; }
        public DateTime UltimaAtualizacao { get; set; }
        public double VlCotacaoDolar { get; set; }
    }
}