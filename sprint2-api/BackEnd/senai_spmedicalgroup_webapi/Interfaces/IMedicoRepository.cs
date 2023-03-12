using senai_spmedicalgroup_webapi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_spmedicalgroup_webapi.Interfaces
{
    interface IMedicoRepository
    {
        List<Medico> ListarTodos();

        void Cadastrar(Medico novoMedico);
    }
}
