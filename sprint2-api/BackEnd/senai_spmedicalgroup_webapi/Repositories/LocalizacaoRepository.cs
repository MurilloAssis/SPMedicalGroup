using MongoDB.Driver;
using senai_spmedicalgroup_webapi.Domains;
using senai_spmedicalgroup_webapi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {

        private readonly IMongoCollection<Localizacao> _localizacoes;

        public LocalizacaoRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SPMedicalGoup");
            _localizacoes = database.GetCollection<Localizacao>("mapas");
        }
        public void Cadastrar(Localizacao novaLocalizacao)
        {
            throw new NotImplementedException();
        }

        public List<Localizacao> listarTodas()
        {
            return _localizacoes.Find(localizacao => true).ToList();
        }
    }
}
